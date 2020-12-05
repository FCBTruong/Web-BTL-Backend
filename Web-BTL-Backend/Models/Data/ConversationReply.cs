using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class ConversationReply
    {
        public int IdCr { get; set; }
        public string ReplyContent { get; set; }
        public string IdUser { get; set; }
        public int? Status { get; set; }
        public int? IdC { get; set; }
    }
}
