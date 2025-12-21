using YaLlega.Entities;
using YaLlega1.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IProductRepository
    {
        public int Create(Product newProduct);
        public bool CheckIfProductExists(int productId);
        public bool CheckIfProductNameExists(string productName);
        public List<Product> GetAll();
        public List<Category>? GetCategories(int productId);
        public List<Cart>? GetCart(int productId);
        public Product? GetById(int productId);
        public void Update(Product updatedProduct, int productId);
        public void Delete(int productId);

    }
}
