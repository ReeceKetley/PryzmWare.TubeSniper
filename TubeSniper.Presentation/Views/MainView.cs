using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TubeSniper.Presentation.Interfaces;
using TubeSniper.Presentation.Presenters;
using TubeSniper.Presentation.Views.Nested;

namespace TubeSniper.Presentation.Views
{
    public partial class MainView : Form, IMainView
    {

        public MainView()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        public ISideMenuView SideMenuView => sideMenuView;
        public IContentView ContentView => contentView;
        public event EventHandler TestClicked;

        private void MainView_Load(object sender, EventArgs e)
        {

        }

        private void contentView_Load(object sender, EventArgs e)
        {

        }

        private void tblContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sideMenuView_Load(object sender, EventArgs e)
        {

        }

        private void pnlLine_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainFooterView1_Load(object sender, EventArgs e)
        {

        }
    }
}
