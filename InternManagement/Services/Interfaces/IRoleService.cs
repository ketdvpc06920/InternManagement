using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;

namespace InternManagement.Services.Interfaces
{
    public interface IRoleService
    {
        ApiResponse<ICollection<RoleResponse>> GetRoles(int page, int pageSize, string search, string sortColumn, string sortOrder);
        ApiResponse<RoleResponse> GetRoleById(long id);
        ApiResponse<RoleResponse> CreateRole(RoleRequest roleRequest);
        ApiResponse<RoleResponse> UpdateRole(long id, RoleRequest roleRequest);
        ApiResponse<RoleResponse> DeleteRole(long id);
    }
}
