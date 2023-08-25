using CarRentals.Models.Auth;
using CarRentals.Models.User;
using CarRentals.Models;

namespace CarRentals.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> GetUser(string userId);
        Task<BaseResponseModel> Register(SignUpViewModel request, string roleName = null);
        Task<UserResponseModel> Login(LoginViewModel request);
    }
}
