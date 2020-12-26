using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class FavoritePosts
    {
        public int IdUser { get; set; }
        public int IdPost { get; set; }

        public virtual Posts IdPostNavigation { get; set; }
    }
}
