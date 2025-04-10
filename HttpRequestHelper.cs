using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json.Nodes;
using HttpRequestService.Models;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace HttpRequestService
{
    public class HttpRequestHelper
    {
        HttpClient client;
        Dictionary<string, string> defaultHeaders;
        Dictionary<string, string> defaultParameters;
        Dictionary<string, string> defaultContentHeaders;
        public HttpRequestHelper(string baseUri) {
            client = new HttpClient(new SocketsHttpHandler());
            client.BaseAddress = new Uri(baseUri);

            //Default values
            defaultHeaders = new Dictionary<string, string>() { {"accept","*/*"} };
            defaultParameters = new Dictionary<string, string>();
            defaultContentHeaders = new Dictionary<string, string>() { { "Content-Type", "application/json" } };
        }

        public Dictionary<string, string> DefaultHeaders
        {
            get { return defaultHeaders; }
            set { defaultHeaders = value; }
        }

        public Dictionary<string, string> DefaultParameters
        {
            get { return defaultParameters; }
            set { defaultParameters = value; }
        }

        public Dictionary<string, string> DefaultContentHeaders
        {
            get { return defaultContentHeaders; }
            set { defaultContentHeaders = value; }
        }

        public void NewDefaultBaseAddress(string newBase)
        {
            client.BaseAddress = new Uri(newBase);
        }
        public List<T> GET<T>(string path, Dictionary<string,string> Query = null, Dictionary<string,string> Headers = null)
        {
            
            //Adding the queries to the uri            
            path += "?";

            foreach (var item in Query is null ? defaultParameters : Query)
            {
                path += item.Key.Trim() + "=" + item.Value.Trim() + "&";
            }
            //If no queries were added, then we should not take the last character away
            //If query is null, then the defaultParameters were added
            if((Query != null && Query.Count > 0) || Query is null) 
                path = path.Substring(0, path.Length - 1);

            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator(HttpMethod.Get, uri, Headers is null ? defaultHeaders : Headers);

            HttpResponseMessage message = client.SendAsync(request).Result;

            message.EnsureSuccessStatusCode();

            List<T> result = MessageDeserializer<T>(message);

            return result;
           
        }
        
        public List<T> PUT<T,G>(string path, G put, Dictionary<string, string> Headers = null, Dictionary<string,string> ContentHeader = null)
        {
            string ser = JsonSerializer.Serialize(put);

            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator<G>(HttpMethod.Put, uri, put, Headers is null ? defaultHeaders : Headers, ContentHeader is null ? defaultContentHeaders : ContentHeader);

            HttpResponseMessage message = client.SendAsync(request).Result;

            message.EnsureSuccessStatusCode();

            List<T> result = MessageDeserializer<T>(message);

            return result;

        }

        public bool DELETE(string path, Dictionary<string, string> Headers = null)
        {
            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator(HttpMethod.Delete, uri, Headers is null ? defaultHeaders : Headers);

            HttpResponseMessage message = client.SendAsync(request).Result;
            
            message.EnsureSuccessStatusCode();

            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Something went wrong with deleting data! Status code: " + message.StatusCode);
                return false;
            }
        }

        public bool DELETE<T>(string path, T deletebody, Dictionary<string, string> Headers = null, Dictionary<string, string> ContentHeaders = null)
        {
            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator(HttpMethod.Delete, uri, deletebody, Headers is null ? defaultHeaders : Headers, ContentHeaders is null ? defaultContentHeaders : ContentHeaders);

            HttpResponseMessage message = client.SendAsync(request).Result;

            message.EnsureSuccessStatusCode();
            
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Something went wrong with deleting data! Status code: " + message.StatusCode);
                return false;
            }
        }

        public List<T> PATCH<T, G>(string path, G patch, Dictionary<string, string> Headers = null, Dictionary<string, string> ContentHeaders = null)
        {

            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator<G>(HttpMethod.Patch,uri, patch, Headers is null ? defaultHeaders : Headers, ContentHeaders is null ? defaultContentHeaders : ContentHeaders);

            HttpResponseMessage message = client.SendAsync(request).Result;

            message.EnsureSuccessStatusCode();

            List<T> result = MessageDeserializer<T>(message);

            return result;
        }

        public List<T> POST<T,G>(string path, G post, Dictionary<string, string> Headers = null, Dictionary<string, string> ContentHeaders = null)
        {
            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator(HttpMethod.Post, uri, post, Headers is null ? defaultHeaders : Headers, ContentHeaders is null ? defaultContentHeaders : ContentHeaders);

            HttpResponseMessage message = client.SendAsync(request).Result;

            message.EnsureSuccessStatusCode();

            List<T> result = MessageDeserializer<T>(message);

            return result;
        }

        public bool POST(string path, Dictionary<string,string> Headers = null)
        {
            Uri uri = new Uri(client.BaseAddress + path);

            HttpRequestMessage request = RequestCreator(HttpMethod.Post, uri, Headers is null ? defaultHeaders : Headers);

            HttpResponseMessage message = client.SendAsync(request).Result;

            message.EnsureSuccessStatusCode();
            
            return true;
        }

        private static List<T> MessageDeserializer<T>(HttpResponseMessage message)
        {
            try
            {
                if (!message.IsSuccessStatusCode)
                {
                    Console.WriteLine("Something went wrong with getting the data! Status code: " + message.StatusCode);
                    return new List<T>();
                }

                string data = "";

                data = message.Content.ReadAsStringAsync().Result;

                var jsonObject = JsonNode.Parse(data);

                List<T> objList = new List<T>();

                if (data is not null)
                {
                    if (data.Contains("["))
                    {
                        objList = jsonObject.Deserialize<List<T>>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        objList.Add(JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
                    }
                }

                if (objList is null)
                {
                    Console.WriteLine("Something went wrong with the deserialization! (Null return)");
                    return new List<T>();
                }

                return objList;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Problem with deserializing the data! Error: " + ex.Message);
                return new List<T>();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Null Reference!" + ex.Message);
                return new List<T>();
            }
            catch (Exception ex) {
                Console.WriteLine("BIG PROBLEM! Error: " + ex.Message);
                return new List<T>();
            }
        }
        private static HttpRequestMessage RequestCreator(HttpMethod method, Uri uri, Dictionary<string,string> Headers)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, uri);

            request.Headers.Clear();

            foreach(var item in Headers)
            {
                request.Headers.Add(item.Key,item.Value);
            }

            Console.WriteLine(request.ToString());

            return request;
        }
        private static HttpRequestMessage RequestCreator<T>(HttpMethod method, Uri uri, T ContentData, Dictionary<string, string> Headers, Dictionary<string,string> ContentHeaders)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, uri);

            request.Headers.Clear();

            foreach (var item in Headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            string cont = JsonSerializer.Serialize(ContentData);

            request.Content = new StringContent(cont);

            request.Content.Headers.Clear();

            foreach (var item in ContentHeaders)
            {
                request.Content.Headers.Add(item.Key, item.Value);
            }

            Console.WriteLine(request.ToString());

            return request;
        }



        ~HttpRequestHelper()
        {
            client.Dispose();
        }   
    }
}


