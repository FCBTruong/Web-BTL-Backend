using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Reports
    {
        public int IdReport { get; set; }
        public int IdRoom { get; set; }
        public string Content { get; set; }
    }
}
