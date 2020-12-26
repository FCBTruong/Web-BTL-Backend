using System.Collections.Generic;

namespace Web_BTL_Backend.Models.ClientSendForm
{
    public class PostForm
    {
        public string title { set; get; }
        public string address { set; get; }
        public double lat { set; get; }
        public double lng { set; get; }
        public int price { set; get; }
        public int area { set; get; }
        public int district { set; get; }
        public int category { set; get; }
        public string phone { set; get; }
        public string description { set; get; }

        public List<int> utilities { set; get; }
    }
}
