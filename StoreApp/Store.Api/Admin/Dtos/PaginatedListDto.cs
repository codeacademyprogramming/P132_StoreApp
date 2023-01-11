namespace Store.Api.Admin.Dtos
{
    public class PaginatedListDto<T>
    {
        public PaginatedListDto(List<T> items,int pageIndex,int pageSize,int totalCount)
        {
            this.Items = items;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        public List<T> Items { get; set; }
        public bool HasNext => PageIndex < TotalPage;
        public bool HasPrev => PageIndex > 1;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
    }
}
