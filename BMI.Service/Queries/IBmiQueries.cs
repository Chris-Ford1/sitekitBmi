using BMI.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMI.Service.Queries
{
    public interface IBmiQueries
    {
        Task<IEnumerable<BmiModel>> GetRecords();

        Task<IEnumerable<AggregatedBmiData>> GetAggregatedRecords();
    }
}
