using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;

namespace InternManagement.Services.Interfaces
{
    public interface IAllowAccessService
    {
        ApiResponse<ICollection<AllowAccessResponse>> GetAllowAccess(int page, int pageSize, string search, string sortColumn, string sortOrder);
        ApiResponse<AllowAccessResponse> GetAllowAccessById(long id);
        ApiResponse<AllowAccessResponse> CreateAllowAccess(AllowAccessRequest allowAccessRequest);
        ApiResponse<AllowAccessResponse> UpdateAllowAccess(long id, AllowAccessRequest allowAccessRequest);
        ApiResponse<AllowAccessResponse> DeleteAllowAccess(long id);
    }
}
