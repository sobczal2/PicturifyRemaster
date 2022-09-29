using System.Drawing;
using PicturifyRemaster.Core.Models.Pixels;

namespace PicturifyRemaster.Core.VirtualAccess;

public class BlackVirtualAccess : IVirtualAccess
{
    private IPixel[,] _pixels;
    private Size _size;
    public BlackVirtualAccess(IPixel[,] pixels)
    {
        _pixels = pixels;
        _size = new Size(pixels.GetLength(0), pixels.GetLength(1));
    }

    public IPixel GetPixel(int x, int y)
    {
        return x < _size.Width && x >= 0 && y < _size.Width && y >= 0 ? _pixels[x, y] : new VRGBAPixel();
    }
}