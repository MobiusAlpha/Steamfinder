using System;
using Steamfinder.Common;

namespace Steamfinder.Shipbuilder.Common
{
    public class ShipSegment
    {
        public Hull Hull { get; set; }
        public Armor Armor { get; set; }
        public SizingBox Box { get; set; }
        public Position<int> Position { get; set; }
        public double Weight { get { return (Hull.Weight+Armor.Weight)*SurfaceArea; } }
        public double Coverage { get; set; }
        public double SurfaceArea { get
        {
            return (2*Box.Height*Box.Width + 2*Box.Width*Box.Length + 2*Box.Length*Box.Height)*Coverage / 9;
        } }
        public int ArmorStructure { get { return (int) Math.Floor(Armor.Structure*SurfaceArea); } }
        public int HullStructure { get { return (int)Math.Floor(Hull.Structure * SurfaceArea); } }
    }
}