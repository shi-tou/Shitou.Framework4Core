/*********************************************************************
*Copyright (c) 2018 石头小神 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：石头小神
*命名空间：Shitou.Framework.Autofac
*文件名：  IService
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-5 11:20:35
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-5 11:20:35
*修改人： Mibin
*描述：first create
*
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Shitou.Framework.Autofac
{
    /// <summary>
    /// 将所有需要进行依赖注入的接口都继承这个空接口
    /// 接口没有任何方法，不会对系统的业务逻辑造成污染
    /// </summary>
    public interface IService
    {
        
    }
}
