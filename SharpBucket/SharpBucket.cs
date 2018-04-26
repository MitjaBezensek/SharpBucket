using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using Serilog;
using SharpBucket.Authentication;
using SharpBucket.Utility;

namespace SharpBucket
{
    /// <summary>
    /// A client for the BitBucket API. It supports V1 and V2 of the API.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/Use+the+Bitbucket+REST+APIs
    /// </summary>
    public class SharpBucket : ISharpBucket
    {
        private Authenticate authenticator;

        /// <summary>
        /// The base URL exposing the BitBucket API.
        /// </summary>
        protected string _baseUrl;

        /// <inheritdoc />
        public void BasicAuthentication(string username, string password)
        {
            authenticator = new BasicAuthentication(username, password, _baseUrl);
        }

        /// <inheritdoc />
        public void OAuth2LeggedAuthentication(string consumerKey, string consumerSecretKey)
        {
            authenticator = new OAuthentication2Legged(consumerKey, consumerSecretKey, _baseUrl);
        }

        /// <inheritdoc />
        public OAuthentication3Legged OAuth3LeggedAuthentication(
            string consumerKey,
            string consumerSecretKey,
            string callback = "oob")
        {
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, callback, _baseUrl);
            return (OAuthentication3Legged)authenticator;
        }
        
        /// <inheritdoc />
        public OAuthentication3Legged OAuth3LeggedAuthentication(
            string consumerKey,
            string consumerSecretKey,
            string oauthToken,
            string oauthTokenSecret)
        {
            authenticator = new OAuthentication3Legged(
                consumerKey,
                consumerSecretKey,
                oauthToken,
                oauthTokenSecret,
                _baseUrl);
            return (OAuthentication3Legged)authenticator;
        }

        /// <inheritdoc />
        public OAuthentication2 OAuthentication2(string consumerKey, string consumerSecretKey)
        {
            authenticator = new OAuthentication2(consumerKey, consumerSecretKey, _baseUrl);
            ((OAuthentication2)authenticator).GetToken();
            return (OAuthentication2)authenticator;
        }

        private T Send<T>(T body, Method method, string overrideUrl = null, IDictionary<string, object> requestParameters = null)
        {
            var relativeUrl = overrideUrl;
            T response;
            try
            {
                response = authenticator.GetResponse(relativeUrl, method, body, requestParameters);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                response = default(T);
            }
            return response;
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="body"></param>
        /// <param name="method"></param>
        /// <param name="overrideUrl"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        private T Send<T>(ILogger logger, T body, Method method, string overrideUrl = null, IDictionary<string, object> requestParameters = null)
        {
            var relativeUrl = overrideUrl;
            T response;
            try
            {
                response = authenticator.GetResponse(logger, relativeUrl, method, body, requestParameters);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                response = default(T);
            }
            return response;
        }

        public T Get<T>(T body, string overrideUrl, object requestParameters = null)
        {
            //Convert to dictionary to avoid refactoring the Send method.
            var parameterDictionary = requestParameters.ToDictionary();
            return Send(body, Method.GET, overrideUrl, parameterDictionary);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="body"></param>
        /// <param name="overrideUrl"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public T Get<T>(ILogger logger, T body, string overrideUrl, object requestParameters = null)
        {
            //Convert to dictionary to avoid refactoring the Send method.
            var parameterDictionary = requestParameters.ToDictionary();
            return Send(logger, body, Method.GET, overrideUrl, parameterDictionary);
        }

        public T Post<T>(T body, string overrideUrl)
        {
            return Send(body, Method.POST, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="body"></param>
        /// <param name="overrideUrl"></param>
        /// <returns></returns>
        public T Post<T>(ILogger logger, T body, string overrideUrl)
        {
            return Send(logger, body, Method.POST, overrideUrl);
        }

        public T Put<T>(T body, string overrideUrl)
        {
            return Send(body, Method.PUT, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="body"></param>
        /// <param name="overrideUrl"></param>
        /// <returns></returns>
        public T Put<T>(ILogger logger, T body, string overrideUrl)
        {
            return Send(logger, body, Method.PUT, overrideUrl);
        }

        public T Delete<T>(T body, string overrideUrl)
        {
            return Send(body, Method.DELETE, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="body"></param>
        /// <param name="overrideUrl"></param>
        /// <returns></returns>
        public T Delete<T>(ILogger logger, T body, string overrideUrl)
        {
            return Send(logger, body, Method.DELETE, overrideUrl);
        }
    }
}