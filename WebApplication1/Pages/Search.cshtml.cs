using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpClientFactory _clientFactory;

        public SearchModel(ApplicationDbContext dbContext, IHttpClientFactory clientFactory)
        {
            _dbContext = dbContext;
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public string SearchQuery { get; set; }

        public List<SearchResult> Results { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Results = await _dbContext.SearchResults.ToListAsync();
        }

        public async Task OnPostAsync()
        {
            var existingResult = await _dbContext.SearchResults.FirstOrDefaultAsync(r => r.SearchQuery == SearchQuery);

            if (existingResult == null)
            {
                var client = _clientFactory.CreateClient();

                client.DefaultRequestHeaders.Add("Authorization", "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");
                client.DefaultRequestHeaders.Add("User-Agent", "Lucky");
                var response = await client.GetAsync($"https://api.github.com/search/repositories?q={SearchQuery}");
                if (response.IsSuccessStatusCode)
                {
                    var resultContent = await response.Content.ReadAsStringAsync();

                    var newSearchResult = new SearchResult
                    {
                        SearchQuery = SearchQuery,
                        ResultJson = resultContent
                    };
                    _dbContext.SearchResults.Add(newSearchResult);
                    await _dbContext.SaveChangesAsync();

                }

                existingResult = await _dbContext.SearchResults.FirstOrDefaultAsync(r => r.SearchQuery == SearchQuery);
                if (existingResult == null)
                {
                    Results = new List<SearchResult> { existingResult };
                }
            }
            else
            {
                Results = new List<SearchResult> { existingResult };
            }
        }
    }
}
