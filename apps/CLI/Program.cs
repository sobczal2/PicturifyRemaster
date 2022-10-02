using System.Numerics;

using PicturifyRemaster.Core;
using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.Pipeline.Logging;
using PicturifyRemaster.PointProcessing.Other;

using Serilog;

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Picturify.Instance.SetLogger(logger);
Picturify.Instance.UseDecorator<TimerPipelineDecorator>();
Picturify.Instance.UseDecorator<ProcessorLoggerPipelineDecorator>();
var image = FastImageFactory.FromFile(@"D:\dev\dotnet\libs\picturify-examples\images.jpg");
image.Process(new SepiaProcessor());
image.Save(@"D:\dev\dotnet\libs\picturify-examples\test.jpg");
