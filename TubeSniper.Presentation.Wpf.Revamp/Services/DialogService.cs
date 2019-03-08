using System;
using Microsoft.Win32;

namespace TubeSniper.Presentation.Wpf.Services
{
    public class DialogService : IDialogService
    {
        public void ShowYesNo(string message, string caption)
        {
        }

        public string OpenFile(string[] filters)
        {
            var dialog = new OpenFileDialog();
            //dialog.Filter = "Text (Tab delimited)|*.txt|All Files (*.*)|*.*";
            dialog.Filter = string.Join("|", filters);
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Multiselect = false;
            var result = dialog.ShowDialog();
            if (result == null || result != true)
            {
                return null;
            }

            return dialog.FileName;
        }

        public string SaveFile(string[] filters)
        {
            var dialog = new SaveFileDialog();
            //dialog.Filter = "Text (Tab delimited)|*.txt|All Files (*.*)|*.*";
            dialog.Filter = string.Join("|", filters);
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var result = dialog.ShowDialog();
            if (result == null || result != true)
            {
                return null;
            }

            return dialog.FileName;
        }

        public string SaveDelimitedFile()
        {
            return SaveFile(new[] { "Text (Tab delimited)|*.txt", "CSV (Comma delimited)|*.csv", "All Files (*.*)|*.*" });
        }

        public string OpenDelimitedFile()
        {
            return OpenFile(new[] { "Text Files (*.txt;*.csv;*.tsv)|*.txt;*.csv;*.tsv", "All Files (*.*)|*.*" });
        }
    }
}
