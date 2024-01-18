namespace SCMS.DTO
{
    public class AccountsDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public int? AccountType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? AccountTypeName { get; set; }
    }
}
