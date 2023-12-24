using _3TierBase.Business.Commons.Paging;
using System.Linq.Expressions;

namespace _3Tier.Business.Common
{
    public static class SortingHelper
    {
        //public static string GetWithSorting(object sortKey, object sortOrder)
        //{
        //    string query = "";
        //    if (sortKey != null && sortOrder != null)
        //    {
        //        sortKey = sortKey.ToString();
        //        sortOrder = (PagingConstant.OrderCriteria)sortOrder;
        //        switch (sortOrder)
        //        {
        //            case PagingConstant.OrderCriteria.DESC:
        //                query = $"ORDER BY {sortKey} {PagingConstant.OrderCriteria.DESC}";
        //                break;
        //            case PagingConstant.OrderCriteria.ASC:
        //                query = $"ORDER BY {sortKey} {PagingConstant.OrderCriteria.ASC}";
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    if (query == "")
        //    {
        //        query = "ORDER BY Id";
        //    }
        //    return query;
        //}

        public static IQueryable<TObject> GetWithSorting<TObject>(this IQueryable<TObject> source,
            string sortKey, PagingConstant.OrderCriteria sortOrder) where TObject : class
        {
            if (source == null) return Enumerable.Empty<TObject>().AsQueryable();

            if (sortKey != null)
            {
                var param = Expression.Parameter(typeof(TObject), "p");
                var prop = Expression.Property(param, sortKey);
                var exp = Expression.Lambda(prop, param);
                string method = "";
                switch (sortOrder)
                {
                    case PagingConstant.OrderCriteria.ASC:
                        method = "OrderBy";
                        break;
                    default:
                        method = "OrderByDescending";
                        break;
                }
                Type[] types = new Type[] { source.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types, source.Expression, exp);
                return source.Provider.CreateQuery<TObject>(mce);
            }
            return source;
        }
    }
}
