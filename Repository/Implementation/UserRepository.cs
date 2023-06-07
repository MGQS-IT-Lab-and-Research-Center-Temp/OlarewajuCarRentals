using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CarRentalsContext context) : base(context)
        {
        }
    }
}
