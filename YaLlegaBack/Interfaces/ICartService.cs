using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICartService
    {
        public GetCartByIdDto? GetById(int cartId);
        public ServiceResult AddProduct(List<ProductDataDto> productsToAdd, int cartId);
        public ServiceResult DeleteProduct(List<ProductDataDto> productsToRemove, int cartId);
        public int? Create(List<int> productsId);
        public ServiceResult Delete(int cartId);
    }
}
