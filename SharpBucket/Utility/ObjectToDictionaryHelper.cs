using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace SharpBucket.Utility{
    public static class ObjectToDictionaryHelper{
        //http://stackoverflow.com/questions/11576886/how-to-convert-object-to-dictionarytkey-tvalue-in-c
        public static Dictionary<string, object> ToDictionary(this object source){
            if (source is ExpandoObject)
                return new Dictionary<string, object>(source as ExpandoObject);

            return source.ToDictionary<object>();
        }

        public static Dictionary<string, T> ToDictionary<T>(this object source){
            if (source == null) return null;

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary){
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T) value);
        }

        private static bool IsOfType<T>(object value){
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull(){
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
