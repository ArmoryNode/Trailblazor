using Trailblazor.Shared.Models;

namespace Trailblazor.Shared.Tests
{
    public class WeightConversionTests
    {
        [Theory]
        [InlineData(1, 16)]
        [InlineData(10, 160)]
        public void When_ConvertingPoundsToOunces_Expect_Ounces(decimal input, decimal expected)
        {
            var actual = Weight.PoundsToOunces(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 453.592)]
        [InlineData(10, 4535.924)]
        public void When_ConvertingPoundsToGrams_Expect_Grams(decimal input, decimal expected)
        {
            var actual = Weight.PoundsToGrams(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0.454)]
        [InlineData(10, 4.536)]
        public void When_ConvertingPoundsToKilograms_Expect_Kilograms(decimal input, decimal expected)
        {
            var actual = Weight.PoundsToKilograms(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0.063)]
        [InlineData(10, 0.625)]
        public void When_ConvertingOuncesToPounds_Expect_Pounds(decimal input, decimal expected)
        {
            var actual = Weight.OuncesToPounds(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 28.350)]
        [InlineData(10, 283.495)]
        public void When_ConvertingOuncesToGrams_Expect_Grams(decimal input, decimal expected)
        {
            var actual = Weight.OuncesToGrams(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0.028)]
        [InlineData(10, 0.283)]
        public void When_ConvertingOuncesToKilograms_Expect_Kilograms(decimal input, decimal expected)
        {
            var actual = Weight.OuncesToKilograms(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0.035)]
        [InlineData(10, 0.353)]
        public void When_ConvertingGramsToOunces_Expect_Ounces(decimal input, decimal expected)
        {
            var actual = Weight.GramsToOunces(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0.002)]
        [InlineData(10, 0.022)]
        public void When_ConvertingGramsToPounds_Expect_Pounds(decimal input, decimal expected)
        {
            var actual = Weight.GramsToPounds(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0.001)]
        [InlineData(10, 0.010)]
        public void When_ConvertingGramsToKilograms_Expect_Kilograms(decimal input, decimal expected)
        {
            var actual = Weight.GramsToKilograms(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 2.205)]
        [InlineData(10, 22.046)]
        public void When_ConvertingKilogramsToPounds_Expect_Pounds(decimal input, decimal expected)
        {
            var actual = Weight.KilogramsToPounds(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 35.274)]
        [InlineData(10, 352.740)]
        public void When_ConvertingKilogramsToOunces_Expect_Ounces(decimal input, decimal expected)
        {
            var actual = Weight.KilogramsToOunces(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1000.000)]
        [InlineData(10, 10000.000)]
        public void When_ConvertingKilogramsToGrams_Expect_Grams(decimal input, decimal expected)
        {
            var actual = Weight.KilogramsToGrams(input);
            Assert.Equal(expected, actual);
        }
    }
}