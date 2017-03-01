using System.Diagnostics.Eventing.Reader;

namespace Steamfinder.Common
{
    public class SizingBox
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }

        public double Largest
        {
            get
            {
                if (Height >= Length && Height >= Width)
                {
                    return Height;
                }
                if (Width > Length && Width > Height)
                {
                    return Width;
                }
                if (Length > Height && Length > Width)
                {
                    return Length;
                }
                return 0;
            }
        }

        public double Middle
        {
            get
            {
                if (Height >= Length && Height <= Width || Height < Length && Height > Width)
                {
                    return Height;
                }
                if (Width > Length && Width < Height || Width < Length && Width > Height)
                {
                    return Width;
                }
                if (Length > Height && Length < Width || Length < Height && Length > Width)
                {
                    return Length;
                }
                return 0;
            }
        }

        public double Smallest
        {
            get
            {
                if (Height <= Length && Height <= Width)
                {
                    return Height;
                }
                if (Width < Length && Width < Height)
                {
                    return Width;
                }
                if (Length < Height && Length < Width)
                {
                    return Length;
                }
                return 0;
            }
        }
    }
}