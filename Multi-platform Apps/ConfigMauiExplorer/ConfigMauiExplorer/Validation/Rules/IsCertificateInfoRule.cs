using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation.Interfaces;

namespace ConfigMauiExplorer.Validation.Rules;

public class IsCertificateInfoRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public bool Check(T value)
    {
        if (value is not CertificateInfo certificateInfo) return false;
        return !string.IsNullOrWhiteSpace(certificateInfo.Password);
    }
}