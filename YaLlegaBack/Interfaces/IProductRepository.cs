using YaLlega.Entities;

namespace YaLlegaBack.Interfaces
{
    public interface IProductRepository
    {
        public int Create(Product newProduct, List<int> categoriesId);
        public bool CheckIfProductExists(int productId);
        public bool CheckIfProductNameExists(string productName, List<Category> categories);
        public List<Category> GetCategories();
        public List<Category>? GetCategoriesOfProduct(int productId);
        public Product? GetById(int productId);
        public void Update(Product updatedProduct, int productId);
        public void Delete(int productId);
        public List<int> GetCategoryId(int productId);
    }
}
