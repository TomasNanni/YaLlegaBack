namespace YaLlegaBack.Interfaces
{
    public interface IRestaurantService
    {
        public bool CheckIfRestaurantExists(int restaurantId);
        public void Delete(int restaurantId);
    }
}
