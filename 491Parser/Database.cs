using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _491Parser
{
    public static class Database
    {
        private static string m_ConnectionString = "Server=EMRE-LAPTOP;Database=KUugle;Trusted_Connection=True;";
        public static void NewPageDiscovered(string URL, string Title)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("NewPageDiscovered", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@URL", SqlDbType.NVarChar).Value = URL;
                    if(Title == null)
                        command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = DBNull.Value;
                    else
                        command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title;

                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine("Arguments " + URL + "," + Title + ": " + e.Message);
                    }
                }
                connection.Close();
            }

        }
        public static void NewInvalidURL(string URL)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("NewInvalidURL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@URL", SqlDbType.NVarChar).Value = URL;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine("Arguments " + URL + ": " + e.Message);
                    }
                }
                connection.Close();
            }
        }
        public static void NewLinkBetweenPages(string source, string destination)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("NewLinkBetweenPages", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Source", SqlDbType.NVarChar).Value = source;
                    command.Parameters.Add("@Destination", SqlDbType.NVarChar).Value = destination;

                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine("Arguments " + source + "," + destination + ": " + e.Message);
                    }
                }
                connection.Close();
            }
        }
        public static void NewKeywordOnPage(string URL, string keyword)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("NewKeywordOnPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@URL", SqlDbType.NVarChar).Value = URL;
                    command.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = keyword;

                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine("Arguments " + URL + "," + keyword + ": " + e.Message);
                    }
                }
                connection.Close();
            }
        }
        public static int GetNumberOfPages()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetNumberOfPages", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    try
                    {
                        result = (int)command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
            return result;
        }
        public static void InitializeImportance()
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("InitializeImportance", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
        }
        public static void PURGEALLTABLES(bool AREYOUSURE)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("PurgeAllTables", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine("Couldn't purge table data : " + e.Message);
                    }
                }
                connection.Close();
            }
        }
        public static List<int> PageHasLinksFrom(int id)
        {
            List<int> result = new List<int>();
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("PageHasLinksFrom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    connection.Open();
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetInt32(0));
                            }
                        }
                        else
                        {
                            if (Program.Log_Verbosity >= 1)
                                Console.WriteLine("No rows found.");
                        }
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
            return result;
        }
        public static float GetImportanceOfPage(int id)
        {
            float result = 0;
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetImportanceOfPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    connection.Open();
                    try
                    {
                        result = (float)(double)command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
            return result;
        }
        public static int GetNumberOfLinksPageHas(int id)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetNumberOfLinksPageHas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    connection.Open();
                    try
                    {
                        result = (int)command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
            return result;
        }
        public static void UpdateImportanceValue(int id, float val)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateImportanceValue", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    command.Parameters.Add("@val", SqlDbType.Float).Value = val;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
        }
        public static void SaveHTMLToDatabase(string URL, string HTML)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SaveHTMLToDatabase", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@URL", SqlDbType.NVarChar).Value = URL;
                    command.Parameters.Add("@HTML", SqlDbType.NVarChar).Value = HTML;
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (Program.Log_Verbosity >= 1)
                            Console.WriteLine(e.Message);
                    }
                }
                connection.Close();
            }
        }
    }
}
