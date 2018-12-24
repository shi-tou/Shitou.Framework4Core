using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Shitou.Framework.Demo.Utils
{
    public class LogUtils
    {
        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void Info(string title, string content)
        {
            SaveLog(title, content, "info");
        }
        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void Debug(string title, string content)
        {
            SaveLog(title, content, "debug");
        }

        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void Error(string title, string content)
        {
            SaveLog(title, content, "error");
        }
        /// <summary>
        /// 记录文件日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public static void Error(string title, Exception ex)
        {
            SaveLog(title, ex);
        }

        private static void SaveLog(string title,string content,string fileName)
        {
            try
            {
                DateTime time = DateTime.Now;
                string dirPath = string.Format("{0}Log/{1}/{2}/{3}", AppDomain.CurrentDomain.BaseDirectory, time.Year, time.Month, time.Day);
                string filePath = string.Format("{0}/{1}.log", dirPath, fileName);
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                if (!File.Exists(filePath))
                {
                    FileStream fsCreate = new FileStream(filePath, FileMode.Create);
                    fsCreate.Close();
                    fsCreate.Dispose();
                }
                FileStream fsWrite = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter streamWrite = new StreamWriter(fsWrite);
                streamWrite.WriteLine(string.Format("{0}{1}[{2}]{3}", "--------------------------------", title, time.ToString("HH:mm"), "--------------------------------"));
                streamWrite.Write(content);
                streamWrite.WriteLine("\r\n");
                streamWrite.WriteLine(" ");
                streamWrite.Flush();
                streamWrite.Close();
            }
            catch { }
        }
        private static void SaveLog(string title, Exception ex)
        {
            string content = "错误消息：" + ex.Message +
                             "\r\n发生时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                             "\r\n错误源： " + ex.Source +
                             "\r\n引发异常的方法： " + ex.TargetSite +
                             "\r\n堆栈信息： " + ex.StackTrace +
                             "\r\n" + (ex.InnerException != null ? ex.InnerException.Message : "");
            SaveLog(title, content, "error");
        }
    }
}