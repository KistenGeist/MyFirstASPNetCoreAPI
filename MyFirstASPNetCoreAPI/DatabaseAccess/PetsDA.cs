using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using MyFirstASPNetCoreAPI.Models;
using MySql.Data.MySqlClient;

namespace MyFirstASPNetCoreAPI.DatabaseAccess
{
    public static class PetsDA
    {
        private static string connectionString = "server=localhost;user id=root;password=;port=3306;database=myfirstaspnetcoreapi;SslMode=Preferred;convert zero datetime=True";
        public static List<Pet> GetPets ()
        {
            List<Pet> lstPets = new();

            using (MySqlConnection sqlConnection = new(connectionString))
            {
                string commandWithVar = "SELECT * FROM pet_tb";

                //try
                //{
                sqlConnection.Open();

                using (MySqlCommand cmd = new(commandWithVar, sqlConnection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Pet pet = new Pet( Convert.ToInt16(reader["pet_id"]),
                                                   Convert.ToString(reader["pet_Name"]),
                                                   (float)Convert.ToDouble(reader["pet_alter"]),
                                                   Convert.ToString(reader["pet_art"]),
                                                   Convert.ToString(reader["pet_Rasse"]),
                                                   Convert.ToBoolean(reader["pet_geimpft"]),
                                                   Convert.ToString(reader["pet_geschlecht"])
                                                 );
                                lstPets.Add(pet);
                            }
                        }
                    }
                }
                sqlConnection.Close();
                //}
                //catch (Exception ex)
                //{
                //    //TODO Fehlermeldung
                //}

            }

            return lstPets;
        }
    }
}
