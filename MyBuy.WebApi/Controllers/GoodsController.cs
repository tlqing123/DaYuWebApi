using MyBuy.BLL;
using MyBuy.IBLL;
using MyBuy.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyBuy.WebApi.Controllers
{
    /*
     * 商品信息的Api
     * xiaoshan
     * 2017-5-29
     */
    [RoutePrefix("api/goods")]
    public class GoodsController : ApiController
    {
        /// <summary>
        /// 创建商品操作实例
        /// </summary>
        public static IGoodsServices GoodsServices = new GoodsServices();
        /// <summary>
        /// 创建商品视图操作实例
        /// </summary>
        public static IViewGoodsServices ViewGoodsServices = new ViewGoodsServices();

        #region 根据ID返回商品（通过视图查询的） OK
        /// <summary>
        /// 根据ID返回商品（通过视图查询的）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetGoodsByID(string id)
        {
            string Msg = "";
            string Status = "404";
            JObject Result = new JObject();
            var goods = ViewGoodsServices.LoadEntityes(u => u.Goods_Idstr == id).FirstOrDefault();
            if (goods != null)
            {
                Status = "200";
                Result.Add("id", goods.Goods_Idstr);
                Result.Add("name", goods.Goods_Namestr);
                Result.Add("descript", goods.Goods_Descriptionstr);
                Result.Add("price", goods.Goods_Priceint);
                Result.Add("old_price", goods.Goods_OldPriceint);
                Result.Add("quality", goods.Goos_Quality);
                Result.Add("imgs", goods.Goods_Imgstr);
                Result.Add("create_at", goods.Goods_Createdate);
                Result.Add("type_name", goods.Type_Namestr);
                Result.Add("type_str", goods.Type_IDstr);
                Result.Add("owner", goods.User_Namestr);
                Result.Add("isdel", goods.Goods_Delflagint);
            }
            else
            {
                Msg = "没有找到该商品";
                Result.Add("msg", Msg);
            }
            Result.Add("status", Status);
            return Json<dynamic>(Result);
        }
        #endregion

        #region 根据类别分页获取所有正在出售的商品(分页获取)
        /// <summary>
        /// 分页获取所有正在出售的商品
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="per_page">每次获取的记录数</param>
        /// <returns></returns>
        [Route("{type}")]
        [HttpGet]
        public IHttpActionResult GetAllGoods(int page, int per_page, string type)
        {
            page = page > 0 ? page : 1;
            per_page = per_page <= 0 ? per_page : 10;
            int TotalCount = 0;
            string Msg = "";
            string Status = "400";
            var Goodses = ViewGoodsServices.LoadPageEntites(page, per_page, out TotalCount, u => u.Type_IDstr == type && u.Goods_Delflagint ==0, u => u.Goods_Createdate, false);//Goods_Delfalgint 0 OK，1 在交易，2 交易成功
            if (Goodses.Count() > 0)
            {
                Status = "200";
            }
            else
            {
                Msg = "没有查到数据";

            }
            return Json<dynamic>(new { msg = Msg, status = Status, goods = Goodses });
        }
        #endregion

        #region 分页获取所有正在出售的商品
        /// <summary>
        /// 分页获取所有正在出售的商品
        /// </summary>
        /// <param name="type">类型ID</param>
        /// <param name="page">当前页码</param>
        /// <param name="per_page">每次获取条目</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetGoodsByType(int page, int per_page)
        {
            page = page > 0 ? page : 1;
            per_page = per_page <= 0 ? per_page : 10;
            string Msg = "";
            string Status = "400";
            int TotalCount = 0;
            var Goods = ViewGoodsServices.LoadPageEntites(page, per_page, out TotalCount, u => u.Goods_Delflagint == 0, u => u.Goods_Createdate, false);
            if (Goods.Count() > 0)
            {
                Status = "200";
            }
            else
            {
                Msg = "没有查找到数据";
            }
            return Json<dynamic>(new { msg = Msg, status = Status, Goods = Goods });
        }
        #endregion



    }
}