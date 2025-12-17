using YaLlegaBack.Models;

namespace YaLlegaBack.Interfaces
{
    public interface IProductService
    {
        public GetProductByIdDto? GetById(int productId);
    }
}
