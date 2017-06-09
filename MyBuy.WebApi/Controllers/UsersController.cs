using Jose;
using MyBuy.BLL;
using MyBuy.Common;
using MyBuy.IBLL;
using MyBuy.Model;
using MyBuy.WebApi.Models;
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

        #region 登录生成token OK
        /// <summary>
        /// 登录生成token
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="pwd">密码</param>
        /// <returns>Json</returns>
        [Route("OAuth")]
        [HttpPost]
        public IHttpActionResult AccessToken(tbUser_Info user)
        {
            string Email = Convert.ToString(user.User_Emailstr);
            string Pwd = Common.Md5.GetMd5String(Convert.ToString(user.User_Pwdstr));

            string Msg = "";
            string Status = "400";
            string Token = string.Empty;
            JObject Result = new JObject();
            tbUser_Info userInfo = userInfoService.LoadEntityes(u => u.User_Emailstr == Email && u.User_Pwdstr == Pwd).FirstOrDefault();
            if (userInfo != null)
            {
                Token = OAuth.CreateAccessToken(userInfo.User_Namestr);
                Msg = "登录成功";
                Status = "200";
                Result.Add("token", Token);
            }
            else
            {
                Msg = "用户名或密码错误！";

            }
            Result.Add("status", Status);
            Result.Add("msg", Msg);
            return Json<dynamic>(Result);
        }
        #endregion


        #region 通过签权，获取用户信息  OK
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
            JObject Obj = OAuth.GetData(access_token);
            string Name = Obj["name"].ToString();
            DateTime Exp = Convert.ToDateTime(Obj["exp"]);
            var userInfo = viewUserInfoServices.LoadEntityes(u => u.User_Namestr == Name).FirstOrDefault();
            if (Exp.Subtract(DateTime.Now).Days < 0)
            {
                Status = "401";//过期
                Msg = "签权过期";
            }
            else if (userInfo != null)
            {
                Status = "200";
            }
            else
            {
                Status = "404";
                Msg = "没有查找到用户";
            }
            return Json<dynamic>(new { msg = Msg, status = Status, user = userInfo });
        }
        #endregion

        #region 用户注册 OK
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user">用户信息对象</param>
        /// <returns>成功失败状态</returns>
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
            userInfo.User_CreateTimedate = DateTime.Now;
            string Code = CreateRenderCode.RenderCode();
            userInfo.User_Codestr = Code;//激活码
            userInfo.User_Sateint = Convert.ToInt32(Sate.Inactive);//0未激活

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
            return Json<dynamic>(new { msg = Msg, status = Status, name = newUserInfo.User_Namestr });
        }
        #endregion

        #region 检测是否已经有User_Name  OK
        /// <summary>
        /// 检测是否已经有User_Name
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("name")]
        [HttpGet]
        public IHttpActionResult UserNameIsExist(string user_name)
        {
            string Status = "404";
            string Msg = "";
            JObject Result = new JObject();
            if (userInfoService.LoadEntityes(u => u.User_Namestr == user_name).FirstOrDefault() != null)
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

        #region 检测是否Email是否存在 OK
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

        #region 账户激活 OK
        /// <summary>
        /// 账户激活
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("code")]
        public IHttpActionResult ActiveAccount(dynamic obj)
        {
            string name = Convert.ToString(obj.name);
            string code = Convert.ToString(obj.code);
            string Msg = "";
            string Status = "400";
            var userInfo = userInfoService.LoadEntityes(u => u.User_Namestr == name).FirstOrDefault();
            if (Convert.ToDateTime(userInfo.User_Expdate).Subtract(DateTime.Now).Days < 0)
            {
                Status = "600";
                Msg = "激活码已经过期";
            }
            else
            {
                if (userInfo.User_Codestr == code)
                {
                    //tbUser_Info tbUser = new tbUser_Info();
                    userInfo.User_Sateint = Convert.ToInt32(Sate.Activated);
                    if (userInfoService.EditEntity(userInfo))
                    {
                        Msg = "激活成功";
                        Status = "200";
                    }
                    else
                    {
                        Msg = "发生了错误，请稍后再试";
                    }
                }
                else
                {
                    Msg = "激活码错误";
                }
            }
            return Json<dynamic>(new { msg = Msg, status = Status });
        }
        #endregion

    }
}