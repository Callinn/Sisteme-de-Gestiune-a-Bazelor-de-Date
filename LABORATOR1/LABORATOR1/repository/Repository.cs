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
                string query = "SELECT achievement_name, description FROM Achievement WHERE game_id = @parentId";
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
                string query = "INSERT INTO Achievements (achievement_name,description,game_id) VALUES (@achievement_name,@description,@game_id)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@achievement_name", achievement_name);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@game_id", game_id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAchievement(String achievement_name, String description, int game_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Achievements SET achievement_name = @achievement_name, description=@description, game_id = @game_id";
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
                string query = "DELETE FROM Achievements WHERE achievement_name = @achievement_name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@achievement_name", achievement_name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
