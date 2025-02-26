namespace InternManagement.DTOs.Responses
{
    public class AllowAccessResponse
    {
        public int? Id { get; set; }
        public string? TableName { get; set; }
        public string? AccessProperties { get; set; }
        public int? RoleId { get; set; }
    }
}
