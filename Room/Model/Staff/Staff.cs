using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCI_Common.Communication;
using MCI_Common.Behaviour;
using Room.Model.Behaviour;

namespace Room.Model.Staff
{
    public abstract class Staff : Movable
    {
        /// <summary>
        /// Name of the staff
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID of the staff
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Movement of the staff
        /// </summary>
        //private IMovable MoveBehaviour;

        /// <summary>
        /// Availability of the staff
        /// </summary>
        public bool Available { get; set; }


        public abstract void WhoAmI();
    }
}
