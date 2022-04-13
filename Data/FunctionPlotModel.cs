using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Function_Plotter.Data
{
    public class FunctionPlotModel
    {
        [Required(ErrorMessage = "Enter F(X)")]
        [StringRange(AllowableValues = new[] { " ", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "+", "-", "*", "/", "^", "x" }, ErrorMessage = "Enter a vlid operator + - / * ^")]
        public string FunctionOfX { get; set; } = String.Empty;

        [Required(ErrorMessage = "Enter min value of x")]
        public int MinValueOfX { get; set; }

        [GreaterOrEqual("MinValueOfX", ErrorMessage = "Max value of x must be greater than min value of x")]
        [Required(ErrorMessage = "Enter max value of x")]
        public int MaxValueOfX { get; set; }
    }
}
public class StringRangeAttribute : ValidationAttribute
{
    public string[] AllowableValues { get; set; }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        char[] ch = new char[value.ToString().Length];

        for (int i = 0; i < value.ToString().Length; i++)
        {
            ch[i] = value.ToString()[i];
        }

        foreach (char c in ch)
        {
            if (AllowableValues?.Contains(c.ToString()) == false)
            {
                var msg = $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
                return new ValidationResult(msg);
            }
        }
        return ValidationResult.Success;
    }
}

public class GreaterOrEqual : ValidationAttribute
{
    private readonly string _other;
    public GreaterOrEqual(string other) { _other = other; }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_other);
        var otherValue = property.GetValue(validationContext.ObjectInstance, null);
        if (otherValue is int && value is int)
        {
            var max = (int)value;
            var min = (int)otherValue;
            if (max >= min) return ValidationResult.Success;
        }
        return new ValidationResult("Max value of x must be greater than min value of x"); ;
    }
}
