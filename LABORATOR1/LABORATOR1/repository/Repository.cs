using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATOR1.repository
{
    public class Repository
    {
        private readonly String connectionString;
        public Repository(String connectionString)
        {
            this.connectionString = connectionString + ";TrustServerCertificate=True;";
        }

        public DataTable GetAllGames()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM GAMES";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetAchievementsByGameId(int parentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT achievement_name, description FROM ACHIEVEMENTS WHERE game_id = @parentId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@parentId", parentId);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void AddAchievement(String achievement_name, String description, int game_id)
        {
      using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Check for existing achievement with the same name and game_id
                string checkQuery = "SELECT COUNT(*) FROM Achievements WHERE achievement_name = @achievement_name AND game_id = @game_id";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@achievement_name", achievement_name);
                checkCommand.Parameters.AddWithValue("@game_id", game_id);
                connection.Open();
                int count = (int)checkCommand.ExecuteScalar();
                connection.Close();

                if (count > 0)
                {
                    throw new Exception("An achievement with the same name already exists for this game.");
                }

                // Insert new achievement
                string insertQuery = "INSERT INTO Achievements (achievement_name,description,game_id) VALUES (@achievement_name,@description,@game_id)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@achievement_name", achievement_name);
                insertCommand.Parameters.AddWithValue("@description", description);
                insertCommand.Parameters.AddWithValue("@game_id", game_id);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateAchievement(String achievement_name, String description, int game_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE ACHIEVEMENTS SET achievement_name = @achievement_name, description=@description, game_id = @game_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@achievement_name", achievement_name);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@game_id", game_id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAchievement(String achievement_name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ACHIEVEMENTS WHERE achievement_name = @achievement_name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@achievement_name", achievement_name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
