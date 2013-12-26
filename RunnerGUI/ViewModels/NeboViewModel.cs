using System;
using System.Collections.ObjectModel;
using System.Threading;
using Caliburn.Micro;
using System.Linq;

namespace RunnerGUI.ViewModels
{
	public class UIAction : PropertyChangedBase
	{
		private bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if (_isActive != value)
				{
					_isActive = value;
					NotifyOfPropertyChange(() => IsActive);
				}
			}
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					NotifyOfPropertyChange(() => Name);
				}
			}
		}

		public Func<bool> ActionBody;
	}
	public class NeboBootstrapper : Bootstrapper<NeboViewModel> { }
	public class NeboViewModel : PropertyChangedBase
	{
		public NeboViewModel()
		{
			Actions = new BindableCollection<UIAction>(NeboClicker.GetActions());
		}
		private string _name = "Bla";
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					NotifyOfPropertyChange(() => Name);
				}
			}
		}

		public void StartActions()
		{
			ThreadPool.QueueUserWorkItem(o =>
			{
				while (true)
				{
					using (NeboClicker clicker = new NeboClicker())
					{
						clicker.LogIn();
						foreach (var action in Actions.Where(action => action.IsActive))
						{
							action.ActionBody();
						}
					}
						Thread.Sleep(new TimeSpan(0, 2, 0));
				}
			});
		}

		private BindableCollection<UIAction> _actions = new BindableCollection<UIAction>();
		public BindableCollection<UIAction> Actions
		{
			get { return _actions; }
			set
			{
				if (_actions != value)
				{
					_actions = value;
					NotifyOfPropertyChange(() => Actions);
				}
			}
		}
	}
}