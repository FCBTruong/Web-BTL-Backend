using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Utilities
    {
        public int IdUtility { get; set; }
        public int? BathRoom { get; set; }
        public int? Kitchen { get; set; }
        public int? AirConditioning { get; set; }
        public int? Balcony { get; set; }
        public int? ElectricityWater { get; set; }
        public int? Wifi { get; set; }
        public string UtilityOthers { get; set; }
    }
}
