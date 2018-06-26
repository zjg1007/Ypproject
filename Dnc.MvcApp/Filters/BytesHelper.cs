using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.MvcApp.Filters
{
    public class BytesHelper
    {
        #region 字节


        /// <summary>
        /// 设置某一位的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index">要设置的位， 值从低到高为 0-31</param>
        /// <param name="flag">要设置的值 true / false</param>
        /// <returns></returns>
     public void set_bit(ref int t, int offset, bool isone)
        {
            if (isone) t |= (1 << offset);
            else t &= (~(1 << offset));
        }   
   
        /// <summary>
        ///  
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte GetSignsSet(byte SignsSet, int index)
        {
            return (byte)((SignsSet >> index) & 1);
        }
        /// <summary>
        /// 实例化指定大小字节数组
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        //internal static byte[] AllocateArray(int size)
        //{
        //    byte[] buffer;
        //    try
        //    {
        //        buffer = new byte[size];
        //    }
        //    catch (OutOfMemoryException exception)
        //    {
        //        throw new InsufficientMemoryException("BufferAllocationFailed", exception);
        //    }
        //    return buffer;
        //}
        #endregion

        #region 字符串字节转换
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                { returnStr += bytes[i].ToString("X2"); }
            }
            return returnStr;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexString">16进制字符串</param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 16进制字符串转字节数组补0
        /// </summary>
        /// <param name="hexString">16进制字符串</param>
        /// <returns></returns>
        public static byte[] HexStringToBytesLeft0(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString = "0" + hexString;
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        #endregion

        #region 异或校验
        /// <summary>
        /// 右移取值
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte Sign(byte b, int index)
        {
            return (byte)((b >> index) & 1);
        }

        /// <summary>
        /// 右移取值
        /// </summary>
        /// <param name="i"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte Sign(int i, int index)
        {
            return (byte)((i >> index) & 1);
        }

        public static byte SignComputer(byte[] buff, int offset, int endoffset)
        {
            byte p = buff[offset];
            for (int i = offset + 1; i < endoffset; i++)
                p = (byte)(p ^ buff[i]);

            return p;
        }

        /// <summary>
        /// 异或校验
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <returns></returns>
        public static bool Sign(byte[] buffer)
        {
            byte p = buffer[0];
            for (int i = 1; i < buffer.Length; i++)
                p = (byte)(p ^ buffer[i]);
            byte recSign = buffer[buffer.Length - 2];

            return p == recSign;
        }

        /// <summary>
        /// 异或校验
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static bool Sign(byte[] buffer, int offset)
        {
            byte p = buffer[0];
            for (int i = 1; i < offset; i++)
                p = (byte)(p ^ buffer[i]);
            byte recSign = buffer[offset];

            return p == recSign;
        }


        /// <summary>
        /// 异或校验
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static bool QySign(byte[] buffer, int offset)
        {
            byte p = buffer[0];
            for (int i = 1; i < offset; i++)
                p = (byte)(p ^ buffer[i]);
            p = (byte)(p ^ 0x7E);
            byte recSign = buffer[offset];

            return p == recSign;
        }

        /// <summary>
        /// 异或校验
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="start">起始位</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static byte Sign(byte[] buffer, int start, int offset)
        {
            byte p = buffer[start];
            for (int i = start + 1; i < offset; i++)
                p = (byte)(p ^ buffer[i]);

            return p;
        }
        #endregion
    }
}
