using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UIAutomationCases
{
	[TestClass]
	public class NeboTests
	{
		[TestMethod]
		public void TestNebo()
		{
			using (var clicker = new NeboClicker())
			{
				clicker.LogIn();
				clicker.ActionLoop();
			}

		}

		[TestMethod]
		public void Bla()
		{
			Console.WriteLine(new Guid(MD5.Create().ComputeHash(Encoding.Default.GetBytes("Bla-bla-bla I'm tired"))).ToString());
			Console.WriteLine(new Guid(MD5.Create().ComputeHash(Encoding.Default.GetBytes("Bla-bla-bla I want to go home and drink a beer"))).ToString());
		}

	}
}
