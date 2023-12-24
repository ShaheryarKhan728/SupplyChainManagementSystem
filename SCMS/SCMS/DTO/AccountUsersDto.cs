namespace SCMS.DTO
{
    public class AccountUsersDto
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public int? UserAccountType { get; set; }

        public string? UserAddress { get; set; }

        public string? UserEmail { get; set; }

        public int? UserPhone1 { get; set; }

        public int? UserPhone2 { get; set; }

        public int? UserCnic { get; set; }

        public string? UserArea { get; set; }

        public string? UserCity { get; set; }

        public decimal? UserRating { get; set; }

        public decimal? UserReturnRate { get; set; }
        public string? UserAccountTypeName { get; set; }
    }
}
