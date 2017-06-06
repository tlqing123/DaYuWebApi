using Jose;
using MyBuy.BLL;
using MyBuy.Common;
using MyBuy.IBLL;
using MyBuy.Model;
using Newtonsoft.Json.Linq;
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
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        /// <summary>
        /// 用户信息数据操作类实例创建
        /// </summary>
        private static IUserInfoServices userInfoService = new UserInfoServices();

        /// <summary>
        /// 用户信息（视图）数据操作类实例创建
        /// </summary>
        private static IViewUserInfoServices viewUserInfoServices = new ViewUserInfoServices();

        #region 登录生成token
        /// <summary>
        /// 登录生成token
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="pwd">密码</param>
        /// <returns>Json</returns>
        [Route("oauth")]
        [HttpPost]
        public IHttpActionResult AccessToken(dynamic user)
        {
            string Email = Convert.ToString(user.email);
            string Pwd = Common.Md5.GetMd5String(Convert.ToString(user.pwd));
            string Msg = "";
            string Status = "400";
            string Token = string.Empty;
            JObject Result = new JObject();
            tbUser_Info userInfo = userInfoService.LoadEntityes(u => u.User_Emailstr == Email && u.User_Pwdstr == Pwd).FirstOrDefault();
            if (userInfo != null)
            {
                Token = OAuth.CreateAccessToken(userInfo.User_Namestr);
                Msg = "登录成功";
                Result.Add("token", Token);
            }
            else
            {
                Msg = "用户名或密码错误！";
                Result.Add("status", Status);
            }
            Result.Add("msg", Msg);
            return Json<dynamic>(Result);
        }
        #endregion


        #region 通过签权，获取用户信息
        ///<summary>
        ///通过唯一标示，获取用户信息（通过视图获取）
        ///</summary>
        ///<param name="access_token"></param>
        ///<returns>用户的个人信息</returns>
        [HttpGet]
        public IHttpActionResult GetUserInfo(string access_token)
        {
            string Msg = "";
            string Status = "400";
            JObject Result = new JObject();
            JObject Obj = OAuth.GetData(access_token);
            string Name = Obj["name"].ToString();
            DateTime Exp = Convert.ToDateTime(Obj["exp"]);
            var userInfo = userInfoService.LoadEntityes(u => u.User_Namestr == Name).FirstOrDefault();
            if (Exp.Subtract(DateTime.Now).Days < 0)
            {
                Status = "401";//过期
                Msg = "签权过期";
                Result.Add("msg", Msg);
                Result.Add("status", Status);
            }
            else if (userInfo != null)
            {
                Result.Add("id", userInfo.User_IDint);
                Result.Add("email", userInfo.User_Emailstr);
                Result.Add("bio", userInfo.User_Biostr);
            }
            else
            {
                Status = "404";
                Msg = "没有查找到用户";
                Result.Add("msg", Msg);
                Result.Add("status", Status);
            }
            return Json<dynamic>(Result);
        }
        #endregion

        #region 用户注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost]
        public IHttpActionResult AddUser(dynamic user)
        {
            string Msg = "";
            string Status = "400";

            string name = Convert.ToString(user.name);
            string email = Convert.ToString(user.email);
            string pwd = Convert.ToString(user.pwd);

            tbUser_Info userInfo = new tbUser_Info();
            userInfo.User_Namestr = name;
            userInfo.User_Emailstr = email;
            userInfo.User_Pwdstr = Md5.GetMd5String(pwd);

            string Code = CreateRenderCode.RenderCode();
            userInfo.User_Codestr = Code;//激活码
            userInfo.User_Sateint = 0;//0未激活

            userInfo.User_Expdate = DateTime.Now.AddDays(2);//两天后过期

            var newUserInfo = userInfoService.AddEntity(userInfo);
            if (newUserInfo != null)
            {
                Status = "200";
                /* if (ALiYnEmail.SendEmail(name, Code, email))
                 {
                     Msg = "邮件发送成功！";
                 }
                 else
                 {
                     Msg = "邮件发送失败！";
                 }*/
            }
            else
            {
                Msg = "系统出错，请稍后重试！";
            }
            return Json<dynamic>(new { msg = Msg, status = Status });
        }
        #endregion

        #region 检测是否已经有User_Name
        /// <summary>
        /// 检测是否已经有User_Name
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("name")]
        [HttpGet]
        public IHttpActionResult UserNameIsExist(string userName)
        {
            string Status = "404";
            string Msg = "";
            JObject Result = new JObject();
            if (userInfoService.LoadEntityes(u => u.User_Namestr == userName).FirstOrDefault() != null)
            {
                Status = "200";
                Msg = "改用户名已存在";
            }
            else
            {
                Msg = "404 Not Found！";
            }
            Result.Add("status", Status);
            Result.Add("msg", Msg);
            return Json<dynamic>(Result);
        }
        #endregion

        #region 检测是否Email是否存在
        /// <summary>
        /// 检测是否Email是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("email")]
        public IHttpActionResult UserEmailIsExist(string email)
        {
            string Status = "404";
            string Msg = "";
            JObject Result = new JObject();
            if (userInfoService.LoadEntityes(u => u.User_Emailstr == email).FirstOrDefault() != null)
            {
                Status = "200";
                Msg = "改用户名已存在";
            }
            else
            {
                Msg = "404 Not Found！";
            }
            Result.Add("status", Status);
            Result.Add("msg", Msg);
            return Json<dynamic>(Result);
        }
        #endregion
    }
}