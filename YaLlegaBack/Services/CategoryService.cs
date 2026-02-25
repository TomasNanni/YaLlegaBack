using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
using YaLlegaBack.Data;
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
            if (_categoryRepository.CheckIfCategoryNameExists(dto.Name) == true)
            {
                return null;
            }
            return _categoryRepository.Create(dto, productsId);
        }
        public ServiceResult RemoveProduct(int categoryId, List<int> productId)
        {
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                return new ServiceResult
                {
                    Message = "No existe categoria con el id indicado.",
                    StatusCode = 404,
                };
            }
            foreach (var id in productId)
            {
                if (_categoryRepository.CheckIfProductBelongs(id, categoryId) == false)
                {
                    return new ServiceResult
                    {
                        Message = $"El producto de id {id} no pertenece a la categoria",
                        StatusCode = 400,
                    };
                }
            }
            _categoryRepository.DeleteProduct(productId, categoryId);
            return new ServiceResult
            {
                Message = "Producto/s borrado/s de categoria correctamente.",
                StatusCode = 204,
            };
        }
        public ServiceResult AddProduct(int categoryId, List<int> productId)
        {
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                return new ServiceResult
                {
                    Message = "No existe categoria con el id indicado.",
                    StatusCode = 404,
                };
            }
            foreach (var id in productId)
            {
                if (_categoryRepository.CheckIfProductBelongs(id, categoryId) == true)
                {
                    return new ServiceResult
                    {
                        Message = $"El producto de id {id} ya pertenece a la categoria",
                        StatusCode = 400,
                    };
                }
            }
            _categoryRepository.AddProduct(productId, categoryId);
            return new ServiceResult
            {
                Message = "Producto/s agreagado/s a categoria correctamente.",
                StatusCode = 204,
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
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                return new ServiceResult
                {
                    Message = "No existe categoria con el id indicado.",
                    StatusCode = 404,
                };
            }
            _categoryRepository.Delete(categoryId);
            return new ServiceResult
            {
                Message = "La categoria se borro correctamente.",
                StatusCode = 201,
            };
        }
        public GetCategoryById? Update(UpdatedCategoryDto dto, int categoryId)
        {
            if (_categoryRepository.CheckIfCategoryExists(categoryId) == false)
            {
                return null;
            }
            Category? category = _categoryRepository.Update(dto, categoryId);
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