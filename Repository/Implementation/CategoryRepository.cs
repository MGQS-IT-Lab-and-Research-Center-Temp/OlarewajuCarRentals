using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Implementation
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CarRentalsContext context) : base(context)
        {
        }
    }
}
