using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Comments
    {
        public int IdUser { get; set; }
        public int IdPost { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Rating { get; set; }
        public int? Status { get; set; }
        public int IdComment { get; set; }
    }
}
