using System;
using System.Globalization;
using System.Windows.Controls;

namespace TasksManager.Application.Utility
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);
            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, "Enter the title");
            }
               return new ValidationResult(true, null);
        }
    }
}
