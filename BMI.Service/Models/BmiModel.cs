using System;

namespace BMI.Service.Models
{
    public class BmiModel
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public double Bmi => CalculateBmi();

        public BmiCategory Category => CalculateCategory();

        private double CalculateBmi()
        {
            var heightSquared = Height * Height;

            return Math.Round(Weight / heightSquared, 2);

        }

        private BmiCategory CalculateCategory()
        {
            if(Bmi < 18.5)
            {
                return BmiCategory.Underweight;
            }

            if (Math.Abs(Bmi - 18.5) < 1 || Bmi <= 24.9)
            {
                return BmiCategory.NormalWeight;
            }

            if(Math.Abs(Bmi - 25.0) < 1 || Bmi <= 29.9)
            {
                return BmiCategory.PreObesity;
            }

            if(Math.Abs(Bmi - 30.0) < 1 || Bmi <= 34.9)
            {
                return BmiCategory.ObesityClassI;
            }

            return BmiCategory.Unknown;
        }
    }
}
