using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace SharpBucket
{
    internal class PlainTextSerializationStrategy : IDeserializer
    {
        public string DateFormat
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        public string Namespace
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        public string RootElement
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public T Deserialize<T>(IRestResponse response) 
        {
            return (T) ((object) response.Content);
        }
    }
}
