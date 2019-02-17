using System;
using TubeSniper.Presentation.Interfaces;

namespace TubeSniper.Presentation.Presenters
{
    public class MainPresenter : IMainPresenter
    {
        private readonly IMainView _view;
        private SideMenuPresenter _sideMenuPresnter;

        public MainPresenter(IMainView view)
        {
            _view = view;
            WireEvents();
            _sideMenuPresnter = new SideMenuPresenter(_view.SideMenuView);
        }

        private void WireEvents()
        {
            Console.WriteLine("Events Wired");
            _view.SideMenuView.HomeClicked += SideMenuView_OnHomeClicked;
            _view.SideMenuView.UsersClicked += SideMenuView_OnUsersClicked;
            _view.SideMenuView.SuccessClicked += SideMenuView_OnSuccessClicked;
            _view.SideMenuView.LicenseClicked += SideMenuView_OnLicenseClicked;
            _view.TestClicked += View_TestClicked;
        }

        private void SideMenuView_OnLicenseClicked(object sender, EventArgs eventArgs)
        {
            _view.ContentView.SwitchToLicense();
        }

        private void SideMenuView_OnSuccessClicked(object sender, EventArgs eventArgs)
        {
            _view.ContentView.SwitchToSuccess();
        }

        private void SideMenuView_OnUsersClicked(object sender, EventArgs eventArgs)
        {
            _view.ContentView.SwitchToAccounts();
        }

        private void SideMenuView_OnHomeClicked(object sender, EventArgs eventArgs)
        {
            _view.ContentView.SwitchToDashBoard();
        }

        private void View_TestClicked(object sender, EventArgs e)
        {
            Console.WriteLine("HELLO");
            var campaign = new Campaign();
        }

        public void Show()
        {
            _view.Show();
        }
    }

    internal class Campaign
    {
        
    }
}