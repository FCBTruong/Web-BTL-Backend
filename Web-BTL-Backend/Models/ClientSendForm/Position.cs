using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_BTL_Backend.Models.ClientSendForm
{
    public class Position
    {
        public double x { set; get; }

        public double y { set; get; }

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
