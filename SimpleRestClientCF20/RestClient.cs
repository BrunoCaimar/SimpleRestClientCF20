using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SimpleRestClientCF20
{
    public class RestClient
    {
        private string _baseUri;
        private string _resourceName;

        public RestClient(string baseUri, string resourceName)
        {
            this._baseUri = baseUri;
            this._resourceName = resourceName;
        }

        public T Get<T>(object resourceId) where T : new()
        {
            return this.Get<T>(string.Empty, resourceId);
        }

        public T Get<T>(string resource, object resourceId) where T : new()
        {
            string addSlash = string.IsNullOrEmpty(resource) ? string.Empty : "/";

            RestRequest request = new RestRequest(Method.GET);

            request.Resource = string.Format("{0}/{1}{2}{{id}}",
                                             this._resourceName,
                                             resource,
                                             addSlash);

            request.AddUrlSegment("id", resourceId.ToString());

            RestResponse response = this.Execute(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public void Post(object data2Post)
        {
            RestRequest request = new RestRequest(Method.POST);
            request.Resource = string.Format("{0}/", this._resourceName);

            List<Object> postData = new List<Object>();
            postData.Add(data2Post);

            request.JsonBody2Send = JsonConvert.SerializeObject(postData);
            RestResponse response = this.Execute(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
        }

        private RestResponse Execute(RestRequest request)
        {
            string resourceBuilded = !string.IsNullOrEmpty(request.GetUrlSegment().Key) ?
                request.Resource.Replace(request.GetUrlSegment().Key, request.GetUrlSegment().Value) :
                request.Resource;

            string url = string.Format("{0}{1}", this._baseUri, resourceBuilded);

            try
            {
                HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
                httpRequest.Method = request.MethodString;
                httpRequest.ContentType = "application/json; charset=utf-8";

                this.AddPostData(httpRequest, request);

                string result;

                using (HttpWebResponse resp = httpRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
                return new RestResponse(result);
            }
            catch (Exception ex)
            {
                return new RestResponse(ex);
            }
        }

        private void AddPostData(HttpWebRequest httpRequest, RestRequest request)
        {
            if (!string.IsNullOrEmpty(request.JsonBody2Send))
            {
                StringBuilder postData = new StringBuilder();
                postData.Append(request.JsonBody2Send);

                byte[] formData = UTF8Encoding.UTF8.GetBytes(postData.ToString());
                httpRequest.ContentLength = formData.Length;

                using (Stream postStream = httpRequest.GetRequestStream())
                {
                    postStream.Write(formData, 0, formData.Length);
                }
            }
        }
    }
}
