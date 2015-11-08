using System;
using System.Net;
using System.Text;
using System.IO;

namespace Cachou.WebAPI
{
    public static class WebAPI
    {
        private enum Method
        {
            GET,
            POST
        }
        public static string Get()
        {
            return CallApi(Method.GET, "http://cachouserver.mybluemix.net/api/test");
        }

        public static string Post()
        {
            return CallApi(Method.POST, "http://cachouserver.mybluemix.net/api/test");
        }

        public static void SendChildAction(string date, string action)
        {
            CallApi(Method.POST, "http://cachouserver.mybluemix.net/api/db/history", "{\"date\":\"" + date + "\", \"action\":\"" + action + "\"}");
        }

        private static string CallApi(Method method, string url, string data = null)
        {
            try
            {
                // Create a request for the URL. 
                WebRequest request = WebRequest.Create(url);

                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;

                if (Method.GET == method)
                {
                    // Get the response.
                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.
                    Stream dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    var result =  responseFromServer;
                    // Clean up the streams and the response.
                    reader.Close();
                    response.Close();

                    return result;
                }
                else if (method == Method.POST)
                {
                    request.Method = "POST";
                    // Create POST data and convert it to a byte array.
                    string postData = data ?? string.Empty;
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/JSON";
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;

                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();

                    // Get the response.
                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    var result = responseFromServer;
                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    return result;
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    Console.WriteLine(resp.StatusCode == HttpStatusCode.NotFound ? "404 not found" : "Other Web Error 1");
                }
                else
                {
                    Console.WriteLine("Other Web Error 2 (No internet)");
                }
            }
            return null;
        }
    }
}