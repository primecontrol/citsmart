using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
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
        public ExtentHtmlReporter HtmlReporter;

        #endregion


        #region Atributos de teste adicionais

        [TestInitialize()]
        public void MyTestInitialize()
        {
            SetEnviornment();
            Driver.Navigate().GoToUrl(Url);
            Driver.Manage().Window.Maximize();

            TestResultsDirectory = TestContext.TestResultsDirectory;

            ExtentFileName = Path.Combine(TestResultsDirectory, TestContext.TestName + '_' + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".html");

            if (!Directory.Exists(TestResultsDirectory))
            {
                Directory.CreateDirectory(TestResultsDirectory);
            }

            if (!File.Exists(ExtentFileName))
            {
                File.Create(ExtentFileName);
            }

            Playback.PlaybackError += Playback_PlaybackError;
            HtmlReporter = new ExtentHtmlReporter(ExtentFileName);
            Extent = new ExtentReports();
            Extent.AttachReporter(HtmlReporter);

            Test = Extent.CreateTest(TestContext.TestName + " " + Title, Description);

            Driver.ElementValueChanged += FiringDriver_ElementValueChanged;
            Driver.ElementClicked += Driver_ElementClicked;
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                Test.Fail(GetErrorMessage());
            }

            Extent.Flush();
            Driver.Quit();
            WebDriver.Quit();
            HtmlReporter.Stop();

            using (StreamWriter sw = new StreamWriter(ExtentFileName, true))
            {
                sw.WriteLine("<style>img {border: 1px solid #ddd;border-radius: 4px;padding: 5px;width: 150px;} img:hover {box-shadow: 0 0 2px 1px rgba(0, 140, 186, 0.5);}</style><script>function OpenImage(src){ var newTab = window.open(); newTab.document.body.innerHTML = " + '"' + "<img src=" + '"' + " + src + " + '"' + ">" + '"' + ";}</script>");
                sw.Close();
            }

            Extent = null;
            HtmlReporter = null;
            Test = null;
            Logger = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ExecuteCmd("taskkill /im chromedriver.exe /f /t");
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

        #region Events

        public void FiringDriver_ElementValueChanged(object sender, WebElementEventArgs e)
        {
            Screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            EvidenceFileName = Path.Combine(TestResultsDirectory, "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

            Screenshot.SaveAsFile(EvidenceFileName, ScreenshotImageFormat.Png);

            TestInfo = "<h5>" + Logger + "</h5><br/>ElementValueChanged: " + GetDescription(e) + "<br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";

            Test.Info(TestInfo);

            File.Delete(EvidenceFileName);

            Logger = String.Empty;
        }

        public void FiringDriver_ElementValueChanging(object sender, WebElementEventArgs e)
        {
            Screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            EvidenceFileName = Path.Combine(TestResultsDirectory, "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

            Screenshot.SaveAsFile(EvidenceFileName, ScreenshotImageFormat.Png);

            TestInfo = "<h5>" + Logger + "</h5><br/>ElementValueChanging: " + GetDescription(e) + "<br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";

            Test.Info(TestInfo);

            File.Delete(EvidenceFileName);

            Logger = String.Empty;
        }

        private void Driver_ElementClicked(object sender, WebElementEventArgs e)
        {
            Screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            EvidenceFileName = Path.Combine(TestResultsDirectory, "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

            Screenshot.SaveAsFile(EvidenceFileName, ScreenshotImageFormat.Png);

            TestInfo = "<h5>" + Logger + "</h5><br/>ElementClicked: " + GetDescription(e) + "<br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";

            Test.Info(TestInfo);

            File.Delete(EvidenceFileName);

            Logger = String.Empty;
        }

        public void FiringDriver_ElementClicking(object sender, WebElementEventArgs e)
        {
            Screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            EvidenceFileName = Path.Combine(TestResultsDirectory, "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

            Screenshot.SaveAsFile(EvidenceFileName, ScreenshotImageFormat.Png);

            TestInfo = "<h5>" + Logger + "</h5><br/>ElementClicking: " + GetDescription(e) + "<br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";

            Test.Info(TestInfo);

            File.Delete(EvidenceFileName);

            Logger = String.Empty;
        }

        #endregion

        #region Methods
        private void Playback_PlaybackError(object sender, PlaybackErrorEventArgs e)
        {
            Test.Fail(GetErrorMessage());
        }

        public void SetEnviornment()
        {
            using (StreamReader sr =
                new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Enviornment.txt")))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != null && line.Contains("Url:"))
                    {
                        Url = line.Split(new[] { "Url:" }, StringSplitOptions.None).Last().Trim();
                    }
                    if (line != null && line.Contains("User:"))
                    {
                        User = line.Split(new[] { "User:" }, StringSplitOptions.None).Last().Trim();
                    }
                    if (line != null && line.Contains("Password:"))
                    {
                        Password = line.Split(new[] { "Password:" }, StringSplitOptions.None).Last().Trim();
                    }
                }
            }
        }

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

            TestInfo = "<h5>CheckPoint: " + message + "</h5><br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";

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
            try
            {
                if (!string.IsNullOrEmpty(element.GetAttribute("Id")))
                {
                    return By.Id(element.GetAttribute("Id"));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("Name")))
                {
                    return By.Name(element.GetAttribute("Name"));
                }

                if (!string.IsNullOrEmpty(GenerateXpath(element)))
                {
                    return By.XPath(GenerateXpath(element));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("Class")))
                {
                    return By.ClassName(element.GetAttribute("Class"));
                }

                if (!string.IsNullOrEmpty(element.GetAttribute("InnerText")))
                {
                    return By.CssSelector(element.GetAttribute("InnerText"));
                }
            }
            catch (Exception)
            {
                return By.Id(Guid.NewGuid().ToString());
            }

            return By.Id(Guid.NewGuid().ToString());
        }

        public void Login(By user, By password, By confirmButton,
            By elementToAssertion)
        {
            Logger = "Set User: " + User;
            GetElement(user).SendKeys(User);

            Logger = "Set Password: " + Password;
            GetElement(password).SendKeys(Password);

            Logger = "Click Confirm Button";
            GetElement(confirmButton).Click();

            Checkpoint(WaitElement(elementToAssertion), "Login Efetuado Com Sucesso");
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

            Playback.Wait(3000);

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
            bool displayed = false;

            while (Driver.FindElements(by).Count.Equals(0) || !displayed)
            {
                if (Driver.FindElements(by).Any(e => e.Displayed))
                {
                    displayed = true;
                }
                
                Playback.Wait(1000);

                count++;

                if (count > timeoutSeconds)
                {
                    break;
                }

            }

            return displayed;
        }

        #endregion
    }
}