using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steamfinder.Common;
using Steamfinder.Shipbuilder.Common;

namespace Steamfinder.Console
{
    public static class HullCostProgram
    {
        public static void Main()
        {
            Material hullMaterial = Materials.Pine;
            Material armorMaterial = Materials.Oak;
            SizingBox box = new SizingBox() { Length = 35, Height = 12.5, Width = 15 };
            double segWidth = box.Width / Math.Ceiling(box.Width / 10.0);
            double segHeight = box.Height / Math.Ceiling(box.Height / 10.0);
            double segLength = box.Length / Math.Ceiling(box.Length / 10.0);

            int xSegs = (int)Math.Ceiling(box.Width / segWidth);
            int ySegs = (int)Math.Ceiling(box.Height / segHeight);
            int ZSegs = (int)Math.Ceiling(box.Length / segLength);

            int totalSegs = xSegs * ySegs * ZSegs;

            int intSegs = Math.Max(xSegs - 2, 0) * Math.Max(ySegs - 2, 0) * Math.Max(ZSegs - 2, 0);
            int extSegs = totalSegs - intSegs;

            double avgSegCvg = segLength * segHeight * segLength / 1000;

            double extSegCvg = .95 / extSegs * avgSegCvg;
            double intSegCvg = .05 / intSegs * avgSegCvg;

            Ship ship = new Ship();
            List<ShipSegment> segments = new List<ShipSegment>();
            ship.Segments = segments;

            double wallThickness = 2;
            double armorThickness = 2;

            for (int x = 0; x < (box.Width / segWidth); x++)
            {
                for (int y = 0; y < (box.Height / segHeight); y++)
                {
                    for (int z = 0; z < (box.Length / segLength); z++)
                    {
                        int edgeCount = new bool[] { x == 0, x == xSegs - 1, y == ySegs - 1, y == 0, z == ZSegs - 1, z == 0 }.Length;

                        segments.Add(new ShipSegment()
                        {
                            Armor = new Armor()
                            {
                                Material = edgeCount > 0 ? armorMaterial : Materials.None,
                                ThicknessInches = armorThickness,
                            },
                            Box = new SizingBox()
                            {
                                Height = segHeight,
                                Length = segLength,
                                Width = segWidth
                            },
                            
                            Coverage = edgeCount > 0 ? extSegCvg : intSegCvg,
                            Hull = new Hull()
                            {
                                Material = hullMaterial,
                                ThicknessInches = wallThickness
                            },
                            Position = new Position<int>() { X = x, Y = y, Z = z }
                        });
                    }
                }
            }

            double sqYdg = Utilities.GetSurfaceArea(box, ShipShape.Boat, 1) / 9;
            int primaryCraftsman = 7;
            int secondaryCraftsman = 2;
            int secondaryCount = 3;
            double craftPerWeek = (15 + primaryCraftsman + secondaryCraftsman * secondaryCount) * 20;
            int timeMult = 2;
            int weeks = (int)Math.Ceiling((hullMaterial.CostPerInch.AsGold()*sqYdg*wallThickness*10.0D + armorMaterial.CostPerInch.AsGold()*sqYdg*armorThickness)/(craftPerWeek*timeMult));
            Currency laborCost = (new Currency(0, 1, 2, 0) * secondaryCount * Math.Sqrt(secondaryCraftsman/2) + new Currency(0,2, 0, 0) * Math.Sqrt(primaryCraftsman/5)) * weeks * Math.Pow(timeMult, 3/2);
            Currency hullCost = hullMaterial.CostPerInch * sqYdg * wallThickness;
            Currency armorCost = armorMaterial.CostPerInch * sqYdg * armorThickness;
            System.Console.WriteLine($"{box.Length}Lx{box.Width}Wx{box.Height}H");
            System.Console.WriteLine($"Board Feet: {sqYdg * wallThickness * 9 }, Cu Ft Steel: {sqYdg * wallThickness * 9 / 12}");
            System.Console.WriteLine($"Cost: \nWood {hullCost}, \nSteel {armorCost}, \nLabor {laborCost}, \nTotal {laborCost + hullCost + armorCost}");
            System.Console.WriteLine($"Structure: Hull {Math.Floor(sqYdg * hullMaterial.StructurePerInch * wallThickness)} pts, Armor {Math.Floor(sqYdg * armorMaterial.StructurePerInch * armorThickness)}");
            System.Console.WriteLine($"Weight: {Math.Ceiling(sqYdg * hullMaterial.WeightPerInch * wallThickness + sqYdg * armorMaterial.WeightPerInch * armorThickness)} lb");
            System.Console.WriteLine($"Build Time: {weeks } weeks");
            System.Console.WriteLine(string.Concat(Enumerable.Repeat('-',System.Console.BufferWidth)));
        }
    }

