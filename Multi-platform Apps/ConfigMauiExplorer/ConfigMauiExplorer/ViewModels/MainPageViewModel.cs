using CommunityToolkit.Mvvm.ComponentModel;
using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation;
using ConfigMauiExplorer.Validation.Rules;

namespace ConfigMauiExplorer.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
   [ObservableProperty]
   private ValidatableObject<AndroidColors> _colors = new();
   
   [ObservableProperty]
   private ValidatableObject<AppConfiguration> _appConfiguration= new();
   
   [ObservableProperty]
   private ValidatableObject<CertificateInfo> _certificateInfo = new();
   
   [ObservableProperty]
   private ValidatableObject<KeystoreInfo> _keyStore = new();
   
   [ObservableProperty]
   private ValidatableObject<PushSettings> _pushSettingsIOS = new();
   [ObservableProperty]
   private ValidatableObject<PushSettings> _pushSettingsAndroid = new();
   
   [ObservableProperty]
   private ValidatableObject<Settings> _settingsIOS = new();
   [ObservableProperty]
   private ValidatableObject<Settings> _settingsAndroid = new();

   public MainPageViewModel()
   {
      
   }

   private void AddValidations()
   {
       Colors.Validations.Add(new IsAndroidColorRule<AndroidColors>()
           { ValidationMessage = "Invalid color value/values." });
       SettingsIOS.Validations.Add(new IsSettingsRule<Settings>()
           { ValidationMessage = "Application Name and Package Name are required." });
       SettingsAndroid.Validations.Add(new IsSettingsRule<Settings>()
           { ValidationMessage = "Application Name and Package Name are required." });
       AppConfiguration.Validations.Add(new IsAppConfigurationRule<AppConfiguration>()
           { ValidationMessage = "Invalid App Configuration." });
       CertificateInfo.Validations.Add(new IsCertificateInfoRule<CertificateInfo>()
           { ValidationMessage = "Invalid Certificate Info." });
       KeyStore.Validations.Add(new IsKeystoreInfoRule<KeystoreInfo>()
           { ValidationMessage = "Invalid Keystore Info." });
       PushSettingsIOS.Validations.Add(new IsPushSettingsRule<PushSettings>()
           { ValidationMessage = "Invalid Push Settings." });
       PushSettingsAndroid.Validations.Add(new IsPushSettingsRule<PushSettings>()
           { ValidationMessage = "Invalid Push Settings." });
   }

}