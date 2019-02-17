using System;

namespace TubeSniper.Presentation.Interfaces
{
    public interface ISideMenuView
    {
        event EventHandler HomeClicked;
        event EventHandler UsersClicked;
        event EventHandler SuccessClicked;
        event EventHandler LicenseClicked;
    }
}
