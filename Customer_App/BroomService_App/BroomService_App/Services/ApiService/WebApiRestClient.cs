using BroomService_App.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BroomService_App.Services.ApiService
{
    public class WebApiRestClient
    {
        private readonly JsonSerializerSettings _jsonSettings;
        public WebApiRestClient()
        {
            _jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            };

            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("AppLocale") && !string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString()))
            {
                var languageculture = Xamarin.Forms.Application.Current.Properties["AppLocale"].ToString();
                client.DefaultRequestHeaders.Add("Accept-Language", languageculture);
            }
            else
            {
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            }
        }

        HttpClient client = new HttpClient();

        public async Task<TResponse> GetAsync<TResponse>(string action, bool IsPropertyApi = false)
        {
            Uri baseUri;
            if (IsPropertyApi)
            {
                baseUri = new Uri(ApiHelpers.PropertyBaseUrl);
            }
            else
            {
                baseUri = new Uri(ApiHelpers.AccountBaseUrl);
            }
            Uri uri = new Uri(baseUri, action);
            try
            {
                var response = await client.GetAsync(uri);
                var responsedata = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responsedata);
                //var response = await client.GetStringAsync(uri);
                //return JsonConvert.DeserializeObject<TResponse>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetApi:-", ex.Message);
                return JsonConvert.DeserializeObject<TResponse>(null);
            }
        }

        public async Task<TResponse> GetParamAsync<TResponse>(string action, bool IsPropertyApi = false)
        {
            Uri baseUri;
            if (IsPropertyApi)
            {
                baseUri = new Uri(ApiHelpers.PropertyBaseUrl);
            }
            else
            {
                baseUri = new Uri(ApiHelpers.AccountBaseUrl);
            }
            Uri uri = new Uri(baseUri, action);
            try
            {
                var response = await client.GetAsync(uri);
                var responsedata = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responsedata);
                //var response = await client.GetStringAsync(uri);
                //return JsonConvert.DeserializeObject<TResponse>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetApi:-", ex.Message);
                return JsonConvert.DeserializeObject<TResponse>(null);
            }
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string action, TRequest request, bool IsPropertyApi = false)
        {
            Uri baseUri;
            if (IsPropertyApi)
            {
                baseUri = new Uri(ApiHelpers.PropertyBaseUrl);
            }
            else
            {
                baseUri = new Uri(ApiHelpers.AccountBaseUrl);
            }
            Uri uri = new Uri(baseUri, action);
            var json = JsonConvert.SerializeObject(request, _jsonSettings);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(uri, content).ConfigureAwait(false);
                var responsedata = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responsedata);
                //return await HandleResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetApi:-", ex.Message);
                return await HandleResponse<TResponse>(null);
            }
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string action, MultipartFormDataContent request, bool IsPropertyApi = false)
        {
            Uri baseUri;
            if (IsPropertyApi)
            {
                baseUri = new Uri(ApiHelpers.PropertyBaseUrl);
            }
            else
            {
                baseUri = new Uri(ApiHelpers.AccountBaseUrl);
            }
            Uri uri = new Uri(baseUri, action);

            try
            {
                var response = await client.PostAsync(uri, request);
                var responsedata = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responsedata);
                //return await HandleResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetApi:-", ex.Message);
                return await HandleResponse<TResponse>(null);
            }
        }

        private async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response)
        {

            string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonConvert.DeserializeObject<TResponse>(responseData);
            return result;
            //if (!response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //    if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
            //    {
            //        throw new Exception(content);
            //    }

            //    throw new HttpRequestException(content);
            //}
            //else
            //{
            //    string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //    var result = JsonConvert.DeserializeObject<TResponse>(responseData);
            //    return result;
            //}
        }
    }
}
