using System.Collections.Generic;

namespace Steamfinder.Constructbuilder.Common
{
    public class ExteriorComponent : IPoweredComponent
    {
        public string Name { get; set; }
        public string Description { get; }
        public int Cost { get; }
        public int Structure { get; }
        public int Hardness { get; }
        public int Weight { get; }
        public Dictionary<string, double> Attributes { get; }
        public double WattageRequirement { get; set; }
        public ActivationType Activation { get; }
    }
}