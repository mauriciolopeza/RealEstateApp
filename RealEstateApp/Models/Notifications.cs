using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class Notifications
    {
        public int NotificationsId { get; set; }
        public int FkUsersId { get; set; }
        public string Notification { get; set; }
        public DateTime? NotificationDatetime { get; set; }

        public virtual Users FkUsers { get; set; }
    }
}
