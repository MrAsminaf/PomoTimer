using System.Collections.Generic;
using PomoTimer.Models;

public class UserDetailsViewModel
{
    public IEnumerable< IEnumerable< TimeModel > > TimeModelsLastWeekGrouped { get; set; }
    public IEnumerable< TimeModel > TimeModelsLastWeekEmpty { get; set; }
}