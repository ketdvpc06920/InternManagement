using AutoMapper;
using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;
using InternManagement.Repositories;
using InternManagement.Services.Interfaces;

namespace InternManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepo _repository;
        private readonly IMapper _mapper;

        public RoleService(RoleRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ApiResponse<RoleResponse> CreateRole(RoleRequest roleRequest)
        {
            var existing = _repository.GetRoles().FirstOrDefault(us => us.RoleName?.ToLower() == roleRequest.RoleName.ToLower());
            if (existing != null)
            {
                return ApiResponse<RoleResponse>.Conflict("Tên vai trò đã tồn tại");
            }

            var created = _repository.CreateRole(new Role() { RoleName = roleRequest.RoleName });
            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(created));
        }

        public ApiResponse<RoleResponse> DeleteRole(long id)
        {
            var role = _repository.GetRoleById(id);
            if (role == null)
            {
                return ApiResponse<RoleResponse>.NotFound("Không tìm thấy vai trò.");
            }

            _repository.DeleteRole(role);
            return ApiResponse<RoleResponse>.Success(null, $"Xóa thành công vai trò #{id}");
        }

        public ApiResponse<RoleResponse> GetRoleById(long id)
        {
            var roles = _repository.GetRoleById(id);
            return roles != null
                ? ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(roles))
                : ApiResponse<RoleResponse>.NotFound($"Không tìm thấy vai trò #{id}");
        }

        public ApiResponse<ICollection<RoleResponse>> GetRoles(int page, int pageSize, string search, string sortColumn, string sortOrder)
        {
            var query = _repository.GetRoles().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(us => us.RoleName.ToLower().Contains(search.ToLower()));
            }

            query = sortColumn switch
            {
                "Name" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(us => us.RoleName) : query.OrderBy(us => us.RoleName),
                "Id" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(us => us.RoleId) : query.OrderBy(us => us.RoleId),
                _ => query.OrderBy(us => us.RoleId)
            };

            var result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var response = _mapper.Map<ICollection<RoleResponse>>(result);

            return result.Any()
                ? ApiResponse<ICollection<RoleResponse>>.Success(response)
                : ApiResponse<ICollection<RoleResponse>>.NotFound("Không có dữ liệu");
        }

        public ApiResponse<RoleResponse> UpdateRole(long id, RoleRequest roleRequest)
        {
            var existingRole = _repository.GetRoleById(id);
            if (existingRole == null)
            {
                return ApiResponse<RoleResponse>.NotFound("Không tìm thấy vai trò.");
            }

            var existing = _repository.GetRoles()
                .FirstOrDefault(us => us.RoleId != id && us.RoleName?.ToLower() == roleRequest.RoleName.ToLower());
            if (existing != null)
            {
                return ApiResponse<RoleResponse>.Conflict("Tên chủ đề đã tồn tại");
            }

            existingRole.RoleName = roleRequest.RoleName;
            _repository.UpdateRole(existingRole);
            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(existingRole));
        }
    }
}
