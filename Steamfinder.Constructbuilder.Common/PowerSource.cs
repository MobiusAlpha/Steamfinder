using System.Collections.Generic;

namespace Steamfinder.Constructbuilder.Common
{
    public class PowerSource : IComponent
    {
        public double SupplyWattage { get; }
        public string Name { get; }
        public string Description { get; }
        public int Cost { get; }
        public int Structure { get; }
        public int Hardness { get; }
        public int Weight { get; }
        public Dictionary<string, double> Attributes { get; }
    }
}