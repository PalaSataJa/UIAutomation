using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using RunnerGUI.ViewModels;

namespace RunnerGUI
{

	public class NeboClicker : IDisposable
	{
		public const string BaseItemCompleted = "[href*='floors/0/']";
		public const string BuyGoods = "[href*='floors/0/2']";
		public const string BuyGoodsAction = "Закупить товар";
		public const string BuyOneGoods = "[href*='floorPanel:product']";
		public const string CacheIn = "[href*='floors/0/5']";
		public const string CacheInAction = "Собрать выручку!";
		public const string Lift = "[href='lift']";
		public const string LiftAction = "Поднять лифт";
		public const string LiftTipsAction = "[href*='tipsBlock:tipsLink']";
		public const string Password = "fortune7";
		public const string Postavka = "[href*='floors/0/3']";
		public const string PostavkaAction = "Выложить товар";
		public const string Refresh = "[href*='toolbarPanel:refresh']";
		public const string UserName = "palasataja";
		public static IWebDriver WebDriver;


		public NeboClicker()
		{

			WebDriver = new FirefoxDriver(new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"), new FirefoxProfile());
		}

		public static List<UIAction> GetActions()
		{
			var result = new List<UIAction>
				{
					new UIAction {IsActive = true, Name = "CacheIn", ActionBody = () => TryDoAllActions(CacheIn, CacheInAction)},
					new UIAction {IsActive = true, Name = "Postavka", ActionBody = () => TryDoAllActions(Postavka, PostavkaAction)},
					new UIAction {IsActive = true, Name = "BuyGoods", ActionBody = () => TryDoAllActions(BuyGoods, BuyGoodsAction, BuyGoodsComplexAction)},
					new UIAction {IsActive = true, Name = "Lift", ActionBody = () => TryDoAllActions(Lift, LiftAction, LiftComplexAction)}
				};
			return result;
		}

		public void ActionLoop()
		{
			while (true)
			{
				if (!TryDoAllActions(CacheIn, CacheInAction))
					;
				if (!TryDoAllActions(Postavka, PostavkaAction))
					;
				if (!TryDoAllActions(BuyGoods, BuyGoodsAction, BuyGoodsComplexAction))
					;
				//проверить если иконка не серая
				//if (!TryDoAllActions(Lift, LiftAction, LiftComplexAction))
				//;

				Thread.Sleep(new TimeSpan(0, 2, 0));
				WebDriver.Navigate().GoToUrl("http://nebo.mobi/home");
			}
		}

		public void Dispose()
		{
			WebDriver.Quit();
		}

		public void LogIn()
		{
			WebDriver.Navigate().GoToUrl("http://nebo.mobi");
			WebDriver.FindElement(By.CssSelector("[href*='login']")).Click();

			WebDriver.FindElement(By.Name("login")).SendKeys(UserName);
			WebDriver.FindElement(By.Name("password")).SendKeys(Password);
			WebDriver.FindElement(By.Name(":submit")).Click();
		}

		public static bool TryDoAllActions(string topLinkSelector, string actionSelector, Action complexAction = null)
		{
			try
			{
				WebDriver.Navigate().GoToUrl("http://nebo.mobi/home");
				WebDriver.FindElement(By.CssSelector(topLinkSelector)).Click();
				while (WebDriver.FindElements(By.PartialLinkText(actionSelector)).Count > 0)
				{
					WebDriver.FindElement(By.PartialLinkText(actionSelector)).Click();
					if (complexAction != null)
					{
						complexAction();
					}
				}
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		private static void BuyGoodsComplexAction()
		{
			WebDriver.FindElement(By.CssSelector(BuyOneGoods)).Click();
		}

		private static void LiftComplexAction()
		{
			while (WebDriver.FindElements(By.LinkText("Улучшить лифт")).Count > 0)
			{
				while (WebDriver.FindElements(By.PartialLinkText(LiftAction)).Count > 0)
				{
					WebDriver.FindElement(By.PartialLinkText(LiftAction)).Click();
				}
				WebDriver.FindElement(By.CssSelector(LiftTipsAction)).Click();
			}
		}
	}
}
