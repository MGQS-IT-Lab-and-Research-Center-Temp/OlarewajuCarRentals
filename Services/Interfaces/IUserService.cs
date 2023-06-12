using CarRentals.Models.Auth;
using CarRentals.Models.User;
using CarRentals.Models;

namespace CarRentals.Services.Interfaces
{
    public interface IUserService
    {
        UserResponseModel GetUser(string userId);
        BaseResponseModel Register(SignUpViewModel request, string roleName = null);
        UserResponseModel Login(LoginViewModel request);
    }
}
