using Jose;
using MyBuy.BLL;
using MyBuy.Common;
using MyBuy.IBLL;
using MyBuy.Model;
using Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace MyBuy.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public IUserInfoServices userInfoService = new UserInfoServices();
        public IHttpActionResult getusers()
        {
             List<tbUser_Info> list=userInfoService.LoadEntityes(u=>true).ToList<tbUser_Info>();
             return Json<List<tbUser_Info>>(list);
        }
        /// <summary>
        /// 登录生成token
        /// </summary>
        /// <param name="name">邮箱</param>
        /// <param name="pwd">密码</param>
        /// <returns>Json</returns>
        [HttpPost] 
        public IHttpActionResult GetJWT(string name, string pwd)
        {
            string msg = "";
            int error = 0;
            string token = string.Empty;
            tbUser_Info userInfo = userInfoService.LoadEntityes(u => u.User_Emailstr == name && u.User_Pwdstr == pwd).FirstOrDefault();
            if (userInfo != null)
            {
                token = TokenHelper.CreatToken(userInfo.User_Emailstr);
                msg = "登录成功";
                error = 1;
            }
            else
            {
                msg = "";
            }
            return Json<dynamic>(new { msg = msg, error = error, token = token });
        }

       
    }
}