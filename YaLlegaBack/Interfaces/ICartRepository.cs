using YaLlega.Entities;

namespace YaLlegaBack.Interfaces
{
    public interface ICartRepository
    {
        public Cart? GetById(int cartId);
        public void AddProduct(List<Product> productsToAdd, int cartId);
        public void DeleteProduct(List<Product> productsToRemove, int cartId);
        public void Delete(int cartId);
        public int Create(List<int> products);
    }
}
