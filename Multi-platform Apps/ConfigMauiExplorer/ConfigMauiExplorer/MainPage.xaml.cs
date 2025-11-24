using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using ConfigMauiExplorer.Interfaces;
using ConfigMauiExplorer.Models;
using ConfigMauiExplorer.Services;
using Microsoft.Maui.Platform;
using System.Text.Json.Serialization;
using ConfigMauiExplorer.ViewModels;
using SensorKit;
using UniformTypeIdentifiers;

namespace ConfigMauiExplorer;

public partial class MainPage : ContentPage
{
    private FileResult NotificationImage { get; set; }
    private FileResult AppIconImage { get; set; }
    private FileResult SplashIconImage { get; set; }
    private FileResult GoogleServices { get; set; } 
    private FileResult Keystore { get; set; }
    private FileResult Certificate { get; set; }
    private FileResult Profile { get; set; }
    
    private readonly IFolderPicker _folderPicker; 
    private readonly MainPageViewModel _viewModel;
    
    private string iOSMulti = "";
    
    public MainPage(IFolderPicker folderPicker, MainPageViewModel viewModel)
    {
        _folderPicker = folderPicker;
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        
        var rows = mainGrid.Count / 2;
        for (int i = 0; i < rows; i++)
        {
            var box = new BoxView();
            box.Color = Colors.Gray;
            box.HeightRequest = 1;
            box.VerticalOptions = LayoutOptions.End;

            var box2 = new BoxView();
            box2.Color = Colors.Gray;
            box2.HeightRequest = 1;
            box2.VerticalOptions = LayoutOptions.End;

            mainGrid.Add(box, 0, i);
            mainGrid.Add(box2, 1, i);
        }
    }

    private async void OnCreateClicked(object? sender, EventArgs e)
    {
        if (_viewModel.ValidateAll())
        {
            AndroidColors androidColors = new AndroidColors();
            iOSColors iOSColors = new iOSColors();
            AppConfiguration androidAppConfig = new AppConfiguration();
            AppConfiguration iOSAppConfig = new AppConfiguration();
            KeystoreInfo keystoreInfo = new KeystoreInfo();
            CertificateInfo certificateInfo = new CertificateInfo();
            Settings iosSettings = new Settings();
            Settings androidSettings = new Settings();
            
            iosSettings.ApplicationName = AppName.Text;
            iosSettings.PackageName = PackageNameIos.Text;
            androidSettings.ApplicationName = AppName.Text;
            androidSettings.PackageName = PackageNameAndroid.Text;
            
            certificateInfo.Password = iOSCertificatePassword.Text;
            keystoreInfo.Password = AndroidKeystorePassword.Text;
            keystoreInfo.Alias = AndroidKeystoreAlias.Text;
            
            iOSColors.AppIconBackgroundColor = AppIconBackgroundColor.Text;
            iOSColors.SplashBackgroundColor = SplashBackgroundColor.Text;
            iOSColors.SplashContentColor = SplashContentColor.Text;
            iOSColors.SplashBackgroundIsLight = IsSplasHBackgroudColorLight.IsChecked;
            
            androidColors.AppIconBackgroundColor = AppIconBackgroundColor.Text;
            androidColors.SplashBackgroundColor = SplashBackgroundColor.Text;
            androidColors.SplashContentColor = SplashContentColor.Text;
            androidColors.SplashBackgroundIsLight = IsSplasHBackgroudColorLight.IsChecked;
            androidColors.NotificationIconBackgroundColor = NotificationIconBackgroundColor.Text;
            
            androidAppConfig.ApiUri = ApiUri.Text;
            androidAppConfig.BaseUri = BaseUri.Text;
            androidAppConfig.LoginUri = LoginUri.Text;
            androidAppConfig.CurrentAppUri = CurrentAppUri.Text;
            androidAppConfig.IsMultibrandedApp = IsMultibrandedApp.IsChecked;
            androidAppConfig.IsNonStoreApp = IsMultibrandedApp.IsChecked;
            androidAppConfig.NativeAppSettingsToken = SettingsTokenAndroid.Text;
            androidAppConfig.AppHostNames = AppHostNames.Text?.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>();
            
            if (IsMultibrandedApp.IsChecked)
            {
                iOSAppConfig.CurrentAppUri = iOSMulti;
                iOSAppConfig.BaseUri = iOSMulti;
            }
            iOSAppConfig.ApiUri = ApiUri.Text;
            iOSAppConfig.BaseUri = BaseUri.Text;
            iOSAppConfig.LoginUri = LoginUri.Text;
            iOSAppConfig.CurrentAppUri = CurrentAppUri.Text;
            iOSAppConfig.IsMultibrandedApp = IsMultibrandedApp.IsChecked;
            iOSAppConfig.IsNonStoreApp = IsMultibrandedApp.IsChecked;
            iOSAppConfig.NativeAppSettingsToken = SettingsTokenIOS.Text;
            iOSAppConfig.AppHostNames = AppHostNames.Text?.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>();
            
            androidAppConfig.PushSettings = new PushSettings()
            {
                ProviderReference = ProviderReferenceAndroid.Text,
                PushProviderType = "firebase"
            };
            
            iOSAppConfig.PushSettings = new PushSettings()
            {
                ProviderReference = ProviderReferenceIos.Text,
                PushProviderType = "apns"
            };
            
            static FilePickerFileType PlatformFolderType() =>
                new(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { UTTypes.Folder.Identifier } },
                    { DevicePlatform.MacCatalyst, new[] { UTTypes.Folder.Identifier } },
                    { DevicePlatform.Android, new[] { "application/vnd.android.package-archive" } },
                    { DevicePlatform.WinUI, new[] { ".folder" } }
                });
            var options = new PickOptions
            {
                PickerTitle = "Please select a folder to save the app configurations",
                FileTypes = PlatformFolderType()
            };
            
