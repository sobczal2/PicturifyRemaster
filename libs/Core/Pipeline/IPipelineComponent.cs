namespace PicturifyRemaster.Core.Pipeline;

public interface IPipelineComponent
{
    void Execute(PipelineContext pipelineContext);
}
