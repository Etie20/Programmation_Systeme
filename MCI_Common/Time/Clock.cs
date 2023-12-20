using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Timer
{
    //TO DO Implement time (hour and minues, night/day shift)
    public sealed class Clock
    {
        /// <summary>
        /// Singleton variables (thread-safe method)
        /// </summary>
        private static Clock instance = null;
        private static readonly object padlock = new object();


        /// <summary>
        /// Period of time (in milliseconds) = 1min, can't be set
        /// </summary>
        private int _period = 60000 ;

        /// <summary>
        /// Public period, equals the clock period divided by scale and speed
        /// ()
        /// </summary>
        public int Period
        {
            get { return (this._period / _scale) / _speed; }
        }

        /// <summary>
        /// Time is divided by speed 
        /// (ex: if period = 1min and speed = 2x, final period = 30sec (1000ms/2 = 500ms) )
        /// Default speed = 1 
        /// </summary>
        private int _speed = 1;
        public int Speed
        {
            get { return this._speed; }
            set { this._speed = value; }
        }

        /// <summary>
        /// Time scale (fixed because need 1hour simulation = 1 second IRL)
        /// </summary>
        private int _scale = 60;


        /// <summary>
        /// Singleton implementation
        /// </summary>
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

