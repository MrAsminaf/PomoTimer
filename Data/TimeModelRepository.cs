using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TimeModelRepository> logger;

        public TimeModelRepository(PomoTimerDbContext context, ILogger<TimeModelRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void AddTimeToUser(string id, DateTime dt, UpdateStatsModel model)
        {
            if (CheckIfTimeModelExists(id, dt, model.TaskName))
            {
                var entry = GetOneTimeModel(id, dt, model.TaskName);
                entry.Minutes += model.Minutes;
            }
            else
            {
                TimeModel entry = new TimeModel();
                entry.ApplicationUserId = id;
                entry.DateTime = dt.Date;
                entry.Minutes += model.Minutes;
                entry.TaskName = model.TaskName;

                context.TimeModels.Add(entry);
            }
        }

        public bool CheckIfTimeModelExists(string id, DateTime dt, string taskName)
        {
            var models = GetAllTimeModelsByUserId(id);
            if (models.Count() == 0)
            {
                return false;
            }

            var result = models.ToList().FindAll(o => o.DateTime.Date == DateTime.Now.Date && o.TaskName == taskName);

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

        public TimeModel GetOneTimeModel(string id, DateTime dt, string taskName)
        {
            return context.TimeModels.ToList().FirstOrDefault(o => o.ApplicationUserId == id && o.DateTime.Date == dt.Date && o.TaskName == taskName);
        }

        public IEnumerable<IEnumerable<TimeModel>> GetUserTimeModelsInLastWeekGrouped(string id)
        {
            var result = GetAllTimeModelsByUserId(id).ToList().FindAll(o => 
                o.DateTime.Date >= DateTime.Now.Date.AddDays(-6));

            List<List< TimeModel >> outer = new List<List< TimeModel >>();

            var taskNames = result.Select(x => x.TaskName).Distinct();

            foreach (var taskName in taskNames)
            {
                var timeModelsFilteredByTaskName = result.OrderBy(x => x.DateTime )
                    .Where(x => x.TaskName == taskName);

                
                List<TimeModel> inner = new List<TimeModel>()
                {
                    new TimeModel { DateTime = DateTime.Now.Date.AddDays(-6), Minutes = 0, TaskName = taskName },
                    new TimeModel { DateTime = DateTime.Now.Date.AddDays(-5), Minutes = 0, TaskName = taskName },
                    new TimeModel { DateTime = DateTime.Now.Date.AddDays(-4), Minutes = 0, TaskName = taskName },
                    new TimeModel { DateTime = DateTime.Now.Date.AddDays(-3), Minutes = 0, TaskName = taskName },
                    new TimeModel { DateTime = DateTime.Now.Date.AddDays(-2), Minutes = 0, TaskName = taskName },
                    new TimeModel { DateTime = DateTime.Now.Date.AddDays(-1), Minutes = 0, TaskName = taskName },
                    new TimeModel { DateTime = DateTime.Now.Date, Minutes = 0, TaskName = taskName },
                };

                foreach (var uninitDay in inner)
                {
                    foreach (var dayInDatabase in timeModelsFilteredByTaskName)
                    {
                        if (uninitDay.DateTime.Date == dayInDatabase.DateTime.Date)
                        {
                            uninitDay.Minutes += dayInDatabase.Minutes;
                        }
                    }
                }
                outer.Add(inner);
            }

            return outer;
        }

        public IEnumerable<TimeModel> GetUserTimeModelsInLastWeekSummed(string id)
        {
            var result = GetUserTimeModelsInLastWeek(id);

            List<TimeModel> final = new List<TimeModel>()
            {
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-6), Minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-5), Minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-4), Minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-3), Minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-2), Minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date.AddDays(-1), Minutes = 0 },
                new TimeModel { DateTime = DateTime.Now.Date, Minutes = 0 },
            };

            foreach (var day in final)
            {
                foreach (var userDay in result)
                {
                    if (day.DateTime.Date == userDay.DateTime.Date)
                    {
                        day.Minutes += userDay.Minutes;
                    }
                }
            }

            return final.OrderBy(x => x.DateTime);
        }

        public IEnumerable<TimeModel> GetUserTimeModelsInLastWeek(string id)
        {
            var result = GetAllTimeModelsByUserId(id).ToList().FindAll(o => 
                o.DateTime.Date >= DateTime.Now.Date.AddDays(-6));

            return result;
        }

        public void AddTask(string id, string taskName, int minutes, DateTime dt)
        {
            if (CheckIfTimeModelExists(id, dt, taskName))
            {
                var timeModel = GetOneTimeModel(id, dt, taskName);

                if (minutes > timeModel.Minutes) { return; }

                timeModel.Minutes += minutes;
            }
            else
            {
                TimeModel timeModel = new TimeModel();
                timeModel.ApplicationUserId = id;
                timeModel.DateTime = dt;
                timeModel.Minutes = minutes;
                timeModel.TaskName = taskName;
            }

            var defaultTimeModel = GetOneTimeModel(id, dt, "");
            defaultTimeModel.Minutes -= minutes;
                
            if (defaultTimeModel.Minutes == 0)
            {
                context.Remove(defaultTimeModel);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
