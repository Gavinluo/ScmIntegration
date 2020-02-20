using Aliyun.Serverless.Core;
using Aliyun.Serverless.Core.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApiHostDemo
{
   public class DemoRequestHandle : FcHttpEntrypoint
    {
        /// <summary>
        /// 阿里系统入口函数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="fcContext"></param>
        /// <returns></returns>
        public override async Task<HttpResponse> HandleRequest(HttpRequest request, HttpResponse response, IFcContext fcContext)
        {
            string msg = "";
            try
            {
                StreamReader sr = new StreamReader(request.Body);
                string requestBody = sr.ReadToEnd();
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Cache-Control", "no-cache");
                msg = "{ \"Flag\" : \"true\", \"Msg\" : \"Reviced Data :"+ requestBody + "\" }";
            }
            catch (Exception e)
            {
                msg = e.Message + e.StackTrace;
            }
            finally
            {
                response.StatusCode = 200;
                response.Headers["Content-Type"] = "application/json;charset=utf-8";
                await response.WriteAsync(msg);
            }


            return response;
        }

        /// <summary>
        /// 不用实现
        /// </summary>
        /// <param name="builder"></param>
        protected override void Init(IWebHostBuilder builder)
        {

        }
    }
}
