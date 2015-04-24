using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenSrsLib.Commands.Lookup;

namespace Tests
{
    [TestFixture]
    public class CommandTests
    {
        [TestFixtureSetUp]
        public void FixtureSetup() { }

        [SetUp]
        public void TestSetup() { }

        [TearDown]
        public void TestTearDown() { }

        [TestFixtureTearDown]
        public void FixtureTearDown() { }

        [Test]
        public void BelongsToRspRequest()
        {
            string expected = File.ReadAllText(@"TestData\BelongsToRspRequestCommand1.xml");
            var command = new BelongsToRspRequest();
            command.Domain = "domain.com";
            command.BuildOpsEnvelope("version", "registrantIp");
            var xml = command.RequestXml();

            Assert.AreEqual(expected, xml);
        }

        [Test]
        public void BelongsToRspResponse_DoesBelong()
        {
            var responseXml = File.ReadAllText(@"TestData\BelongsToRspResponseReply1.xml");
            
            var response = new BelongsToRspResponse(responseXml);
            
            Assert.AreEqual("XCP", response.Protocol);
            Assert.AreEqual("REPLY", response.Action);
            Assert.AreEqual("DOMAIN", response.Object);
            Assert.True(response.BelongsToRsp);
            Assert.AreEqual(new DateTime(2007,8,26,11,40,14), response.DomainExpiryDate);
            Assert.AreEqual("Query successful", response.ResponseText);
            Assert.True(response.IsSuccess);
            Assert.AreEqual(200, response.ResponseCode);
        }

        [Test]
        public void BelongsToRspResponse_DoesNotBelong()
        {
            var responseXml = File.ReadAllText(@"TestData\BelongsToRspResponseReply2.xml");

            var response = new BelongsToRspResponse(responseXml);

            Assert.AreEqual("XCP", response.Protocol);
            Assert.AreEqual("REPLY", response.Action);
            Assert.AreEqual("DOMAIN", response.Object);
            Assert.False(response.BelongsToRsp);
            Assert.IsNull(response.DomainExpiryDate);
            Assert.AreEqual("Query successful", response.ResponseText);
            Assert.True(response.IsSuccess);
            Assert.AreEqual(200, response.ResponseCode);
        }
    }
}
