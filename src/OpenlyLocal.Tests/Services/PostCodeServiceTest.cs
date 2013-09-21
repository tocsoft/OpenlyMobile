using Moq;
using NUnit.Framework;
using OpenlyLocal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenlyLocal.Tests.Services
{
    [TestFixture]
    public class PostCodeServiceTest
    {
        PostCodeService _service;
        MockIJsonRestClient _restClient;
        [SetUp]
        public void SetUp(){
            _restClient=new MockIJsonRestClient();
            _service = new PostCodeService(_restClient);
        }
        
        [Test]
        public void DoesServiceRequestCorrectUrl(){

            var postcodeObject = new Core.Models.Postcode();
            var postcode = "CH41 4LZ";
            //setup
            _restClient.RequestIntercepter = r =>
            {
                Assert.AreEqual("http://openlylocal.com/areas/postcodes/ch414lz.json", r.Uri.ToString());
                return new Core.Models.PostcodeRootObject { postcode = postcodeObject };
            };

            _service.GetPostcode(postcode, p => {

            }, e => {
                Assert.Fail();
            });
    
        }

        [Test]
        public void DoesServiceRequestCorrectlyReturnCachedResults()
        {
            var postcodeObject = new Core.Models.Postcode();
            var postcode = "CH41 4LZ";
            int requestCount = 0;
            //setup
            _restClient.RequestIntercepter = r =>
            {
                requestCount++;
                return new Core.Models.PostcodeRootObject { postcode = postcodeObject };
            };

            _service.GetPostcode(postcode, p =>
            {
            }, e =>
            {
                Assert.Fail();
            });

            //ensure request only called once
            Assert.AreEqual(1, requestCount);
        }

        [Test]
        public void DoesErrorGetCalled()
        {
            var postcodeObject = new Core.Models.Postcode();
            var postcode = "CH41 4LZ";
            //setup
            _restClient.RequestIntercepter = r =>
            {
                throw new Exception();
            };

            _service.GetPostcode(postcode, p =>
            {
                Assert.Fail();
            }, e =>
            {

            });

        }
    }
}
