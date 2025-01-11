using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Attributes
{
    /// <summary>
    /// Custom validation attribute to compare a DateTime property with another DateTime property,
    /// ensuring the current value is greater than the comparison property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the name of the property to compare against.
        /// </summary>
        public string ComparisonProperty { get; private set; }

        /// <summary>
        /// Gets or sets the range of time to add to the comparison value (default is 1).
        /// </summary>
        public int ComparisonRange { get; private set; } = 1;

        /// <summary>
        /// Gets or sets the unit of time for comparison (e.g., Day, Hour, Month, Year).
        /// Default is "Day".
        /// </summary>
        public string ComparisonType { get; private set; } = "Day";

        /// <summary>
        /// Initializes a new instance of the <see cref="DateGreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="comparisonProperty">The name of the property to compare against.</param>
        /// <param name="comparisonRange">The range of time to add to the comparison value (optional, default is 1).</param>
        /// <param name="comparisonType">The unit of time for comparison (e.g., Day, Hour, Month, Year).</param>
        public DateGreaterThanAttribute(string comparisonProperty, int comparisonRange = 1, string comparisonType = "Day")
        {
            ComparisonProperty = comparisonProperty;
            ComparisonRange = comparisonRange;
            ComparisonType = comparisonType;
        }

        /// <summary>
        /// Validates that the current property value is greater than the specified comparison property.
        /// </summary>
        /// <param name="value">The current property value.</param>
        /// <param name="validationContext">The context of the validation operation.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether validation succeeded or failed.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // If value is null, no validation error (use [Required] for mandatory fields)
            }

            var currentValue = (DateTime)value;
            var comparisonPropertyInfo = validationContext.ObjectType.GetProperty(ComparisonProperty);
            if (comparisonPropertyInfo == null)
            {
                throw new ArgumentException($"Property '{ComparisonProperty}' not found.");
            }

            var comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == null)
            {
                return ValidationResult.Success; // If the comparison value is null, skip validation
            }

            if (comparisonValue is not DateTime)
            {
                throw new ArgumentException("Comparison property must be of type DateTime.");
            }

            var comparisonDateTime = (DateTime)comparisonValue;
            var adjustedComparisonValue = AdjustComparisonValue(comparisonDateTime);

            // Perform the comparison
            if (currentValue <= adjustedComparisonValue)
            {
                var errorMessage = string.IsNullOrEmpty(ErrorMessage)
                    ? $"The {validationContext.DisplayName} must be greater than {ComparisonProperty} {ComparisonRange} {ComparisonType}."
                    : ErrorMessage;

                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Adjusts the comparison value based on the comparison type and range.
        /// </summary>
        /// <param name="comparisonValue">The original comparison DateTime value.</param>
        /// <returns>The adjusted comparison DateTime value.</returns>
        private DateTime AdjustComparisonValue(DateTime comparisonValue)
        {
            return ComparisonType switch
            {
                "Day" => comparisonValue.AddDays(ComparisonRange),
                "Hour" => comparisonValue.AddHours(ComparisonRange),
                "Month" => comparisonValue.AddMonths(ComparisonRange),
                "Year" => comparisonValue.AddYears(ComparisonRange),
                _ => comparisonValue // Default to the original value if no valid type is provided
            };
        }
    }
}