            //var folderPath = await _folderPicker.PickFolderAsync();
            var folderPath = await FilePicker.PickAsync(options);
            if (!string.IsNullOrEmpty(folderPath.FullPath))
            {
                var androidPath = Path.Combine(folderPath.FullPath, AppName.Text, "Android");
                var iosPath = Path.Combine(folderPath.FullPath, AppName.Text, "iOS");
                Directory.CreateDirectory(androidPath);
                Directory.CreateDirectory(iosPath);
                
                var iosAssetsDirectory = Path.Combine(iosPath, "Assets");
                Directory.CreateDirectory(iosAssetsDirectory);
                var androidAssetsDirectory = Path.Combine(androidPath, "Assets");
                Directory.CreateDirectory(androidAssetsDirectory);
                
                await File.WriteAllTextAsync(Path.Combine(androidPath, "appConfiguration.json"), JsonSerializer.Serialize(androidAppConfig));
                await File.WriteAllTextAsync(Path.Combine(iosPath, "appConfiguration.json"), JsonSerializer.Serialize(iOSAppConfig));
                await File.WriteAllTextAsync(Path.Combine(androidPath, "colors.json"), JsonSerializer.Serialize(androidColors));
                await File.WriteAllTextAsync(Path.Combine(iosPath, "colors.json"), JsonSerializer.Serialize(iOSColors));
                await File.WriteAllTextAsync(Path.Combine(androidPath, "keystore-info.json"), JsonSerializer.Serialize(keystoreInfo));
                await File.WriteAllTextAsync(Path.Combine(iosPath, "certificate-info.json"), JsonSerializer.Serialize(certificateInfo));
                await File.WriteAllTextAsync(Path.Combine(androidPath, "settings.json"), JsonSerializer.Serialize(androidSettings));
                await File.WriteAllTextAsync(Path.Combine(iosPath, "settings.json"), JsonSerializer.Serialize(iosSettings));

                await using (var stream = await GoogleServices.OpenReadAsync())
                {
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    await File.WriteAllBytesAsync(Path.Combine(androidPath, "google-services.json"), ms.ToArray());
                }
                
                if (!string.IsNullOrWhiteSpace(Keystore.FileName))
                {
                    await using (var stream = await Keystore.OpenReadAsync())
                    {
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);
                        ms.Position = 0;
                        await File.WriteAllBytesAsync(Path.Combine(androidPath, "keystore.p12"), ms.ToArray());
                    }
                }
                
                if(Certificate != null)
                {
                    if (string.IsNullOrWhiteSpace(Certificate.FileName) == false)
                    {
                        await using (var stream = await Certificate.OpenReadAsync())
                        {
                            using var ms = new MemoryStream();
                            await stream.CopyToAsync(ms);
                            ms.Position = 0;
                            await File.WriteAllBytesAsync(Path.Combine(iosPath, "certificate.p12"), ms.ToArray());
                        }
                    }
                }

