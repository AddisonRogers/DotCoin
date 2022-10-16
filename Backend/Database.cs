using System;
using System.Data;
using Npgsql;

namespace DotCoin3
{
    public class Database
    {
        public void Start()
        {
            var bitHost = "db.bit.io";
            var bitApiKey = "v2_3umqE_gmtLXd9R9bQvhwyuwD28rbr"; // from the "Password" field of the "Connect" menu

            // For this example, look at the public 2020 census reapportionment data
            var bitUser = "AddisonRogers";
            var bitDbName = "AddisonRogers/DC";

            var cs = $"Host={bitHost};Username={bitUser};Password={bitApiKey};Database={bitDbName}";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            /*
            var sql = "SELECT * FROM \"dliden/2020_Census_Reapportionment\".\"Historical Apportionment\" limit 10;";
    
            using var cmd = new NpgsqlCommand(sql, con);
    
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            DataTable schemaTable = reader.GetSchemaTable();
    
            // Show schema details
            foreach (DataRow row in schemaTable.Rows)
            {
                foreach (DataColumn column in schemaTable.Columns)
                {
                    Console.WriteLine(String.Format("{0} = {1}",
                        column.ColumnName, row[column]));
                }
            }
    
            // Show all data
            while (reader.Read()) 
            {
                for (int colNum = 0; colNum < reader.FieldCount; colNum++) 
                {
                    Console.Write(reader.GetName(colNum) + "=" +  reader[colNum] + " ");
                }
                Console.Write("\n");
            }*/
        }
    }
}