using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace PlanAndRide.BusinessLogic
{
    class Route
    {
        public string Name { get; set; }
        public double StartingPosition { get; set; } 
        public double DestinationPosition { get; set; } 
        public string Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public Review Review { get; set; }

    }
}
