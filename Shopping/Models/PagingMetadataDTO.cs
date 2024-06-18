namespace Shopping.Models
{
    public class PagingMetadataDTO
    {
        public int TotalItemCount { get; set; }

        public double TotalPageCount => Math.Ceiling(TotalItemCount / (double)PageSize);

        public int PageSize { get; set; }

        public int PageNumber{ get; set; }
    }
}
