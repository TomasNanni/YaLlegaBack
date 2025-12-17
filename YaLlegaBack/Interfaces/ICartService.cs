using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICartService
    {
        public GetCartByIdDto? GetById(int cartId);
        public ServiceResult AddProduct(List<NewUpdatedProductDto> productsToAdd, int cartId);
        public ServiceResult DeleteProduct(List<NewUpdatedProductDto> productsToRemove, int cartId);
        public int? Create(List<NewUpdatedProductDto> products);
        public ServiceResult Delete(int cartId);
    }
}
