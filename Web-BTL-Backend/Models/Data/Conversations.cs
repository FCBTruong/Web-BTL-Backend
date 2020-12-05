using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Conversations
    {
        public int IdC { get; set; }
        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public int? Status { get; set; }
    }
}
