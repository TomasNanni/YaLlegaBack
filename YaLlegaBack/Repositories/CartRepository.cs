using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
using YaLlegaBack.Data;
using YaLlegaBack.Interfaces;

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
            {
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");
            }

            var notFoundIds = new List<int>();
            foreach (var productId in productIds)
            {
                var product = _context.Products.Find(productId);
                if (product == null)
                {
                    notFoundIds.Add(productId);
                }
                else
                {
                    cart.Products.Add(product);
                }
            }

            if (notFoundIds.Count > 0)
            {
                throw new ArgumentException($"Los siguientes productos no fueron encontrados: {string.Join(", ", notFoundIds)}");
            }

            _context.SaveChanges();
        }

        public void DeleteProduct(List<int> productIds, int cartId)
        {
            var cart = _context.Carts.Include(c => c.Products).FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
            {
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");
            }

            var notFoundIds = new List<int>();
            foreach (var productId in productIds)
            {
                var product = cart.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    notFoundIds.Add(productId);
                }
                else
                {
                    cart.Products.Remove(product);
                }
            }

            if (notFoundIds.Count > 0)
            {
                throw new ArgumentException($"Los siguientes productos no fueron encontrados en el carrito: {string.Join(", ", notFoundIds)}");
            }

            _context.SaveChanges();
        }

        public int Create(int productId)
        {
            Product? product = _context.Products.Find(productId);
            if (product == null)
            {
                throw new ArgumentException($"Producto con id {productId} no encontrado");
            }
            Cart newCart = new()
            {
                Products = new List<Product> { product },
            };
            var createdCart = _context.Carts.Add(newCart).Entity;
            _context.SaveChanges();
            return createdCart.Id;
        }

        public void Delete(int cartId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
            {
                throw new ArgumentException($"Carrito con id {cartId} no encontrado");
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public Cart? GetById(int cartId)
        {
            return _context.Carts
                .Include(cart => cart.Products)
                    .ThenInclude(p => p.Categories)
                        .ThenInclude(c => c.Restaurant)
                .FirstOrDefault(cart => cart.Id == cartId);
        }
    }
}
