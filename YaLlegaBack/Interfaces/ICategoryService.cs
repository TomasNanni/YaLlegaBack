using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICategoryService
    {
        public ServiceResult RemoveProduct(int categoryId, List<int> productId);
        public int? Create(NewCategoryDto dto, List<int> productsId);
        public ServiceResult AddProduct(int categoryId, List<int> productId);
        public ServiceResult GetById(int categoryId);
        public ServiceResult Delete(int categoryId);
        public ServiceResult Update(UpdatedCategoryDto dto, int categoryId);
    }
}
