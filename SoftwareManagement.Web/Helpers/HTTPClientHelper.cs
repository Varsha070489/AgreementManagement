
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Web.Abstractions;

namespace SoftwareManagement.Web.Helpers
{
    public class HTTPClientHelper : IHTTPClientHelper
    {
        IHttpClientFactory httpClientFactory;
        HttpClient client;
        String ClientName;
        private readonly IHttpContextAccessor _contextAccessor;
        public HTTPClientHelper(IHttpClientFactory httpClientFactory,
            string ClientName,
            IHttpContextAccessor contextAccessor
            )
        {
            this.httpClientFactory = httpClientFactory;
            this.ClientName = ClientName;
            this._contextAccessor = contextAccessor;
        }

        #region Generic, Async, static HTTP functions for GET, POST, PUT, and DELETE             

        public async Task<T> GetAsync<T>(string url)
        {
            T data;
            client = httpClientFactory.CreateClient(ClientName);
            var token = _contextAccessor.HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            Object o = new Object();
            return (T)o;
        }

        public async Task<T> PostAsync<T>(string url, HttpContent contentPost)
        {
            T data;
            client = httpClientFactory.CreateClient(ClientName);
            var token = _contextAccessor.HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (HttpResponseMessage response = await client.PostAsync(url, contentPost))
            {
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            Object o = new Object();
            return (T)o;
        }

        public async Task<T> PutAsync<T>(string url, HttpContent contentPut)
        {
            T data;
            client = httpClientFactory.CreateClient(ClientName);
            var token = _contextAccessor.HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (HttpResponseMessage response = await client.PutAsync(url, contentPut))
            using (HttpContent content = response.Content)
            {
                string d = await content.ReadAsStringAsync();
                if (d != null)
                {
                    data = JsonConvert.DeserializeObject<T>(d);
                    return (T)data;
                }
            }
            Object o = new Object();
            return (T)o;
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            T newT;
            client = httpClientFactory.CreateClient(ClientName);
            var token = _contextAccessor.HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (HttpResponseMessage response = await client.DeleteAsync(url))
            using (HttpContent content = response.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    newT = JsonConvert.DeserializeObject<T>(data);
                    return newT;
                }
            }
            Object o = new Object();
            return (T)o;
        }
        #endregion
    }

}
