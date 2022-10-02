using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.Processing;

namespace PicturifyRemaster.Core.Pipeline;

public class FastImageExecutor : IPipelineComponent
{
    public void Execute(PipelineContext pipelineContext)
    {
        pipelineContext.FastImage.Execute(pipelineContext.ProcessorBase);
    }
}
