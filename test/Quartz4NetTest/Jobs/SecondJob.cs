using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz4NetTest.Jobs
{
    public class SecondJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            JobDetailImpl jobDetail = (JobDetailImpl)context.JobDetail;
            Console.WriteLine(string.Format("--------------SecondJob(10s):{0}----------------", context.FireTimeUtc));
            //Console.WriteLine("FirstJob->Name：" + jobDetail.FullName);
            //Console.WriteLine("FirstJob->Group：" + jobDetail.Group);
            //Console.WriteLine("Trigger类型->：" + context.Trigger.GetType());
            return null;
        }
    }
}
