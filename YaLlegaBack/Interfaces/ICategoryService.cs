using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICategoryService
    {
        public ServiceResult RemoveProduct(int categoryId, List<int> productId);
        public int? Create(NewCategoryDto dto, List<int> productsId);
        public GetCategoryById? GetById(int categoryId);
        public ServiceResult Delete(int categoryId);
        public GetCategoryById? Update(UpdatedCategoryDto dto, int categoryId);
        public List<GetCategoryById>? GetRestaurantCategories(int restaurantId);
        public int? GetOwnerId(int categoryId);
    }
}
