using System;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public partial class CommunicationLog
    {
        public int CommunicationLogId { get; set; }
        public DateTime? CommunicationDate { get; set; }
        public string CommunicationType { get; set; }
        public string Notes { get; set; }
    }
}
