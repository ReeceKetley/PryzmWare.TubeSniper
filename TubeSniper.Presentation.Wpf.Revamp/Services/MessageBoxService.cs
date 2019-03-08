using System.Windows.Forms;
using TubeSniper.Core;

namespace TubeSniper.Presentation.Wpf.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void OkayInformation(string message, string caption)
        {
            System.Windows.Forms.MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OkayInformation(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, Config.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool YesNoQuestion(string message, string caption)
        {
            return System.Windows.Forms.MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public bool YesNoQuestion(string message)
        {
            return System.Windows.Forms.MessageBox.Show(message, Config.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void OkayError(string message, string caption)
        {
            System.Windows.Forms.MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OkayError(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, Config.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
