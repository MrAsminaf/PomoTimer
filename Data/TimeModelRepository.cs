using Microsoft.VisualBasic;
using PomoTimer.Models;
using System;
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

        public void AddTimeToUser(string id, DateTime dt, int minutes)
        {
            if (CheckIfTimeModelExists(id, dt))
            {
                var model = GetOneTimeModel(id, dt);
                model.minutes += minutes;
            }
            else
            {
                TimeModel model = new TimeModel();
                model.ApplicationUserId = id;
                model.DateTime = dt;
                model.minutes += minutes;

                context.TimeModels.Add(model);
            }
        }

        public bool CheckIfTimeModelExists(string id, DateTime dt)
        {
            var models = GetAllTimeModelsByUserId(id);
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

        public IEnumerable<TimeModel> GetAllTimeModelsByUserId(string id)
        {
            return context.TimeModels.ToList().FindAll(o => o.ApplicationUserId == id);
        }

        public TimeModel GetOneTimeModel(string id, DateTime dt)
        {
            return context.TimeModels.ToList().FirstOrDefault(o => o.ApplicationUserId == id && o.DateTime == dt);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