    public static class SegmentBreakerProgram
    {
        public static void Main()
        {
            int armorThickness = 2;
            int hullThickness = 6;

            SizingBox box = new SizingBox() { Length = 65, Height = 45, Width = 25 };
            double segWidth = box.Width/Math.Ceiling(box.Width/10.0);
            double segHeight = box.Height / Math.Ceiling(box.Height / 10.0);
            double segLength = box.Length / Math.Ceiling(box.Length / 10.0);

            int xEdgeSegs = (int)(box.Width / segWidth);
            int yEdgeSegs = (int)(box.Height / segHeight);
            int zEdgeSegs = (int)(box.Length / segLength);

            int edgeSegs = Math.Max(2*((xEdgeSegs - 1)*(yEdgeSegs - 1)), 0) + Math.Max(2*((xEdgeSegs -1)* (zEdgeSegs - 1)), 0) + Math.Max(2 * ((yEdgeSegs - 1) * (zEdgeSegs - 1)), 0);
            int totalSegs = xEdgeSegs*yEdgeSegs*zEdgeSegs;

            double avgSegCvg = segLength * segHeight * segLength / 1000;

            Ship ship = new Ship();
            List<ShipSegment> segments = new List<ShipSegment>();
            ship.Segments = segments;

            for (int x = 0; x < (box.Width/segWidth); x++)
            {
                for (int y = 0; y < (box.Height / segHeight); y++)
                {
                    for (int z = 0; z < (box.Length / segLength); z++)
                    {
                        int edgeCount = new bool[] { x == 0 , x == xEdgeSegs-1 , y == yEdgeSegs-1 , y == 0 , z== zEdgeSegs-1 , z == 0}.Length;

                        segments.Add(new ShipSegment()
                        {
                            Armor = new Armor()
                            {
                                Material = edgeCount > 1 ? Materials.Steel : Materials.None,
                                ThicknessInches = armorThickness,
                            },
                            Box = new SizingBox()
                            {
                                Height = segHeight,
                                Length = segLength,
                                Width = segWidth
                            },
                            Coverage = avgSegCvg,
                            Hull = new Hull()
                            {
                                Material = edgeCount > 1 ? Materials.Oak : Materials.None,
                                ThicknessInches = hullThickness
                            },
                            Position = new Position<int>() { X = x, Y = y, Z = z}
                        });
                    }
                }
            }

            double steelVolume = ship.GetMaterialVolume(Materials.Steel);
            double oakVolume = ship.GetMaterialVolume(Materials.Oak);
            Currency cost = Materials.Steel.CostPerInch*(steelVolume*12) + Materials.Oak.CostPerInch*(oakVolume*12);
            double craftPerWeek = (15 + 13 + 2 * 3) * 20;

            System.Console.WriteLine($"{box.Length}Lx{box.Width}Wx{box.Height}H");
            System.Console.WriteLine($"Volume: (Oak) { oakVolume * 9 * 12 } bd. ft., (Steel) { steelVolume * 9 } cu. ft.");
            System.Console.WriteLine($"Cost: { cost }");
            System.Console.WriteLine($"Structure: (Armor) {ship.TotalArmorStructure} pts, (Hull) {ship.TotalHullStructure} pts");
            System.Console.WriteLine($"Weight: { ship.TotalWeight } lb");
            System.Console.WriteLine($"Build Time: {Math.Ceiling(cost.AsGold() * 10.0D / craftPerWeek)} weeks");
            System.Console.WriteLine($"Segments: {ship.Segments.Count()}");
            System.Console.WriteLine($"Avg Segment Structure: {ship.Segments.Average(segment => segment.ArmorStructure + segment.HullStructure)}");
            System.Console.WriteLine(string.Concat(Enumerable.Repeat('-', System.Console.BufferWidth)));
        }
    }

    public static class ExtParamProgram
    {
        public static void Main()
        {
            SizingBox box = new SizingBox() { Length = 60, Height = 30, Width = 25 };

            bool[][][] surfaceMap = Utilities.GetSurfaceDiagram(box, ShipShape.Boat);

            System.Console.WriteLine($"SqFt: {surfaceMap.SelectMany(x => x.SelectMany(y => y.Select(z => z))).Count(x => x)}");

            double segWidth = box.Width / Math.Ceiling(box.Width / 10.0);
            double segHeight = box.Height / Math.Ceiling(box.Height / 10.0);
            double segLength = box.Length / Math.Ceiling(box.Length / 10.0);

            double segBase = 2*segLength*segHeight + 2*segLength*segWidth + 2*segHeight + segWidth;

            List<List<List<double>>> segVals = new List<List<List<double>>>();

            var valBreak = surfaceMap.SelectMany((xv, xi) => xv.SelectMany((yv, yi) => yv.Select((zv, zi) => new { X = xi, Y = yi, Z = zi, Value = zv })));

            var groups = valBreak.GroupBy(p => new {SegX = (int)(p.X/segWidth), SegY = (int)(p.Y/segHeight), SegZ = (int)(p.Z/segLength)}).Select(p => new {p.Key.SegX, p.Key.SegY, p.Key.SegZ, Value = p.Count(x => x.Value), Prop = p.Count(x => x.Value) / segBase});
        }
    }

    public static class ComboProgram
    {
        public static void Main()
        {
            //ExtParamProgram.Main();
            SizingBox box = new SizingBox() { Length = 30, Height = 30, Width = 30 };

            double segWidth = box.Width / Math.Ceiling(box.Width / 10.0);
            double segHeight = box.Height / Math.Ceiling(box.Height / 10.0);
            double segLength = box.Length / Math.Ceiling(box.Length / 10.0);

            int xSegs = (int) Math.Ceiling(box.Width/ segWidth);
            int ySegs = (int)Math.Ceiling(box.Height / segHeight);
            int ZSegs = (int)Math.Ceiling(box.Length / segLength);

            int totalSegs = xSegs*ySegs*ZSegs;

            int intSegs = Math.Max(xSegs - 2, 0)*Math.Max(ySegs - 2, 0)*Math.Max(ZSegs - 2, 0);
            int extSegs = totalSegs - intSegs;
            
            System.Console.ReadLine();
        }
    }
}
