using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMI.Service.Models;
using BMI.Service.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace BMI.Service.Queries
{
    public class BmiQueries : IBmiQueries
    {
        private readonly IBmiRepository _bmiRepository;

        public BmiQueries(IBmiRepository bmiRepository)
        {
            _bmiRepository = bmiRepository ?? throw new ArgumentNullException(nameof(bmiRepository));
        }

        public async Task<IEnumerable<BmiModel>> GetRecords()
        {
            return await _bmiRepository.GetRecords();
        }

        public async Task<IEnumerable<AggregatedBmiData>> GetAggregatedRecords()
        {
            var records =  await _bmiRepository.GetRecords();

            if (records == null || !records.Any())
            {
                return new List<AggregatedBmiData>();
            }

            var groupedData = records.GroupBy(info => info.Category)
                .Select(group => new AggregatedBmiData(
                    group.Key,
                    group.Count()
                ))
                .OrderBy(x => x.Category);

            return groupedData;
        }

       
    }
}
