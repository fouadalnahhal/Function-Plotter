using System.ComponentModel.DataAnnotations;

namespace Function_Plotter.Data
{
    public class FunctionPlotModel
    {
        [Required]
        [StringRange(AllowableValues = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "+", "-", "*", "/", "^", "x" }, ErrorMessage = "Enter a vlid operator + - / * ^")]
        public string FunctionOfX { get; set; } = String.Empty;

        [Required]
        public int MinValueOfX { get; set; }

        [Required]
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
