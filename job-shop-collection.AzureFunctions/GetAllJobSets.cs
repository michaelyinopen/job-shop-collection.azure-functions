using AutoMapper;
using AutoMapper.QueryableExtensions;
using job_shop_collection.Data;
using job_shop_collection.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace job_shop_collection.AzureFunctions
{
    public class GetAllJobSets
    {
        private readonly JobShopCollectionDbContext _jobShopCollectionDbContext;
        private readonly IMapper _mapper;

        public GetAllJobSets(
            JobShopCollectionDbContext jobShopCollectionDbContext,
            IMapper mapper)
        {
            _jobShopCollectionDbContext = jobShopCollectionDbContext;
            _mapper = mapper;
        }

        [FunctionName("GetAllJobSets")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "job-sets")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processing request GetAllJobSets.");

            int? pageToken = int.TryParse(req.Query["pageToken"], out int pageTokenResult) ? pageTokenResult : default(int?);

            int limitDefault = 100;
            int limit = int.TryParse(req.Query["limit"], out int limitResult) ? limitResult : limitDefault;

            IQueryable<JobSet> dataQuery = _jobShopCollectionDbContext.JobSet;
            if (pageToken.HasValue)
            {
                dataQuery = dataQuery.Where(j => j.Id < pageToken);
            }
            var data = await dataQuery
                .OrderByDescending(j => j.Id)
                .Take(limit)
                .ProjectTo<JobSetHeaderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            int? nextPageToken = data.Count == limit ? data[^1].Id : default(int?);

            var result = new JobSetHeadersDto
            {
                Data = data,
                NextPageToken = nextPageToken
            };

            return new OkObjectResult(result);
        }
    }
}
