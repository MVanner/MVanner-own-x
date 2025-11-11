namespace ConfigMauiExplorer.Interfaces;

public interface IFolderPicker
{
    Task<string> PickFolderAsync();
}