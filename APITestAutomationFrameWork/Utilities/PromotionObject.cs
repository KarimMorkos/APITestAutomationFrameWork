using APITestAutomationFrameWork.BaseObjects;
using APITestAutomationFrameWork.Model;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace APITestAutomationFrameWork.Utilities
{
    public class PromotionObject : ResourceObject
    {
        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private RestRequest _request;

        public PromotionObject() : base()
        {
            _request = new RestRequest();
            _request.AddHeader("Content-type", "application/json");
        }

        public PromotionObject SetPath(string path)
        {
            var url = new Uri(ConfigurationManager.AppSettings["BaseUrl"] + path);
            RestClient = new RestClient(url);
            return this;
        }

        public PromotionObject SetMethod(Method method)
        {
            _request.Method = method;
            return this;
        }

        public PromotionObject AddQueryParam(string key, string value)
        {
            _request.AddQueryParameter(key, value);
            return this;
        }

        public Catalogue GetPromotions()
        {
            return ConvertRestResponseToJsonObject<Catalogue>(Execute<List<Promotion>>(_request));
        }
    }
}
