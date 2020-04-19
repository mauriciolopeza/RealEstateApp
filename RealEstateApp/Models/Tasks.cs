using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Tasks
    {
        public int TasksId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int FkUsersId { get; set; }
        public DateTime? TaskDate { get; set; }
        public DateTime? TaskReminderDate { get; set; }

        public virtual Users FkUsers { get; set; }
    }
}
