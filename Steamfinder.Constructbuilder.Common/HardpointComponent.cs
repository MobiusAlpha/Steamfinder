using System.Collections.Generic;
using RogueSharp.DiceNotation;

namespace Steamfinder.Constructbuilder.Common
{
    public class HardpointComponent : IPoweredComponent
    {
        public string Name { get; }
        public string Description { get; }
        public double WattageRequirement { get; }
        public ActivationType Activation { get; }
        public IEnumerable<HardpointType> Requirements { get; }
        public int Cost { get; }
        public int Structure { get; }
        public int Hardness { get; }
        public int Weight { get; }
        public Dictionary<string, double> Attributes { get; }
    }

    public class WeaponComponent : HardpointComponent
    {
        public DiceExpression DamageDice { get; }
        public int CriticalMultiplier { get; }
        public int RateOfFire { get; }
        public IEnumerable<Ammunition> UsableAmmo { get; }
        public IEnumerable<WeaponAccessoryComponent> Accessories { get; }
    }

    public class WeaponAccessoryComponent : AccessoryComponent
    {
        
    }

    public class Ammunition
    {
        
    }
}