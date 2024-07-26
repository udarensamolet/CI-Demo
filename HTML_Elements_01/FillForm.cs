using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HTMLElements01
{
    public class FillForm
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
             ChromeOptions options = new ChromeOptions();
            // Ensure Chrome runs in headless mode
            options.AddArguments("headless");
            // Bypass OS security model, required for running in Docker
            options.AddArguments("no-sandbox");
            // Overcome limited resource problems
            options.AddArguments("disable-dev-shm-usage");
            // Applicable to Windows OS only
            options.AddArguments("disable-gpu");
            // Set window size to ensure elements are visible
            options.AddArguments("window-size=1920x1080");
            // Disable extensions
            options.AddArguments("disable-extensions");
            // Remote debugging port
            options.AddArguments("remote-debugging-port=9222");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [Test]
        public void Test_RegisterUser()
        {
            // Click on My Account link
            driver.FindElement(By.LinkText("My Account")).Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            // Fill in the form
            driver.FindElement(By.CssSelector("input[type='radio'][value='f']")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Fiona");
            driver.FindElement(By.Name("lastname")).SendKeys("Apple");
            driver.FindElement(By.Id("dob")).SendKeys("09/13/1977");

            // Generate a unique email address
            Random rnd = new Random();
            // Generate a random number between 1000 and 9999
            int num = rnd.Next(1000, 9999);  
            String email = "fiona.apple" + num.ToString() + "@example.com";

            driver.FindElement(By.Name("email_address")).SendKeys(email);
            driver.FindElement(By.Name("company")).SendKeys("Example Inc.");
            driver.FindElement(By.Name("street_address")).SendKeys("123 Main Str.");
            driver.FindElement(By.Name("suburb")).SendKeys("London");
            driver.FindElement(By.Name("postcode")).SendKeys("223242");
            driver.FindElement(By.Name("city")).SendKeys("London");
            driver.FindElement(By.Name("state")).SendKeys("London");

            // Select country from dropdown
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("United Kingdom");

            driver.FindElement(By.Name("telephone")).SendKeys("2432424112");
            driver.FindElement(By.Name("newsletter")).Click();

            driver.FindElement(By.Name("password")).SendKeys("fiona_123456");
            driver.FindElement(By.Name("confirmation")).SendKeys("fiona_123456");


            // Submit the form
            driver.FindElement(By.Id("tdb4")).Submit();

            // Assert account creation success
            Assert.IsTrue(driver.PageSource.Contains("Your Account Has Been Created!"), "Account creation failed.");
            
            // Click on Log Off link
            driver.FindElement(By.LinkText("Log Off")).Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Account Created with email: " + email);

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
    
}