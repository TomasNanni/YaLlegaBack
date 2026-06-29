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

        public void AddProduct(List<int> productIds, int cartId)
        {
            var cart = _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");

            var notFoundIds = new List<int>();
            foreach (var productId in productIds)
            {
                var product = _context.Products.Find(productId);
                if (product == null)
                {
                    notFoundIds.Add(productId);
                    continue;
                }

                var existingOrder = cart.Products.FirstOrDefault(cpo => cpo.ProductId == productId);
                if (existingOrder != null)
                    existingOrder.Amount++;
                else
                    cart.Products.Add(new CartProductOrder { ProductId = productId, CartId = cartId, Amount = 1 });
            }

            if (notFoundIds.Count > 0)
                throw new ArgumentException($"Los siguientes productos no fueron encontrados: {string.Join(", ", notFoundIds)}");

            _context.SaveChanges();
        }

        public void DeleteProduct(List<int> productIds, int cartId)
        {
            var cart = _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");

            var notFoundIds = new List<int>();
            foreach (var productId in productIds)
            {
                var order = cart.Products.FirstOrDefault(cpo => cpo.ProductId == productId);
                if (order == null)
                    notFoundIds.Add(productId);
                else
                    cart.Products.Remove(order);
            }

            if (notFoundIds.Count > 0)
                throw new ArgumentException($"Los siguientes productos no fueron encontrados en el carrito: {string.Join(", ", notFoundIds)}");

            _context.SaveChanges();
        }

        public int Create(List<int> productIds)
        {
            var notFoundIds = new List<int>();
            var orders = new List<CartProductOrder>();

            foreach (var productId in productIds)
            {
                if (_context.Products.Find(productId) == null)
                {
                    notFoundIds.Add(productId);
                    continue;
                }

                var existing = orders.FirstOrDefault(o => o.ProductId == productId);
                if (existing != null)
                    existing.Amount++;
                else
                    orders.Add(new CartProductOrder { ProductId = productId, Amount = 1 });
            }

            if (notFoundIds.Count > 0)
                throw new ArgumentException($"Los siguientes productos no fueron encontrados: {notFoundIds}");

            Cart newCart = new() { Products = orders };
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
