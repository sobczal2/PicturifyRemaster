using PicturifyRemaster.Core.Models.Pixels;

namespace PicturifyRemaster.Core.VirtualAccess;

public interface IVirtualAccess
{
    IPixel GetPixel(int x, int y);
}