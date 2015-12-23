using NUnit.Framework;
using System;
namespace BeeCloud.Tests
{
    [TestFixture()]
    public class BCUtilTests
    {
        [Test()]
        public void GetTimeStampTest()
        {
            long timeStamp = 1449849600000;
            long actual = BCUtil.GetTimeStamp(new DateTime(2015, 12, 12, 0, 0, 0, DateTimeKind.Local));
            Assert.IsTrue(actual.Equals(timeStamp), "BCUtilTests GetTimeStampTest failuer");
        }

        [Test()]
        public void GetDateTimeTest()
        {
            DateTime time = new DateTime(2015, 12, 12, 0, 0, 0, DateTimeKind.Local);
            DateTime actual = BCUtil.GetDateTime(1449849600000);
            Assert.IsTrue(actual.Equals(time));
        }

        [Test()]
        public void GetUUIDTest()
        {
            string uuid = BCUtil.GetUUID();
            Assert.IsFalse(uuid.Contains("-"));
        }

        [Test()]
        public void GetSignTest()
        {
            BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", null);
            string sign = "0837478bbead245e8b5523b0ec1d65f4";
            string actual = BCUtil.GetSign("1449849600000");
            Assert.IsTrue(actual.Equals(sign));
        }
    }
}
