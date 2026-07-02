using YaLlega.Entities;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }
        public int? Create(NewUpdatedProductDto newProduct)
        {
            if (newProduct == null)
            {
                throw new ArgumentException("Los datos del producto no pueden estar vacíos");
            }
            if (string.IsNullOrWhiteSpace(newProduct.Name))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío");
            }
            List<Category> categories = _productRepository.GetCategories().Where(c => newProduct.categoriesId.Contains(c.Id)).ToList();
            if (_productRepository.CheckIfProductNameExists(newProduct.Name, categories) == true)
            {
                throw new ArgumentException("Ya existe un producto con ese nombre");
            }
            Product product = new Product
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                UrlImage = newProduct.UrlImage,
                BasePrice = newProduct.BasePrice,
                Discount = newProduct.Discount,
                IsStandout = newProduct.IsStandout,
                HappyHourStart = newProduct.HappyHourStart,
                HappyHourEnd = newProduct.HappyHourEnd,
            };
            var result = _productRepository.Create(product, newProduct.categoriesId);
            if (result == null || result <= 0)
            {
                throw new ArgumentException("No se pudo crear el producto");
            }
            return result;
        }

        public ServiceResult Delete(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("El id del producto debe ser mayor a 0");
            }
            if (_productRepository.CheckIfProductExists(productId) == false)
            {
                throw new ArgumentException("El producto no existe.");
            }
            List<int> categoriesId = _productRepository.GetCategoryId(productId);
            foreach (var categoryId in categoriesId)
            {
                _categoryService.RemoveProduct(categoryId, new List<int> { productId });
            }
            _productRepository.Delete(productId);
            return new ServiceResult
            {
                Message = "Producto borrado correctamente.",
                StatusCode = 200
            };
        }

        public List<int>? GetCategoryIds(int productId)
        {
            if (_productRepository.CheckIfProductExists(productId) == false)
            {
                return null;
            }
            return _productRepository.GetCategoryId(productId);
        }

        public ProductDataDto? GetById(int productId)
        {
            Product? product = _productRepository.GetById(productId);
            if (product == null)
            {
                return null;
            }
            else
            {
                return new ProductDataDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    UrlImage = product.UrlImage,
                    BasePrice = product.BasePrice,
                    Discount = product.Discount,
                    IsStandout = product.IsStandout,
                    HappyHourEnd = product.HappyHourEnd,
                    HappyHourStart = product.HappyHourStart,
                };
            }
        }

        public ServiceResult Update(NewUpdatedProductDto updatedProduct, int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("El id del producto debe ser mayor a 0");
            }
            if (updatedProduct == null)
            {
                throw new ArgumentException("Los datos del producto no pueden estar vacíos");
            }
            if (string.IsNullOrWhiteSpace(updatedProduct.Name))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío");
            }
            if (_productRepository.CheckIfProductExists(productId) == false)
            {
                throw new ArgumentException("El producto a actualizar no existe");
            }
            List<Category> categories = _productRepository.GetCategoriesOfProduct(productId);
            if (_productRepository.CheckIfProductNameExists(updatedProduct.Name, categories) == true)
            {
                throw new ArgumentException("Ya existe un producto con ese nombre dentro de su restaurante");
            }
            Product product = new Product
            {
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                BasePrice = updatedProduct.BasePrice,
                UrlImage = updatedProduct.UrlImage,
                Discount = updatedProduct.Discount,
                IsStandout = updatedProduct.IsStandout,
                HappyHourEnd = updatedProduct.HappyHourEnd,
                HappyHourStart = updatedProduct.HappyHourStart,
            };
            _productRepository.Update(product, productId);
            return new ServiceResult
            {
                Message = "Producto actualizado correctamente",
                StatusCode = 200,
            };
        }
    }
}
