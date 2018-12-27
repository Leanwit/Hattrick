using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Hattrick.Dto;
using Hattrick.Selenium.Helper;
using Hattrick.Selenium.Impl;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Hattrick.Selenium
{
    public class Importer : WebDriver
    {
        private readonly ChromeOptions _chromeOptions = new ChromeOptions();
        private IConfiguration _configuration;

        public Importer(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.AddExtensions();
        }

        private void AddExtensions()
        {
            this._chromeOptions.AddExtension(GetResourcesPath("adblockpluschrome-1.8.3.crx"));
            this._chromeOptions.AddExtension(GetResourcesPath("foxtrick-0.17.9.1136.crx"));
        }

        public List<PlayerDto> Get()
        {
            try
            {
                var resourcesFolder = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Resources";
                using (var driver = new ChromeDriver(resourcesFolder, this._chromeOptions))
                {
                    driver.Navigate().GoToUrl("https://hattrick.org");
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(100);

                    IConfigurationSection credentiales = this._configuration.GetSection("Hattrick");
                    driver.Login(credentiales["User"], credentiales["Password"]);

                    driver.GoToClub();
                    driver.GotoPlayer();
                    driver.Wait();

                    List<PlayerDto> players = driver.GetPlayersInformation();

                    return players;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        private static string GetResourcesPath(string filename = "")
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Resources/" + filename;
        }
    }
}