﻿using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using SharpBucket.Authentication;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2
{
    internal class RequestExecutorV2 : RequestExecutor
    {
        protected override BitbucketException BuildBitbucketException(IRestResponse response)
        {
            // response.ErrorException is not useful for caller in that case, so it's useless to transmit it as an inner exception
            return new BitbucketV2Exception(response.StatusCode, ExtractErrorResponse(response));
        }

        private static ErrorResponse ExtractErrorResponse(IRestResponse response)
        {
            try
            {
                var errorResponse = SimpleJson.DeserializeObject<ErrorResponse>(response.Content);
                if (errorResponse?.error is null)
                {
                    var errorResponse2 = SimpleJson.DeserializeObject<ErrorResponse2>(response.Content);
                    errorResponse = errorResponse2.ToErrorResponse();
                }

                if (!(errorResponse?.error is null))
                {
                    return errorResponse;
                }
                
            }
            catch (Exception)
            {
                // ignored
            }

            return new ErrorResponse
            {
                type = "Undefined",
                error = new Error
                {
                    message = !string.IsNullOrWhiteSpace(response.Content)
                        ? response.Content
                        : !string.IsNullOrWhiteSpace(response.StatusDescription)
                            ? response.StatusDescription
                            : response.StatusCode.ToString()
                }
            };
        }

        protected override void AddBody(IRestRequest request, object body)
        {
            // Use a custom JsonSerializerStrategy to be able to ignore null properties during the serialization
            // https://stackoverflow.com/questions/20006813/restsharp-how-to-skip-serializing-null-values-to-json
            var jsonString = SimpleJson.SerializeObject(body, new JsonSerializerStrategy());
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
        }

        private class JsonSerializerStrategy : PocoJsonSerializerStrategy
        {
            protected override object SerializeEnum(Enum p)
            {
                if (p is IssuePriority)
                    return p.ToString().ToLower();

                if (p is IssueKind)
                    return p.ToString().ToLower();

                if (p is IssueStatus)
                    return p.ToString().ToLower();
                
                return p.ToString();
            }

            protected override bool TrySerializeUnknownTypes(object input, out object output)
            {
                var returnValue = base.TrySerializeUnknownTypes(input, out output);

                if (output is IDictionary<string, object> obj)
                {
                    output = obj.Where(o => o.Value != null).ToDictionary(o => o.Key, o => o.Value);
                }

                return returnValue;
            }
        }
    }
}
