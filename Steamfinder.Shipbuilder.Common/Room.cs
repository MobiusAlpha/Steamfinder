using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steamfinder.Shipbuilder.Common
{
    class Room
    {
        private Dictionary<CrewType, int> CrewRequirement { get; set; }
        public int CrewCapacity { get; set; }

    }
}
/*needs:
specialist crew required
crew capacity
normal crew requirement
cost to acquire
cost to maintain
weight
# of spaces
any special costs
bonuses to ship
individual structure if applicable
name
contents
description of room
eligible upgrades
acquired upgrades
*/