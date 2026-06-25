using YaLlega.Entities;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public int? Create(NewCategoryDto dto, List<int> productsId)
        {
            if (dto == null)
            {
                throw new ArgumentException("Los datos de la categoría no pueden estar vacíos");
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío");
            }
            if (_categoryRepository.CheckIfCategoryNameExists(dto.Name) == true)
            {
                throw new ArgumentException("Ya existe una categoría con ese nombre");
            }
            var result = _categoryRepository.Create(dto, productsId);
            if (result == null || result <= 0)
            {
                throw new ArgumentException("No se pudo crear la categoría");
            }
            return result;
        }
        public ServiceResult RemoveProduct(int categoryId, List<int> productId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("El id de la categoría debe ser mayor a 0");
            }
            if (productId == null || productId.Count == 0)
            {
                throw new ArgumentException("La lista de ids de productos no puede estar vacía");
            }
            if (productId.Any(id => id <= 0))
            {
                throw new ArgumentException("Todos los ids de productos deben ser mayores a 0");
            }
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                throw new ArgumentException("No existe categoría con el id indicado.");
            }
            foreach (var id in productId)
            {
                if (_categoryRepository.CheckIfProductBelongs(id, categoryId) == false)
                {
                    throw new ArgumentException($"El producto de id {id} no pertenece a la categoría");
                }
            }
            _categoryRepository.DeleteProduct(productId, categoryId);
            return new ServiceResult
            {
                Message = "Producto/s borrado/s de categoría correctamente.",
                StatusCode = 200,
            };
        }
        public ServiceResult AddProduct(int categoryId, List<int> productId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("El id de la categoría debe ser mayor a 0");
            }
            if (productId == null || productId.Count == 0)
            {
                throw new ArgumentException("La lista de ids de productos no puede estar vacía");
            }
            if (productId.Any(id => id <= 0))
            {
                throw new ArgumentException("Todos los ids de productos deben ser mayores a 0");
            }
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                throw new ArgumentException("No existe categoría con el id indicado.");
            }
            foreach (var id in productId)
            {
                if (_categoryRepository.CheckIfProductBelongs(id, categoryId) == true)
                {
                    throw new ArgumentException($"El producto de id {id} ya pertenece a la categoría");
                }
            }
            _categoryRepository.AddProduct(productId, categoryId);
            return new ServiceResult
            {
                Message = "Producto/s agregado/s a categoría correctamente.",
                StatusCode = 200,
            };
        }
        public GetCategoryById? GetById(int categoryId)
        {
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                return null;
            }
            Category? category = _categoryRepository.GetById(categoryId);
            GetCategoryById categoryToReturn = new()
            {
                Description = category.Description,
                Name = category.Name,
                Products = category.Products.Select(product => new ProductDataDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    BasePrice = product.BasePrice,
                    Discount = product.Discount,
                    HappyHourEnd = product.HappyHourEnd,
                    HappyHourStart = product.HappyHourStart,
                    IsStandout = product.IsStandout,
                    UrlImage = product.UrlImage,
                }).ToList(),
            };
            return categoryToReturn;
        }
        public ServiceResult Delete(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("El id de la categoría debe ser mayor a 0");
            }
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                throw new ArgumentException("No existe categoría con el id indicado.");
            }
            _categoryRepository.Delete(categoryId);
            return new ServiceResult
            {
                Message = "La categoría se borró correctamente.",
                StatusCode = 200,
            };
        }
        public GetCategoryById? Update(UpdatedCategoryDto dto, int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("El id de la categoría debe ser mayor a 0");
            }
            if (dto == null)
            {
                throw new ArgumentException("Los datos de la categoría no pueden estar vacíos");
            }
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                throw new ArgumentException("No existe categoría con el id indicado.");
            }
            Category? category = _categoryRepository.Update(dto, categoryId);
            if (category == null)
            {
                throw new ArgumentException("No se pudo actualizar la categoría");
            }
            GetCategoryById categoryToReturn = new()
            {
                Description = category.Description,
                Name = category.Name,
                Products = category.Products.Select(product => new ProductDataDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    BasePrice = product.BasePrice,
                    Discount = product.Discount,
                    HappyHourEnd = product.HappyHourEnd,
                    HappyHourStart = product.HappyHourStart,
                    IsStandout = product.IsStandout,
                    UrlImage = product.UrlImage,
                }).ToList(),
            };
            return categoryToReturn;
        }
        public List<GetCategoryById>? GetRestaurantCategories(int restaurantId)
        {
            var categories = _categoryRepository.GetRestaurantCategories(restaurantId);
            if (categories == null)
            {
                return null;
            }
            else
            {
                return categories.Select(category => new GetCategoryById
                {
                    Id = category.Id,
                    Description = category.Description,
                    Name = category.Name,
                    Products = category.Products.Select(product => new ProductDataDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        BasePrice = product.BasePrice,
                        Discount = product.Discount,
                        HappyHourEnd = product.HappyHourEnd,
                        HappyHourStart = product.HappyHourStart,
                        IsStandout = product.IsStandout,
                        UrlImage = product.UrlImage,
                    }).ToList(),
                }).ToList();

            }
        }
    }
}