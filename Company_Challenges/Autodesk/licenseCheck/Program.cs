#pragma warning disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace licenseCheck
{
    class Program
    {
        static void Main()
        {
            var users = new Dictionary<string, List<string>>();
            var products = new Dictionary<string, List<string>>();
            
            string connStr = "server=localhost;user=test;database=ri_db;SslMode=none";
            using(MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                
                var cmd = new MySqlCommand("SELECT * FROM users", connection);
                var rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    users.Add(rdr[0].ToString(), 
                            new List<string>(Regex.Split(rdr[1].ToString(), @"\W")));
                }
                rdr.Close();
                
                cmd = new MySqlCommand("SELECT * FROM products", connection);
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    products.Add(rdr[0].ToString(), 
                            new List<string>(Regex.Split(rdr[1].ToString(), @"\W")));
                }
                rdr.Close();
                
                connection.Close();
            }
            
            foreach (var user in users)
            {
                var sb = new StringBuilder();
                
                sb.AppendLine($"User {user.Key}:");
                
                foreach (var product in products)
                {
                    var flag = user.Value.Any(product.Value.Contains);
                    sb.AppendLine($"  {product.Key}: {flag.ToString().ToLower()}");
                    
                }
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
