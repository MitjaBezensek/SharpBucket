using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                result = result ?? (result = new Dictionary<string, object>());
                result.Add(kvp.Value);                
            }

            return result;
        }
    }
}
