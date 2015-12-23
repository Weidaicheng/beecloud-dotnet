using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeCloud;
using NUnit.Framework;
namespace BeeCloud.Tests
{
    [TestFixture()]
    public class BCPrivateUtilTests
    {
        [TearDown()]
        public void reset()
        {
            BeeCloud.registerApp(null, null, null);
        }

        [Test()]
        public void getAppSignatureTest()
        {
            try
            {
                string failAcutal = BCPrivateUtil.getAppSignature("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", null, "1449849600000");
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }

            string sign = "baabfeede3298010e35f539eade96c96";
            string actual = BCPrivateUtil.getAppSignature("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "1449849600000");
            Assert.IsTrue(actual.Equals(sign));
        }

        [Test()]
        public void getAppSignatureByMasterSecretTest()
        {
            try
            {
                string failAcutal = BCPrivateUtil.getAppSignatureByMasterSecret("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", null, "1449849600000");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
            string sign = "cd0b6209aec51eeaf7a7833740e9d205";
            string actual = BCPrivateUtil.getAppSignatureByMasterSecret("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "e14ae2db-608c-4f8b-b863-c8c18953eef2", "1449849600000");
            Assert.IsTrue(actual.Equals(sign));
        }
    }
}
