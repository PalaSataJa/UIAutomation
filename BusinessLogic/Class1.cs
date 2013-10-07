using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace BusinessLogic
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
			WebDriver = new FirefoxDriver();
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
				if (!TryDoAllActions(Lift, LiftAction, LiftComplexAction))
					;

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

		public bool TryDoAllActions(string topLinkSelector, string actionSelector, Action complexAction = null)
		{
			try
			{
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

		private void BuyGoodsComplexAction()
		{
			WebDriver.FindElement(By.CssSelector(BuyOneGoods)).Click();
		}

		private void LiftComplexAction()
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
