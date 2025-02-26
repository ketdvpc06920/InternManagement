using AutoMapper;
using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;

namespace InternManagement.Mappers
{
    public class AllowAccessMapper : Profile
    {
        public AllowAccessMapper()
        {
            // us - res
            CreateMap<AllowAccess, AllowAccessResponse>();
            // res - us
            CreateMap<AllowAccessRequest, AllowAccess>();
        }
    }
}
