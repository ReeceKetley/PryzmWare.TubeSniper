using System;
using System.Drawing;
using System.Windows.Forms;
using TubeSniper.Presentation.Interfaces;

namespace TubeSniper.Presentation.Views.Nested
{
    public partial class SideMenuView : UserControl, ISideMenuView
    {
        private Button _activeButton;

        public SideMenuView()
        {
            InitializeComponent();
        }

        private void tblContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        public event EventHandler HomeClicked;
        public event EventHandler UsersClicked;
        public event EventHandler SuccessClicked;
        public event EventHandler LicenseClicked;

        protected virtual void OnHomeClicked()
        {
            HomeClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUsersClicked()
        {
            UsersClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSuccessClicked()
        {
            SuccessClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnLicenseClicked()
        {
            LicenseClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnCampaigns_Click(object sender, EventArgs e)
        {
            if (ToggleButton((Button)sender))
            {
                OnHomeClicked();
            }
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            if (ToggleButton((Button)sender))
            {
                OnUsersClicked();
            }
        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            if (ToggleButton((Button)sender))
            {
                OnSuccessClicked();
            }
        }

        private void btnLicecne_Click(object sender, EventArgs e)
        {
            if (ToggleButton((Button)sender))
            {
                OnLicenseClicked();
            }
        }

        private bool ToggleButton(Button button)
        {
            if (button == _activeButton)
            {
                return false;
            }

            if (_activeButton != null)
            {
                SetButtonState(_activeButton, false);
            }

            _activeButton = button;
            SetButtonState(_activeButton, true);
            return true;
        }

        private void SetButtonState(Button button, bool toggled)
        {
            if (toggled)
            {
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 82, 83);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 82, 83);
                button.BackColor = Color.FromArgb(164, 73, 71);
            }
            else
            {
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 82, 83);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(185, 82, 83);
                button.BackColor = Color.IndianRed;
            }
        }
    }
}