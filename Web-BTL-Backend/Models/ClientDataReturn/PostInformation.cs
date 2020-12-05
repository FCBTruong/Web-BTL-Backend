using System.Collections.Generic;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Models.ClientDataReturn
{
    public class PostInformation
    {
        public Posts post { set; get; }
        public Users owner { set; get; }
        public Motelrooms motelInfor { set; get; }
        public List<PostComment> comments { set; get; }
    }
}
