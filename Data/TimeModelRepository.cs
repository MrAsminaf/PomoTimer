using Microsoft.VisualBasic;
using PomoTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;

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
                model.DateTime = dt.Date;
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

            var result = models.ToList().FindAll(o => o.DateTime.Date == DateTime.Now.Date);

            if (!result.Any())
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
            return context.TimeModels.ToList().FirstOrDefault(o => o.ApplicationUserId == id && o.DateTime.Date == dt.Date);
        }

        public IEnumerable<TimeModel> GetUserTimeModelsInLastWeek(string id)
        {
            var result = GetAllTimeModelsByUserId(id).ToList().FindAll(o => 
                o.DateTime.Date >= DateTime.Now.Date.AddDays(-6));

            List<TimeModel> final = new List<TimeModel>()
            {
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-6), minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-5), minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-4), minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-3), minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-2), minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-1), minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date, minutes = 0 },
            };

            foreach (var day in final)
            {
                foreach (var userDay in result)
                {
                    if (day.DateTime.Date == userDay.DateTime.Date)
                    {
                        day.minutes = userDay.minutes;
                    }
                }
            }

            return final.OrderBy(x=>x.DateTime);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
