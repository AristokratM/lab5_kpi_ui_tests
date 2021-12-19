using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Allure.Commons;
using AutomatedTesting.PageObjects;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomatedTesting
{
    [AllureNUnit]
    [AllureSuite("Rozetka tests")]
    public class Tests
    {
        private HomePage _homePage;
        private IWebDriver _driver;
        private const string WebDriverPath = "C:\\Users\\1\\RiderProjects\\AutomatedUITesting";

        [OneTimeSetUp]
        public void Setup()
        {
            var chrome_options = new ChromeOptions();
            chrome_options.AddArgument("--use-fake-ui-for-media-stream");
            _driver = new ChromeDriver(WebDriverPath, chrome_options);
        }

        [Test(Description = "Test website logo to have correct hypertext reference value")]
        [AllureTag("UI")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSubSuite("Logo")]
        public void TestLogoHypertextReference()
        {
            // Arrange
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _homePage = new HomePage(_driver, wait);
            const string expectedHypertextReferenceValue = "https://rozetka.com.ua/";

            //Act
            var actualHypertextReferenceValue = _homePage.Logo.GetAttribute("href");

            //Assert
            Assert.IsTrue(actualHypertextReferenceValue.Contains(expectedHypertextReferenceValue));
        }

        [Test(Description = "Test search page result has correct catalog heading")]
        [AllureTag("Functional")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("Search")]
        public void TestSearch()
        {
            //Arrange
            const string searchText = "NVIDIA GeForce GTX 1650";
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _homePage = new HomePage(_driver, wait);

            //Act
            _homePage.SearchInput.SendKeys(searchText);
            _homePage.SearchInput.SendKeys(Keys.Enter);
            var searchResult = new SearchResultPage(wait);
            var resultCatalogHeading = searchResult.SearchPageResultCatalogHeading.Text;

            //Assert
            Assert.IsTrue(resultCatalogHeading.Contains(searchText));
        }

        [Test(Description = "Test that after click on microphone button search voice div is displayed")]
        [AllureTag("Functional")]
        [AllureSubSuite("Microphone")]
        public void TestMicrophoneButton()
        {
            //Arrange
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _homePage = new HomePage(_driver, wait);
            
            //Act
            _homePage.MicrophoneButton.Click();

            //Assert
            Assert.True(_homePage.SearchVoiceDiv.Displayed);
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
