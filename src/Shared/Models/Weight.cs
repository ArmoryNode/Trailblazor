﻿using Trailblazor.Shared.Extensions;

namespace Trailblazor.Shared.Models
{
    public record struct Weight : IEquatable<Weight>
    {
        public decimal Amount { get; set; }
        public WeightUnit Unit { get; init; }

        public Weight(decimal amount)
        {
            Amount = amount;
            Unit = default;
        }

        public Weight(decimal amount, WeightUnit unit)
        {
            Amount = amount;
            Unit = unit;
        }

        #region [Utilities]

        public decimal As(WeightUnit unit)
        {
            if (unit == Unit)
                return Amount;

            return Unit switch
            {
                WeightUnit.Pounds => unit switch
                {
                    WeightUnit.Ounces => PoundsToOunces(Amount),
                    WeightUnit.Grams => PoundsToGrams(Amount),
                    WeightUnit.Kilograms => PoundsToKilograms(Amount),
                    _ => Amount
                },
                WeightUnit.Ounces => unit switch
                {
                    WeightUnit.Pounds => OuncesToPounds(Amount),
                    WeightUnit.Grams => OuncesToGrams(Amount),
                    WeightUnit.Kilograms => OuncesToKilograms(Amount),
                    _ => Amount
                },
                WeightUnit.Grams => unit switch
                {
                    WeightUnit.Kilograms => GramsToKilograms(Amount),
                    WeightUnit.Ounces => GramsToOunces(Amount),
                    WeightUnit.Pounds => GramsToPounds(Amount),
                    _ => Amount
                },
                WeightUnit.Kilograms => unit switch
                {
                    WeightUnit.Pounds => KilogramsToPounds(Amount),
                    WeightUnit.Ounces => KilogramsToOunces(Amount),
                    WeightUnit.Grams => KilogramsToGrams(Amount),
                    _ => Amount
                },
                _ => Amount
            };
        }

        #endregion

        #region [Converters]

        public static decimal GramsToKilograms(decimal grams, int decimals = 3) => Math.Round(grams / 1000m, decimals, MidpointRounding.AwayFromZero);
        public static decimal GramsToOunces(decimal grams, int decimals = 3) => Math.Round(grams / 28.34952m, decimals, MidpointRounding.AwayFromZero);
        public static decimal GramsToPounds(decimal grams, int decimals = 3) => Math.Round(grams / 453.59237m, decimals, MidpointRounding.AwayFromZero);
        public static decimal KilogramsToGrams(decimal kilograms, int decimals = 3) => Math.Round(kilograms * 1000m, decimals, MidpointRounding.AwayFromZero);
        public static decimal KilogramsToOunces(decimal kilograms, int decimals = 3) => Math.Round(kilograms / 0.02834952m, decimals, MidpointRounding.AwayFromZero);
        public static decimal KilogramsToPounds(decimal kilograms, int decimals = 3) => Math.Round(kilograms / 0.45359237m, decimals, MidpointRounding.AwayFromZero);
        public static decimal OuncesToGrams(decimal ounces, int decimals = 3) => Math.Round(ounces * 28.34952m, decimals, MidpointRounding.AwayFromZero);
        public static decimal OuncesToKilograms(decimal ounces, int decimals = 3) => Math.Round(ounces * 0.02834952m, decimals, MidpointRounding.AwayFromZero);
        public static decimal OuncesToPounds(decimal ounces, int decimals = 3) => Math.Round(ounces / 16m, decimals, MidpointRounding.AwayFromZero);
        public static decimal PoundsToGrams(decimal pounds, int decimals = 3) => Math.Round(pounds * 453.59237m, decimals, MidpointRounding.AwayFromZero);
        public static decimal PoundsToKilograms(decimal pounds, int decimals = 3) => Math.Round(pounds * 0.45359237m, decimals, MidpointRounding.AwayFromZero);
        public static decimal PoundsToOunces(decimal pounds, int decimals = 3) => Math.Round(pounds * 16m, decimals, MidpointRounding.AwayFromZero);

        #endregion

        #region [Operators]

        public static bool operator >(Weight a, Weight b) => a.Amount > b.As(a.Unit);
        public static bool operator <(Weight a, Weight b) => a.Amount < b.As(a.Unit);

        public static bool operator >=(Weight a, Weight b) => a.Amount >= b.As(a.Unit);
        public static bool operator <=(Weight a, Weight b) => a.Amount <= b.As(a.Unit);

        public static Weight operator +(Weight a, Weight b) => new(a.Amount + b.As(a.Unit), a.Unit);
        public static Weight operator -(Weight a, Weight b) => new(a.Amount - b.As(a.Unit), a.Unit);

        #endregion

        #region [Equality]

        public bool Equals(Weight other) => Amount == other.As(Unit);

        #endregion

        #region [Overrides]

        public override int GetHashCode() => (int)Unit ^ (int)Amount;

        public override string ToString()
        {
            var name = Amount == 1 || Amount == -1 ? Unit.GetName() : Unit.GetName() + "s";
            return $"{Amount}\u0020{name}";
        }

        public string ToShortString() => $"{Amount}{Unit.GetShortName()}";

        #endregion
    }
}
