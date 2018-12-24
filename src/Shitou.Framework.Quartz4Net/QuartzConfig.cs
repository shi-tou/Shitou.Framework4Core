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
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Quartz4Net
{
    /// <summary>
    /// Quartz配置
    /// </summary>
    public class QuartzConfig
    {
        public static QuartzConfig Instance;

        public List<JobItem> Jobs { get; set; }
        public List<TriggerItem> Triggers { get; set; }
    }

    /// <summary>
    /// 作业配置项
    /// </summary>
    public class JobItem
    {
        /// <summary>
        /// 作业名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 作业分组
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 作业对应类型（如MyQuartz.Jobs.FirstJob）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 触发器
        /// </summary>
        public string Trigger { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }

    /// <summary>
    /// 触发器配置项
    /// </summary>
    public class TriggerItem
    {
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 触发器分组
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }
        /// <summary>
        /// 开始执行时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束执行时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
