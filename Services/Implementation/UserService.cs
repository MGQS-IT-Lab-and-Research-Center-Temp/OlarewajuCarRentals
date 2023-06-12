﻿using CarRentals.Entities;
using CarRentals.Helper;
using CarRentals.Models;
using CarRentals.Models.Auth;
using CarRentals.Models.User;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;

namespace CarRentals.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public UserResponseModel GetUser(string userId)
        {
            var response = new UserResponseModel();
            var user = _unitOfWork.Users.GetUser(x => x.Id == userId);

            if (user is null)
            {
                response.Message = $"User with {userId} does not exist";
                return response;
            }

            response.Data = new UserViewModel
            {
                UserName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
            };
            response.Message = $"User successfully retrieved";
            response.Status = true;

            return response;
        }

        public UserResponseModel Login(LoginViewModel request)
        {
            var response = new UserResponseModel();

            try
            {
                var user = _unitOfWork.Users.GetUser(x =>
                                x.Email == request.Email);

                if (user is null)
                {
                    response.Message = $"Account does not exist!";
                    return response;
                }

                string hashedPassword = HashingHelper.HashPassword(request.Password, user.HashSalt);

                if (!user.PasswordHash.Equals(hashedPassword))
                {
                    response.Message = $"Incorrect username or password!";
                    return response;
                }

                response.Data = new UserViewModel
                {
                    Id = user.Id,
                    UserName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleName,
                };
                response.Message = $"Welcome {user.FirstName} {user.LastName}";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Register(SignUpViewModel request, string roleName)
        {
            var response = new BaseResponseModel();
            string saltString = HashingHelper.GenerateSalt();
            string hashedPassword = HashingHelper.HashPassword(request.Password, saltString);
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userExist = _unitOfWork.Users.Exists(x => x.Email == request.Email);

            if (userExist)
            {
                response.Message = $"User with  {request.Email} already exist";
                return response;
            }

            roleName ??= "AppUser";

            var role = _unitOfWork.Roles.Get(x => x.RoleName == roleName);

            if (role is null)
            {
                response.Message = $"Role does not exist";
                return response;
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                CreatedBy = createdBy,
            };

            try
            {
                _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
                response.Message = $"You have succesfully signed up on carrentals";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Message = $"Unable to signup, an error occurred {ex.Message}"
                };
            }
        }
    }
}
