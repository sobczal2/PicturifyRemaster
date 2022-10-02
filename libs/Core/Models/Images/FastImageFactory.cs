using System.Drawing;

namespace PicturifyRemaster.Core.Models.Images;

public static class FastImageFactory
{
    public static IFastImage Empty(Size pSize)
    {
        return new FastImage(pSize);
    }

    public static IFastImage FromImageData(FastImageData fastImageData)
    {
        return new FastImage(fastImageData);
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
        var imageData = new FastImageData(size);
        for (var h = 0; h < size.Height; h++)
        for (var w = 0; w < size.Width; w++)
        {
            imageData.AlphaArray[w, h] = rand.NextSingle() * 255f;
            imageData.AlphaArray[w, h] = rand.NextSingle() * 255f;
            imageData.AlphaArray[w, h] = rand.NextSingle() * 255f;
            imageData.AlphaArray[w, h] = rand.NextSingle() * 255f;
        }

        return new FastImage(imageData);
    }

    public static IFastImage FromStream(Stream stream)
    {
        return new FastImage(stream);
    }
}
