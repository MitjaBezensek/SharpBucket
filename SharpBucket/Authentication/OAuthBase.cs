using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace SharpBucket.Authentication{
    namespace OAuth{
        public sealed class OAuthBase{
            /// <summary>
            /// Provides a predefined set of algorithms that are supported officially by the protocol
            /// </summary>
            private enum SignatureTypes{
                HMACSHA1,
                PLAINTEXT,
                RSASHA1
            }

            /// <summary>
            /// Provides an internal structure to sort the query parameter
            /// </summary>
            private class QueryParameter{
                private readonly string name;
                private readonly string value;

                public QueryParameter(string name, string value){
                    this.name = name;
                    this.value = value;
                }

                public string Name{
                    get { return name; }
                }

                public string Value{
                    get { return value; }
                }
            }

            /// <summary>
            /// Comparer class used to perform the sorting of the query parameters
            /// </summary>
            private class QueryParameterComparer : IComparer<QueryParameter>{
                #region IComparer<QueryParameter> Members

                public int Compare(QueryParameter x, QueryParameter y){
                    if (x.Name == y.Name){
                        return String.CompareOrdinal(x.Value, y.Value);
                    }
                    else{
                        return String.CompareOrdinal(x.Name, y.Name);
                    }
                }

                #endregion
            }

            private const string OAuthVersion = "1.0";
            private const string OAuthParameterPrefix = "oauth_";

            //
            // List of know and used oauth parameters' names
            //        
            private const string OAuthConsumerKeyKey = "oauth_consumer_key";
            private const string OAuthCallbackKey = "oauth_callback";
            private const string OAuthVersionKey = "oauth_version";
            private const string OAuthSignatureMethodKey = "oauth_signature_method";
            private const string OAuthSignatureKey = "oauth_signature";
            private const string OAuthTimestampKey = "oauth_timestamp";
            private const string OAuthNonceKey = "oauth_nonce";
            private const string OAuthTokenKey = "oauth_token";
            private const string OAuthTokenSecretKey = "oauth_token_secret";

            private const string HMACSHA1SignatureType = "HMAC-SHA1";
            private const string PlainTextSignatureType = "PLAINTEXT";
            private const string RSASHA1SignatureType = "RSA-SHA1";

            private readonly Random random = new Random();

            private const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            /// <summary>
            /// Helper function to compute a hash value
            /// </summary>
            /// <param Name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
            /// <param Name="data">The data to hash</param>
            /// <returns>a Base64 string of the hash value</returns>
            private string ComputeHash(HashAlgorithm hashAlgorithm, string data){
                if (hashAlgorithm == null){
                    throw new ArgumentNullException("hashAlgorithm");
                }

                if (string.IsNullOrEmpty(data)){
                    throw new ArgumentNullException("data");
                }

                byte[] dataBuffer = Encoding.ASCII.GetBytes(data);
                byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

                return Convert.ToBase64String(hashBytes);
            }

            /// <summary>
            /// Internal function to cut out all non oauth query string parameters (all parameters not begining with "oauth_")
            /// </summary>
            /// <param Name="parameters">The query string part of the Url</param>
            /// <returns>A list of QueryParameter each containing the parameter Name and value</returns>
            private List<QueryParameter> GetQueryParameters(string parameters){
                if (parameters.StartsWith("?")){
                    parameters = parameters.Remove(0, 1);
                }

                var result = new List<QueryParameter>();

                if (!string.IsNullOrEmpty(parameters)){
                    string[] p = parameters.Split('&');
                    foreach (string s in p){
                        if (!string.IsNullOrEmpty(s) && !s.StartsWith(OAuthParameterPrefix)){
                            if (s.IndexOf('=') > -1){
                                string[] temp = s.Split('=');
                                result.Add(new QueryParameter(temp[0], temp[1]));
                            }
                            else{
                                result.Add(new QueryParameter(s, string.Empty));
                            }
                        }
                    }
                }

                return result;
            }

            /// <summary>
            /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
            /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
            /// </summary>
            /// <param Name="value">The value to Url encode</param>
            /// <returns>Returns a Url encoded string</returns>
            private string UrlEncode(string value){
                var result = new StringBuilder();

                foreach (char symbol in value){
                    if (unreservedChars.IndexOf(symbol) != -1){
                        result.Append(symbol);
                    }
                    else{
                        result.Append('%' + String.Format("{0:X2}", (int) symbol));
                    }
                }

                return result.ToString();
            }

            /// <summary>
            /// Normalizes the request parameters according to the spec
            /// </summary>
            /// <param Name="parameters">The list of parameters already sorted</param>
            /// <returns>a string representing the normalized parameters</returns>
            private string NormalizeRequestParameters(IList<QueryParameter> parameters){
                var sb = new StringBuilder();
                for (int i = 0; i < parameters.Count; i++){
                    QueryParameter p = parameters[i];
                    sb.AppendFormat("{0}={1}", p.Name, p.Value);

                    if (i < parameters.Count - 1){
                        sb.Append("&");
                    }
                }

                return sb.ToString();
            }

            /// <summary>
            /// Generate the signature base that is used to produce the signature
            /// </summary>
            /// <param Name="url">The full url that needs to be signed including its non OAuth url parameters</param>
            /// <param Name="consumerKey">The consumer key</param>        
            /// <param Name="token">The token, if available. If not available pass null or an empty string</param>
            /// <param Name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
            /// <param Name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
            /// <param Name="nonce"></param>
            /// <param Name="signatureType">The signature type. To use the default values use <see cref="OAuthBase.SignatureTypes">OAuthBase.SignatureTypes</see>.</param>
            /// <param Name="timeStamp"></param>
            /// <param Name="normalizedUrl"></param>
            /// <param Name="normalizedRequestParameters"></param>
            /// <returns>The signature base</returns>
            private string GenerateSignatureBase(Uri url, string consumerKey, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, string signatureType, out string normalizedUrl, out string normalizedRequestParameters){
                if (token == null){
                    token = string.Empty;
                }

                if (tokenSecret == null){
                    tokenSecret = string.Empty;
                }

                if (string.IsNullOrEmpty(consumerKey)){
                    throw new ArgumentNullException("consumerKey");
                }

                if (string.IsNullOrEmpty(httpMethod)){
                    throw new ArgumentNullException("httpMethod");
                }

                if (string.IsNullOrEmpty(signatureType)){
                    throw new ArgumentNullException("signatureType");
                }

                normalizedUrl = null;
                normalizedRequestParameters = null;

                List<QueryParameter> parameters = GetQueryParameters(url.Query);
                parameters.Add(new QueryParameter(OAuthVersionKey, OAuthVersion));
                parameters.Add(new QueryParameter(OAuthNonceKey, nonce));
                parameters.Add(new QueryParameter(OAuthTimestampKey, timeStamp));
                parameters.Add(new QueryParameter(OAuthSignatureMethodKey, signatureType));
                parameters.Add(new QueryParameter(OAuthConsumerKeyKey, consumerKey));

                if (!string.IsNullOrEmpty(token)){
                    parameters.Add(new QueryParameter(OAuthTokenKey, token));
                }

                parameters.Sort(new QueryParameterComparer());

                normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
                if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443))){
                    normalizedUrl += ":" + url.Port;
                }
                normalizedUrl += url.AbsolutePath;
                normalizedRequestParameters = NormalizeRequestParameters(parameters);

                var signatureBase = new StringBuilder();
                signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
                signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
                signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));

                return signatureBase.ToString();
            }

            /// <summary>
            /// Generate the signature value based on the given signature base and hash algorithm
            /// </summary>
            /// <param Name="signatureBase">The signature based as produced by the GenerateSignatureBase method or by any other means</param>
            /// <param Name="hash">The hash algorithm used to perform the hashing. If the hashing algorithm requires initialization or a key it should be set prior to calling this method</param>
            /// <returns>A base64 string of the hash value</returns>
            private string GenerateSignatureUsingHash(string signatureBase, HashAlgorithm hash){
                return ComputeHash(hash, signatureBase);
            }

            /// <summary>
            /// Generates a signature using the HMAC-SHA1 algorithm
            /// </summary>		
            /// <param Name="url">The full url that needs to be signed including its non OAuth url parameters</param>
            /// <param Name="consumerKey">The consumer key</param>
            /// <param Name="consumerSecret">The consumer seceret</param>
            /// <param Name="token">The token, if available. If not available pass null or an empty string</param>
            /// <param Name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
            /// <param Name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
            /// <param Name="timeStamp"></param>
            /// <param Name="nonce"></param>
            /// <param Name="normalizedUrl"></param>
            /// <param Name="normalizedRequestParameters"></param>
            /// <returns>A base64 string of the hash value</returns>
            public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, out string normalizedUrl, out string normalizedRequestParameters){
                return GenerateSignature(url, consumerKey, consumerSecret, token, tokenSecret, httpMethod, timeStamp, nonce, SignatureTypes.HMACSHA1, out normalizedUrl, out normalizedRequestParameters);
            }

            /// <summary>
            /// Generates a signature using the specified signatureType 
            /// </summary>		
            /// <param Name="url">The full url that needs to be signed including its non OAuth url parameters</param>
            /// <param Name="consumerKey">The consumer key</param>
            /// <param Name="consumerSecret">The consumer seceret</param>
            /// <param Name="token">The token, if available. If not available pass null or an empty string</param>
            /// <param Name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
            /// <param Name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
            /// <param Name="nonce"></param>
            /// <param Name="signatureType">The type of signature to use</param>
            /// <param Name="timeStamp"></param>
            /// <param Name="normalizedUrl"></param>
            /// <param Name="normalizedRequestParameters"></param>
            /// <returns>A base64 string of the hash value</returns>
            private string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, SignatureTypes signatureType, out string normalizedUrl, out string normalizedRequestParameters){
                normalizedUrl = null;
                normalizedRequestParameters = null;

                switch (signatureType){
                    case SignatureTypes.PLAINTEXT:
                        return UrlEncode(string.Format("{0}&{1}", consumerSecret, tokenSecret));
                    case SignatureTypes.HMACSHA1:
                        var signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, httpMethod, timeStamp, nonce, HMACSHA1SignatureType, out normalizedUrl, out normalizedRequestParameters);

                        var hmacsha1 = new HMACSHA1{
                            Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}",
                                UrlEncode(consumerSecret), string.IsNullOrEmpty(tokenSecret) ? "" : UrlEncode(tokenSecret)))
                        };

                        return GenerateSignatureUsingHash(signatureBase, hmacsha1);
                    case SignatureTypes.RSASHA1:
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentException("Unknown signature type", "signatureType");
                }
            }

            /// <summary>
            /// Generate the timestamp for the signature        
            /// </summary>
            /// <returns></returns>
            public string GenerateTimeStamp(){
                // Default implementation of UNIX time of the current UTC time
                var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds).ToString(CultureInfo.InvariantCulture);
            }

            /// <summary>
            /// Generate a nonce
            /// </summary>
            /// <returns></returns>
            public string GenerateNonce(){
                // Just a simple implementation of a random number between 123400 and 9999999
                return random.Next(123400, 9999999).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}