using System.Windows.Forms;
using TubeSniper.Presentation.Interfaces;

namespace TubeSniper.Presentation.Views.Nested
{
    public partial class MainContentView : UserControl, IContentView
    {
        public MainContentView()
        {
            InitializeComponent();
        }

        public void SwitchToDashBoard()
        {
            spPages.SelectedTab = tpDashboard;
        }

        public void SwitchToAccounts()
        {
            spPages.SelectedTab = tpUsers;
        }

        public void SwitchToSuccess()
        {
            spPages.SelectedTab = tpComments;
        }

        public void SwitchToLicense()
        {
            spPages.SelectedTab = tpLicence;
        }

        private void dashboardPage1_Load(object sender, System.EventArgs e)
        {

        }
    }
}
