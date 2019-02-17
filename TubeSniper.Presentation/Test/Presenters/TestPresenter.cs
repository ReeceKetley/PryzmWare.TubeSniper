using System;
using System.Windows.Forms;
using TubeSniper.Presentation.Test.Views;

namespace TubeSniper.Presentation.Test.Presenters
{
    public class TestPresenter
    {
        private readonly ITestView _view;

        public TestPresenter(ITestView view)
        {
            _view = view;
            _view.StartClicked += ViewOnStartClicked;
        }

        private void ViewOnStartClicked(object sender, EventArgs eventArgs)
        {
            _view.ShowError();
        }

        public void Show()
        {
            _view.Show();
            Application.Run();
        }
    }
}
