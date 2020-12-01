using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Motelrooms
    {
        public uint IdRoom { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Area { get; set; }
        public int Views { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public uint IdUser { get; set; }
        public int IdCategory { get; set; }
        public int IdDistrict { get; set; }
        public string IdUtility { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Phone { get; set; }
        public string Slug { get; set; }
        public int? Likes { get; set; }
        public int? Status { get; set; }
        public int IsGeneral { get; set; }
    }
}
