using System;
using System.ComponentModel;
using TubeSniper.Domain;
using TubeSniper.Domain.Auth;

namespace TubeSniper.Presentation.Wpf.ViewModels.Auth
{
	public class AuthLoadingViewModel : ViewModelBase
	{
		private readonly IAuthService _authService;
		private BackgroundWorker _worker = new BackgroundWorker();


		public AuthLoadingViewModel(IAuthService authService)
		{
			_authService = authService;
		}

		public CheckNewActivationCode CheckNewActivationResult { get; set; }

		public MyIsGenuineResult CheckActivationResult { get; set; }

		public event EventHandler CheckNewActivationCompleted;

		public void CheckNewActivation()
		{
			_worker.DoWork += Worker_CheckNewDoWork;
			_worker.RunWorkerCompleted += Worker_CheckNewCompleted;
			_worker.RunWorkerAsync();
		}

		private void Worker_CheckNewCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//CheckActivationResult = (MyIsGenuineResult) e.Result;
			Console.WriteLine(CheckActivationResult.ToString());
			OnFinished();
		}

		private void Worker_CheckNewDoWork(object sender, DoWorkEventArgs e)
		{
			CheckNewActivationResult = _authService.CheckNewActivation();
		}

		public void CheckActivation()
		{
			_worker.DoWork += Worker_CheckDoWork;
			_worker.RunWorkerCompleted += Worker_CheckCompleted;
			_worker.RunWorkerAsync();
		}

		private void Worker_CheckCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Console.WriteLine(CheckNewActivationResult.ToString());
			OnFinished();
		}

		private void Worker_CheckDoWork(object sender, DoWorkEventArgs e)
		{
			CheckActivationResult = _authService.CheckActivation();
		}

		protected virtual void OnFinished()
		{
			Globals.SelectorPayload = _authService.GetSelectorPayload(_authService.GetStoredKey().Value);

			CheckNewActivationCompleted?.Invoke(this, EventArgs.Empty);
		}
	}
}