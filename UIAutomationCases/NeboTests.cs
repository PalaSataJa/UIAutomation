using System;
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

	}
}
