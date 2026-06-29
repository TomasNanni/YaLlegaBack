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

        public ServiceResult AddProduct(int productId, int cartId)
        {
            if (productId <= 0)
                throw new ArgumentException("El id del producto debe ser mayor a 0");

            _cartRepository.AddProduct(productId, cartId);
            return new ServiceResult { Message = "Producto agregado correctamente.", StatusCode = 200 };
        }

        public int? Create(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("El id del producto debe ser mayor a 0");

            var result = _cartRepository.Create(productId);
            if (result <= 0)
                throw new ArgumentException("No se pudo crear el carrito");

            return result;
        }

        public ServiceResult Delete(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("El id del carrito debe ser mayor a 0");

            _cartRepository.Delete(cartId);
            return new ServiceResult { Message = "Pedido borrado exitosamente.", StatusCode = 200 };
        }

        public ServiceResult DeleteProduct(int productId, int cartId)
        {
            if (productId <= 0)
                throw new ArgumentException("El id del producto debe ser mayor a 0");

            _cartRepository.DeleteProduct(productId, cartId);
            return new ServiceResult { Message = "Producto removido correctamente del carrito.", StatusCode = 200 };
        }

        public GetCartByIdDto? GetById(int cartId)
        {
            Cart? cart = _cartRepository.GetById(cartId);
            if (cart == null)
                return null;

            GetCartByIdDto cartForController = new() { Id = cart.Id };
            cartForController.Products = cart.Products.Select(order =>
            {
                var product = order.Product!;
                var restaurant = product.Categories.First().Restaurant;
                return new ProductDataDto
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
                    RestaurantName = restaurant.Name,
                    RestaurantId = restaurant.UserId,
                };
            }).ToList();
            return cartForController;
        }
    }
}
