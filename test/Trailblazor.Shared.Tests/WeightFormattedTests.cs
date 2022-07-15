using Trailblazor.Shared.Models;

namespace Trailblazor.Shared.Tests
{
    public class WeightFormattedTests
    {
        [Theory]
        [InlineData(WeightUnit.Kilograms, "Kilograms")]
        [InlineData(WeightUnit.Grams, "Grams")]
        [InlineData(WeightUnit.Pounds, "Pounds")]
        [InlineData(WeightUnit.Ounces, "Ounces")]
        public void When_CallingGetDisplayNameOnWeightUnit_Expect_DisplayName(WeightUnit unit, string expected)
        {
            var actual = unit.GetDisplayName();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, WeightUnit.Kilograms, "1 Kilogram")]
        [InlineData(2, WeightUnit.Kilograms, "2 Kilograms")]
        [InlineData(1, WeightUnit.Grams, "1 Gram")]
        [InlineData(2, WeightUnit.Grams, "2 Grams")]
        [InlineData(1, WeightUnit.Pounds, "1 Pound")]
        [InlineData(2, WeightUnit.Pounds, "2 Pounds")]
        [InlineData(1, WeightUnit.Ounces, "1 Ounce")]
        [InlineData(2, WeightUnit.Ounces, "2 Ounces")]
        public void When_CallingToStringOnWeight_Expect_CorrectlyFormattedString(decimal amount, WeightUnit unit, string expected)
        {
            var weight = new Weight(amount, unit);
            var actual = weight.ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, WeightUnit.Kilograms, "1kg")]
        [InlineData(1, WeightUnit.Grams, "1g")]
        [InlineData(1, WeightUnit.Pounds, "1lb")]
        [InlineData(1, WeightUnit.Ounces, "1oz")]
        public void When_CallingToShortStringOnWeight_Expect_CorrectlyFormattedShortString(decimal amount, WeightUnit unit, string expected)
        {
            var weight = new Weight(amount, unit);
            var actual = weight.ToShortString();

            Assert.Equal(expected, actual);
        }
    }
}
