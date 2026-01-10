namespace WarframeCalculator
{
    public static class ExperienceCalculatorr
    {
        public static CalculationResult Calculate(double currentExp,
                                                 double requiredExp,
                                                 double multiplier)
        {
            var result = new CalculationResult();

            double a = requiredExp - currentExp;
            double b = a + (a * 24 / 100);
            double cleanExp = b / multiplier;

            result.CleanExperience = Convert.ToInt32(cleanExp);
            result.Gold = Convert.ToInt32(cleanExp / 9700 * 5);

            return result;
        }
    }

    public class CalculationResult
    {
        public int CleanExperience { get; set; }
        public int Gold { get; set; }
    }
}