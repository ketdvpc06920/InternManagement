using System.ComponentModel.DataAnnotations;

namespace InternManagement.DTOs.Requests
{
    public class AllowAccessRequest
    {
        [Required(ErrorMessage = "Tên bảng không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên bảng không được vượt quá 100 ký tự")]
        public string? TableName { get; set; }

        [Required(ErrorMessage = "Quyền truy cập không được để trống")]
        [MaxLength(500, ErrorMessage = "Danh sách quyền truy cập không được vượt quá 500 ký tự")]
        public string? AccessProperties { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public int? RoleId { get; set; }
    }
}
