using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Validators.CampaignEditor;

namespace TubeSniper.Presentation.Wpf.ViewModels.CampaignEditor
{
	public class CampaignEditorViewModel : ViewModelBase, IDataErrorInfo
	{
		private readonly IAccountsRepository _accountsRepository;
		private readonly ICampaignService _campaignService;
		private readonly ISearchService _searchService;
		private readonly ICaptchaService _captchaService;
		private readonly IProxyTestService _proxyTestService;
		private readonly IProxyRepository _proxyRepository;
		private readonly CampaignEditorViewModelValidator _validator;
		private Guid _campaignId;
		private ICommand _submitCommand;
		private ICommand _selectAllAccountsCommand;
		private ICommand _selectAllProxiesCommand;

		public CampaignEditorViewModel(IAccountsRepository accountsRepository, IProxyRepository proxyRepository, ICampaignService campaignService, ISearchService searchService, ICaptchaService captchaService, IProxyTestService proxyTestService)
		{
			//SelectedProxies.CollectionChanged += SelectedProxies_CollectionChanged;
			_accountsRepository = accountsRepository;
			_proxyRepository = proxyRepository;
			_campaignService = campaignService;
			_searchService = searchService;
			_captchaService = captchaService;
			_proxyTestService = proxyTestService;

			_validator = new CampaignEditorViewModelValidator();

			foreach (var account in accountsRepository.GetAll().ToList())
			{
				if (Accounts.Contains(account.Credentials.Email))
				{
					continue;
				}

				Accounts.Add(account.Credentials.Email);
			}

			foreach (var proxyEntry in _proxyRepository.GetAll().ToList())
			{
				if (Proxies.Contains(proxyEntry.Proxy.Address.Host + ":" + proxyEntry.Proxy.Address.Port))
				{
					continue;
				}

				Proxies.Add(proxyEntry.Proxy.Address.Host + ":" + proxyEntry.Proxy.Address.Port);
			}
		}

		public ICommand SubmitCommand => _submitCommand ?? (_submitCommand = new RelayCommand(Submit, CanSubmit));

		public ICommand SelectAllProxiesCommand => _selectAllProxiesCommand ?? (_selectAllProxiesCommand = new RelayCommand(SelectAllProxies));

		public ICommand SelectAllAccountsCommand => _selectAllAccountsCommand ?? (_selectAllAccountsCommand = new RelayCommand(SelectAllAccounts));

		public Action CloseAction { get; set; }

		public ObservableCollection<string> Accounts { get; } = new ObservableCollection<string>();

		public ObservableCollection<string> Proxies { get; } = new ObservableCollection<string>();

		public string Title { get; set; }

		public string SearchKeyword { get; set; }

		public string Comment { get; set; }

		public int WorkersCount { get; set; }

		public int MaxResults { get; set; }

		public bool PostAsReply { get; set; }

		public ObservableCollection<string> SelectedAccounts { get; set; } = new ObservableCollection<string>();

		public ObservableCollection<string> SelectedProxies { get; set; } = new ObservableCollection<string>();

		public ObservableCollection<string> SelectedProxyItem { get; set; }

		public ObservableCollection<string> SelectedAccountItem { get; set; }

		public string this[string columnName]
		{
			get
			{
				var firstOrDefault = _validator.Validate(this).Errors.FirstOrDefault(x => x.PropertyName == columnName);
				if (firstOrDefault != null)
				{
					return _validator != null ? firstOrDefault.ErrorMessage : "";
				}

				return "";
			}
		}

		public string Error
		{
			get
			{
				var results = _validator?.Validate(this);
				if (results != null && results.Errors.Any())
				{
					var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
					return errors;
				}

				return string.Empty;
			}
		}

		private void Submit(object obj)
		{
			var model = GetCampaign();
			if (_campaignId == Guid.Empty)
			{
				_campaignService.Add(model);
			}
			else
			{
				_campaignService.Update(model);
			}

			CloseAction?.Invoke();
		}

		private bool CanSubmit(object obj)
		{
			return _validator.Validate(this).IsValid;
		}

		private void SelectAllProxies(object obj)
		{
			if (SelectedProxies.Count > 0)
			{
				SelectedProxies.Clear();
			}
			else
			{
				foreach (var proxy in Proxies)
				{
					SelectedProxies.Add(proxy);
				}
			}
		}

		private void SelectAllAccounts(object obj)
		{
			if (SelectedAccounts.Count > 0)
			{
				SelectedAccounts.Clear();
			}
			else
			{
				foreach (var account in Accounts)
				{
					SelectedAccounts.Add(account);
				}
			}
		}

		private Campaign GetCampaign()
		{
			var campaignMeta = new CampaignMeta();
			campaignMeta.Title = Title;
			campaignMeta.SearchTerm = SearchKeyword;
			var accounts = new List<YoutubeAccount>();
			foreach (var account in SelectedAccounts)
			{
				accounts.AddRange(_accountsRepository.GetAll().Where(youtubeAccount => youtubeAccount.Credentials.Email == account));
			}

			var proxies = new List<ProxyEntry>();
			foreach (var proxy in SelectedProxies)
			{
				proxies.AddRange(_proxyRepository.GetAll().Where(proxyEntry => proxyEntry.Proxy.Address.Host + ":" + proxyEntry.Proxy.Address.Port == proxy));
			}

			var proxyRegister = new ProxyCollection(proxies);
			campaignMeta.Accounts = accounts;
			var campaign = new Campaign(proxyRegister, campaignMeta, new StandardAccountRegister(accounts), SearchKeyword, new CommentGenerator(new CommentTemplate(Comment)), PostAsReply, _searchService, _captchaService, _proxyTestService);
			campaign.Id = _campaignId; // Not sure if this the error TODO: Check this.
			return campaign;
		}

		private void SelectedProxies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			foreach (var selectedProxy in SelectedProxies)
			{
				//Console.WriteLine(selectedProxy);
			}
		}

		public void SetCampaign(Campaign campaign)
		{
			Console.WriteLine(campaign.Id);
			_campaignId = campaign.Id;
			Title = campaign.Meta.Title;
			SearchKeyword = campaign.Meta.SearchTerm;
			Comment = campaign.Comment;
			WorkersCount = campaign.Workers;
			MaxResults = campaign.MaxResults;
			PostAsReply = campaign.AsReply;
			foreach (var account in Accounts)
			{
				SelectedAccounts.Add(account);
			}

			foreach (var proxy in Proxies)
			{
				SelectedProxies.Add(proxy);
			}
		}
	}
}