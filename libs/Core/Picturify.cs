using PicturifyRemaster.Core.Pipeline;
using PicturifyRemaster.Core.VirtualAccess;

using Serilog;
using Serilog.Core;

namespace PicturifyRemaster.Core;

public class Picturify
{
    private static Picturify? _instance;
    public static Picturify Instance => _instance ??= new Picturify();
    private IPipelineComponent _last = new FastImageExecutor();
    public ILogger Log { get; private set; } = Logger.None;
    public ParallelOptions ParallelOptions = new();

    private Picturify()
    {
    }
    
    public void SetLogger(ILogger logger)
    {
        Log = logger;
    }
    
    public void UseDecorator<T>()
        where T : PipelineDecoratorBase
    {
        _last = (IPipelineComponent)Activator.CreateInstance(typeof(T), _last);
    }

    public void ExecuteWithDecorators(PipelineContext pipelineContext)
    {
        _last.Execute(pipelineContext);
    }
}
