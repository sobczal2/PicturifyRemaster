using System.Drawing.Imaging;

using PicturifyRemaster.Core.Processing;

namespace PicturifyRemaster.Core.Models.Images;

public interface IFastImage
{
    public void Save(Stream stream, ImageFormat format);
    public void Save(string path);
    void Execute(IProcessorBase processorBase);
}
