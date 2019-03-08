namespace TubeSniper.Presentation.Wpf.Services
{
    public interface IMessageBoxService
    {
        void OkayInformation(string message, string caption);
        void OkayInformation(string message);
        bool YesNoQuestion(string message, string caption);
        bool YesNoQuestion(string message);
        void OkayError(string message, string caption);
        void OkayError(string message);
    }
}
