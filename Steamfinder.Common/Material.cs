using System.Threading;

namespace Steamfinder.Common
{
    public class Material
    {
        private static int MatId = 0;

        public Material()
        {
            MaterialId = Interlocked.Increment(ref MatId);
        }

        public int MaterialId { get; }
        public int Hardness { get; set; }
        public int StructurePerInch { get; set; }
        public double WeightPerInch { get; set; }
        public Currency CostPerInch { get; set; }
        public string Name { get; set; }
    }

    public static class Materials
    {
        public static Material Oak { get; } = new Material() { CostPerInch = new Currency(0, 1, 3, 5), Name = "Oak", WeightPerInch = 30.6, Hardness = 6, StructurePerInch = 12 };
        public static Material Teak { get; } = new Material() { CostPerInch = new Currency(0, 1, 2, 7), Name = "Teak", WeightPerInch = 31.5, Hardness = 8, StructurePerInch = 10 };
        public static Material Pine { get; } = new Material() { CostPerInch = new Currency(0, 1, 0, 2), Name = "Pine", WeightPerInch = 19.88, Hardness = 5, StructurePerInch = 10 };
        public static Material Balsa { get; } = new Material() { CostPerInch = new Currency(0, 9, 4, 1), Name = "Balsa", WeightPerInch = 6, Hardness = 3, StructurePerInch = 5 };
        public static Material Ebony { get; } = new Material() { CostPerInch = new Currency(0, 11, 6, 2), Name = "Ebony", WeightPerInch = 62.25, Hardness = 9, StructurePerInch = 18 };
        public static Material Steel { get; } = new Material() { CostPerInch = new Currency(0, 8, 1, 8) * 9, Name = "Steel", WeightPerInch = 337, Hardness = 12, StructurePerInch = 30};
        public static Material Iron { get; } = new Material() { CostPerInch = new Currency(0, 3, 0, 2) * 9, Name = "Iron", WeightPerInch = 389, Hardness = 9, StructurePerInch = 27 };
        public static Material Copper { get; } = new Material() { CostPerInch = new Currency(0, 1, 4, 0) * 9, Name = "Copper", WeightPerInch = 419, Hardness = 6, StructurePerInch = 20 };
        public static Material None { get; } = new Material() { CostPerInch = new Currency(0, 0,0, 0) * 9, Name = "None", WeightPerInch = 0, Hardness = 0, StructurePerInch = 0 };
    }
}