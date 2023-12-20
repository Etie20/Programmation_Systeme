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

        /// <summary>
        /// Set position with x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            posX = x;
            posY = y;
        }

        /// <summary>
        /// Set position with Position object
        /// </summary>
        /// <param name="pos"></param>
        public void SetPosition(Position pos)
        {
            posX = pos.posX;
            posY = pos.posY;
        }
    }
}
