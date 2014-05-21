using System;

namespace Hopnscotch.Integration.AmoCRM
{
    [AttributeUsage(System.AttributeTargets.Class)]
    public class AmoCrmResponseTypeAttribute : Attribute
    {
        private readonly string _responseType;

        public AmoCrmResponseTypeAttribute(string _responseType)
        {
            this._responseType = _responseType;
        }

        public string ResponseType
        {
            get { return _responseType; }
        }
    }
}