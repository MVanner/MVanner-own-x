using ConfigMauiExplorer.Interfaces;
using AppKit;
using Foundation;
using UIKit;
using UniformTypeIdentifiers;

namespace ConfigMauiExplorer.Services;

public class FolderPicker : IFolderPicker
{
    public Task<string> PickFolderAsync()
    {
        var tcs = new TaskCompletionSource<string>();
        var picker = new UIDocumentPickerViewController([UTTypes.Folder, UTTypes.Image])
        {
            AllowsMultipleSelection = false
        };

        picker.DidPickDocument += (s, e) =>
        {
            var url = e.Url;
            tcs.SetResult(url?.Path);
        };
        
        picker.WasCancelled += (s, e) =>
        {
            tcs.SetResult(null);
        };
        
        MainThread.BeginInvokeOnMainThread( () =>
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            window.RootViewController.PresentViewController(picker, true, null);
        });
        
        return tcs.Task;
    }
}