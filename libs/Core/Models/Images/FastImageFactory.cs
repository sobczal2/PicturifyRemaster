using System.Drawing;
using System.Reflection;
using PicturifyRemaster.Core.Models.Pixels;

namespace PicturifyRemaster.Core.Models.Images;

public static class FastImageFactory
{
    public static IFastImage Empty(Size pSize)
    {
        return new FastImage(pSize);
    }

    public static IFastImage FromImage(Image image)
    {
        return new FastImage(image);
    }

    public static IFastImage FromFile(string path)
    {
        return new FastImage(path);
    }

    public static IFastImage Random(Size size, Random? rand = null)
    {
        rand ??= new Random();
        var pixels = new IPixel[size.Width, size.Height];
        for (var h = 0; h < size.Height; h++)
        {
            for (var w = 0; w < size.Width; w++)
            {
                pixels[w, h] = new VRGBAPixel(rand.NextSingle() * 255f, 
                    rand.NextSingle() * 255f,
                    rand.NextSingle() * 255f,
                    rand.NextSingle() * 255f);
            }
        }

        return new FastImage(pixels);
    }

    public static IFastImage FromStream(Stream stream)
    {
        return new FastImage(stream);
    }
}