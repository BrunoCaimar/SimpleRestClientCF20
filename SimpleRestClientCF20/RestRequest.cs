using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRestClientCF20
{
    public class RestRequest
    {
        Method _method;

        public string MethodString
        {
            get
            {
                return this._method == Method.GET ? "GET" : "POST";
            }
        }

        public RestRequest(Method method)
        {
            this._method = method;
        }

        private string _resource;

        public string Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        private KeyValuePair<string, string> _urlSegment;

        public KeyValuePair<string, string> GetUrlSegment()
        {
            return this._urlSegment;
        }

        public void AddUrlSegment(string resourceId, string resourceValue)
        {
            this._urlSegment = new KeyValuePair<string, string>(
                string.Format("{{{0}}}", resourceId),
                resourceValue);
        }

        private string jsonBody2Send;

        public string JsonBody2Send
        {
            get { return jsonBody2Send; }
            set { jsonBody2Send = value; }
        }
    }
}
