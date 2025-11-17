using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsColorRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    
    public bool Check(T value) => value is string str && !string.IsNullOrWhiteSpace(str) && IsValidColor(str);

    private bool IsValidColor(string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            // A valid hex color can be in the format #RRGGBB or #RGB
            if (str.StartsWith("#"))
            {
                str = str.Substring(1);
                return str.Length == 6 && int.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out _);
            }
        }
        return false;
    }
}