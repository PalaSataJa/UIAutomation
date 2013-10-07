using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;

namespace RunnerGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}


		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			new TaskFactory().StartNew(() =>
			{
				using (var clicker = new NeboClicker())
				{
					clicker.LogIn();
					clicker.ActionLoop();
				}
			});
		}

		private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
		{

		}
	}
}
