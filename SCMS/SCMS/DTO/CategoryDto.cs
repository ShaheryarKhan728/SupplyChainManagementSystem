namespace SCMS.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal? CategoryMargin { get; set; }
        public int? CategoryHeaderId { get; set; }
        public string? CategoryHeaderName { get; set; }
        public bool? Status { get; set; }
    }
}
