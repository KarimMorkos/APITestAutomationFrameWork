using System.Linq;
using APITestAutomationFrameWork.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using TechTalk.SpecFlow;

namespace UnitTestProject1
{
    [Binding]
    public class SpecFlowUnitTest
    {
        private PromotionObject _promotionObject;
        public SpecFlowUnitTest()
        {
            _promotionObject = new PromotionObject();
        }



        [Given("I have this path url (.*)")]
        public void GevinHavingThisEndPoint(string path)
        {
            _promotionObject.SetPath(path);
        }

        [Given("using Get Method")]
        public void UsingGetMethod( )
        {
            _promotionObject.SetMethod(Method.GET);
        }

        [When("using this qurey param (.*)")]
        public void UsingThisQueryParam(string parameters)
        {
            var queryParams = parameters.Split(',');

            _promotionObject.AddQueryParam(queryParams[0], queryParams[1]);
        }

        [Then("I get api response in json format")]
        public void GettingResponseInJsonFormat()
        {
            var result = _promotionObject.GetPromotions();
            Assert.AreEqual(result.Name, "Carbon credits");
            Assert.IsTrue(result.CanRelist);
            var description = result.Promotions.Where(x => x.Name == "Gallery").Select(x => x.Description).First();
            Assert.IsTrue(description.Contains("2x larger image"));


        }
        

    }
}
