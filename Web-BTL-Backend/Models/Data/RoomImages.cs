using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class RoomImages
    {
        public int IdRoom { get; set; }
        public string ImagePath { get; set; }
        public int IdImage { get; set; }

        public virtual Motelrooms IdRoomNavigation { get; set; }
    }
}
