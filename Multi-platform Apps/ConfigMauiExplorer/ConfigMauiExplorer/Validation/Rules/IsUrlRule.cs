using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsUrlRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public bool Check(T value)
    {
        if (value is not string str) return false;
        return !string.IsNullOrEmpty(str) && Uri.IsWellFormedUriString(str, UriKind.Absolute);
    }
}