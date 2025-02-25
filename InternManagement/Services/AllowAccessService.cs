using AutoMapper;
using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;
using InternManagement.Repositories;
using InternManagement.Services.Interfaces;

namespace InternManagement.Services
{
    public class AllowAccessService : IAllowAccessService
    {
        private readonly AllowAccessRepo _repository;
        private readonly RoleRepo _RoleRepository;
        private readonly IMapper _mapper;

        public AllowAccessService(AllowAccessRepo repository, RoleRepo RoleRepository, IMapper mapper)
        {
            _repository = repository;
            _RoleRepository = RoleRepository;
            _mapper = mapper;
        }
        public ApiResponse<AllowAccessResponse> CreateAllowAccess(AllowAccessRequest allowAccessRequest)
        {
            //var existing = _repository.GetAllowAccesss().FirstOrDefault(us => us.TableName?.ToLower() == allowAccessRequest.TableName.ToLower());
            //if (existing != null)
            //{
            //    return ApiResponse<AllowAccessResponse>.Conflict("Tên bảng đã tồn tại");
            //}

            var roleExists = _RoleRepository.GetRoles()
            .Any(m => m.RoleId == allowAccessRequest.RoleId);
            if (!roleExists)
            {
                return ApiResponse<AllowAccessResponse>.Conflict("Vai trò không tồn tại");
            }

            var created = _repository.CreateAllowAccess(new AllowAccess()
            {
                TableName = allowAccessRequest.TableName,
                AccessProperties = allowAccessRequest.AccessProperties,
                RoleId = allowAccessRequest.RoleId,
            });

            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(created));
        }

        public ApiResponse<AllowAccessResponse> DeleteAllowAccess(long id)
        {
            var allowAccess = _repository.GetAllowAccessById(id);
            if (allowAccess == null)
            {
                return ApiResponse<AllowAccessResponse>.NotFound("Không tìm thấy quyền truy cập.");
            }

            _repository.DeleteAllowAccess(allowAccess);
            return ApiResponse<AllowAccessResponse>.Success(null, $"Xóa thành công quyền truy cập #{id}");
        }

        public ApiResponse<ICollection<AllowAccessResponse>> GetAllowAccess(int page, int pageSize, string search, string sortColumn, string sortOrder)
        {
            var query = _repository.GetAllowAccesss().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(us => us.TableName.ToLower().Contains(search.ToLower()));
            }

            query = sortColumn switch
            {
                "Name" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(us => us.TableName) : query.OrderBy(us => us.TableName),
                "Id" => sortOrder.ToLower() == "desc" ? query.OrderByDescending(us => us.Id) : query.OrderBy(us => us.Id),
                _ => query.OrderBy(us => us.Id)
            };

            var result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var response = _mapper.Map<ICollection<AllowAccessResponse>>(result);

            return result.Any()
                ? ApiResponse<ICollection<AllowAccessResponse>>.Success(response)
                : ApiResponse<ICollection<AllowAccessResponse>>.NotFound("Không có dữ liệu");
        }

        public ApiResponse<AllowAccessResponse> GetAllowAccessById(long id)
        {
            var allowAccess = _repository.GetAllowAccessById(id);
            return allowAccess != null
                ? ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(allowAccess))
                : ApiResponse<AllowAccessResponse>.NotFound($"Không tìm thấy quyền truy cập #{id}");
        }

        public ApiResponse<AllowAccessResponse> UpdateAllowAccess(long id, AllowAccessRequest allowAccessRequest)
        {
            var existingAllowAcces = _repository.GetAllowAccessById(id);
            if (existingAllowAcces == null)
            {
                return ApiResponse<AllowAccessResponse>.NotFound("Không tìm thấy quyền truy cập.");
            }

            //var existing = _repository.GetAllowAccesss().FirstOrDefault(us => us.TableName?.ToLower() == allowAccessRequest.TableName.ToLower());
            //if (existing != null)
            //{
            //    return ApiResponse<AllowAccessResponse>.Conflict("Tên bảng đã tồn tại");
            //}

            existingAllowAcces.TableName = allowAccessRequest.TableName;
            existingAllowAcces.AccessProperties = allowAccessRequest.AccessProperties;
            existingAllowAcces.RoleId = allowAccessRequest.RoleId;
            _repository.UpdateAllowAccess(existingAllowAcces);
            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(existingAllowAcces));
        }
    }
}
