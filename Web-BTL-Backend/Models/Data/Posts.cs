using System;
using System.Collections.Generic;

namespace Web_BTL_Backend.Models.Data
{
    public partial class Posts
    {
        public Posts()
        {
            FavoritePosts = new HashSet<FavoritePosts>();
        }

        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public int IdRoom { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime ExpireDate { get; set; }

        public virtual ICollection<FavoritePosts> FavoritePosts { get; set; }
    }
}
