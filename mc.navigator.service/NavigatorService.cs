using mc.navigator.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace mc.navigator
{
    public class NavigatorService: INavigator
    {
        private readonly HttpClient _httpClient;

        public Method RequestMethod { get; set; }
        public HttpHeaders Headers => this._httpClient.DefaultRequestHeaders;

        public int TimeOut { get; set; } = 5000;
        public Dictionary<string, string> Form { get; set; }

        public NavigatorService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public string Navigate(Uri uri, Method method)
        {
            HttpResponseMessage responseMessage = null;
            switch (RequestMethod)
            {
                case Method.get:
                    var get = this._httpClient.GetAsync(uri);
                    get.Wait(this.TimeOut);
                    responseMessage = get.Result;
                    break;
                case Method.post:
                    var form = new FormUrlEncodedContent(this.Form);
                    var post = this._httpClient.PostAsync(uri, form);
                    post.Wait(this.TimeOut);
                    responseMessage = post.Result;
                    break;
            }

            string response = responseMessage?.Content.ReadAsStringAsync().Result;

            return response;
        }
    }
}
