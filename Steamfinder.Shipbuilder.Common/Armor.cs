using System;
using Steamfinder.Common;

namespace Steamfinder.Shipbuilder.Common
{
    public class Armor
    {
        public Material Material { get; set; }
        public double ThicknessInches { get; set; }
        public int Structure { get { return (int)Math.Floor(ThicknessInches * Material.StructurePerInch); } }
        public double Weight { get { return ThicknessInches * Material.WeightPerInch; } }
    }
}