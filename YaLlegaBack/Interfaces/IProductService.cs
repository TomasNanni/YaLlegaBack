using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IProductService
    {
        public int? Create(NewUpdatedProductDto newProduct);
        public List<ProductDataDto> GetAll();
        public List<CartDataDto>? GetCart(int productId);
        public ProductDataDto? GetById(int productId);
        public ServiceResult Update(NewUpdatedProductDto updatedProduct, int productId);
        public ServiceResult Delete(int productId);
    }
}
