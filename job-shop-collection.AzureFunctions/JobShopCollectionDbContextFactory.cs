using job_shop_collection.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace job_shop_collection.AzureFunctions
{
    public class JobShopCollectionDbContextFactory : IDesignTimeDbContextFactory<JobShopCollectionDbContext>
    {
        public JobShopCollectionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JobShopCollectionDbContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("JobShopCollectionConnectionString"));

            return new JobShopCollectionDbContext(optionsBuilder.Options);
        }
    }
}
