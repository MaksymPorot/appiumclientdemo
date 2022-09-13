using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Drawing;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace appiumclientdemo
{
    public class Tests
    {
        private AppiumDriver<AndroidElement> _driver;
        private static string appium = "http://localhost:4723/wd/hub";

        [SetUp]
        public void Setup()
        {
            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android 33");
            driverOption.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");

            _driver = new AndroidDriver<AndroidElement>(new Uri(appium), driverOption);
        }


        public void SwipeUp()
        {
            TouchAction swipeUp = new TouchAction(_driver);
            swipeUp.Press(550, 1500)
                .Wait(0).MoveTo(550, 500)
                .Release()
                .Perform();
        }

        [Test]
        public void GmailSendingMessenge()
        {
            SwipeUp();

            _driver.FindElementByAccessibilityId("Gmail")
                .Click();
            _driver.FindElementById("com.google.android.gm:id/compose_button")
                .Click();
            _driver.FindElementByXPath("//android.widget.EditText[@text=\"\"]\r\n")
                .SendKeys("testpmv2022@gmail.com");
            _driver.FindElementById("com.google.android.gm:id/peoplekit_listview_flattened_row")
                .Click();
            _driver.FindElementById("com.google.android.gm:id/subject")
                .SendKeys("HomeWorkAuto");
            _driver.HideKeyboard();
            _driver.FindElementByAccessibilityId("Send")
                .Click();

            WebDriverWait forSent = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            forSent.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//android.widget.TextView[@text=\"Sent\"]\r\n")));

            _driver.FindElementByAccessibilityId("Open navigation drawer")
                .Click();
            _driver.FindElementByXPath("//android.widget.TextView[@text=\"Sent\"]\r\n")
                .Click();
            _driver.FindElementByXPath("//android.widget.TextView[@text=\"HomeWorkAuto\"]\r\n");
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
                _driver.Quit();
        }
    }
}