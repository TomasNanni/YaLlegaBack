using YaLlega.Entities;

namespace YaLlegaBack.Interfaces
{
    public interface ICartRepository
    {
        public Cart? GetById(int cartId);
        public void AddProduct(List<int> productIds, int cartId);
        public void DeleteProduct(List<int> productIds, int cartId);
        public void Delete(int cartId);
        public int Create(List<int> productIds);
    }
}
