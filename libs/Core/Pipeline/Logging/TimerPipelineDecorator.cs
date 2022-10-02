using System.Diagnostics;

namespace PicturifyRemaster.Core.Pipeline.Logging;

public class TimerPipelineDecorator : PipelineDecoratorBase
{
    public TimerPipelineDecorator(IPipelineComponent wrappee) : base(wrappee)
    {
    }

    public override void Execute(PipelineContext pipelineContext)
    {
        var stopwatch = Stopwatch.StartNew();
        base.Execute(pipelineContext);
        stopwatch.Stop();
        Picturify.Instance.Log.Information($"{nameof(TimerPipelineDecorator).Replace("Decorator", "")}: {pipelineContext.ProcessorBase.GetType().Name} took {stopwatch.ElapsedMilliseconds} ms.");
    }
}
