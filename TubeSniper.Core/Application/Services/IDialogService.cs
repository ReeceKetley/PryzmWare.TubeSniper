namespace TubeSniper.Presentation.Wpf.Services
{
    public interface IDialogService
    {
        string OpenFile(string[] filters);
        string SaveFile(string[] filters);

        string SaveDelimitedFile();
        string OpenDelimitedFile();
    }
}
