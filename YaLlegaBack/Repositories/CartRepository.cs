using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
using YaLlegaBack.Data;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Repositories
{
    public class CartRepository : ICartRepository
    {
        private YaLlegaBackContext _context;

        public CartRepository(YaLlegaBackContext context)
        {
            _context = context;
        }

        public void AddProduct(int productId, int cartId)
        {
            var cart = _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");

            var product = _context.Products.Find(productId);
            if (product == null)
                throw new ArgumentException($"Producto con id {productId} no encontrado");

            var existingOrder = cart.Products.FirstOrDefault(cpo => cpo.ProductId == productId);
            if (existingOrder != null)
                existingOrder.Amount++;
            else
                cart.Products.Add(new CartProductOrder { ProductId = productId, CartId = cartId, Amount = 1 });

            _context.SaveChanges();
        }

        public void DeleteProduct(int productId, int cartId)
        {
            var cart = _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");

            var order = cart.Products.FirstOrDefault(cpo => cpo.ProductId == productId);
            if (order == null)
                throw new ArgumentException($"Producto con id {productId} no encontrado en el carrito");

            cart.Products.Remove(order);
            _context.SaveChanges();
        }

        public int Create(int productId)
        {
            Product? product = _context.Products.Find(productId);
            if (product == null)
                throw new ArgumentException($"Producto con id {productId} no encontrado");

            Cart newCart = new()
            {
                Products = new List<CartProductOrder> { new CartProductOrder { ProductId = productId, Amount = 1 } }
            };
            var createdCart = _context.Carts.Add(newCart).Entity;
            _context.SaveChanges();
            return createdCart.Id;
        }

        public void Delete(int cartId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");

            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public Cart? GetById(int cartId)
        {
            return _context.Carts
                .Include(cart => cart.Products)
                    .ThenInclude(cpo => cpo.Product)
                        .ThenInclude(p => p!.Categories)
                            .ThenInclude(c => c.Restaurant)
                .FirstOrDefault(cart => cart.Id == cartId);
        }
    }
}
