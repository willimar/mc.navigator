using System;
using System.Collections.Generic;
using System.Text;

namespace mc.navigator.domain.Interfaces
{
    public interface IProviderService<T> where T : class, new()
    {
        Method Method { get; }
        Dictionary<string, string> Form { get; }
        T Parser(string value);
        string GetUrl(params string[] values);
    }
}
