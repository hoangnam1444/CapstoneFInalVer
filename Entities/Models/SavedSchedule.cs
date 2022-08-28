using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class SavedSchedule
    {
        public string ScheduleId { get; set; }
        public int Day { get; set; }
    }
}
