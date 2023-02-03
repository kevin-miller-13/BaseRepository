using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Korbitec.Licensing.ControllerFunctions.Tests.Helpers
{
    public static class RequestHelpers
    {
        public static Tuple<string, T> MakeHttpRequestParameter<T>(string parameterName, T value) => Tuple.Create(parameterName, value);

        public static DefaultHttpRequest BuildHttpRequest<T>(params Tuple<string, T>[] parameters)
        {
            var encodedParameters = new List<string>();

            foreach (Tuple<string, T> param in parameters)
            {
                string name = WebUtility.UrlEncode(param.Item1);
                string value = WebUtility.UrlEncode(param.Item2.ToString());

                encodedParameters.Add($"{name}={value}");
            }

            string joinedParameters = string.Join("&", encodedParameters);

            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                QueryString = new QueryString("?" + joinedParameters)
            };
        }

        public static DefaultHttpRequest BuildHttpRequestWithContent<T>(T requestObject)
        {
            string serialisedObject = JsonConvert.SerializeObject(requestObject);

            var defaultHttpRequest = new DefaultHttpRequest(new DefaultHttpContext())
            {
                ContentType = "application/json",
                Body = new MemoryStream()
            };

            var stream = new StreamWriter(defaultHttpRequest.Body);
            stream.Write(serialisedObject);
            stream.Flush();

            defaultHttpRequest.Body.Position = 0;

            return defaultHttpRequest;
        }
    }
}
