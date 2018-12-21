using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.EndPoints
{
    public abstract class ParametersBase
    {
        protected class Parameter
        {
            public bool Condition { get; set; }
            public string Key { get; set; }
            public object Value { get; set; }

            public Parameter(bool condition, string key, object value)
            {
                Condition = condition;
                Key = key;
                Value = value;
            }
        }

        internal abstract IDictionary<string, object> ToDictionary();

        protected IDictionary<string, object> DictionaryFromParameters(IEnumerable<Parameter> parameters)
        {
            IDictionary<string, object> result = null;

            foreach (var option in parameters)
            {
                if (option.Condition)
                {
                    result = result ?? (result = new Dictionary<string, object>());
                    result[option.Key] = option.Value;
                }
            }

            return result;
        }
    }
}
