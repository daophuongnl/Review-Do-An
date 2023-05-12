using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class HttpClientUtil
    {
        private static HttpClient? httpClient;
        private static HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                }
                return httpClient;
            }
        }

        public static string CombineUrl(string domain, params KeyValuePair<string, string>[] keyValuePairs)
        {
            var url = new StringBuilder($"{domain}?");

            foreach (var kvp in keyValuePairs)
            {
                url.Append($"{kvp.Key}={kvp.Value}&");
            }
            url.Remove(url.Length - 1, 0);

            return url.ToString();
        }

        public static dynamic? Get<T>(string? bearerToken, string url)
        {
            if (bearerToken != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            using (var response = HttpClient.GetAsync(url).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
                }
                else
                {
                    return new EntHttpResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = response.IsSuccessStatusCode };
                }
            }
        }

        public static void Get(string bearerToken, string url)
        {
            if (bearerToken != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            using (var response = HttpClient.GetAsync(url).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static dynamic? Get<T>(string? bearerToken, string domain, params KeyValuePair<string, string>[] keyValuePairs)
        {
            return Get<T>(bearerToken, CombineUrl(domain, keyValuePairs));
        }

        public static void Get(string bearerToken, string domain, params KeyValuePair<string, string>[] keyValuePairs)
        {
            Get(bearerToken, CombineUrl(domain, keyValuePairs));
        }

        public static T Post<T>(string bearerToken, string domain, params KeyValuePair<string, string>[] keyValuePairs)
        {
            if (bearerToken != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            HttpContent q = new FormUrlEncodedContent(keyValuePairs);

            var url = domain;

            using (var response = HttpClient.PostAsync(url, q).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static void Post(string bearerToken, string domain, string controller, params KeyValuePair<string, string>[] keyValuePairs)
        {
            if (bearerToken != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            HttpContent q = new FormUrlEncodedContent(keyValuePairs);

            var url = domain;

            using (var response = HttpClient.PostAsync(url, q).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static T Post<T>(string bearerToken, string domain, string dataContent)
        {
            if (bearerToken != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            //HttpContent q = new FormUrlEncodedContent(keyValuePairs);
            var stringContent = new System.Net.Http.StringContent(dataContent, UnicodeEncoding.UTF8, "application/json");
            //HttpStringContent stringContent = new HttpStringContent(dataContent, UnicodeEncoding.Utf8, "application/json");


            var url = domain;

            using (var response = HttpClient.PostAsync(url, stringContent).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static T Put<T>(string bearerToken, string domain, string dataContent)
        {
            if (bearerToken != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            var stringContent = new System.Net.Http.StringContent(dataContent, UnicodeEncoding.UTF8, "application/json");
            var url = domain;

            using (var response = HttpClient.PutAsync(url, stringContent).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
