using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace FinanceScaper
{
    
    
        public class StockContext : DbContext
        {
            public DbSet<Stock> Stocks{ get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("Data Source= Stock.db");
        }

    
      public class Stock
      {

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string ChangeRate { get; set; }
        public string Currency { get; set; }
        public string MarketTime { get; set; }
        public string Volume { get; set; }
        public string Shares { get; set; }
        public string AverageVolume { get; set; }
        public string MarketCap { get; set; }


      }

}