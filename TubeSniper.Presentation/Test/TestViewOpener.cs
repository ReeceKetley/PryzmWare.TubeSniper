using TubeSniper.Core.Interfaces;
using TubeSniper.Presentation.Test.Presenters;
using TubeSniper.Presentation.Test.Views;

namespace TubeSniper.Presentation.Test
{
    public class TestViewOpener : ITestViewOpener
    {
        public void Open()
        {
            var view = new TestView();
            var presenter = new TestPresenter(view);
            presenter.Show();
        }
    }
}
