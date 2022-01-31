using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransactionMonitoring.Web.Models;

namespace TransactionMonitoring.Web.Controllers
{
    public class BaseController : Controller
    {
        IConfiguration _configuration;
        //public string apiBaseUrl;
        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; set; }
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            //apiBaseUrl = _configuration.GetValue<string>("WebAPIBaseUrl");
            BaseEndpoint = new Uri(_configuration.GetValue<string>("WebAPIBaseUrl"));
            _httpClient = new HttpClient();
        }

        #region httpClient helper
        
        

        protected async Task<T> GetAsync<T>(Uri requestUrl)
        {
            addHeaders();
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);

        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        protected async Task<Message<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }
        protected async Task<Message<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }

        protected Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        protected HttpContent CreateHttpContent<T>(T content)
        {
            //var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);

            var jsonSettings = new JsonSerializerSettings();
            //jsonSettings.DateFormatString = "dd/MM/yyyy hh:mm:ss";

            string json = JsonConvert.SerializeObject(content, jsonSettings);


            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        protected void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
        }
        #endregion

    }
}
