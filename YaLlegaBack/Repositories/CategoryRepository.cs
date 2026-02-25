using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using YaLlega.Entities;
using YaLlegaBack.Data;
using YaLlegaBack.Interfaces;
using YaLlegaBack.Models;

namespace YaLlegaBack.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private YaLlegaBackContext _context;

        public CategoryRepository(YaLlegaBackContext context)
        {
            _context = context;
        }
        public void AddProduct(List<int> productsToAddId, int categoryId)
        {
            foreach (var id in productsToAddId)
            {
                _context.Categories.Include(c => c.Products).First(c => c.Id == categoryId).Products.Add(_context.Products.FirstOrDefault(product => product.Id == id));
            }
            return;
        }

        public void DeleteProduct(List<int> productsToRemoveId, int categoryId)
        {
            foreach (var id in productsToRemoveId)
            {
                _context.Categories.Include(c => c.Products).First(c => c.Id == categoryId).Products.Remove(_context.Products.FirstOrDefault(product => product.Id == id));
            };
            _context.SaveChanges();
            return;
        }

        public int Create(NewCategoryDto dto, List<int> productsId)
        {
            Category newCategory = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                RestaurantUserId = dto.RestaurantUserId,
                Products = _context.Products.Where(p => productsId.Contains(p.Id)).ToList(),
            };
            var createdCategory = _context.Categories.Add(newCategory).Entity;
            _context.SaveChanges();
            return createdCategory.Id;
        }

        public void Delete(int categoryId)
        {
            _context.Categories.Remove(_context.Categories.Single(category => category.Id == categoryId));
            _context.SaveChanges();
            return;
        }

        public Category? GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(category => category.Id == categoryId);
        }

        public Category? Update(UpdatedCategoryDto dto, int categoryId)
        {
            var categoryToEdit = _context.Categories.First(category => category.Id == categoryId);
            categoryToEdit.Description = dto.Description;
            categoryToEdit.Name = dto.Name;
            categoryToEdit.Products = dto.ProductIds.Select(pId => _context.Products.FirstOrDefault(product => product.Id == pId)).Where(product => product != null).ToList()!;
            _context.SaveChanges();
            return categoryToEdit;
        }
        public bool CheckIfProductBelongs (int productId, int categoryId)
        {
            return _context.Categories.Include(category => category.Products).Any(category => category.Id == categoryId && category.Products.Any(product => product.Id == productId));
        }

        public bool CheckIfCategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id  == categoryId);
        }
        public bool CheckIfCategoryNameExists(string categoryName)
        {
            return _context.Categories.Any (c => c.Name == categoryName);
        }
        public List<Category>? GetRestaurantCategories(int restaurantId)
        {
            var restaurant = _context.Restaurants.Include(restaurant => restaurant.Categories).ThenInclude(category => category.Products).FirstOrDefault(restaurant => restaurant.UserId == restaurantId);
            if (restaurant == null)
            {
                return null;
            }
            else
            {
                return restaurant.Categories.ToList();
            }
        }  

    }
}
