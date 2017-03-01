using System.Collections.Generic;

namespace Steamfinder.Constructbuilder.Common
{
    public class InteriorComponent : IPoweredComponent
    {
        public string Name { get; }
        public string Description { get; }
        public double WattageRequirement { get; }
        public ActivationType Activation { get; }
        public int Cost { get; }
        public int Structure { get; }
        public int Hardness { get; }
        public int Weight { get; }
        public Dictionary<string, double> Attributes { get; }
    }
}