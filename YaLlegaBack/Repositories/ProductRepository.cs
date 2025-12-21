using YaLlega.Entities;
using YaLlegaBack.Data;
using YaLlegaBack.Interfaces;

namespace YaLlegaBack.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private YaLlegaBackContext _context;

        public ProductRepository(YaLlegaBackContext context)
        {
            _context = context;
        }
        public bool CheckIfProductExists(int productId)
        {
            return _context.Products.Any(product => product.Id == productId);
        }

        public bool CheckIfProductNameExists(string productName)
        {
            return _context.Products.Any(product => product.Name == productName);
        }

        public int Create(Product newProduct)
        {
            var createdProduct = _context.Products.Add(newProduct).Entity;
            _context.SaveChanges();
            return createdProduct.Id;
        }

        public void Delete(int productId)
        {
            _context.Products.Remove(_context.Products.Single(product => product.Id == productId));
            _context.SaveChanges();
            return;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product? GetById(int productId)
        {
            return _context.Products.FirstOrDefault(product => product.Id == productId);
        }

        public List<Category>? GetCategories(int productId)
        {
            return _context.Products.FirstOrDefault(product => product.Id == productId).Categories.ToList();
        }
        public List<Cart>? GetCart(int productId)
        {
            return _context.Products.FirstOrDefault(product => product.Id == productId).Carts.ToList();
        }

        public void Update(Product updatedProduct, int productId)
        {
            var productToEdit = _context.Products.First(product => product.Id == productId);
            productToEdit.Name = updatedProduct.Name;
            productToEdit.Description = updatedProduct.Description;
            productToEdit.UrlImage = updatedProduct.UrlImage;
            productToEdit.BasePrice = updatedProduct.BasePrice;
            productToEdit.Discount = updatedProduct.Discount;
            productToEdit.IsStandout = updatedProduct.IsStandout;
            productToEdit.HappyHourStart = updatedProduct.HappyHourStart;
            productToEdit.HappyHourEnd = updatedProduct.HappyHourEnd;
            _context.SaveChanges();
            return;
        }
    }
}
