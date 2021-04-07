using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace job_shop_collection.Data.Models
{
    public class JobSetsQuery
    {
        public int Limit { get; set; } = 100;

        public int? PageToken { get; set; }
    }
}
