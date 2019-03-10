using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TubeSniper.Domain.Accounts;
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
		private readonly IAccountEntryRepository _accountEntryRepository;
		private readonly ICampaignService _campaignService;
		private readonly ISearchService _searchService;
		private readonly ICaptchaService _captchaService;
		private readonly IProxyTestService _proxyTestService;
		private readonly IProxyEntryRepository _proxyEntryRepository;
		private readonly CampaignEditorViewModelValidator _validator;
		private Guid _campaignId;
		private ICommand _submitCommand;
		private ICommand _selectAllAccountsCommand;
		private ICommand _selectAllProxiesCommand;
		private Campaign _campaign;

		public CampaignEditorViewModel(IAccountEntryRepository accountEntryRepository, IProxyEntryRepository proxyEntryRepository, ICampaignService campaignService, ISearchService searchService, ICaptchaService captchaService, IProxyTestService proxyTestService)
		{
			//SelectedProxies.CollectionChanged += SelectedProxies_CollectionChanged;
			_accountEntryRepository = accountEntryRepository;
			_proxyEntryRepository = proxyEntryRepository;
			_campaignService = campaignService;
			_searchService = searchService;
			_captchaService = captchaService;
			_proxyTestService = proxyTestService;

			_validator = new CampaignEditorViewModelValidator();

			foreach (var account in accountEntryRepository.GetAll())
			{
				Accounts.Add(account.Id, account.Credentials.Username.Value);
			}

			foreach (var proxyEntry in _proxyEntryRepository.GetAll())
			{
				Proxies.Add(proxyEntry.Id, proxyEntry.Proxy.Address.ToString());
			}
		}

		public ICommand SubmitCommand => _submitCommand ?? (_submitCommand = new RelayCommand(Submit, CanSubmit));

		public ICommand SelectAllProxiesCommand => _selectAllProxiesCommand ?? (_selectAllProxiesCommand = new RelayCommand(SelectAllProxies));

		public ICommand SelectAllAccountsCommand => _selectAllAccountsCommand ?? (_selectAllAccountsCommand = new RelayCommand(SelectAllAccounts));

		public Action CloseAction { get; set; }

		public Dictionary<Guid, string> Accounts { get; } = new Dictionary<Guid, string>();

		public Dictionary<Guid, string> Proxies { get; } = new Dictionary<Guid, string>();

		public string Title { get; set; }

		public string SearchKeyword { get; set; }

		public string Comment { get; set; }

		public int WorkersCount { get; set; }

		public int MaxResults { get; set; }

		public bool PostAsReply { get; set; }

		public CommentMethod CommentMethod { get; set; }

		public ObservableCollection<KeyValuePair<Guid, string>> SelectedAccounts { get; set; } = new ObservableCollection<KeyValuePair<Guid, string>>();

		public ObservableCollection<KeyValuePair<Guid, string>> SelectedProxies { get; set; } = new ObservableCollection<KeyValuePair<Guid, string>>();

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
			CommentMethod = PostAsReply == false ? CommentMethod.Comment : CommentMethod.Reply;
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
			var accounts = new List<AccountEntry>();
			foreach (var pair in SelectedAccounts)
			{
				accounts.Add(_accountEntryRepository.GetById(pair.Key));
			}

			var proxies = new List<ProxyEntry>();
			foreach (var pair in SelectedProxies)
			{
				proxies.Add(_proxyEntryRepository.GetById(pair.Key));
			}


			var campaign = new Campaign();
			campaign.Id = _campaignId;
			campaign.Title = new CampaignTitle(Title);
			campaign.Keyword = new Keyword(SearchKeyword);
			campaign.Accounts = accounts;
			campaign.Proxies = proxies;
			campaign.Comment = new Comment(Comment);
			campaign.CommentMethod = CommentMethod;
			campaign.MaxComments = MaxResults;
			campaign.NumberOfWorkers = WorkersCount;
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
			_campaign = campaign;
			_campaignId = campaign.Id;
			Title = campaign.Title.Value;
			SearchKeyword = campaign.Keyword.Value;
			Comment = campaign.Comment.Value;
			WorkersCount = campaign.NumberOfWorkers;
			MaxResults = campaign.MaxComments;
			CommentMethod = campaign.CommentMethod;
			SetLists(campaign);

			PostAsReply = CommentMethod != CommentMethod.Comment;
		}

		private async void SetLists(Campaign campaign)
		{
			await Task.Delay(200);
			foreach (var account in campaign.Accounts)
			{
				foreach (var pair in Accounts)
				{
					if (account.Id == pair.Key)
					{
						SelectedAccounts.Add(pair);
					}
				}
			}

			foreach (var proxy in campaign.Proxies)
			{
				foreach (var pair in Proxies)
				{
					if (proxy.Id == pair.Key)
					{
						SelectedProxies.Add(pair);
					}
				}
			}
		}
	}
}