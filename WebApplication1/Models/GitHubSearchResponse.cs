using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class GitHubSearchResponse
    {
        //public int total_count { get; set; }
        public List<GitHubProjectInfo> items { get; set; }
    }
}
