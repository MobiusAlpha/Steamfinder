using System.Collections.Generic;

namespace Steamfinder.Constructbuilder.Common
{
    public class HardpointProviderComponent : ExteriorComponent
    {
        public IEnumerable<HardpointType> Provided { get; }
    }
}