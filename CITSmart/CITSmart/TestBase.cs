using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;


namespace CITSmart
{
    [CodedUITest]
    public abstract class TestBase
    {
        #region Globals

        private static readonly IWebDriver WebDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
        public static readonly EventFiringWebDriver Driver = new EventFiringWebDriver(WebDriver);
        public static readonly Actions Action = new Actions(Driver);
        public string Url { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ExtentFileName;
        public static string TestResultsDirectory;
        public static string EvidenceFileName;
        public static string TestInfo;
        public static string Logger = string.Empty;
        public string Description = string.Empty;
        public string Title = string.Empty;
        public ExtentReports Extent;
        public static ExtentTest Test;
        public static Screenshot Screenshot;

        #endregion


        #region Atributos de teste adicionais

        [TestInitialize()]
        public void MyTestInitialize()
        {
            Driver.Navigate().GoToUrl(new Uri("https://google.com"));
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {

        }

        #endregion

        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        private TestContext _testContextInstance;

        #region Methods

        public static string ConvertImageToBase64(string fileName)
        {
            byte[] imageArray = File.ReadAllBytes(fileName);
            return Convert.ToBase64String(imageArray);
        }

        public static void Checkpoint(bool condition, string message)
        {
            Screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            EvidenceFileName = Path.Combine(TestResultsDirectory, "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

            Screenshot.SaveAsFile(EvidenceFileName, ScreenshotImageFormat.Png);

            TestInfo = "<h5>" + message + "</h5><br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";

            if (condition)
            {
                Test.Pass(TestInfo);

                File.Delete(EvidenceFileName);
            }
            else
            {
                Test.Fail(TestInfo);

                File.Delete(EvidenceFileName);

                Assert.Fail(message);
            }
        }

        private static string GenerateXpath(IWebElement element, string current = "")
        {
            #region Params

            string elementTag = element.TagName;
            int count = 0;

            #endregion 

            if (elementTag.Equals("html"))
            {
                return "/html" + current;
            }

            IWebElement parentElement = element.FindElement(By.XPath(".."));
            IList<IWebElement> childrenElements = parentElement.FindElements(By.XPath("*"));

            foreach (var childrenElement in childrenElements)
            {
                string childrenElementTag = childrenElement.TagName;

                if (elementTag.Equals(childrenElementTag))
                {
                    count++;
                }

                if (childrenElement.GetType().GetProperties()[0].GetValue(childrenElement).Equals(element.GetType().GetProperties()[0].GetValue(element)))
                {
                    return GenerateXpath(parentElement, "/" + elementTag + "[" + count + "]" + current);
                }
            }

            return null;
        }

        public static By GetSelectors(IWebElement element)
        {
            #region Params

            By by;

            #endregion

            try
            {
                by = By.Id("");

                if (!string.IsNullOrEmpty(element.GetAttribute("Id")))
                {
                    by = By.Id(element.GetAttribute("Id"));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("Name")))
                {
                    by = By.Name(element.GetAttribute("Name"));
                }

                if (!string.IsNullOrEmpty(GenerateXpath(element)))
                {
                    by = By.XPath(GenerateXpath(element));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("ClassName")))
                {
                    by = By.ClassName(element.GetAttribute("ClassName"));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("CssSelector")))
                {
                    by = By.CssSelector(element.GetAttribute("CssSelector"));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("LinkText")))
                {
                    by = By.LinkText(element.GetAttribute("LinkText"));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("PartialLinkText")))
                {
                    by = By.PartialLinkText(element.GetAttribute("PartialLinkText"));
                }
            }
            catch (Exception)
            {
                by = By.Id(Guid.NewGuid().ToString());
            }

            return by;
        }

        public void Login(IWebElement user, IWebElement password, IWebElement confirmButton,
            IWebElement elementToAssertion)
        {
            user.SendKeys(User);
            password.SendKeys(Password);
            confirmButton.Click();

            Checkpoint(WaitElement(GetSelectors(elementToAssertion)), "Logins Efetuado Com Sucesso");
        }

        public string GetErrorMessage()
        {
            const BindingFlags privateGetterFlags = BindingFlags.GetField |
                                                    BindingFlags.GetProperty |
                                                    BindingFlags.NonPublic |
                                                    BindingFlags.Instance |
                                                    BindingFlags.FlattenHierarchy;

            var mMessage = string.Empty; // Returns empty if TestOutcome is not failed
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                // Get hold of TestContext.m_currentResult.m_errorInfo.m_stackTrace (contains the stack trace details from log)
                var field = TestContext.GetType().GetField("m_currentResult", privateGetterFlags);
                if (field != null)
                {
                    var mCurrentResult = field.GetValue(TestContext);
                    field = mCurrentResult.GetType().GetField("m_errorInfo", privateGetterFlags);
                    if (field != null)
                    {
                        var mErrorInfo = field.GetValue(mCurrentResult);
                        field = mErrorInfo.GetType().GetField("m_stackTrace", privateGetterFlags);
                        if (field != null) mMessage = field.GetValue(mErrorInfo) as string;
                    }
                }
            }

            return mMessage;
        }

        public string GetDescription(WebElementEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.Element.GetAttribute("id")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [Id=" + e.Element.GetAttribute("id") + ']';
                }

                if (!String.IsNullOrEmpty(e.Element.GetAttribute("class")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [Class=" + e.Element.GetAttribute("class") + ']';
                }

                if (!String.IsNullOrEmpty(e.Element.GetAttribute("name")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [Name=" + e.Element.GetAttribute("name") + ']';
                }

                if (!String.IsNullOrEmpty(e.Element.GetAttribute("innertext")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [InnerText=" + e.Element.GetAttribute("innertext") + ']';
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return "";
        }

        public List<KeyValuePair<string, string>> ExecuteCmd(string command)
        {
            Process process = new Process();
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.WaitForExit();

            list.Add(new KeyValuePair<string, string>("Output", process.StandardOutput.ReadToEnd()));
            list.Add(new KeyValuePair<string, string>("Error", process.StandardError.ReadToEnd()));

            return list;
        }

        public static IWebElement GetElement(By by, int timeoutSeconds = 10)
        {
            WaitElement(by, timeoutSeconds);

            if (Driver.FindElements(by).Count > 0)
            {
                return Driver.FindElement(by);
            }

            return null;
        }

        public static bool WaitElement(By by, int timeoutSeconds = 10)
        {
            int count = 0;

            while (Driver.FindElements(by).Count.Equals(0))
            {
                Playback.Wait(1000);

                count++;

                if (count > timeoutSeconds)
                {
                    break;
                }
            }

            return Driver.FindElements(by).Count > 0;
        }

        #endregion
    }
}