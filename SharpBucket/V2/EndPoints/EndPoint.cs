using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SharpBucket.V1.Pocos;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints {
    public class EndPoint {

        protected SharpBucketV2 _sharpBucketV2;
        protected string _baseUrl;

        protected List<T> GetAllValues<T, TInfo> (string overrideUrl) where TInfo : ResourceInfo<T>, new() {
            var response = _sharpBucketV2.Get(new TInfo(), overrideUrl);
            var values = new List<T>();
            values.AddRange(response.values);

            if (!overrideUrl.EndsWith("/")) {
                overrideUrl += "/";
            }

            while (response != null && !String.IsNullOrEmpty(response.next)) {
                var bits = response.next.Split(new[] { overrideUrl }, StringSplitOptions.None);
                var path = overrideUrl + bits[1];
                response = _sharpBucketV2.Get(new TInfo(), path);

                if (response != null && values != null) {
                    values.AddRange(response.values);
                }
            }

            return values;
        }
    }
}
