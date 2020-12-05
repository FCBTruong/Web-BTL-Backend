using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Models.ClientDataReturn
{
    public class PostComment
    {
        public Comments comment { set; get; }
        public Users commenter { set; get; }
    }
}
