using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FinanceScaper
{
    public class Scrap
    {
        public static void Driver()
        
        {






            ChromeOptions options = new ChromeOptions();
            options.AddArgument("no-sandbox");


            IWebDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            driver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(1200));

            driver.Navigate().GoToUrl("https://finance.yahoo.com/");

             driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1200);

             WebDriverWait waitLogin = new WebDriverWait(driver, TimeSpan.FromMinutes(30));
            waitLogin.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("uh-signedin")));

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(10);


            IWebElement loginButton = driver.FindElement(By.Id("uh-signedin"));
            loginButton.Click();

            WebDriverWait waitEnterUsername = new WebDriverWait(driver, TimeSpan.FromMinutes(50));
            waitEnterUsername.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("login-username")));

            IWebElement userName = driver.FindElement(By.Id("login-username"));

            userName.SendKeys("xaviercarty@yahoo.com");
            userName.SendKeys(Keys.Enter);

            WebDriverWait waitEnterPassword = new WebDriverWait(driver, TimeSpan.FromMinutes(50));
            waitEnterPassword.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("login-passwd")));
            IWebElement password = driver.FindElement(By.Id("login-passwd"));

            password.SendKeys("Soccer_1995");
            password.SendKeys(Keys.Enter);

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(10);

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view");

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(10);

            WebDriverWait waitDataTable = new WebDriverWait(driver, TimeSpan.FromMinutes(50));
            waitDataTable.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//tr")));

            IWebElement stockTable = driver.FindElement(By.XPath("//tbody"));
            List<IWebElement> stocks = driver.FindElements(By.XPath("//tr")).ToList();

            List<IWebElement> rows = stockTable.FindElements(By.XPath("//tr")).ToList();
            int rowsCount = rows.Count;
            Console.WriteLine(rows);

            using (var db = new StockContext()) 

            { 
                for (int row = 1; row < rowsCount; row++)
                {
                    
                    List<IWebElement> cells = rows.ElementAt(row).FindElements(By.TagName("td")).ToList();
                    int cellsCount = cells.Count;




                    string symbolData = cells.ElementAt(0).Text;
                    Console.WriteLine("Symbol: " + symbolData);
                    string lastPriceData = cells.ElementAt(1).Text;
                    Console.WriteLine("Last Price: " + lastPriceData);
                    string changeData = cells.ElementAt(2).Text;
                    Console.WriteLine("Change: " + changeData);
                    string changeRateData = cells.ElementAt(3).Text;
                    Console.WriteLine("Change Rate: " + changeRateData);
                    string currencyData = cells.ElementAt(4).Text;
                    Console.WriteLine("Currency: " + currencyData);
                    string marketTimeData = cells.ElementAt(5).Text;
                    Console.WriteLine("Market Time: " + marketTimeData);
                    string volumeData = cells.ElementAt(6).Text;
                    Console.WriteLine("Volume: " + volumeData);
                    string shareData = cells.ElementAt(7).Text;
                    Console.WriteLine("Shares: " + shareData);
                    string averageVolumeData = cells.ElementAt(8).Text;
                    Console.WriteLine("Avg Vol Data: " + averageVolumeData);
                    string marketCapData = cells.ElementAt(12).Text;
                    Console.WriteLine("Market Cap: " + marketCapData);


                    var stocky = new Stock
                    {
                        Symbol = symbolData,
                        LastPrice = lastPriceData,
                        Change = changeData,
                        ChangeRate = changeRateData,
                        Currency = currencyData,
                        MarketTime = marketTimeData,
                        Volume = volumeData,
                        Shares = shareData,
                        AverageVolume = averageVolumeData,
                        MarketCap = marketCapData



                    };

                    db.Stocks.Add(stocky);
                    db.SaveChanges();

                }
            }

        }

    }
}
