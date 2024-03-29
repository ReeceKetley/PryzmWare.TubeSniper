﻿using System;
using System.Windows.Input;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.ViewModels.MainWindow
{
	public class MenuItemViewModel : BindableBase
	{
		private object _icon;
		private string _text;
		private bool _isEnabled = true;
		private RelayCommand _command;
		private Uri _navigationDestination;

		public object Icon
		{
			get { return this._icon; }
			set { this.SetProperty(ref this._icon, value); }
		}

		public string Text
		{
			get { return this._text; }
			set { this.SetProperty(ref this._text, value); }
		}

		public bool IsEnabled
		{
			get { return this._isEnabled; }
			set { this.SetProperty(ref this._isEnabled, value); }
		}

		public ICommand Command
		{
			get { return this._command; }
			set { this.SetProperty(ref this._command, (RelayCommand)value); }
		}

		public Uri NavigationDestination
		{
			get { return this._navigationDestination; }
			set { this.SetProperty(ref this._navigationDestination, value); }
		}

		public bool IsNavigation => this._navigationDestination != null;
	}
}