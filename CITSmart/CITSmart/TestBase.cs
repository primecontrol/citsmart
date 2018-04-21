using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CITSmart
{
    [CodedUITest]
    public class TestBase
    {
        #region Atributos de teste adicionais

        [TestInitialize()]
        public void MyTestInitialize()
        {
            
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
    }
}