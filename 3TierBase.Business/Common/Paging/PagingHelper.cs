namespace _3TierBase.Business.Commons.Paging
{
    public static class PagingHelper
    {
        //public static string GetWithPaging(int pageIndex, int pageSize)
        //{
        //    pageSize = pageSize < 1 ? 1 : pageSize;
        //    pageIndex = pageIndex < 1 ? 1 : pageIndex;

        //    string query = $"OFFSET {(pageIndex - 1) * pageSize} ROWS" +
        //        $" FETCH NEXT {pageSize} ROWS ONLY";
        //    return query;
        //}

        public static IEnumerable<TObject> GetWithPaging<TObject>(this IEnumerable<TObject> source, int pageIndex, int? pageSize)
            where TObject : class
        {
            if (source == null || pageSize == null)
            {
                return Enumerable.Empty<TObject>();
            }

            pageSize = pageSize < 1 ? 1 : pageSize;
            pageIndex = pageIndex < 1 ? 1 : pageIndex;

            source = source
                .Skip((int)(pageIndex == 1 ? 0 : pageSize * (pageIndex - 1))) // Paging
                .Take((int)pageSize); // Take only a number of items

            return source;
        }
    }
}
