using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Timer
{
    public sealed class Clock
    {
      
        private static Clock instance = null;
        private static readonly object padlock = new object();


        private int _period = 60000 ;

  
        public int Period
        {
            get { return (this._period / _scale) / _speed; }
        }


        private int _speed = 1;
        public int Speed
        {
            get { return this._speed; }
            set { this._speed = value; }
        }

        private int _scale = 60;


        public static Clock Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Clock();
                    }
                    return instance;
                }
            }
        }

    }
}

