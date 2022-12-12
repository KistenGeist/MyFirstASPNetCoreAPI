using MyFirstASPNetCoreAPI.Models;
using MySql.Data.MySqlClient;

namespace MyFirstASPNetCoreAPI.DatabaseAccess
{
    public static class PetsDA
    {
        private static string connectionString = "server=localhost;user id=root;password=;port=3306;database=myfirstaspnetcoreapi;SslMode=Preferred;convert zero datetime=True";
        public static List<Pet> GetPets()
        {
            List<Pet> lstPets = new();

            using (MySqlConnection sqlConnection = new(connectionString))
            {
                MySqlCommand cmd = new("SELECT * FROM pet_tb", sqlConnection);

                //try
                //{
                sqlConnection.Open();

                using (cmd)
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Pet pet = new Pet(Convert.ToInt16(reader["pet_id"]),
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

        public static Pet GetPetById(int id)
        {
            Pet pet = new();

            using (MySqlConnection sqlConnection = new(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM pet_tb WHERE pet_id = @pet_id", sqlConnection);
                cmd.Parameters.Add("@pet_id", MySqlDbType.Int64);
                cmd.Parameters["@pet_id"].Value = id;

                //try
                //{
                sqlConnection.Open();

                using (cmd)
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                pet = new Pet(Convert.ToInt16(reader["pet_id"]),
                                              Convert.ToString(reader["pet_Name"]),
                                              (float)Convert.ToDouble(reader["pet_alter"]),
                                              Convert.ToString(reader["pet_art"]),
                                              Convert.ToString(reader["pet_Rasse"]),
                                              Convert.ToBoolean(reader["pet_geimpft"]),
                                              Convert.ToString(reader["pet_geschlecht"])
                                             );
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

            return pet;
        }

        public static string UpdatePet(Pet pet)
        {
            string result = "";
            using (MySqlConnection sqlConnection = new(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE pet_tb SET pet_Name = @pet_Name, pet_alter = @pet_alter, pet_art = @pet_art, pet_Rasse = @pet_Rasse, pet_geimpft = @pet_geimpft, pet_geschlecht = @pet_geschlecht WHERE pet_id = @pet_id", sqlConnection);
                cmd.Parameters.Add("@pet_Name", MySqlDbType.String);
                cmd.Parameters.Add("@pet_alter", MySqlDbType.Float);
                cmd.Parameters.Add("@pet_art", MySqlDbType.String);
                cmd.Parameters.Add("@pet_Rasse", MySqlDbType.String);
                cmd.Parameters.Add("@pet_geimpft", MySqlDbType.Bit);
                cmd.Parameters.Add("@pet_geschlecht", MySqlDbType.String);
                cmd.Parameters.Add("@pet_id", MySqlDbType.Int32);

                cmd.Parameters["@pet_Name"].Value = pet.Name;
                cmd.Parameters["@pet_alter"].Value = pet.Alter;
                cmd.Parameters["@pet_art"].Value = pet.Art;
                cmd.Parameters["@pet_Rasse"].Value = pet.Rasse;
                cmd.Parameters["@pet_geimpft"].Value = pet.Geimpft;
                cmd.Parameters["@pet_geschlecht"].Value = pet.Geschlecht;
                cmd.Parameters["@pet_id"].Value = pet.Id;
                
                try
                {
                    sqlConnection.Open();

                    using (cmd)
                    {
                        int rowsAdded = cmd.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    //TODO Fehlermeldung
                    return "Error while updating pet in DB: " + ex.Message;
                }

            }

            return result;
        }

        public static string InsertPet(Pet pet)
        {
            string result = "";
            using (MySqlConnection sqlConnection = new(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO pet_tb (pet_Name, pet_alter, pet_art, pet_Rasse, pet_geimpft, pet_geschlecht)  VALUES(@pet_Name, @pet_alter, @pet_art, @pet_Rasse, @pet_geimpft, @pet_geschlecht)", sqlConnection);
                cmd.Parameters.Add("@pet_Name", MySqlDbType.String);
                cmd.Parameters.Add("@pet_alter", MySqlDbType.Float);
                cmd.Parameters.Add("@pet_art", MySqlDbType.String);
                cmd.Parameters.Add("@pet_Rasse", MySqlDbType.String);
                cmd.Parameters.Add("@pet_geimpft", MySqlDbType.Bit);
                cmd.Parameters.Add("@pet_geschlecht", MySqlDbType.String);

                cmd.Parameters["@pet_Name"].Value = pet.Name;
                cmd.Parameters["@pet_alter"].Value = pet.Alter;
                cmd.Parameters["@pet_art"].Value = pet.Art;
                cmd.Parameters["@pet_Rasse"].Value = pet.Rasse;
                cmd.Parameters["@pet_geimpft"].Value = pet.Geimpft;
                cmd.Parameters["@pet_geschlecht"].Value = pet.Geschlecht;

                try
                {
                    sqlConnection.Open();

                    using (cmd)
                    {
                        int rowsAdded = cmd.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    //TODO Fehlermeldung
                    return "Error while inserting pet in DB: " + ex.Message;
                }

            }

            return result;
        }

        public static string DeletePet(int id)
        {
            string result = "";
            using (MySqlConnection sqlConnection = new(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM pet_tb WHERE pet_id = @pet_id", sqlConnection);
                cmd.Parameters.Add("@pet_id", MySqlDbType.Int64);
                cmd.Parameters["@pet_id"].Value = id;

                try
                {
                    sqlConnection.Open();

                    using (cmd)
                    {
                        int rowsAdded = cmd.ExecuteNonQuery();
                    }

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    //TODO Fehlermeldung
                    return "Error while deleting pet in DB: " + ex.Message;
                }

            }

            return result;
        }
    }
}
