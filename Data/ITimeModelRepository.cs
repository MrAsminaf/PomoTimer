using PomoTimer.Models;
using System;
using System.Collections.Generic;

namespace PomoTimer.Data
{
    public interface ITimeModelRepository
    {
        IEnumerable<TimeModel> GetAllTimeModels();
        IEnumerable<TimeModel> GetAllTimeModelsByUserId(string id);
        IEnumerable<TimeModel> GetUserTimeModelsInLastWeek(string id);
        TimeModel GetOneTimeModel(string id, DateTime dt);
        bool CheckIfTimeModelExists(string id, DateTime dt);
        void AddTimeToUser(string id, DateTime td, int minutes);
        void Save();
    }
}
