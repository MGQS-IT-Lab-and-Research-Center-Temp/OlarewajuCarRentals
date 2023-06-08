using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(string id);
        User GetUser(Expression<Func<User, bool>> expression);
        List<User> GetUsers(Expression<Func<User, bool>> expression);
    }
}
