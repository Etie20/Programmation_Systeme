using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Timer
{
    public class Randomizer
    {
        /// <summary>
        /// Singleton variables (thread-safe method)
        /// </summary>
        private static Randomizer instance = null;
        private static readonly object padlock = new object();

        public Random R { get; }

        Randomizer()
        {
            R = new Random();
        }

        /// <summary>
        /// Singleton implementation
        /// </summary>
        public static Randomizer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Randomizer();
                    }
                    return instance;
                }
            }
        }
    }
}
