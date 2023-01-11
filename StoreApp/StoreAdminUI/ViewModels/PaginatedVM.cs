namespace StoreAdminUI.ViewModels
{
    public class PaginatedVM<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; } 
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
    }
}
