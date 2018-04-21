using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CITSmart
{
    [CodedUITest]
    [DeploymentItem(@"CITSmart\Drivers")]
    public class Tests : TestBase
    {
        [TestMethod]
        public void Test1()
        {

        }
    }
}