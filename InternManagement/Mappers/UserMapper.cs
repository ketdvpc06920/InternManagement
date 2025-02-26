using AutoMapper;
using InternManagement.DTOs.Requests;
using InternManagement.DTOs.Responses;
using InternManagement.Models;

namespace InternManagement.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            // us - res
            CreateMap<User, UserResponse>();
            // res - us
            CreateMap<UserRequest, User>();
        }
    }
}
