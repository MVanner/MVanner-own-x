using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsPushSettingsRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public bool Check(T value)
    {
        if (value is not PushSettings pushSettings) return false;
        return !string.IsNullOrWhiteSpace(pushSettings.ProviderReference) &&
               !string.IsNullOrWhiteSpace(pushSettings.PushProviderType);
    }
}