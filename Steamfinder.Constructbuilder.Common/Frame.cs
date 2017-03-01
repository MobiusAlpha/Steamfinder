using System;
using Steamfinder.Common;

namespace Steamfinder.Constructbuilder.Common
{
    public class Frame
    {
        public Material Material { get; set; }

        public double Volume
        {
            get { return Math.Pow(Box.Largest, 4)/(Box.Middle * 12); }
        }
        public SizingBox Box { get; set; }
        public int Hardness { get { return Material.Hardness; } }
        public int Structure { get { return (int) Math.Floor(Volume/12*Material.StructurePerInch); } }
        public int Weight { get { return (int) Math.Floor(Volume/12*Material.WeightPerInch); } }
        public int Cost { get { return (int) Math.Ceiling(Volume/12 * (Material.CostPerInch.Gold + Material.CostPerInch.Platinum * 10) / 1000); } }
    }
}