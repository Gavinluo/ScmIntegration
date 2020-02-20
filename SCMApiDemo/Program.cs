using Newtonsoft.Json;
using RestSharp;
using System;

namespace SCMApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new ApiHelper();
            //登录换取Token , 这里需要设置你自己的Apikey
            var token = helper.LoginByKey("");
            //调用保存入库单
            var receiptId = "GavinTest" + DateTime.Now.ToString("yyyy-mm-dd");
            helper.SaveReceipt(receiptId, token);
            //修改入库单
            helper.UpdateReceipt(receiptId, token);
            //删除入库单
            helper.DeleteReceipt(receiptId, token);
        }

        
    }
}
