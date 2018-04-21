using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace CITSmart
{
    [CodedUITest]
    public abstract class TestBase
    {
        #region Globals

        IWebDriver _driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);

        #endregion


        #region Atributos de teste adicionais

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _driver.Navigate().GoToUrl(new Uri("https://google.com"));
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

        public IWebDriver Driver { get => Driver1; set => Driver1 = value; }
        public IWebDriver Driver1 { get => _driver; set => _driver = value; }

        private TestContext _testContextInstance;
    }
}