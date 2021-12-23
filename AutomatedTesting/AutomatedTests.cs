using System;
using Allure.Commons;
using AutomatedTesting.PageObjects;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomatedTesting
{
    [AllureNUnit]
    [AllureSuite("Rozetka tests")]
    public class Tests
    {
        private HomePage _homePage;
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private const string WebDriverPath = "C:\\Users\\1\\RiderProjects\\AutomatedUITesting";

        [OneTimeSetUp]
        public void Setup()
        {
            var chrome_options = new ChromeOptions();
            chrome_options.AddArgument("--use-fake-ui-for-media-stream");
            _driver = new ChromeDriver(WebDriverPath, chrome_options);

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test(Description = "Test website logo to have correct hypertext reference value")]
        [AllureTag("UI")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureSubSuite("Logo")]
        public void TestLogoHypertextReference()
        {
            _homePage = new HomePage(_driver, _wait);
            const string expectedHypertextReferenceValue = "https://rozetka.com.ua/";
            
            var actualHypertextReferenceValue = _homePage.Logo.GetAttribute("href");
            
            Assert.IsTrue(actualHypertextReferenceValue.Contains(expectedHypertextReferenceValue));
        }

        [Test(Description = "Test search page result has correct catalog heading")]
        [AllureTag("Functional")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("Search")]
        public void TestSearch()
        {
            const string searchText = "NVIDIA GeForce GTX 1650";
            _homePage = new HomePage(_driver, _wait);
            
            _homePage.SearchInput.SendKeys(searchText);
            _homePage.SearchInput.SendKeys(Keys.Enter);
            var searchResult = new SearchResultPage(_wait);
            var resultCatalogHeading = searchResult.SearchPageResultCatalogHeading.Text;
            
            Assert.IsTrue(resultCatalogHeading.Contains(searchText));
        }

        [Test(Description = "Test that after click on microphone button search voice div is displayed")]
        [AllureTag("Functional")]
        [AllureSubSuite("Microphone")]
        public void TestMicrophoneButton()
        {
            _homePage = new HomePage(_driver, _wait);
            
            _homePage.MicrophoneButton.Click();

            Assert.True(_homePage.SearchVoiceDiv.Displayed);
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
