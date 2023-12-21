using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Behaviour
{
    public class Position
    {
        public int posX { get; set; }

        public int posY { get; set; }


        public Position(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void SetPosition(int x, int y)
        {
            posX = x;
            posY = y;
        }


        public void SetPosition(Position pos)
        {
            posX = pos.posX;
            posY = pos.posY;
        }
    }
}
