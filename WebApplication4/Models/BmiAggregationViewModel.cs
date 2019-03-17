using System.Collections.Generic;
using BMI.Service.Models;

namespace WebApplication4.Models
{
    public class BmiAggregationViewModel
    {
        public IEnumerable<AggregatedBmiData> AggregatedBmi { get; }
        
        public BmiAggregationViewModel(IEnumerable<AggregatedBmiData> aggregatedBmi)
        {
            AggregatedBmi = aggregatedBmi;
        }
    }
}
