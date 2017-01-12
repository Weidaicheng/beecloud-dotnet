using NUnit.Framework;
using System;
namespace BeeCloud.Tests
{
    [TestFixture()]
    public class BCUtilTests
    {
        [Test()]
        public void GetUUIDTest()
        {
            string uuid = BCUtil.GetUUID();
            Assert.IsFalse(uuid.Contains("-"));
        }

        [Test()]
        public void GetSignTest()
        {
            BeeCloud.registerApp("afae2a33-c9cb-4139-88c9-af5c1df472e1", "fc8865bb-9dca-454e-ba8e-0d8ed6cc83a2", "506371c7-b095-45da-bfce-9ae857c41a85", null);
            string sign = "e204d63775d3d29dafe6788a0a978755";
            string actual = BCUtil.GetSign("5f0c1466d8bd11e6b66d00163e005bb3", "PAY", "BC", 1);
            Assert.IsTrue(actual.Equals(sign));
        }
    }
}
