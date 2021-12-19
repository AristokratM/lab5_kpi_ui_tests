using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace AutomatedTesting.PageObjects
{
    public class HomePage
    {
        private const string Url = "https://www.rozetka.com.ua/";
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HomePage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _driver.Url = Url;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "a.header__logo")]
        public IWebElement Logo { get; set; }
        
        [FindsBy(How = How.CssSelector, Using="input.search-form__input")]
        public IWebElement SearchInput { get; set; }
        
        [FindsBy(How = How.CssSelector, Using="button.search-form__microphone")]
        public IWebElement MicrophoneButton { get; set; }
        
        public IWebElement SearchVoiceDiv => _wait.Until(
        ExpectedConditions.ElementExists(By.CssSelector("div.search-voice")));
    }
}