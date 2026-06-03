using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICartService
    {
        public GetCartByIdDto? GetById(int cartId);
        public ServiceResult AddProduct(List<int> productIds, int cartId);
        public ServiceResult DeleteProduct(List<int> productIds, int cartId);
        public int? Create(int productId);
        public ServiceResult Delete(int cartId);
    }
}
