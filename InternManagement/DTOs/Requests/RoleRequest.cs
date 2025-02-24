using System.ComponentModel.DataAnnotations;

namespace InternManagement.DTOs.Requests
{
    public class RoleRequest
    {
        [Required(ErrorMessage = "Tên vai trò không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên vai trò không được vượt quá 100 ký tự")]
        public string? RoleName { get; set; } = string.Empty;
    }
}
