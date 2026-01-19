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
        public ServiceResult AddProduct(List<ProductDataDto> productsToAdd, int cartId)
        {
            List<Product> products = new();
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
            return new ServiceResult
            {
                Message = "Productos agregados correctamente.",
                StatusCode = 200
            };
        }

        public int? Create(List<int> productsId)
        {
            return _cartRepository.Create(productsId);
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

        public ServiceResult DeleteProduct(List<ProductDataDto> productsToRemove, int cartId)
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
            GetCartByIdDto cartForController = new()
            {
               Id = cart.Id,
            };
            cartForController.Products = cart.Products.Select(product => new ProductDataDto 
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
            }).ToList();
            return cartForController;
        }
    }
}
