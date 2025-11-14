using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsSettingsRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public bool Check(T value)
    {
        if (value is not Settings settings) return false;
        return !string.IsNullOrWhiteSpace(settings.ApplicationName) &&
               !string.IsNullOrWhiteSpace(settings.PackageName);
    }
}