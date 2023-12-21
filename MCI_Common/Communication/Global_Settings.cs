using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Common.Communication
{

    static public class Global_Settings
    {
        
        static private int _nbRankChief = 2;
        static public int nbRankChief
        {
            get { return _nbRankChief; }
            set { _nbRankChief = value; }
        }
        

        static private int _nbServers = 2;
        static public int nbServers
        {
            get { return _nbServers; }
            set { _nbServers = value; }
        }

 
        static private int _nbCooks = 2;
        static public int nbCooks
        {
            get { return _nbCooks; }
            set { _nbCooks = value; }
        }
        
        static private int _nbCltPerShift = 50;
        static public int nbCltPerShift
        {
            get { return _nbCltPerShift; }
            set { _nbCltPerShift = value; }
        }

        
        static private int _timeMain = 30;
        static public int timeMain
        {
            get { return _timeMain; }
            set { _timeMain = value; }
        }


        static private int _timeStarter = 200;
        static public int timeStarter
        {
            get { return _timeStarter; }
            set { _timeStarter = value; }
        }


        static private int _timeDessert = 200;
        static public int timeDessert
        {
            get { return _timeDessert; }
            set { _timeDessert = value; }
        }




    }
}
