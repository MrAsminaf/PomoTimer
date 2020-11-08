using Microsoft.VisualBasic;
using PomoTimer.Models;
using System.Collections.Generic;
using System.Linq;

namespace PomoTimer.Data
{
    public class TimeModelRepository : ITimeModelRepository
    {
        private PomoTimerDbContext context;

        public TimeModelRepository(PomoTimerDbContext context)
        {
            this.context = context;
        }

        public bool CheckIfTimeModelExists(string id, DateAndTime dt)
        {
            var models = GetTimeModelsByUserId(id);
            if (models.Count() == 0)
            {
                return false;
            }

            models.ToList().FindAll(o => o.DateTime.Equals(dt));

            if (!models.Any())
            {
                return false;
            }
            return true;
        }

        public IEnumerable<TimeModel> GetAllTimeModels()
        {
            return context.TimeModels;
        }

        public IEnumerable<TimeModel> GetTimeModelsByUserId(string id)
        {
            return context.TimeModels.ToList().FindAll(o => o.ApplicationUserId == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
