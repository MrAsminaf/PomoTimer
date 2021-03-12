using PomoTimer.Models;
using System;
using System.Collections.Generic;

namespace PomoTimer.Data
{
    public interface ITimeModelRepository
    {
        IEnumerable<TimeModel> GetAllTimeModels();
        IEnumerable<TimeModel> GetAllTimeModelsByUserId(string id);
        IEnumerable<TimeModel> GetUserTimeModelsInLastWeekSummed(string id);
        IEnumerable<IEnumerable<TimeModel>> GetUserTimeModelsInLastWeekGrouped(string id);
        TimeModel GetOneTimeModel(string id, DateTime dt, string taskName);
        bool CheckIfTimeModelExists(string id, DateTime dt, string taskName);
        void AddTimeToUser(string id, DateTime td, UpdateStatsModel model);
        void Save();
    }
}
