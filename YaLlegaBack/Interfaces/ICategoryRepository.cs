using YaLlega.Entities;
using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICategoryRepository
    {
        public bool CheckIfCategoryExists (int  categoryId);
        public Category? Update(UpdatedCategoryDto dto, int categoryId);
        public Category? GetById(int categoryId);
        public void Delete(int categoryId);
        public int Create(NewCategoryDto dto, List<int> productsId);
        public void DeleteProduct(List<int> productsToRemoveId, int categoryId);
        public void AddProduct(List<int> productsToAddId, int categoryId);
        public bool CheckIfProductBelongs(int productId, int categoryId);
        public bool CheckIfCategoryNameExists(string categoryName);
    }
}