                if (Profile != null)
                {
                    if (string.IsNullOrWhiteSpace(Profile.FileName) == false)
                    {
                        await using (var stream = await Profile.OpenReadAsync())
                        {
                            using var ms = new MemoryStream();
                            await stream.CopyToAsync(ms);
                            ms.Position = 0;
                            await File.WriteAllBytesAsync(Path.Combine(iosPath, "provisioning-profile.mobileprovision"), ms.ToArray());
                        }
                    }
                }
                
                await GenerateAssets(Path.Combine(folderPath.FullPath, AppName.Text), androidAssetsDirectory, iosAssetsDirectory);
                
                await DisplayAlert("Success", "Apps created successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No folder selected!", "OK");
            }
        }
    }

    private async Task GenerateAssets(string folder, string androidAssetsDirectory, string iosAssetsDirectory)  
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://native.relesysapp.net/api/builds/assets/generate?code=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(AppIconBackgroundColor.Text), "IconBackgroundColor");
        content.Add(new StringContent("android,ios"), "Targets");
        content.Add(new StreamContent(File.OpenRead(NotificationImage.FullPath)), "NotificationIcon", NotificationImage.FullPath);
        content.Add(new StreamContent(File.OpenRead(AppIconImage.FullPath)), "AppIcon", AppIconImage.FullPath);
        content.Add(new StreamContent(File.OpenRead(SplashIconImage.FullPath)), "SplashImage", SplashIconImage.FullPath);
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var fileData = await response.Content.ReadAsByteArrayAsync();
        var zipPath = Path.Combine(folder, "assets.zip");
        await File.WriteAllBytesAsync(zipPath, fileData);

        // Extract to temp folder
        var tempExtractPath = Path.Combine(folder, "TempExtract");
        ZipFile.ExtractToDirectory(zipPath, tempExtractPath);

        // Move ios assets
        var iosSource = Path.Combine(tempExtractPath, "ios");
        CopyDirectory(iosSource, iosAssetsDirectory);

        // Move android assets
        var androidSource = Path.Combine(tempExtractPath, "android");
        CopyDirectory(androidSource, androidAssetsDirectory);

        // Clean up temp folder
        Directory.Delete(tempExtractPath, true);
    }

    // Helper to copy directories recursively
    private void CopyDirectory(string sourceDir, string targetDir)
    {
        Directory.CreateDirectory(targetDir);
        foreach (var file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
        {
            var relativePath = Path.GetRelativePath(sourceDir, file);
            var destPath = Path.Combine(targetDir, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(destPath));
            File.Copy(file, destPath, true);
        }
    }


    private async void OnPickAppIconImageClicked(object? sender, EventArgs e)
    {
        try
        {
            // Open the image picker
            var result = await PickAndShow(PickOptions.Images);

            if (result != null)
            {
                // Load the selected image into the Image control
                AppIconImage = result;
                var stream = await result.OpenReadAsync();
                SelectedAppIcon.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void OnPickSplashImageClicked(object? sender, EventArgs e)
    {
        try
        {   
            var result = await PickAndShow(PickOptions.Images);

            if (result == null)
                return;
            SplashIconImage = result;
            var stream = await result.OpenReadAsync();
            SelectedSplashIcon.Source = ImageSource.FromStream(() => stream); 
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return null;
    }

    private async void OnPickNotificationImageClicked(object? sender, EventArgs e)
    {
        try
        {   
            var result = await PickAndShow(PickOptions.Images);

            if (result == null)
                return;

            NotificationImage = result;
            var stream = await result.OpenReadAsync();
            SelectedNotificationIcon.Source = ImageSource.FromStream(() => stream); 
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async void OnPickKeystoreClicked(object? sender, EventArgs e)
    {
        try
        {   
            var result = await PickAndShow(PickOptions.Default);

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            Keystore = result;
            KeystoreText.Text = result.FullPath;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async void OnPickCertificateClicked(object? sender, EventArgs e)
    {
        try
        {   
            var result = await PickAndShow(PickOptions.Default);

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            Certificate = result;
            CertificateText.Text = result.FullPath;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async void OnPickProfileClicked(object? sender, EventArgs e)
    {
         try
        {   
            var result = await PickAndShow(PickOptions.Default);

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            Profile = result;
            ProfileText.Text = result.FullPath;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async void OnPickGoogleServiceClicked(object? sender, EventArgs e)
    {
        try
        {   
            var result = await PickAndShow(PickOptions.Default);

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            GoogleServices = result;
            Google.Text = result.FullPath;

            using var reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();
            
            JsonNode node = JsonNode.Parse(json);
            LoadJsonAndCreateFields(json);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
    private void LoadJsonAndCreateFields(string json)
    {
        GoogleFileStack.Children.Clear();
        AddFieldsRecursive(JsonNode.Parse(json), "");
    }
    
    private void AddFieldsRecursive(JsonNode node, string prefix)
    {
        if (node is JsonObject obj)
        {
            foreach (var prop in obj)
            {
                var key = string.IsNullOrEmpty(prefix) ? prop.Key : $"{prop.Key}";
                AddFieldsRecursive(prop.Value, key);
            }
        }
        else if (node is JsonArray arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                AddFieldsRecursive(arr[i], $"{prefix}[{i}]");
            }
        }
        else if (node is JsonValue val)
        {
            HorizontalStackLayout fieldStack = new HorizontalStackLayout();
            
            object rawValue = val.GetValue<object>();
            fieldStack.Children.Add(new Label { Text = prefix + ": ", FontAttributes = FontAttributes.Bold });
    
            if (rawValue is bool boolVal)
            {
                fieldStack.Children.Add(new Label() { Text = boolVal.ToString() });
            }
            else
            {
                fieldStack.Children.Add(new Label() { Text = rawValue?.ToString() ?? "" });
            }
            
            GoogleFileStack.Children.Add(fieldStack);
        }
    }

    private async void OnPickDataJsonClicked(object? sender, EventArgs e)
    {
        try
        {   
            var result = await PickAndShow(PickOptions.Default);

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            using (var reader = new StreamReader(stream))
            {
                string json = await reader.ReadToEndAsync();
            
                JsonNode node = JsonNode.Parse(json);

                if (node is JsonObject obj)
                {
                    foreach (var prop in obj)
                    {
                        if (prop.Value is JsonArray array)
                        {
                            continue;
                        }

                        var value = prop.Value.ToString();
                        var key = prop.Key;
                        if (prop.Key == "RELESYS_DOMAIN_PREFIX")
                        {
                            _viewModel.BaseUri.Value = $"https://{value}.relesysapp.net";
                            _viewModel.CurrentAppUri.Value = $"https://{value}.relesysapp.net";
                        }
                        else if (key == "RELESYS_APP_NAME")
                        {
                            _viewModel.ApplicationName.Value = value;
                        }
                        else if (key == "RELESYS_SPLASH_BACKGROUND")
                        {
                            _viewModel.SplashBackgroundColor.Value = "#" + value;
                        }
                        else if (key == "RELESYS_STATUS_BAR_CONTENT_STYLE_COLOR")
                        {
                            _viewModel.SplashContentColor.Value = "#" + value;
                        }
                        else if (key == "RELESYS_STATUS_BAR_CONTENT_STYLE")
                        {
                            IsSplasHBackgroudColorLight.IsChecked = value != "dark";
                        }
                        else if (key == "RELESYS_BUNDLE_IDENTIFIER")
                        {
                            _viewModel.KeystoreAlias.Value = value;
                            _viewModel.SettingsPackageNameAndroid.Value = value;
                        }
                        else if (key == "RELESYS_BUNDLE_IDENTIFIER_IOS")
                        {
                            _viewModel.SettingsPackageNameiOS.Value = value;
                        }
                        else if (key == "RELESYS_MULTIBRANDED")
                        {
                            IsMultibrandedApp.IsChecked = value == "true";
                        }
                        else if (key == "RELESYS_CONTENT_PAGE")
                        {
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                _viewModel.BaseUri.Value = $"https://{value}.relesysapp.net";
                                _viewModel.CurrentAppUri.Value = $"https://{value}.relesysapp.net";
                            }
                        }
                        else if (key == "RELESYS_CONTENT_PAGE_IOS")
                        {
                            iOSMulti = value;
                        }
                        else if (key == "RELESYS_PUSH_PROVIDER_REFERENCE_DROID")
                        {
                            _viewModel.PushProviderSRefAndroid.Value = value;
                        }
                        else if (key == "RELESYS_PUSH_PROVIDER_REFERENCE_IOS")
                        {
                            _viewModel.PushProviderSRefIOS.Value = value;
                        }
                    }
                }
                
            } 
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}