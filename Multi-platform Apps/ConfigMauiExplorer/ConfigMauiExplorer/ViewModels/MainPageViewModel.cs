using CommunityToolkit.Mvvm.ComponentModel;
using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Validation;
using ConfigMauiExplorer.Validation.Interfaces;
using ConfigMauiExplorer.Validation.Rules;

namespace ConfigMauiExplorer.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
   [ObservableProperty]
   private ValidatableObject<string> _splashBackgroundColor = new();
   [ObservableProperty]
   private ValidatableObject<string> _splashContentColor = new();
   [ObservableProperty]
   private ValidatableObject<string> _appIconBackgroundColor = new();
   [ObservableProperty]
   private ValidatableObject<string> _notificationIconBackgroundColor= new();

   [ObservableProperty]
   private ValidatableObject<string> _apiUri = new();

   [ObservableProperty]
   private ValidatableObject<string> _nativeAppSettingsToken = new();

   [ObservableProperty]
   private ValidatableObject<string> _baseUri = new();

   [ObservableProperty]
   private ValidatableObject<string> _currentAppUri = new();

   [ObservableProperty]
   private ValidatableObject<string> _loginUri = new();

   [ObservableProperty]
   private ValidatableObject<string> _pushProviderSRefIOS = new();
   [ObservableProperty]
   private ValidatableObject<string> _pushProviderSRefAndroid = new();
   
   [ObservableProperty]
   private ValidatableObject<List<string>> _appHostNames = new();
   
   
   
   [ObservableProperty]
   private ValidatableObject<string> _certificatePassword = new();
   
   [ObservableProperty]
   private ValidatableObject<string> _keystorePassword = new();
   [ObservableProperty]
   private ValidatableObject<string> _keystoreAlias = new();
   
   [ObservableProperty]
   private ValidatableObject<string> _settingsPackageNameiOS = new();
   [ObservableProperty]
   private ValidatableObject<string> _settingsPackageNameAndroid = new();
   [ObservableProperty]
   private ValidatableObject<string> _applicationName = new();

   public MainPageViewModel()
   {
      AddValidations();
   }

   private void AddValidations()
   {
      SplashBackgroundColor.Validations.Add(new IsColorRule<string>() { ValidationMessage = "Invalid color format for Splash Background Color" });
      SplashContentColor.Validations.Add(new IsColorRule<string>() { ValidationMessage = "Invalid color format for Splash Content Color" });
      AppIconBackgroundColor.Validations.Add(new IsColorRule<string>() { ValidationMessage = "Invalid color format for App Icon Background Color" });
      NotificationIconBackgroundColor.Validations.Add(new IsColorRule<string>() { ValidationMessage = "Invalid color format for Notification Icon Background Color" });
      ApiUri.Validations.Add(new IsUrlRule<string>() { ValidationMessage = "Invalid URL format for App Uri" });
      CurrentAppUri.Validations.Add(new IsUrlRule<string>() { ValidationMessage = "Current App Uri cannot be empty" });
      LoginUri.Validations.Add(new IsUrlRule<string>() { ValidationMessage = "Login Uri cannot be empty" });
      NativeAppSettingsToken.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Native App Settings Token cannot be empty" });
      BaseUri.Validations.Add(new IsUrlRule<string>() { ValidationMessage = "Base Uri cannot be empty" });
      PushProviderSRefIOS.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Push Provider SRef iOS cannot be empty" });
      PushProviderSRefAndroid.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Push Provider SRef Android cannot be empty" });
      AppHostNames.Validations.Add(new IsNullOrEmptyRule<List<string>>() { ValidationMessage = "App Host Names cannot be empty" });
      CertificatePassword.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Certificate Password cannot be empty" });
      KeystorePassword.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Keystore Password cannot be empty" });
      KeystoreAlias.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Keystore Alias cannot be empty" });
      SettingsPackageNameiOS.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Settings Package Name iOS cannot be empty" });
      SettingsPackageNameAndroid.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Settings Package Name Android cannot be empty" });
      ApplicationName.Validations.Add(new IsNullOrEmptyRule<string>() { ValidationMessage = "Application Name cannot be empty" });
   }

   public bool ValidateAll()
   {
      bool isValid = true;
      isValid &= SplashBackgroundColor.Validate();
      isValid &= SplashContentColor.Validate();
      isValid &= AppIconBackgroundColor.Validate();
      isValid &= NotificationIconBackgroundColor.Validate();
      isValid &= ApiUri.Validate();
      isValid &= CurrentAppUri.Validate();
      isValid &= LoginUri.Validate();
      isValid &= NativeAppSettingsToken.Validate();
      isValid &= BaseUri.Validate();
      isValid &= PushProviderSRefIOS.Validate();
      isValid &= PushProviderSRefAndroid.Validate();
      isValid &= AppHostNames.Validate();
      isValid &= CertificatePassword.Validate();
      isValid &= KeystorePassword.Validate();
      isValid &= KeystoreAlias.Validate();
      isValid &= SettingsPackageNameiOS.Validate();
      isValid &= SettingsPackageNameAndroid.Validate();
      isValid &= ApplicationName.Validate();
      return isValid;
   }
}