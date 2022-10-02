using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.Pipeline;
using PicturifyRemaster.Core.Processing;

namespace PicturifyRemaster.Core;

public static class PicturifyExtensions
{
    public static void Process(this IFastImage image, IProcessorBase processorBase)
    {
        Picturify.Instance.ExecuteWithDecorators(new PipelineContext(image, processorBase));
    }
}
