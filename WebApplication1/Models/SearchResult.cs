using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string SearchQuery { get; set; }
        public string ResultJson { get; set; }
    }
}
