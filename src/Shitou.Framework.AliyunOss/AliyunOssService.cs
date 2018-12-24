/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Caching.Redis
*文件名：  RedisOptions
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-6 11:37:11
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-6 11:37:11
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using Aliyun.OSS;
using Aliyun.OSS.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Shitou.Framework.AliyunOss
{
    public class AliyunOssService
    {
        /// <summary>
        /// 异步上传回调-委托
        /// </summary>
        /// <param name="result"></param>
        public delegate void UploadFileAsyncCallBack(PutObjectResult result);
        /// <summary>
        /// oss 客户端实例
        /// </summary>
        private OssClient _client;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// 分块大小(除了最后一块Part，其他Part的大小不能小于100KB，否则会导致在调用CompleteMultipartUpload接口的时候失败。 )
        /// 默认50M
        /// </summary>
        private readonly int partSize = 50 * 1024 * 1024;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public AliyunOssService(ILogger<AliyunOssService> logger)
        {
            _logger = logger;
            _client = new OssClient(AliyunOssConfig.Instance.Endpoint,
                AliyunOssConfig.Instance.AccessKeyId,
                AliyunOssConfig.Instance.AccessKeySecret);
            if (AliyunOssConfig.Instance.PartSize > 0)
            {
                partSize = AliyunOssConfig.Instance.PartSize * 1024 * 1024;
            }
        }
        public AliyunOssService()
        {
            _client = new OssClient(AliyunOssConfig.Instance.Endpoint,
                AliyunOssConfig.Instance.AccessKeyId,
                AliyunOssConfig.Instance.AccessKeySecret);
            if (AliyunOssConfig.Instance.PartSize > 0)
            {
                partSize = AliyunOssConfig.Instance.PartSize * 1024 * 1024;
            }
        }

        #region Bucket
        /// <summary>
        /// 新建存储空间()
        /// 存储空间（Bucket）是OSS上的命名空间，也是计费、权限控制、日志记录等高级功能的管理实体；
        /// </summary>
        /// <param name="bucketName"></param>
        public bool CreateBucket(string bucketName)
        {
            try
            {
                _client.CreateBucket(bucketName);
                _logger?.LogInformation("Created bucket name:{0} succeeded ", bucketName);
                return true;
            }
            catch (OssException ex)
            {
                _logger?.LogError("Failed with error info: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                                  ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                return false;
            }
        }
        /// <summary>
        /// 列出账户下所有的存储空间信息
        /// </summary>
        public List<Bucket> ListBuckets()
        {
            try
            {
                var buckets = _client.ListBuckets();
                _logger?.LogInformation("List bucket succeeded");
                return buckets.ToList();
            }
            catch (Exception ex)
            {
                _logger?.LogError("List bucket failed. {0}", ex.Message);
                return null;
            }
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传指定的本地文件
        /// </summary>
        /// <param name="bucketName">存储空间名</param>
        /// <param name="key">唯一标识key</param>
        /// <param name="fileToUpload">本地文件路径</param>
        /// <returns></returns>
        public bool UploadFile(string bucketName, string key, string fileToUpload)
        {
            try
            {
                _client.PutObject(bucketName, key, fileToUpload);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError("Put object failed. {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 异步上传指定的本地文件
        /// 使用异步上传时需要实现自己的回调处理函数
        /// </summary>
        /// <param name="bucketName">存储空间名</param>
        /// <param name="key">唯一标识key</param>
        /// <param name="fileToUpload">本地文件路径</param>
        /// <param name="asyncCallback"></param>
        /// <returns></returns>
        public void UploadFileAsync(string bucketName, string key, string fileToUpload, UploadFileAsyncCallBack asyncCallback)
        {
            AutoResetEvent _event = new AutoResetEvent(false);
            try
            {
                using (var fs = File.Open(fileToUpload, FileMode.Open))
                {
                    var metadata = new ObjectMetadata
                    {
                        CacheControl = "No-Cache",
                        ContentType = "text/html"
                    };
                    _client.BeginPutObject(bucketName, key, fs, metadata,
                        delegate (IAsyncResult ar)
                        {
                            var result = _client.EndPutObject(ar);
                            asyncCallback(result);
                            _event.Set();
                        },
                        new string('a', 8));
                    _event.WaitOne();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Put object failed. {0}", ex.Message);
            }
        }
        
        /// <summary>
        /// 追加内容到指定的OSS文件中。
        /// </summary>
        /// <param name="bucketName">指定的存储空间名称。</param>
        /// <param name="key">OSS上会被追加内容的文件名称。</param>
        /// <param name="fileToUpload">指定被追加的文件的路径。</param>
        public bool AppendObject(string bucketName, string key, string fileToUpload)
        {
            //第一次追加文件的时候，文件可能已经存在，先获取文件的当前长度，如果不存在，position为0
            long position = 0;
            try
            {
                var metadata = _client.GetObjectMetadata(bucketName, key);
                position = metadata.ContentLength;
            }
            catch (Exception) { }
            try
            {
                using (var fs = File.Open(fileToUpload, FileMode.Open))
                {
                    var request = new AppendObjectRequest(bucketName, key)
                    {
                        ObjectMetadata = new ObjectMetadata(),
                        Content = fs,
                        Position = position
                    };
                    var result = _client.AppendObject(request);
                    // 设置下次追加文件时的position位置
                    position = result.NextAppendPosition;
                    _logger?.LogInformation("Append object succeeded, next append position:{0}", position);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError("Append object failed, {0}", ex.Message);
                return false;
            }
        }

        #endregion

        #region 上传文件(大文件)-分块
        /// <summary>
        /// 使用分片方式上传文件
        /// </summary>
        /// <param name="bucketName">存储空间名</param>
        /// <param name="key">唯一标识key</param>
        /// <param name="fileToUpload">本地文件路径</param>
        /// <returns></returns>
        public bool UploadFileByMultipart(string bucketName, string key, string fileToUpload)
        {
            try
            {
                //初始化一个分片上传事件（UploadId-区分分片上传事件的唯一标识）
                var result = _client.InitiateMultipartUpload(new InitiateMultipartUploadRequest(bucketName, key));
                string uploadId = result.UploadId;
                //分块上传，得到ETags编号
                var partETags = UploadParts(bucketName, key, fileToUpload, uploadId);
                //完成分片（逐一验证每个数据Part的有效性）
                var completeMultipartUploadRequest = new CompleteMultipartUploadRequest(bucketName, key, uploadId);
                foreach (var partETag in partETags)
                {
                    completeMultipartUploadRequest.PartETags.Add(partETag);
                }
                _client.CompleteMultipartUpload(completeMultipartUploadRequest);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError("Put object by multipart failed. {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 分块上传
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name=""></param>
        private List<PartETag> UploadParts(string bucketName, string key, string fileToUpload, string uploadId)
        {
            var partETags = new List<PartETag>();
            var fi = new FileInfo(fileToUpload);
            var fileSize = fi.Length;
            //计算上传块数
            var partCount = CalPartCount(fi.Length, partSize);
            using (var fs = File.Open(fileToUpload, FileMode.Open))
            {
                for (var i = 0; i < partCount; i++)
                {
                    var skipBytes = (long)partSize * i;
                    //定位到本次上传块应该开始的位置
                    fs.Seek(skipBytes, 0);
                    //计算本次上传的片大小，最后一片为剩余的数据大小，其余片都是partSize大小。
                    var size = (partSize < fileSize - skipBytes) ? partSize : (fileSize - skipBytes);
                    //创建上传请求，执行上传
                    var request = new UploadPartRequest(bucketName, key, uploadId)
                    {
                        InputStream = fs,
                        PartSize = size,
                        PartNumber = i + 1
                    };
                    //调用UploadPart接口执行上传功能，返回结果中包含了这个数据片的ETag值
                    var result = _client.UploadPart(request);
                    //每次上传part之后，OSS的返回结果会包含一个 PartETag 对象，它是上传片的ETag与片编号（PartNumber）的组合，
                    //在后续完成分片上传的步骤中会用到它，因此我们需要将其保存起来。一般来讲我们将这些 PartETag 对象保存到List中。
                    partETags.Add(result.PartETag);
                    _logger?.LogError("finish {0}/{1}", partETags.Count, partCount);
                }
            }
            return partETags;
        }

        /// <summary>
        /// 取消分块上传
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="uploadId"></param>
        /// <returns></returns>
        public bool AbortMultipartUpload(string bucketName, string key, string uploadId)
        {
            try
            {
                var request = new AbortMultipartUploadRequest(bucketName, key, uploadId);
                _client.AbortMultipartUpload(request);
                return true;
            }
            catch(Exception ex)
            {
                _logger?.LogError("Put object by multipart failed. {0}", ex.Message);
                return false;
            }
            
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="bucketName">存储空间的名称</param>
        /// <param name="key">文件的名称</param>
        /// <param name="fileToDownload">文件保存的本地路径</param>
        public bool DownloadFile(string bucketName, string key, string fileToDownload)
        {
            try
            {
                //获取文件
                var obj = _client.GetObject(bucketName, key);
                //保存文件到fileToDownload
                using (var requestStream = obj.Content)
                {
                    byte[] buf = new byte[1024];
                    var fs = File.Open(fileToDownload, FileMode.OpenOrCreate);
                    var len = 0;
                    while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                    {
                        fs.Write(buf, 0, len);
                    }
                    fs.Close();
                }
                _logger?.LogInformation("Get object succeeded");
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError("List bucket failed. {0}", ex.Message);
                return false;
            }
        }

        #endregion

        #region 下载文件(大文件)-分块
        /// <summary>
        ///  分块下载文件（大文件）
        /// </summary>
        /// <param name="bucketName">存储空间的名称</param>
        /// <param name="key">文件的名称</param>
        /// <param name="fileToDownload">文件保存的本地路径</param>
        public bool DownloadFileByMultipart(string bucketName, string key, string fileToDownload)
        {
            try
            {
                using (var fileStream = new FileStream(fileToDownload, FileMode.OpenOrCreate))
                {
                    var bufferedStream = new BufferedStream(fileStream);
                    var objectMetadata = _client.GetObjectMetadata(bucketName, key);
                    var fileLength = objectMetadata.ContentLength;
                    //计算下载的块数
                    var partCount = CalPartCount(fileLength, partSize);

                    for (var i = 0; i < partCount; i++)
                    {
                        var startPos = partSize * i;
                        var endPos = partSize * i + (partSize < (fileLength - startPos) ? partSize : (fileLength - startPos)) - 1;
                        //循环-分块下载
                        DownloadParts(bufferedStream, startPos, endPos, bucketName, key);
                    }
                    bufferedStream.Flush();
                }
                _logger?.LogInformation("Get object by range succeeded");
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError("Get object by range failed. {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 分块下载
        /// </summary>
        /// <param name="bufferedStream"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="bucketName"></param>
        /// <param name="fileKey"></param>
        private void DownloadParts(BufferedStream bufferedStream, long startPos, long endPos, string bucketName, string fileKey)
        {
            Stream contentStream = null;
            try
            {
                //使用 GetObjectRequest 来读取文件
                var getObjectRequest = new GetObjectRequest(bucketName, fileKey);
                //读取文件中的startPos到endPos个字符，包括第startPos和第endPos个字符
                getObjectRequest.SetRange(startPos, endPos);
                var ossObject = _client.GetObject(getObjectRequest);
                //将读到的数据写到fileToDownload文件中去
                byte[] buffer = new byte[1024 * 1024];
                var bytesRead = 0;
                bufferedStream.Seek(startPos, SeekOrigin.Begin);
                contentStream = ossObject.Content;
                while ((bytesRead = contentStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    bufferedStream.Write(buffer, 0, bytesRead);
                }
            }
            finally
            {
                if (contentStream != null)
                {
                    contentStream.Dispose();
                }
            }
        }
        #endregion

        #region common method
        /// <summary>
        /// 计算上传/下载的块数
        /// </summary>
        /// <param name="fileLength"></param>
        /// <param name="partSize"></param>
        /// <returns></returns>
        private int CalPartCount(long fileLength, long partSize)
        {
            var partCount = (int)(fileLength / partSize);
            if (fileLength % partSize != 0)
            {
                partCount++;
            }
            return partCount;
        }
        #endregion
    }
}
