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
        public void AddProduct(List<Product> productsToAdd, int cartId)
        {
            foreach (var product in productsToAdd)
            {
                _context.Carts.First(c => c.Id == cartId).Products.Add(product);
            }
            return;
        }

        public void DeleteProduct(List<Product> productsToRemove, int cartId)
        {
            foreach (var product in productsToRemove)
            {
                _context.Carts.First(c => c.Id == cartId).Products.Remove(product);
            }
            return;
        }

        public int Create(List<int> productsId)
        {
            List<Product> products = productsId.Select(id => _context.Products.Find(id)).Where(product => product  != null).ToList();
            Cart newCart = new()
            {
                Products = products,
            };
            var createdCart = _context.Carts.Add(newCart).Entity;
            _context.SaveChanges();
            return createdCart.Id;
        }

        public void Delete(int cartId)
        {
            _context.Carts.Remove(_context.Carts.Single(cart => cart.Id == cartId));
            _context.SaveChanges();
            return;
        }

        public Cart? GetById(int cartId)
        {
            return _context.Carts.Include(cart => cart.Products).FirstOrDefault(cart => cart.Id == cartId);
        }
    }
}
