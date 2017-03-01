using System.Collections.Generic;

namespace Steamfinder.Constructbuilder.Common
{
    public class AuxPowerSource : InteriorComponent
    {
        public string Name { get; }
        public double WattageRequirement { get; }
        public ActivationType Activation { get; }
        public int Cost { get; }
        public int Structure { get; }
        public int Hardness { get; }
        public Dictionary<string, double> Attributes { get { return PowerSource.Attributes; } }
        public PowerSource PowerSource { get; }
    }
}