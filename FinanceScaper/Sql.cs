using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace FinanceScaper
{
    public class Sql
    {
        public void InsertDB(string Symbol , string LastPrice , string Changes , string Currency , string Volume , string AvgVolume, string MarketCap )
        {

           

            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();

            connBuilder.Add("Database", "financials");
            connBuilder.Add("Data Source", "localhost");
            connBuilder.Add("User Id", "root");
            connBuilder.Add("Password", "Chelsea#1995");

            MySqlConnection connection = new MySqlConnection(connBuilder.ConnectionString);

            MySqlCommand cmd = connection.CreateCommand();

            connection.Open();
            string cmdString;
            cmdString = "INSERT INTO yahoo(Symbol , LastPrice , Changes , Currency , Volume , AvgVolume, MarketCap) VALUES(' " + Symbol + "', '" + LastPrice + "' , ' " + Changes + " ', ' " + Currency + " ' , ' " + Volume + " ' , ' " + AvgVolume + "' , ' " + MarketCap + " ')";

            cmd = new MySqlCommand(cmdString, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

        }
    }
}