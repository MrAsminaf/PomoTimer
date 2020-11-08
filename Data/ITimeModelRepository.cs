using Microsoft.VisualBasic;
using PomoTimer.Models;
using System.Collections.Generic;

namespace PomoTimer.Data
{
    public interface ITimeModelRepository
    {
        IEnumerable<TimeModel> GetAllTimeModels();
        IEnumerable<TimeModel> GetTimeModelsByUserId(string id);
        bool CheckIfTimeModelExists(string id, DateAndTime dt); 
        void Save();
    }
}
