using System.Drawing;

using PicturifyRemaster.Core.Models.Images;

namespace PicturifyRemaster.Core.VirtualAccess;

public class BlackVirtualAccess : IVirtualAccess
{
    private FastImageData _fastImageData;

    public BlackVirtualAccess(FastImageData fastImageData)
    {
        _fastImageData = fastImageData;
    }

    public float GetRed(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _fastImageData.Size.Width || y >= _fastImageData.Size.Width)
            return 0;
        return _fastImageData.RedArray[x, y];
    }

    public float GetGreen(int x, int y)
    {
        if(x < 0 || y < 0 || x >= _fastImageData.Size.Width || y >= _fastImageData.Size.Width)
            return 0;
        return _fastImageData.RedArray[x, y];
    }

    public float GetBlue(int x, int y)
    {
        if(x < 0 || y < 0 || x >= _fastImageData.Size.Width || y >= _fastImageData.Size.Width)
            return 0;
        return _fastImageData.RedArray[x, y];
    }

    public float GetAlpha(int x, int y)
    {
        if(x < 0 || y < 0 || x >= _fastImageData.Size.Width || y >= _fastImageData.Size.Width)
            return 0;
        return _fastImageData.RedArray[x, y];
    }
}
