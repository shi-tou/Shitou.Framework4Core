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

using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Shitou.Framework.Quartz4Net
{
    public class QuartzService
    {
        /// <summary>
        /// 作业调度池
        /// </summary>
        public static IScheduler Scheduler;
        static QuartzService()
        {
            //用调度工厂创建作业调度池
            Scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        }

        #region 作业调度管理
        /// <summary>
        /// 开始执行
        /// </summary>
        public static void Start()
        {
            Scheduler.Start();
        }
        /// <summary>
        /// 停止执行
        /// </summary>
        public static void Pause()
        {
            Scheduler.PauseAll();
        }
        /// <summary>
        /// 恢复执行
        /// </summary>
        public static void Resume()
        {
            Scheduler.ResumeAll();
        }
        /// <summary>
        /// 线束执行
        /// </summary>
        public static void Stop()
        {
            Scheduler.Shutdown();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            try
            {
                if (QuartzConfig.Instance != null)
                {
                    Type[] types = GetJobClass();
                    foreach (JobItem item in QuartzConfig.Instance.Jobs)
                    {
                        if (item.Enabled)
                        {
                            IJobDetail jobDetail = CreateJob(item, types);
                            ICronTrigger trigger = CreateCronTrigger(QuartzConfig.Instance.Triggers.Where(a => a.Name == item.Trigger).FirstOrDefault());
                            Scheduler.ScheduleJob(jobDetail, trigger);
                        }
                    }
                    Start();
                }
                else
                {
                    throw new Exception("找不到配置信息！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建Job
        /// </summary>
        /// <param name="jobItem"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IJobDetail CreateJob(JobItem jobItem, Type[] types)
        {
            return new JobDetailImpl(jobItem.Name, types.Where(t => t.FullName == jobItem.Type).FirstOrDefault());
        }

        /// <summary>
        /// 创建CronTrigger
        /// </summary>
        /// <returns></returns>
        public static ICronTrigger CreateCronTrigger(TriggerItem triggerItem)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create();
            if (!string.IsNullOrEmpty(triggerItem.StartTime))
            {
                triggerBuilder = triggerBuilder.StartAt(Convert.ToDateTime(triggerItem.StartTime));
            }
            if (!string.IsNullOrEmpty(triggerItem.EndTime))
            {
                triggerBuilder = triggerBuilder.EndAt(Convert.ToDateTime(triggerItem.EndTime));
            }
            triggerBuilder = triggerBuilder.WithCronSchedule(triggerItem.CronExpression);
            ICronTrigger trigger = (ICronTrigger)triggerBuilder
                .WithCronSchedule(triggerItem.CronExpression)
                .WithIdentity(triggerItem.Name, triggerItem.Group)
                .Build();
            return trigger;
        }


        /// <summary>  
        /// 反射获取程序集中的继承了IJob接口的类
        /// </summary>  
        /// <param name="assemblyName">程序集</param>  
        private static Type[] GetJobClass()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IJob))))
                .ToArray();
            return types;
        }
    }
}
