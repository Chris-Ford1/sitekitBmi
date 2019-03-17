namespace BMI.Service.Models
{
    public class AggregatedBmiData
    {
        public BmiCategory Category { get; }

        public int Count { get; }

        public AggregatedBmiData(BmiCategory bmiCategory, int count)
        {
            Category = bmiCategory;
            Count = count;
        }
    }
}
