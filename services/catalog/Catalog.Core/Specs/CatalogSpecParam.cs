namespace Catalog.Core.Specs
{
    public class CatalogSpecParam
    {
        private const int MaxPageSize = 80;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int PageIndex { get; set; } = 1;

        //Filtering
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }

        //Sorting 
        public string? Sort { get; set; }

        //Searching 
        public string? Search { get; set; }
    }
}
