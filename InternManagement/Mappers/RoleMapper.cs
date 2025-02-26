using AutoMapper;
using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;

namespace InternManagement.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            // us - res
            CreateMap<Role, RoleResponse>();
            // res - us
            CreateMap<RoleRequest, Role>();
        }
    }
}
