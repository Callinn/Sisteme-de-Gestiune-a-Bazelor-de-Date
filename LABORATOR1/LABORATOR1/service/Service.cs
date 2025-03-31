using LABORATOR1.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATOR1.service
{
    public class Service
    {
        private readonly Repository repository;
        public Service(Repository repository)
        {
            this.repository = repository;
        }

        public DataTable GetAllGames()
        {
            return repository.GetAllGames();
        }

        public DataTable GetAchievementsByGameId(int parentId)
        { 
            return repository.GetAchievementsByGameId(parentId);
        }
        
        public void AddAchievement(String achievement_name, String description, int game_id)
        {
            repository.AddAchievement(achievement_name, description, game_id);
        }

        public void UpdateAchievement(String achievement_name, String description, int game_id)
        {
            repository.UpdateAchievement(achievement_name, description, game_id);
        }

        public void DeleteAchievement(String achievement_name)
        {
            repository.DeleteAchievement(achievement_name);
        }
    }
}
