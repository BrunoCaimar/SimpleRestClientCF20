using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleRestClientCF20
{
    public class RestResponse
    {
        public RestResponse(Exception ex)
        {
            this._errorException = ex;
        }

        public RestResponse(string result)
        {
            this._content = result;
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private Exception _errorException;

        public Exception ErrorException
        {
            get { return _errorException; }
            set { _errorException = value; }
        }
    }
}
