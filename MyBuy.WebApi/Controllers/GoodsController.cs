using MyBuy.BLL;
using MyBuy.IBLL;
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

        #region 根据ID返回商品（通过视图查询的）
        /// <summary>
        /// 根据ID返回商品（通过视图查询的）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{type}/{id}")]
        public IHttpActionResult GetGoods(string id)
        {
            string Msg = "";
            string Status = "404";
            var goods = ViewGoodsServices.LoadEntityes(u => u.Goods_Idstr == id).FirstOrDefault();
            if (goods != null)
            {
                Status = "200";
                Msg = "查找成功";
            }
            else
            {
                Msg = "没有找到该商品";
            }
            return Json<dynamic>(new { msg = Msg, status = Status, goods = goods });
        }
        #endregion


        #region 获取所有正在出售的商品(分页获取)
        /// <summary>
        /// 分页获取所有正在出售的商品
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每次获取的记录数</param>
        /// <returns></returns>

        [Route("{pageIndex:int=1}/{pageSize:int=10}")]
        [HttpGet]
        public IHttpActionResult GetAllGoods(int pageIndex, int pageSize)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize <= 0 ? pageSize : 10;
            int TotalCount = 0;
            string Msg = "";
            string Status = "400";
            var Goods = ViewGoodsServices.LoadPageEntites(pageIndex, pageSize, out TotalCount, u => true, u => u.Goods_Createdate, false);//Goods_Delfalgint 0 OK，1 在交易，2 交易成功
            if (Goods.Count() > 0)
            {
                Status = "200";
            }
            else
            {
                Msg = "没有查到数据";
            }
            return Json<dynamic>(new { msg = Msg, status = Status, goods = Goods });
        }
        #endregion

        #region 根据类别分页获取所有正在出售的商品
        /// <summary>
        /// 根据类别分页获取所有正在出售的商品
        /// </summary>
        /// <param name="type">类型ID</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每次获取条目</param>
        /// <returns></returns>

        [HttpGet]
        [Route("{type:int}/{pageIndex:int}/{pageSize:int}")]
        public IHttpActionResult GetGoodsByType(int type, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize <= 0 ? pageSize : 10;
            string Msg = "";
            string Status = "400";
            int TotalCount = 0;
            var Goods = ViewGoodsServices.LoadPageEntites(pageIndex, pageSize, out TotalCount,u=>true, u => u.Goods_Createdate, false);
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