using System;
using System.ComponentModel.DataAnnotations;
using InternManagement.Models;

namespace InternManagement.DTOs.Requests
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [MaxLength(255, ErrorMessage = "Họ và tên không được vượt quá 255 ký tự")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [MaxLength(100, ErrorMessage = "Mật khẩu không được vượt quá 100 ký tự")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Vai trò không hợp lệ")]
        public int? RoleId { get; set; }

        //public Role? Role { get; set; }
    }
}
