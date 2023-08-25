using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserById(string id);
        Task<User> GetUser(Expression<Func<User, bool>> expression);
        Task<List<User>> GetUsers(Expression<Func<User, bool>> expression);
    }
}
