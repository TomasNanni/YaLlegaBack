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

        public ServiceResult AddProduct(List<int> productIds, int cartId)
        {
            if (productIds == null || productIds.Count == 0)
                throw new ArgumentException("La lista de ids de productos no puede estar vacía");
            if (productIds.Any(id => id <= 0))
                throw new ArgumentException("Todos los ids de productos deben ser mayores a 0");

            _cartRepository.AddProduct(productIds, cartId);
            return new ServiceResult { Message = "Productos agregados correctamente.", StatusCode = 200 };
        }

        public int? Create(List<int> productIds)
        {
            if (productIds == null || productIds.Count == 0)
                throw new ArgumentException("Debe ingresar al menos un producto");
            if (productIds.Any(id => id <= 0))
                throw new ArgumentException("Todos los ids de productos deben ser mayores a 0");

            var result = _cartRepository.Create(productIds);
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

        public ServiceResult DeleteProduct(List<int> productIds, int cartId)
        {
            if (productIds == null || productIds.Count == 0)
                throw new ArgumentException("La lista de ids de productos no puede estar vacía");
            if (productIds.Any(id => id <= 0))
                throw new ArgumentException("Todos los ids de productos deben ser mayores a 0");

            _cartRepository.DeleteProduct(productIds, cartId);
            return new ServiceResult { Message = "Productos removidos correctamente del carrito.", StatusCode = 200 };
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
                return new CartProductDataDto
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
                    Amount = order.Amount,
                };
            }).ToList();
            return cartForController;
        }
    }
}
