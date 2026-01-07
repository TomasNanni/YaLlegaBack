using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface ICategoryService
    {
        public ServiceResult RemoveProduct(int categoryId, List<int> productId);
    }
}
