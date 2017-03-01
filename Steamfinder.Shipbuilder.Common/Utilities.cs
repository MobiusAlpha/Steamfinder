using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steamfinder.Common;

namespace Steamfinder.Shipbuilder.Common
{
    public static class Utilities
    {
        public static double GetSurfaceArea(this SizingBox box, ShipShape shape, int decks = 0)
        {
            switch (shape)
            {
                case ShipShape.Boat:
                    return _boatSurfaceArea(box, decks);
                default:
                    return 0.0;
            }
        }

        public static bool[][][] GetSurfaceDiagram(this SizingBox box, ShipShape shape)
        {
            switch (shape)
            {
                case ShipShape.Boat:
                    return _boatSurfaceDiagram(box);
                default:
                    return new bool[][][] {};
            }
        }

        private static bool[][][] _boatSurfaceDiagram(SizingBox box)
        {
            bool[][][] volume = CreateJaggedArray<bool[][][]>((int) Math.Ceiling(box.Width), (int) Math.Ceiling(box.Height), (int) Math.Ceiling(box.Length));

            for (int x = 0; x < volume.Length; x++)
            {
                for (int y = 0; y < volume[x].Length; y++)
                {
                    for (int z = 0; z < volume[x][y].Length; z++)
                    {
                        if (z == 0)
                        {
                            if (y <= box.Height - box.Width/2)
                            {
                                volume[x][y][z] = true;
                            }
                            else
                            {
                                volume[x][y][z] = Math.Floor(Math.Sqrt(Math.Pow(x - box.Width / 2, 2) + Math.Pow(y - (box.Height - box.Width / 2), 2))) <= (box.Width / 2) ;
                            } 
                        }
                        else if (z > 0 && z <= box.Length - box.Width/2)
                        {
                            if (y <= box.Height - box.Width / 2)
                            {
                                volume[x][y][z] = x == 0 || x == volume.Length - 1;
                            }
                            else
                            {
                                volume[x][y][z] = Math.Abs(Math.Floor(Math.Sqrt(Math.Pow(x - box.Width / 2, 2) + Math.Pow(y - (box.Height - box.Width / 2), 2))) - Math.Floor(box.Width / 2)) < 0.5;
                            }
                        }
                        else
                        {
                            if (y <= box.Height - box.Width/2)
                            {
                                volume[x][y][z] = Math.Abs(Math.Floor(Math.Sqrt(Math.Pow(x - box.Width / 2, 2) + Math.Pow(z - (box.Length - box.Width / 2), 2))) - Math.Floor(box.Width / 2)) < 0.5;
                            }
                            else
                            {
                                volume[x][y][z] = Math.Abs(Math.Floor(Math.Sqrt(Math.Pow(x - box.Width / 2, 2) + Math.Pow(z - (box.Length - box.Width / 2), 2) + Math.Pow(y - (box.Height - box.Width / 2), 2))) - Math.Floor(box.Width / 2)) < 0.5;
                            }
                        }
                    }
                }
            }

            return volume;
        }

        static T CreateJaggedArray<T>(params int[] lengths)
        {
            return (T)InitializeJaggedArray(typeof(T).GetElementType(), 0, lengths);
        }

        static object InitializeJaggedArray(Type type, int index, int[] lengths)
        {
            Array array = Array.CreateInstance(type, lengths[index]);
            Type elementType = type.GetElementType();

            if (elementType != null)
            {
                for (int i = 0; i < lengths[index]; i++)
                {
                    array.SetValue(
                        InitializeJaggedArray(elementType, index + 1, lengths), i);
                }
            }

            return array;
        }

        private static double _boatSurfaceArea(SizingBox box, int decks)
        {

            double backend = Circle.Area(box.Width/2) + Rectangle.Area(Math.Max(box.Height - box.Width/2, 0), box.Width);
            double sides = 2 * Rectangle.Area(Math.Max(box.Height - box.Width/2, 0), Math.Max(box.Length - box.Width/2, 0));
            double front = Circle.Circumference(box.Width/2)/2 * Math.Max(box.Height - box.Width/2, 0);
            double bottom = front;
            double keelpoint = Sphere.SurfaceArea(box.Width/2)/4;

            double deck = Circle.Area(box.Width/2) + Rectangle.Area(Math.Max(box.Largest - box.Width/2, 0), box.Width);

            return backend + sides + front + bottom + keelpoint + (deck * decks);
        }

        public const double TALL_FLOORS = 8.5;
        public const double NORM_FLOORS = 7;
        public const double SHORT_FLOORS = 5.5;
    }

    public enum ShipShape
    {
        Boat
    }

    public static class Circle
    {
        public static double Area(double radius)
        {
            return Math.PI*Math.Pow(radius, 2);
        }

        public static double Circumference(double radius)
        {
            return 2*Math.PI*radius;
        }
    }

    public static class Sphere
    {
        public static double SurfaceArea(double radius)
        {
            return 4*Math.PI*Math.Pow(radius, 2);
        }
    }

    public static class Rectangle
    {
        public static double Area(double length, double height)
        {
            return length*height;
        }
    }
}
