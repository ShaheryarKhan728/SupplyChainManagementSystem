namespace SCMS.DTO
{
    public class AccountUsersDto
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? UserAddress { get; set; }

        public string? UserPhone1 { get; set; }

        public string? UserPhone2 { get; set; }

        public string? UserCnic { get; set; }

        public string? UserArea { get; set; }

        public string? UserCity { get; set; }

        public decimal? UserRating { get; set; }

        public decimal? UserReturnRate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? Approve { get; set; }

        public string? UserAccountTypeName { get; set; }
    }
}
