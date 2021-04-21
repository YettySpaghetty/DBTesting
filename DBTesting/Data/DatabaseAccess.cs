using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Test.Models;

namespace DBTesting.Data
{
    public class DatabaseAccess
    {
        public string GetMariaDBConnectionString()
        {
            return "";
        }

        public static string GetCrateDBConnectionString()
        {
            return "";
        }

        public List<Person> ReadMariaDBData(int numOfEntries)
        {
            List<Person> data = new List<Person>();

            string strQuery = "select * from People WHERE id = @id";
            try
            {
                if (string.IsNullOrEmpty(strQuery) == true)
                    return null;

                using (var mysqlconnection = new MySqlConnection(GetMariaDBConnectionString()))
                {
                    mysqlconnection.Open();
                    using (MySqlCommand cmd = mysqlconnection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;
                        cmd.CommandText = strQuery;

                        var reader = cmd.ExecuteReader();

                        while (reader.Read() && (numOfEntries != 0))
                        {
                            data.Add(new Person
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Age = int.Parse(reader["Age"].ToString())
                            });
                            numOfEntries--;
                        }

                        mysqlconnection.Close();

                        if (data == null)
                            return null;
                        else
                            return data;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }


        public static List<Person> ReadCrateDBData(int numOfEntries)
        {
            var data = new List<Person>();

            using SqlConnection myConnection = new SqlConnection(GetCrateDBConnectionString());

            string query = "SELECT * from People";

            SqlCommand sqlCommand = new SqlCommand(query, myConnection);

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = query;
            sqlCommand.CommandTimeout = 300;

            myConnection.Open();

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read() && (numOfEntries != 0))
            {
                data.Add(new Person
                {
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Age = int.Parse(reader["Age"].ToString())
                });
                numOfEntries--;
            }

            myConnection.Close();

            if (data == null)
            {
                return null;
            }
            else
            {
                return data;
            }
        }
    }
}
