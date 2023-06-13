using CarRentals.Models.Role;
using CarRentals.Models;

namespace CarRentals.Services.Interfaces
{
    public interface IRoleService
    {

        BaseResponseModel CreateRole(CreateRoleViewModel request);
        BaseResponseModel DeleteRole(string roleId);
        BaseResponseModel UpdateRole(string roleId, UpdateRoleViewModel request);
        RoleResponseModel GetRole(string roleId);
        RolesResponseModel GetAllRole();
    }
}
