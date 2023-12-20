using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCI_Common.Behaviour
{
    public abstract class Movable
    {
        public Position Position { get; set; }

        public event EventHandler MoveEvent;
        
        public void MoveTo(int x, int y)
        {
            
            if (x >= 0 && y >= 0)
            {
                this.Position.posX = x;
                this.Position.posY = y;
            }
            Console.WriteLine("Move");
            this.OnMoveEvent(EventArgs.Empty);

        }
        
        public void MoveTo(Position position)
        {
            if(this.Position.posX > position.posX && this.Position.posY > position.posY)
            {
                while (this.Position.posX > position.posX)
                {
                    this.Position.posX -= 1;
                    Thread.Sleep(50);
                }

                while (this.Position.posY > position.posY)
                {
                    this.Position.posY -= 1;
                    Thread.Sleep(50);
                }
            }

            if (this.Position.posX < position.posX && this.Position.posY > position.posY)
            {
                while (this.Position.posX < position.posX)
                {
                    this.Position.posX += 1;
                    Thread.Sleep(50);
                }

                while (this.Position.posY > position.posY)
                {
                    this.Position.posY -= 1;
                    Thread.Sleep(50);
                }
            }

            if (this.Position.posX > position.posX && this.Position.posY < position.posY)
            {
                while (this.Position.posX > position.posX)
                {
                    this.Position.posX -= 1;
                    Thread.Sleep(50);
                }

                while (this.Position.posY < position.posY)
                {
                    this.Position.posY += 1;
                    Thread.Sleep(50);
                }
            }

            if (this.Position.posX < position.posX && this.Position.posY < position.posY)
            {
                while (this.Position.posX < position.posX)
                {
                    this.Position.posX += 1;
                    Thread.Sleep(50);
                }

                while (this.Position.posY < position.posY)
                {
                    this.Position.posY += 1;
                    Thread.Sleep(50);
                }
            }

        }

        public void MoveTeleport(Position position)
        {
            this.Position.posX = position.posX;
            this.Position.posY = position.posY;
        }

        /// <summary>
        /// Move sprite according to vector along X and Y axis
        /// (can be negative or positive)
        /// </summary>
        /// <param name="vectorX"></param>
        /// <param name="vectorY"></param>
        public void Move(int vectorX, int vectorY)
        {
            this.Position.posX += vectorX;
            this.Position.posY += vectorY;
        }

        protected virtual void OnMoveEvent(EventArgs e)
        {
            MoveEvent?.Invoke(this, e);
        }
    }
}
