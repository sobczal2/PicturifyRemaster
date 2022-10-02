namespace PicturifyRemaster.Core.Pipeline;

public class PipelineDecoratorBase : IPipelineComponent
{
    private readonly IPipelineComponent _wrappee;

    public PipelineDecoratorBase(IPipelineComponent wrappee)
    {
        _wrappee = wrappee;
    }

    public virtual void Execute(PipelineContext pipelineContext)
    {
        _wrappee.Execute(pipelineContext);
    }
}
