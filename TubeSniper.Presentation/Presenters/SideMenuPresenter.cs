using System;
using TubeSniper.Presentation.Interfaces;

namespace TubeSniper.Presentation.Presenters
{
    public class SideMenuPresenter
    {
        private readonly ISideMenuView _view;

        public SideMenuPresenter(ISideMenuView view)
        {
            _view = view;
            InstallEvents();
        }

        private void InstallEvents()
        {
            _view.HomeClicked += View_OnHomeClicked;
            _view.UsersClicked += View_OnUsersClicked;
            _view.SuccessClicked += View_OnSuccessClicked;
            _view.LicenseClicked += View_OnLicenseClicked;
        }

        private void View_OnHomeClicked(object sender, EventArgs eventArgs)
        {

        }

        private void ViewOnHomeClicked(object sender, EventArgs eventArgs)
        {
            //throw new NotImplementedException();
        }

        private void View_OnUsersClicked(object sender, EventArgs eventArgs)
        {
            //throw new NotImplementedException();
        }

        private void View_OnSuccessClicked(object sender, EventArgs eventArgs)
        {
            //throw new NotImplementedException();
        }

        private void View_OnLicenseClicked(object sender, EventArgs eventArgs)
        {
            //throw new NotImplementedException();
        }
    }
}