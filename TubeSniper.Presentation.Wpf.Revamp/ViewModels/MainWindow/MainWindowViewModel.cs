namespace TubeSniper.Presentation.Wpf.ViewModels.MainWindow
{
	public class MainWindowViewModel : ViewModelBase
	{
		public int SelectedTopItem { get; set; }
		public int SelectedBottomItem { get; set; }
		public int SelectedPageIndex { get; set; }

		public MainWindowViewModel()
		{
			SelectedTopItem = -1;
			SelectedBottomItem = -1;
			SelectedTopItem = 0;

		}

		private void OnSelectedTopItemChanged()
		{
			if (SelectedTopItem == -1)
			{
				return;
			}

			SelectedBottomItem = -1;

			if (SelectedTopItem == 0)
			{
				SelectedPageIndex = 0;
			}
			else if (SelectedTopItem == 1)
			{
				SelectedPageIndex = 1;
			}
			else if (SelectedTopItem == 2)
			{
				SelectedPageIndex = 2;
			}
			else if (SelectedTopItem == 3)
			{
				SelectedPageIndex = 3;
			}
		}

		private void OnSelectedBottomItemChanged()
		{
			if (SelectedBottomItem == -1)
			{
				return;
			}

			SelectedTopItem = -1;

			if (SelectedBottomItem == 0)
			{
				SelectedPageIndex = 4;
			}
			else if (SelectedBottomItem == 1)
			{
				SelectedPageIndex = 5;
			}
		}

	}
}