using life_is_feudal_your_own.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web;

namespace life_is_feudal_your_own.utils
{
    
    public static class LifeIsFeudalApi
    {
#if DEBUG

        private static string URL = "http://localhost:3000/";
#else
        private static string URL = "";
#endif

        public static string callApi(string method, string data)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL+method);
                request.ContentType = "application/json;";
                
                //request.PreAuthenticate = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    Console.WriteLine(reader.ReadToEnd());
                    return reader.ReadToEnd();
                }
                    
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string callApi(string method)
        {
            
            try
            {
                HttpWebRequest _request;
                HttpWebResponse _response;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate
                {
                    return true;
                });
                ServicePointManager.Expect100Continue = false;
                _request = (HttpWebRequest)WebRequest.Create(URL+method);
                _request.KeepAlive = false;
                _request.ProtocolVersion = HttpVersion.Version10;
                _request.ContentType = "application/json; charset=UTF-8";
                _request.Accept = "application/text";
                _request.Method = "GET";
                _response = (HttpWebResponse)_request.GetResponse();
                Stream objResponseStream = _response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(objResponseStream, encode);
                return readStream.ReadToEnd();

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string callApiPost(string method,string json)
        {

            try
            {
                HttpWebRequest _request;
                HttpWebResponse _response;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate
                {
                    return true;
                });
                ServicePointManager.Expect100Continue = false;
                _request = (HttpWebRequest)WebRequest.Create(URL + method);
                _request.KeepAlive = false;
                _request.ProtocolVersion = HttpVersion.Version10;
                _request.ContentType = "application/json; charset=UTF-8";
                _request.Accept = "application/text";
                _request.Method = "POST";
                using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                _response = (HttpWebResponse)_request.GetResponse();
                Stream objResponseStream = _response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(objResponseStream, encode);
                return readStream.ReadToEnd();

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string callApiPut(string method, string json)
        {

            try
            {
                HttpWebRequest _request;
                HttpWebResponse _response;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate
                {
                    return true;
                });
                ServicePointManager.Expect100Continue = false;
                _request = (HttpWebRequest)WebRequest.Create(URL + method);
                _request.KeepAlive = false;
                _request.ProtocolVersion = HttpVersion.Version10;
                _request.ContentType = "application/json; charset=UTF-8";
                _request.Accept = "application/text";
                _request.Method = "PUT";
                using (var streamWriter = new StreamWriter(_request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                _response = (HttpWebResponse)_request.GetResponse();
                Stream objResponseStream = _response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(objResponseStream, encode);
                return readStream.ReadToEnd();

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string GetAllItems()
        {
            return callApi("Items").ToString();
        }
        public static string SaveItem(string data)
        {
            return callApiPost($"Item/", data);
        }

        public static string GetAllItemQuality()
        {
            return callApi("ItemQualities/").ToString();
        }
        public static string GetItemQuality(long id)
        {
            return callApi($"ItemQuality/{id}");
        }
        
        public static string SaveItemQuality(string data)
        {
            return callApiPost($"ItemQuality/", data);
        }

        public static string SaveOrderForm(string data)
        {
            return callApiPost($"OrderForm", data);
        }
        public static string SaveOrderFormProduct(string data)
        {
            return callApiPost($"OrderFormProducts/", data);
        }
        public static string GetItemQualityTypes()
        {
            return callApi("ItemQualityTypes/");
        }

        public static string GetCategories() {
            return callApi("Categories");
        }
        public static string GetSubCategories()
        {
            return callApi("SubCategories");
        }
    }
}