using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination
{
    public class Pagination
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public Pagination(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void Page()
        {
            var myList = new List<string>();
            int pageSize = 10;
            var totalCount = myList.Count();
            var pageNumber = 1;
            var number = Math.Min((pageSize * pageNumber), totalCount);
            var index = 0;
            while (index < totalCount)
            {
                List<Task<bool>> lstTasks = new List<Task<bool>>();
                for (int i = pageNumber - 1; index < number; i++)
                {
                    //var q = lstDashboardQueryList[index];
                    //var task = Task.Factory.StartNew(() => Sync(syncServiceType, hospital, q, strDate));
                    //lstTasks.Add(task);

                    index++;
                };
                pageNumber++;
                number = (pageNumber * pageSize);
                if (number > totalCount)
                {
                    number = totalCount;
                }
            }
        }

        public object Page2<T>(IQueryable<T> items, int pageSize)
        {
            var totalPages = (int)Math.Ceiling(items.Count() / (double)pageSize);

            var paginationHeader = new
            {
                TotalCount = items.Count(),
                TotalPages = totalPages
            };
            httpContextAccessor.HttpContext.Response.Headers.TryAdd("X-Pagination", JsonConvert.SerializeObject(paginationHeader));
            httpContextAccessor.HttpContext.Response.Headers.TryAdd("Access-Control-Expose-Headers", "X-Pagination");

            return paginationHeader;
        }
    }
}
