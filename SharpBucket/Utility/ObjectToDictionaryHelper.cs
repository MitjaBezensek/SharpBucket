using System.Collections.Generic;
using System.ComponentModel;

namespace SharpBucket.Utility
{
    public static class ObjectToDictionaryHelper
    {
        // http://stackoverflow.com/questions/11576886/how-to-convert-object-to-dictionarytkey-tvalue-in-c
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            if (source == null)
            {
                return null;
            }

            if (source is IDictionary<string, object> sourceDictionary)
            {
                return sourceDictionary;
            }

            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                var value = property.GetValue(source);
                dictionary.Add(property.Name, value);
            }

            return dictionary;
        }
    }
}