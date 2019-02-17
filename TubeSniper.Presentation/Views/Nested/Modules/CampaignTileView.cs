using System;
using System.Windows.Forms;

namespace TubeSniper.Presentation.Views.Nested.Modules
{
    public partial class CampaignTileView : UserControl, ICampaginTileView
    {
        public CampaignTileView()
        {
            InitializeComponent();
        }

        private void CampaignOverviewModule_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            OnStartClicked();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            OnDetailsClicked();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditClicked();
        }

        public event EventHandler StartClicked;
        public event EventHandler DetailsClicked;
        public event EventHandler EditClicked;

        protected virtual void OnStartClicked()
        {
            StartClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDetailsClicked()
        {
            DetailsClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEditClicked()
        {
            EditClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class CampaignTilePresenter
    {
        private readonly ICampaginTileView _view;

        public CampaignTilePresenter(ICampaginTileView view)
        {
            _view = view;

        }

        public void InstallEvents()
        {
            _view.StartClicked += View_OnStartClicked;
            _view.DetailsClicked += View_OnDetailsClicked;
            _view.EditClicked += View_OnEditClicked;
        }

        private void View_OnStartClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void View_OnEditClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void View_OnDetailsClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICampaginTileView
    {
        event EventHandler StartClicked;
        event EventHandler DetailsClicked;
        event EventHandler EditClicked;
    }
}
