using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace mc.navigator.domain.Interfaces
{
    public enum Method
    {
        get,
        post
    }

    public interface INavigator
    {
        Method RequestMethod { get; set; }
        HttpRequestHeaders Headers { get; }
        int TimeOut { get; set; }
        Dictionary<string, string> Form { get; set; }

        string Navigate(Uri uri, Method method);
    }
}
