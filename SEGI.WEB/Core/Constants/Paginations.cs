using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEGI.Core.Helpers;

namespace SEGI.Core.Constants
{
    public static class Paginations
    {
        public static PagedData<T> PagedResult<T>(this IEnumerable<T> list, int PageNumber, int PageSize) where T : class
        {
            var result = new PagedData<T>();
            result.Data = list.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
            result.TotalPages = Convert.ToInt32(Math.Ceiling((double)list.Count() / PageSize));
            result.CurrentPage = PageNumber;
            return result;
        }

        public static DataSingleModel<T> Result<T>(this T model) where T : class
        {
            var result = new DataSingleModel<T>();
            result.Data = model;
            return result;
        }
    }
}
