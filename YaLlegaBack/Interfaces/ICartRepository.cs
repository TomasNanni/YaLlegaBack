using YaLlega.Entities;

namespace YaLlegaBack.Interfaces
{
    public interface ICartRepository
    {
        public Cart? GetById(int cartId);
        public void AddProduct(int productId, int cartId);
        public void DeleteProduct(int productId, int cartId);
        public void Delete(int cartId);
        public int Create(int productId);
    }
}
