﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBuy.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyBuyServerEntities : DbContext
    {
        public MyBuyServerEntities()
            : base("name=MyBuyServerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<tbComments> tbComments { get; set; }
        public DbSet<tbGoods> tbGoods { get; set; }
        public DbSet<tbStars> tbStars { get; set; }
        public DbSet<tbType> tbType { get; set; }
        public DbSet<tbUser_Info> tbUser_Info { get; set; }
        public DbSet<VI_COMMENTS> VI_COMMENTS { get; set; }
        public DbSet<VI_GOODSES> VI_GOODSES { get; set; }
        public DbSet<VI_USER_INFO> VI_USER_INFO { get; set; }
    }
}