using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.EndPoints
{
    internal class FilterQuery : Dictionary<string, object>
    {
        public FilterQuery(string filter)
        {
            if (!String.IsNullOrWhiteSpace(filter))
                Add("q", filter.Replace('\'', '"'));
        }
    }
}
