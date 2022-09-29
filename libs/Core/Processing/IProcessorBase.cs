using PicturifyRemaster.Core.Models.Pixels;
using PicturifyRemaster.Core.VirtualAccess;

namespace PicturifyRemaster.Core;

public interface IProcessorBase
{
    public void Process(IPixel[,] pixels, IVirtualAccess virtualAccess);
}