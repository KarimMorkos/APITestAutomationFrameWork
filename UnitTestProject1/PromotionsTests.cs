using APITestAutomationFrameWork.BaseObjects;
using APITestAutomationFrameWork.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using RestSharp;

namespace UnitTestProject1
{

    [TestClass]
    public class PromotionsTests
    {
        PromotionObject promotionObject;
        public PromotionsTests()
        {
             promotionObject = new PromotionObject();
        }

        [TestInitialize]
        public void IninitializeTest()
        {

        }
        

        [TestMethod]
        public void VerifyCataloguePromotionProperties()
        {
            var result = promotionObject
                .SetMethod(Method.GET)
                .SetPath("/Categories/6327/Details.json")
                .AddQueryParam("Catalguge","false")
                .GetPromotions();
            Assert.AreEqual(result.Name, "Carbon credits");
            Assert.IsTrue(result.CanRelist);
            var description = result.Promotions.Where(x => x.Name == "Gallery").Select(x => x.Description).First();
            Assert.IsTrue(description.Contains("2x larger image"));
        }

        [TestCleanup]
        public void CleanTest()
        {
            
        }

    }
    }
