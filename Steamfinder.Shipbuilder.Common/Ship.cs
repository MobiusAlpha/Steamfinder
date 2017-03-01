using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamfinder.Common;

namespace Steamfinder.Shipbuilder.Common
{
    public class Ship
    {
        public IEnumerable<ShipSegment> Segments;
        public int TotalHullStructure { get { return Segments.Sum(segment => segment.HullStructure); } }
        public int TotalArmorStructure { get { return Segments.Sum(segment => segment.ArmorStructure); } }
        public IEnumerable<string> Materials
        {
            get
            {
                return
                    Segments.Select(segment => segment.Hull.Material.Name)
                        .Union(Segments.Select(segment => segment.Armor.Material.Name)).Distinct();
            }
        }
        public int TotalWeight { get { return (int)Math.Ceiling(Segments.Sum(segment => segment.Weight)); } }
        
        public double GetMaterialVolume(Material material)
        {
            return Segments.Sum(segment =>
                segment.SurfaceArea *
                (
                    (segment.Armor.Material.MaterialId == material.MaterialId
                        ? segment.Armor.ThicknessInches/(double) 12
                        : 0.0) +
                    (segment.Hull.Material.MaterialId == material.MaterialId
                        ? segment.Hull.ThicknessInches/(double) 12
                        : 0.0)));
        }
    }
}
