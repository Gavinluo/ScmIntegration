using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCMApiDemo
{
   public class ApiHelper
    {

      
        public string LoginByKey(string key)
        {
            var data = GetRequestData("https://app.360scm.com/SCM.TMS7.WebApi/Oauth/GetToken?apikey=" + key);
            var jsonobj = JsonConvert.DeserializeObject<dynamic>(data);
            if (jsonobj.flag=="true")
            {
                Console.WriteLine("调用成功:" + jsonobj.msg);
                return jsonobj.token;
            }
            else
            {
                Console.WriteLine("调用错误:"+jsonobj.msg);
                return "";
            }
        }

        
        public void SaveReceipt(string receiptId,string token)
        {
            var requestData = "{" +
                    "\"receiptEditDTO\": " +
                    "{" +
                        "\"WH_ID\": \"wh1\"," +
                        "\"RECEIPT_ID\": \"" + receiptId + "\"," +
                        "\"OWNER_ID\": \"zhongliang\"," +
                        "\"RECEIPT_TYPE_SC\": \"PO\"" +
                    "}," +
                    "\"whgid\": \"wh1\"," +
                    "\"token\": \"" + token + "\"" +
                "}";
            var data = PostRequestData("https://wms.360scm.com/SCM.WMS7.WebApi/WMS/SaveReceipt", requestData);
            OutPutData(data);
        }
        
        /// <summary>
        /// 删除入库订单
        /// </summary>
        /// <param name="receiptId"></param>
        /// <param name="token"></param>
        public void DeleteReceipt(string receiptId, string token)
        {
            var requestData = "{" +
                    "\"receiptid\": \"" + receiptId + "\"," +
                    "\"whgid\": \"wh1\"," +
                    "\"token\": \"" + token + "\"" +
                "}";
            var data = PostRequestData("https://wms.360scm.com/SCM.WMS7.WebApi/WMS/DeleteReceipt", requestData);
            OutPutData(data);
        }
        /// <summary>
        /// 修改入库订单 , 把自定义字段 R_UDF1 修改为 Update
        /// </summary>
        /// <param name="receiptId"></param>
        /// <param name="token"></param>
        public void UpdateReceipt(string receiptId,string token)
        {
            var requestData = "{" +
                    "\"receiptEditDTO\": " +
                    "{" +
                        "\"RECEIPT_ID\": \"" + receiptId + "\"," +
                        "\"R_UDF1\": \"Update\"" +
                    "}," +
                    "\"whgid\": \"wh1\"," +
                    "\"token\": \"" + token + "\"" +
                "}";
            var data = PostRequestData("https://wms.360scm.com/SCM.WMS7.WebApi/WMS/UpdateReceipt", requestData);
            OutPutData(data);
        }

        /// <summary>
        /// Restharp 进行Get 请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetRequestData(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("---------------------------");
            Console.WriteLine("请求地址: ");
            Console.WriteLine(url);
            Console.WriteLine("请求参数: ");
            Console.WriteLine("");
            Console.WriteLine("返回参数: ");
            Console.WriteLine(response.Content);
            Console.WriteLine("---------------------------");
            return response.Content;
        }
        /// <summary>
        /// 用RestSharp 进行 Post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private string PostRequestData(string url, string postData)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", postData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("---------------------------");
            Console.WriteLine("请求地址: ");
            Console.WriteLine(url);
            Console.WriteLine("请求参数: ");
            Console.WriteLine(postData);
            Console.WriteLine("返回参数: ");
            Console.WriteLine(response.Content);
            Console.WriteLine("---------------------------");
            return response.Content;
        }
        /// <summary>
        /// 输出到Console
        /// </summary>
        /// <param name="data"></param>
        private static void OutPutData(string data)
        {
            var jsonobj = JsonConvert.DeserializeObject<dynamic>(data);
            if (jsonobj.flag == "true")
            {
                Console.WriteLine("调用成功:" + jsonobj.msg);
            }
            else
            {
                Console.WriteLine("调用错误:" + jsonobj.msg);
            }
        }
    }
}
