using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    /// <summary>
    /// Paginated response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T> : Result, IPagingResult<T>
    {
        public PagingResult(List<T> data, int totalItemCount, bool success, string message) : base(success, message)
        {
            Data = data;
            TotalItemCount = totalItemCount;
        }

        public List<T> Data { get; set; }
        public int TotalItemCount { get; set; }
    }
}
