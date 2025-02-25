using AutoMapper;
using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;
using InternManagement.Repositories;
using InternManagement.Services.Interfaces;

namespace InternManagement.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepo _repository;
        private readonly RoleRepo _RoleRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepo repository, RoleRepo RoleRepository, IMapper mapper)
        {
            _repository = repository;
            _RoleRepository = RoleRepository;
            _mapper = mapper;
        }
        public ApiResponse<UserResponse> CreateUser(UserRequest userRequest)
        {
            var existing = _repository.GetUsers().FirstOrDefault(us => us.Email?.ToLower() == userRequest.Email.ToLower());
            if (existing != null)
            {
                return ApiResponse<UserResponse>.Conflict("Email đã được sử dụng");
            }

            var roleExists = _RoleRepository.GetRoles()
            .Any(m => m.RoleId == userRequest.RoleId);
            if (!roleExists)
            {
                return ApiResponse<UserResponse>.Conflict("Vai trò không tồn tại");
            }

            var created = _repository.CreateUser(new User()
            {
                FullName = userRequest.FullName,
                DateOfBirth = userRequest.DateOfBirth.HasValue
                 ? DateTime.SpecifyKind(userRequest.DateOfBirth.Value, DateTimeKind.Utc)
                : null,
                Email = userRequest.Email,
                Password = userRequest.Password,
                RoleId = userRequest.RoleId,
            });

            return ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(created));
        }

        public ApiResponse<UserResponse> DeleteUser(long id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return ApiResponse<UserResponse>.NotFound("Không tìm thấy người dùng.");
            }

            _repository.DeleteUser(user);
            return ApiResponse<UserResponse>.Success(null, $"Xóa thành công người dùng #{id}");
        }

        public ApiResponse<UserResponse> GetUserById(long id)
        {
            var users = _repository.GetUserById(id);
            return users != null
                ? ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(users))
                : ApiResponse<UserResponse>.NotFound($"Không tìm thấy người dùng #{id}");
        }

        public ApiResponse<ICollection<UserResponse>> GetUsers(int page, int pageSize, string search, string sortColumn, string sortOrder)
        {
            var query = _repository.GetUsers().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(us => us.FullName.ToLower().Contains(search.ToLower()));
            }

            query = sortColumn switch
            {
                "Name" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(us => us.FullName) : query.OrderBy(us => us.FullName),
                "Id" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(us => us.UserId) : query.OrderBy(us => us.UserId),
                _ => query.OrderBy(us => us.UserId)
            };

            var result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var response = _mapper.Map<ICollection<UserResponse>>(result);

            return result.Any()
                ? ApiResponse<ICollection<UserResponse>>.Success(response)
                : ApiResponse<ICollection<UserResponse>>.NotFound("Không có dữ liệu");
        }

        public ApiResponse<UserResponse> UpdateUser(long id, UserRequest userRequest)
        {
            var existingUser = _repository.GetUserById(id);
            if (existingUser == null)
            {
                return ApiResponse<UserResponse>.NotFound("Không tìm thấy người dùng.");
            }

            var existing = _repository.GetUsers().FirstOrDefault(us => us.Email?.ToLower() == userRequest.Email.ToLower());
            if (existing != null)
            {
                return ApiResponse<UserResponse>.Conflict("Email đã được sử dụng");
            }

            existingUser.FullName = userRequest.FullName;
            existingUser.DateOfBirth = userRequest.DateOfBirth.HasValue
             ? DateTime.SpecifyKind(userRequest.DateOfBirth.Value, DateTimeKind.Utc)
            : null;
            existingUser.Email = userRequest.Email;
            existingUser.Password = userRequest.Password;
            existingUser.RoleId = userRequest.RoleId;
            _repository.UpdateUser(existingUser);
            return ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(existingUser));
        }
    }
}
