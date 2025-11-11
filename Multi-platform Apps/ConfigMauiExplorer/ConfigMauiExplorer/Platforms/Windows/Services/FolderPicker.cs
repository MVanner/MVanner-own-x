using ConfigMauiExplorer.Interfaces;

namespace ConfigMauiExplorer.Services;

public class FolderPicker : IFolderPicker
{
   public async Task<string> PickFolderAsync()
   {
      var picker = new FolderPicker();
      picker.FileTypeFilter.Add("*");
      var hwnd = ((MauiWinUIWindow)App.Current.Windows[0].Handler.PlatformView).WindowHandle;
      WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
      StorageFolder folder = await picker.PickSingleFolderAsync();
      return folder?.Path;
   }
}