using System.Collections.Generic;
using System.Linq;

namespace SharpBucket.V2.EndPoints
{
    public abstract class ParametersBase
    {
        internal abstract IDictionary<string, object> ToDictionary();

        protected KeyValuePair<string, object>? KvpOrNull(bool condition, string key, object value)
        {            
            if (condition)
                return new KeyValuePair<string, object>(key, value);
            else
                return null;
        }

        protected IDictionary<string, object> DictionaryFromKvps(params KeyValuePair<string, object>?[] kvps)
        {
            IDictionary<string, object> result = null;

            foreach (var kvp in kvps.Where(p => p.HasValue))
            {
                result = result ?? new Dictionary<string, object>();
                result.Add(kvp.Value);                
            }

            return result;
        }

        protected static IDictionary<string, object> AddParameterToDictionary(IDictionary<string, object> dictionary, string key, object value)
        {
            if (value == null) return dictionary;

            if (dictionary is null)
            {
                dictionary = new Dictionary<string, object>();
            }
            dictionary.Add(key, value);

            return dictionary;
        }
    }
}
