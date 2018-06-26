using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Services.Utilities
{
    public static class people
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        //public static void Reques()
        //{
        //    //请求路径
        //    string url = "http://localhost:27221/api/Charging/SaveData";

        //    //定义request并设置request的路径
        //    WebRequest request = WebRequest.Create(url);
        //    request.Method = "post";

        //    //初始化request参数
        //    string postData = "{ ID: \"1\", NAME: \"Jim\", CREATETIME: \"1988-09-11\" }";

        //    //设置参数的编码格式，解决中文乱码
        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //    //设置request的MIME类型及内容长度
        //    request.ContentType = "application/json";
        //    request.ContentLength = byteArray.Length;

        //    //打开request字符流
        //    Stream dataStream = request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();

        //    //定义response为前面的request响应
        //    WebResponse response = request.GetResponse();

        //    //获取相应的状态代码
        //    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

        //    //定义response字符流
        //    dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();//读取所有
        //    Console.WriteLine(responseFromServer);
        //}
    }
}
