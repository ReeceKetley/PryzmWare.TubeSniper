using System;
using TubeSniper.Core.Interfaces;
using TubeSniper.Presentation.Presenters;
using TubeSniper.Presentation.Views;

namespace TubeSniper.Presentation.Openers
{
    public class MainViewOpener : IMainViewOpener
    {
        public MainViewOpener()
        {
        }

        public void Open()
        {
            var view = new MainView();
            var presenter = new MainPresenter(view);
            Console.WriteLine("LOLOLOL");
            presenter.Show();
        }
    }
}
