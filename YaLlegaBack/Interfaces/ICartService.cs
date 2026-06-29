using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICartService
    {
        public GetCartByIdDto? GetById(int cartId);
        public ServiceResult AddProduct(int productId, int cartId);
        public ServiceResult DeleteProduct(int productId, int cartId);
        public int? Create(int productId);
        public ServiceResult Delete(int cartId);
    }
}
