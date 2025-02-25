using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;

namespace InternManagement.Services.Interfaces
{
    public interface IUserService
    {
        ApiResponse<ICollection<UserResponse>> GetUsers(int page, int pageSize, string search, string sortColumn, string sortOrder);
        ApiResponse<UserResponse> GetUserById(long id);
        ApiResponse<UserResponse> CreateUser(UserRequest userRequest);
        ApiResponse<UserResponse> UpdateUser(long id, UserRequest userRequest);
        ApiResponse<UserResponse> DeleteUser(long id);
    }
}
