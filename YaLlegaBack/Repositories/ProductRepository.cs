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

        public bool CheckIfProductNameExists(string productName, List<Category> categories)
        {
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    if (product.Name == productName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int Create(Product newProduct, List<int> categoriesId)
        {
            var categories = _context.Categories.Where(c => categoriesId.Contains(c.Id)).ToList();
            newProduct.Categories = categories;
            var createdProduct = _context.Products.Add(newProduct).Entity;
            _context.SaveChanges();
            return createdProduct.Id;
        }

        public void Delete(int productId)
        {
            _context.Products.Remove(_context.Products.Single(product => product.Id == productId));
            _context.SaveChanges();
        }

        public Product? GetById(int productId)
        {
            return _context.Products.FirstOrDefault(product => product.Id == productId);
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public List<Category>? GetCategoriesOfProduct(int productId)
        {
            return _context.Products.FirstOrDefault(product => product.Id == productId)?.Categories.ToList();
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
        }

        public List<int> GetCategoryId(int productId)
        {
            return _context.Products.Where(p => p.Id == productId).SelectMany(p => p.Categories).Select(c => c.Id).ToList();
        }
    }
}
