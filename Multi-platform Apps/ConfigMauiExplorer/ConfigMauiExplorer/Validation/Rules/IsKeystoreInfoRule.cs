using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsKeystoreInfoRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public bool Check(T value)
    {
        if (value is not KeystoreInfo keystoreInfo) return false;
        return !string.IsNullOrWhiteSpace(keystoreInfo.Password) &&
               !string.IsNullOrWhiteSpace(keystoreInfo.Alias);
    }
}