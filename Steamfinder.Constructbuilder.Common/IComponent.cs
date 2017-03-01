using System.Collections.Generic;

namespace Steamfinder.Constructbuilder.Common
{
    public interface IComponent
    {
        string Name { get; }
        string Description { get; }
        int Cost { get; }
        int Structure { get; }
        int Hardness { get; }
        int Weight { get; }
        Dictionary<string,double> Attributes { get; } 
    }
}