using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class GitHubProjectInfo
    {
        public string name { get; set; }
        public Owner owner { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
        public string html_url { get; set; }
    }

    public class Owner
    {
        public string login { get; set; }
    }
}
