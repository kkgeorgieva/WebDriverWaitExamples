using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitExamples
{
    public class WaitTests
    {

        private WebDriver driver;
        private WebDriverWait wait;

        [TearDown]
        public void ShutDown()
        {
            //close driver after every test
            driver.Quit();
        }
        [Test]
        public void Test_Wait_ThreadSleep()
        {
            //Thread Sleep waiting method is NOT the prefered one


            //Creating the browser
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();


            driver.Url = "https://www.hugedomains.com/domain_profile.cfm?d=uitestpractice.com";

            var element = driver.FindElement(By.PartialLinkText(
                "How do I transfer to another registrar such"
                ));

            ScrollToElement(element);

            element.Click();

            //Adding Thread Sleep
            Thread.Sleep(1000);

            //Checking for revealed element
            var textElement = driver.FindElement(By.ClassName("faq-bi-content")).Text;
            //Asserting its contents
            Assert.IsNotEmpty(textElement);
            driver.Quit();

        }

        [Test]
        public void Test_Wait_ImplicitWait()
        {
            //Creating the browser
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //Adding imlicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Url = "https://www.hugedomains.com/domain_profile.cfm?d=uitestpractice.com";

            var element = driver.FindElement(By.PartialLinkText(
                "How do I transfer to another registrar such"
                ));

            ScrollToElement(element);

            element.Click();

            //Checking for revealed element
            var textElement = driver.FindElement(By.ClassName("faq-bi-content")).Text;

            //Asserting its contents
            Assert.IsNotEmpty(textElement);

            driver.Quit();

        }

        [Test]
        public void Test_Wait_ExplicitWait()
        {
            //Creating the browser
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //Adding explicit wait
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Url = "https://www.hugedomains.com/domain_profile.cfm?d=uitestpractice.com";


            var text_element = wait.Until(d => {
                return driver.FindElement(By.PartialLinkText(
                    "How do I transfer to another registrar such"));
            });

            ScrollToElement(text_element);

            text_element.Click();

            //Checking for revealed element
            var textElement = driver.FindElement(By.ClassName("faq-bi-content")).Text;

            //Asserting its contents
            Assert.IsNotEmpty(textElement);

            driver.Quit();

        }
        [Test]
        public void Test_Wait_ExpectedConditions()
        {
            //Creating the browser
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //Adding explicit wait
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Url = "https://www.hugedomains.com/domain_profile.cfm?d=uitestpractice.com";

            var text_Element = this.wait.Until(
                ExpectedConditions.ElementIsVisible(By.PartialLinkText(
                    "How do I transfer to another registrar such"))
                );
            ScrollToElement(text_Element);

            text_Element.Click();

            Assert.IsNotEmpty(text_Element.Text);

            //Checking for revealed element
            var textElement = driver.FindElement(By.ClassName("faq-bi-content")).Text;

            //Asserting its contents
            Assert.IsNotEmpty(textElement);

            driver.Quit();

        }
        private void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}