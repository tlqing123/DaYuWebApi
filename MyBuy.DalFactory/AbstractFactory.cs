using MyBuy.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.DalFactory
{
    /*
    * 工厂类，负责数据类的生成
    * author:xiaoshan
    * date:2017/5/7
    */
    public class AbstractFactory
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];

        /// <summary>
        /// 实现UserInfoDal的创建
        /// </summary>
        /// <returns>UserInfoDal</returns>
        public static IUserInfoDal CreateUserInfoDal()
        {
            string fullClassName = NameSpace + ".UserInfoDal";
            return CreateInstance(fullClassName) as IUserInfoDal;
        }

        /// <summary>
        /// 通过反射找到指定的命名空间
        /// </summary>
        /// <param name="fullClassName">完成的类名</param>
        /// <returns>命名空间</returns>
        private static Object CreateInstance(string fullClassName)
        {
            var assemblyPath = Assembly.Load(AssemblyPath);
            return assemblyPath.CreateInstance(fullClassName);
        }
    }
}
