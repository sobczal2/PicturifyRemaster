using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.VirtualAccess;

namespace PicturifyRemaster.Core.Processing;

public interface IProcessorBase
{
    public void Process(FastImageData fastImageData, IVirtualAccess virtualAccess);
}
