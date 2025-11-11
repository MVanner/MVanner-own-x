namespace ConfigMauiExplorer.Models;

public class AppConfiguration
{
    public string ApiUri { get; set; }
    public string NativeAppSettingsToken { get; set; }
    public string BaseUri { get; set; }
    public string CurrentAppUri { get; set; }
    public string LoginUri { get; set; }
    public bool IsNonStoreApp { get; set; }
    public bool IsMultibrandedApp { get; set; }
    public PushSettings PushSettings { get; set; }
    public List<string> AppHostNames { get; set; }
}