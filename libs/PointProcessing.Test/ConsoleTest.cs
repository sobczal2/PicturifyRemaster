using System.Diagnostics;

using PicturifyRemaster.Core;
using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.Models.ProcessingParams;
using PicturifyRemaster.Core.Pipeline.Logging;
using PicturifyRemaster.PointProcessing.Grayscale;
using PicturifyRemaster.PointProcessing.Other;

using Serilog;
using Serilog.Core;

using Xunit.Abstractions;

namespace PicturifyRemaster.PointProcessing.Test;

public class ConsoleTest
{
    private readonly ITestOutputHelper _output;

    public ConsoleTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test()
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.XunitTestOutput(_output)
            .CreateLogger();
        Picturify.Instance.SetLogger(logger);
        Picturify.Instance.UseDecorator<TimerPipelineDecorator>();
        Picturify.Instance.UseDecorator<ProcessorLoggerPipelineDecorator>();
        var po = new ParallelOptions();
        po.MaxDegreeOfParallelism = 3;
        Picturify.Instance.ParallelOptions = po;
        var image = FastImageFactory.FromFile(@"D:\dev\dotnet\libs\picturify-examples\images.png");
        image.Process(new SepiaProcessor(intensity: new Intensity(1f)));
        image.Save(@"D:\dev\dotnet\libs\picturify-examples\test.png");
    }
}
