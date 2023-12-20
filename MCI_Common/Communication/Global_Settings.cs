using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Communication
{
    /// <summary>
    /// Application settings
    /// </summary>
    static public class Global_Settings
    {
        // Put here everything you can modify in controller

        //Staff population

        /// <summary>
        /// Number of rank chiefs (default = 2)
        /// </summary>
        static private int _nbRankChief = 2;
        static public int nbRankChief
        {
            get { return _nbRankChief; }
            set { _nbRankChief = value; }
        }
        

        /// <summary>
        /// Number of waiters (default = 2)
        /// </summary>
        static private int _nbServers = 2;
        static public int nbServers
        {
            get { return _nbServers; }
            set { _nbServers = value; }
        }

        /// <summary>
        /// Number of cooks (default = 2)
        /// </summary>
        static private int _nbCooks = 2;
        static public int nbCooks
        {
            get { return _nbCooks; }
            set { _nbCooks = value; }
        }

        //Client population

        /// <summary>
        /// Number of clients per shift
        /// </summary>
        static private int _nbCltPerShift = 10;
        static public int nbCltPerShift
        {
            get { return _nbCltPerShift; }
            set { _nbCltPerShift = value; }
        }


        // Time for each tasks

        /// <summary>
        /// Time to eat meal
        /// </summary>
        static private int _timeMain = 30;
        static public int timeMain
        {
            get { return _timeMain; }
            set { _timeMain = value; }
        }

        /// <summary>
        /// Time to eat starter
        /// </summary>
        static private int _timeStarter = 200;
        static public int timeStarter
        {
            get { return _timeStarter; }
            set { _timeStarter = value; }
        }

        /// <summary>
        /// Time to eat dessert
        /// </summary>
        static private int _timeDessert = 200;
        static public int timeDessert
        {
            get { return _timeDessert; }
            set { _timeDessert = value; }
        }

        /// <summary>
        /// Number of clients per shift
        /// </summary>
        //static private int _nbCltPerShift = 200;
        //static public int nbCltPerShift
        //{
        //    get { return _nbCltPerShift; }
        //    set { _nbCltPerShift = value; }
        //}





    }
}
