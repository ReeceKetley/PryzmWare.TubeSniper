using System;
using System.Windows.Forms;

namespace TubeSniper.Presentation.Test.Views
{
    public partial class TestView : Form, ITestView
    {
        public event EventHandler StartClicked;
        public event EventHandler StopClicked;
        public TestView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnStartClicked();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnStopClicked();
        }

        protected virtual void OnStartClicked()
        {
            StartClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStopClicked()
        {
            StopClicked?.Invoke(this, EventArgs.Empty);
        }

        public void ShowError()
        {
            button1.Text = "started";
        }
    }

    public interface ITestView
    {
        void ShowError();
        event EventHandler StartClicked;
        event EventHandler StopClicked;
        void Show();
    }
}
