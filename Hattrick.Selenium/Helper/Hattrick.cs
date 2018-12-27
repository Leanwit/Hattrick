using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hattrick.Dto;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Hattrick.Selenium.Helper
{
    public static class HattrickAutomation
    {
        public static void Login(this IWebDriver driver, string username, string password)
        {
            FindElement(driver, By.Name("ctl00$CPContent$ucLogin$txtUserName"), 5).SendKeys(username);

            FindElement(driver, By.Name("ctl00$CPContent$ucLogin$txtPassword"), 5).SendKeys(password);
            FindElement(driver, By.Name("ctl00$CPContent$ucLogin$butLogin"), 30).Click();
        }

        public static void GotoPlayer(this IWebDriver driver)
        {
            FindElement(driver, By.Id("playersLink"), 5).Click();
        }

        public static void GoToClub(this IWebDriver driver)
        {
            FindElement(driver, By.ClassName("ft-dialog-button"), 30).Click();
            FindElement(driver, By.Id("myClubLink"), 30).Click();
        }

        public static void Wait(this IWebDriver driver)
        {
            System.Threading.Thread.Sleep(5000);
        }

        private static List<PlayerWebElement> GetPlayersUrlsProfiles(this IWebDriver driver)
        {
            IWebElement playerListElement = driver.FindElement(By.ClassName("playerList"));

            List<PlayerWebElement> playersUrls = new List<PlayerWebElement>();

            foreach (var item in playerListElement.FindElements(By.TagName("h3")))
            {
                var playerElement = item.FindElement(By.TagName("a"));
                playersUrls.Add(new PlayerWebElement()
                    {Url = playerElement.GetAttribute("href"), Name = playerElement.Text});
            }

            return playersUrls;
        }

        public static List<PlayerDto> GetPlayersInformation(this IWebDriver driver)
        {
            List<PlayerWebElement> playerWebElements = driver.GetPlayersUrlsProfiles();

            List<PlayerDto> players = new List<PlayerDto>();
            foreach (var playerWebElement in playerWebElements)
            {
                driver.Navigate().GoToUrl(playerWebElement.Url);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(100);

                PlayerDto aPlayer = new PlayerDto();
                aPlayer.Name = playerWebElement.Name;

                IWebElement table = driver.FindElement(By.Id("ft-ppe-table"));

                aPlayer.Positions = GeneratePositionsByText(table.Text);
                players.Add(aPlayer);
            }

            return players;
        }

        public static IWebElement FindElement(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }

            return driver.FindElement(by);
        }

        private static List<PositionDto> GeneratePositionsByText(string lines)
        {
            string[] textPosition = lines.Split("\n");
            List<PositionDto> listPosition = new List<PositionDto>();
            foreach (var position in textPosition)
            {
                if (!position.ToLower().Contains("aporte"))
                {
                    var aux1 = new PositionDto(position);
                    if (aux1.Value != 0)
                    {
                        listPosition.Add(aux1);
                    }
                }
            }

            return listPosition;
        }
    }

    internal class PlayerWebElement
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}