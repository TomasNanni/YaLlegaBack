using YaLlega.Entities;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public ServiceResult AddProduct(List<NewUpdatedProductDto> productsToAdd, int cartId)
        {
            var products = new List<Product>();
            foreach (var productData in productsToAdd)
            {
                Product product = new Product
                {
                    Name = productData.Name,
                    Description = productData.Description,
                    UrlImage = productData.UrlImage,
                    BasePrice = productData.BasePrice,
                    Discount = productData.Discount,
                    IsStandout = productData.IsStandout,
                    HappyHourStart = productData.HappyHourStart,
                    HappyHourEnd = productData.HappyHourEnd,
                };
                products.Add(product);
            }
            _cartRepository.AddProduct(products, cartId);
            var result = new ServiceResult
            {
                Message = "Productos agregados correctamente.",
                StatusCode = 200
            };
            return result;
        }

        public int? Create(List<NewUpdatedProductDto> productsToAdd)
        {
            var products = new List<Product>();
            foreach (var productData in productsToAdd)
            {
                Product product = new Product
                {
                    Name = productData.Name,
                    Description = productData.Description,
                    UrlImage = productData.UrlImage,
                    BasePrice = productData.BasePrice,
                    Discount = productData.Discount,
                    IsStandout = productData.IsStandout,
                    HappyHourStart = productData.HappyHourStart,
                    HappyHourEnd = productData.HappyHourEnd,
                };
                products.Add(product);
            }
            return _cartRepository.Create(products);
        }

        public ServiceResult Delete(int cartId)
        {
            _cartRepository.Delete(cartId);
            var result = new ServiceResult
            {
                Message = "Pedido borrado exitosamente.",
                StatusCode = 200,
            };
            return result;
        }

        public ServiceResult DeleteProduct(List<NewUpdatedProductDto> productsToRemove, int cartId)
        {
            var products = new List<Product>();
            foreach (var productData in productsToRemove)
            {
                Product product = new Product
                {
                    Name = productData.Name,
                    Description = productData.Description,
                    UrlImage = productData.UrlImage,
                    BasePrice = productData.BasePrice,
                    Discount = productData.Discount,
                    IsStandout = productData.IsStandout,
                    HappyHourStart = productData.HappyHourStart,
                    HappyHourEnd = productData.HappyHourEnd,
                };
                products.Add(product);
            }
            _cartRepository.DeleteProduct(products, cartId);
            var result = new ServiceResult
            {
                Message = "Productos removidos correctamente del carrito.",
                StatusCode = 200
            };
            return result;
        }

        public GetCartByIdDto? GetById(int cartId)
        {
            Cart? cart = _cartRepository.GetById(cartId);
            if (cart == null)
            {
                return null;
            }
            GetCartByIdDto cartForController = new GetCartByIdDto
            {
               Id = cart.Id,
            };
            foreach (var product in cart.Products)
            {
                var productData = new ProductDataDto
                {
                  Id = product.Id,
                  Name = product.Name,
                  Description = product.Description,
                  UrlImage = product.UrlImage,
                  BasePrice = product.BasePrice,
                  Discount = product.Discount,
                  IsStandout = product.IsStandout,
                  HappyHourStart = product.HappyHourStart,
                  HappyHourEnd = product.HappyHourEnd,
                };
                cartForController.Products.Add(productData);
            }
            return cartForController;
        }
    }
}
