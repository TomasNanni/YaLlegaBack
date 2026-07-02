using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IProductService
    {
        public int? Create(NewUpdatedProductDto newProduct);
        public ProductDataDto? GetById(int productId);
        public ServiceResult Update(NewUpdatedProductDto updatedProduct, int productId);
        public ServiceResult Delete(int productId);
        public List<int>? GetCategoryIds(int productId);
    }
}
