using System.Xml.Linq;
using YaLlega.Entities;
using YaLlegaBack.Data;
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
            List<Category> categories = _productRepository.GetCategories().Where(c => newProduct.categoriesId.Contains(c.Id)).ToList();
            if (_productRepository.CheckIfProductNameExists(newProduct.Name, categories) == true)
            {
                return null;
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
            return _productRepository.Create(product, newProduct.categoriesId);
        }

        public ServiceResult Delete(int productId)
        {
            if (_productRepository.CheckIfProductExists(productId) == false)
            {
                return new ServiceResult
                {
                    Message = "El producto no existe",
                    StatusCode = 404,
                };
            }
            List<int> categoriesId = _productRepository.GetCategoryId(productId);
            if (_categoryService.RemoveProduct(productId, categoriesId).StatusCode == 200)
            {
                _productRepository.Delete(productId);
                return new ServiceResult
                {
                    Message = "Producto borrado correctamente.",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResult
                {
                    Message = "No se encontro categoria a la cual pertenezca el producto.",
                    StatusCode = 400
                };
            }
        }

        public List<ProductDataDto> GetAll()
        {
            List<Product> products = _productRepository.GetAll();
            return products.Select(product => new ProductDataDto
            {
                Name = product.Name,
                Description = product.Description,
                UrlImage = product.UrlImage,
                BasePrice = product.BasePrice,
                Discount = product.Discount,
                IsStandout = product.IsStandout,
                HappyHourStart = product.HappyHourStart,
                HappyHourEnd = product.HappyHourEnd,
            }).ToList();
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

        public List<CartDataDto>? GetCart(int productId)
        {
            List<Cart>? carts = _productRepository.GetCart(productId);
            return carts.Select(cart => new CartDataDto
            {
                Products = cart.Products.Select(product => new ProductDataDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    BasePrice = product.BasePrice,
                    UrlImage = product.UrlImage,
                    Discount = product.Discount,
                    IsStandout = product.IsStandout,
                    HappyHourEnd = product.HappyHourEnd,
                    HappyHourStart = product.HappyHourStart,
                }).ToList()
            }).ToList();
        }

        public List<CategoryDataDto>? GetCategories(int productId)
        {
            List<Category>? categories = _productRepository.GetCategoriesOfProduct(productId);
            return categories.Select(categories => new CategoryDataDto
            {
                Name = categories.Name,
                Description = categories.Description,
                Products = categories.Products.Select(product => new ProductDataDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    BasePrice = product.BasePrice,
                    UrlImage = product.UrlImage,
                    Discount = product.Discount,
                    IsStandout = product.IsStandout,
                    HappyHourEnd = product.HappyHourEnd,
                    HappyHourStart = product.HappyHourStart,
                }).ToList()
            }).ToList();
        }

        public ServiceResult Update(NewUpdatedProductDto updatedProduct, int productId)
        {
            if (_productRepository.CheckIfProductExists(productId) == false)
            {
                return new ServiceResult
                {
                    Message = "El producto a actualizar no existe",
                    StatusCode = 404,
                };
            }
            List<Category> categories = _productRepository.GetCategoriesOfProduct(productId);
            if (_productRepository.CheckIfProductNameExists(updatedProduct.Name, categories) == true)
            {
                return new ServiceResult
                {
                    Message = "Ya existe un prodcuto con ese nombre dentro de su restaurante",
                    StatusCode = 400,
                };
            }
            else
            {
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
}
