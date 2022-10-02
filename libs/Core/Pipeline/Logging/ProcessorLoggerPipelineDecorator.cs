namespace PicturifyRemaster.Core.Pipeline.Logging;

public class ProcessorLoggerPipelineDecorator : PipelineDecoratorBase
{
    public ProcessorLoggerPipelineDecorator(IPipelineComponent wrappee) : base(wrappee)
    {
    }

    public override void Execute(PipelineContext pipelineContext)
    {
        Picturify.Instance.Log.Information($"{pipelineContext.ProcessorBase.GetType().Name} started.");
        base.Execute(pipelineContext);
        Picturify.Instance.Log.Information($"{pipelineContext.ProcessorBase.GetType().Name} exited.");
    }
}
