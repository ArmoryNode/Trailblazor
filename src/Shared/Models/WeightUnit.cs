using System.ComponentModel.DataAnnotations;

namespace Trailblazor.Shared.Models
{
    public enum WeightUnit
    {
        [Display(Name = "Gram", ShortName = "g")]
        Grams,
        [Display(Name = "Kilogram", ShortName = "kg")]
        Kilograms,
        [Display(Name = "Ounce", ShortName = "oz")]
        Ounces,
        [Display(Name = "Pound", ShortName = "lb")]
        Pounds
    }
}
