using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsAppConfigurationRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public bool Check(T value)
    {
        if (value is not AppConfiguration appConfig) return false;
        return IsValidUri(appConfig.ApiUri) &&
               IsValidUri(appConfig.BaseUri) &&
               IsValidUri(appConfig.CurrentAppUri) &&
               IsValidUri(appConfig.LoginUri) &&
               Guid.TryParse(appConfig.NativeAppSettingsToken, out _);
    }

    private bool IsValidUri(string uri)
    {
        return !string.IsNullOrWhiteSpace(uri) &&
               Uri.IsWellFormedUriString(uri, UriKind.Absolute);
    }
}