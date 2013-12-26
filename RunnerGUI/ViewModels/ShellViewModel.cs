using BusinessLogic;
using System.ComponentModel;
using System.Windows;

namespace RunnerGUI
{
	public class ShellViewModel : IShell
	{
		public ShellViewModel()
		{
			worker.DoWork += worker_DoWork;
			worker.RunWorkerCompleted += worker_RunWorkerCompleted;
		}

		private BackgroundWorker worker = new BackgroundWorker();
		private NeboClicker clicker;

		public void GoNebo()
		{
			worker.RunWorkerAsync();
		}
		public bool CanGoNebo()
		{
			return true;
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			clicker = new NeboClicker();

			clicker.LogIn();
			clicker.ActionLoop();
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			clicker.Dispose();
		}
	}
}