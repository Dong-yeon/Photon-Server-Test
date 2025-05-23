﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace Photon.Plugins.Common
{
    /// <summary>
    /// Codes returned as a result of process of queued HTTP request. 
    /// </summary>
    public abstract class HttpRequestQueueResult
    {
        /// <summary>
        /// The HTTP request was succesfully processed.
        /// </summary>
        /// <remarks>
        /// If the endpoint returns a successful HTTP status code. 
        /// i.e. 2xx codes.
        /// </remarks>
        public const byte Success = 0;

        /// <summary>
        /// The HTTP request timed out.
        /// </summary>
        /// <remarks>
        /// This happens if the endpoint does not return a response in a timely manner. 
        /// </remarks>
        public const byte RequestTimeout = 1;

        /// <summary>
        /// The HTTP request queue timed out.
        /// </summary>
        /// <remarks>
        /// A timer starts when a request is put into the HttpRequestQueue.
        /// A request can timeout if takes too much time inside the queue.
        /// </remarks>
        public const byte QueueTimeout = 2;

        /// <summary>
        /// If the application's respective HTTP queries' queue is in offline mode.
        /// </summary>
        /// <remarks>If this return code is received, no HttpRequest should be sent during 10 seconds which 
        /// is the time the HTTP queue takes to reconnect.
        /// </remarks>
        public const byte Offline = 3;

        /// <summary>
        /// The HTTP request queue is full.
        /// </summary>
        /// <remarks>
        /// This happens if the queue of HTTP queries has reached a certain threshold for the respective application.  
        /// </remarks>
        public const byte QueueFull = 4;

        /// <summary>
        /// An error has occurred while processing the HTTP request. 
        /// </summary>
        /// <remarks>
        /// If the request's URL couldn't be parsed or the hostname couldn't be resolved or
        /// the web service is unreachable. Also this may happen if the endpoint returns an 
        /// error HTTP status code. e.g. 400 (BAD REQUEST)
        /// </remarks>
        public const byte Error = 5;

        public static string ToString(int value)
        {
            switch (value)
            {
                case 0:
                    return "Success";
                case 1:
                    return "RequestTimeout";
                case 2:
                    return "QueueTimeout";
                case 3:
                    return "Offline";
                case 4:
                    return "QueueFull";
                case 5:
                    return "Error";
                default:
                    return string.Format("HttpRequestQueueResult unknown value:{0}", value);
            }
        }
    }

    /// <summary>
    /// HTTP request to be sent.
    /// </summary>
    public class HttpRequest
    {
        #region Constants and Fields

        /// <summary>
        /// Accept HTTP header. Specifies what media types are expected from response.
        /// </summary>
        public string Accept;

        /// <summary>
        /// Method that should be called when the HTTP response is received.
        /// </summary>
        public HttpRequestCallback Callback;

        /// <summary>
        /// ContentType HTTP header. Sets the type of the request's data.
        /// </summary>
        public string ContentType;

        /// <summary>
        /// HTTP request body data.
        /// </summary>
        public MemoryStream DataStream;

        /// <summary>
        /// Default predefined HTTP headers.
        /// </summary>
        public IDictionary<HttpRequestHeader, string> Headers;

        /// <summary>
        /// Key/Value strings: custom HTTP headers.
        /// </summary>
        public IDictionary<string, string> CustomHeaders;

        /// <summary>
        /// HTTP verb/method: e.g. GET, POST, PUT, DELETE, etc.
        /// </summary>
        public string Method;

        /// <summary>
        /// HTTP query absolute URL path.
        /// </summary>
        public string Url;

        public object UserState;

        /// <summary>
        /// Indicates whether the process of plugins fiber the should be blocked or not.
        /// </summary>
        public bool Async;

        #endregion
    }

    /// <summary>
    /// Delegate of HTTP request callback.
    /// </summary>
    /// <param name="response">The HTTP response.</param>
    /// <param name="userState"></param>
    public delegate void HttpRequestCallback(IHttpResponse response, object userState);

    /// <summary>
    /// Base interface of HTTP response.
    /// </summary>
    public interface IHttpResponse
    {
        #region Properties

        /// <summary>
        /// The corresponding HTTP request.
        /// </summary>
        HttpRequest Request { get; }

        /// <summary>
        /// The HTTP code returned from external web service.
        /// </summary>
        int HttpCode { get; }

        /// <summary>
        /// The human readable form of the returned <see cref="HttpCode"/>.
        /// </summary>
        string Reason { get; }

        /// <summary>
        /// HTTP response data returned from external web service as byte[].
        /// </summary>
        byte[] ResponseData { get; }

        /// <summary>
        /// HTTP response data returned from external web service as string.
        /// </summary>
        string ResponseText { get; }

        /// <summary>
        /// The <see cref="HttpRequestQueueResult"/> returned from Photon servers.
        /// </summary>
        byte Status { get; }

        /// <summary>
        /// Reason of failure.
        /// </summary>
        int WebStatus { get; }

        /// <summary>
        /// call info of plugin call which initiated http request
        /// </summary>
        ICallInfo CallInfo { get; }

        /// <summary>
        /// gets headers for response
        /// </summary>
        NameValueCollection Headers { get; }
        #endregion
    }

}