using Android.App;
using Android.Content.PM;

namespace AppNameChange;

[Activity(Theme = "@style/Maui.SplashTheme", Name = "com.companyname.appnamechange.ExtraActivity", Label = "{AppInfo.Name} Extra Activity",
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class ExtraActivity : MainActivity
{
    
}