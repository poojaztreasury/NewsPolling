using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPolling.Models
{

    public interface INews
    {

        Task<List<News>> GetPaginatedResult(int currentPage, int pageSize = 5);
        Task<int> GetCount();
    }
    public class News: INews
    {
        public int newsid { get; set; }
        public string title { get; set; }

        public string description { get; set; }
        public int? repoterid { get; set; }
        public string email { get; set; }
        public int votes { get; set; }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<List<News>> GetPaginatedResult(int currentPage, int pageSize = 5)
        {
            throw new NotImplementedException();
        }
    }
}
