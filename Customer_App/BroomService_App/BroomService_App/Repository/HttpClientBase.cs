using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace BroomService_App.Repository
{
    public class HttpClientBase : HttpClient
    {
        private static readonly HttpClientBase _instance = new HttpClientBase();
        CancellationTokenSource cts;
        static HttpClientBase()
        {

        }
        public HttpClientBase() : base()
        {
            TimeSpan time = new TimeSpan(0, 0, 60);
            Timeout = time;
            cts = new CancellationTokenSource();
            cts.CancelAfter(time);
        }

        private HttpContent GetHttpContentForm(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        private HttpContent GetHttpContentJson(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
