using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Reports
    {
        public int IdReport { get; set; }
        public int IdPost { get; set; }
        public string Content { get; set; }
        public int? IdUser { get; set; }
        public int? ReportType { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Posts IdPostNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
