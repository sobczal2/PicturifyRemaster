using Xunit.Abstractions;

namespace PicturifyRemaster.Fourier.Test;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    [Fact]
    public void Test1()
    {
        // var logger = new LoggerConfiguration()
        //     .WriteTo.Console()
        //     .WriteTo.XunitTestOutput(_output)
        //     .CreateLogger();
        // Picturify.Instance.SetLogger(logger);
        // Picturify.Instance.UseDecorator<TimerPipelineDecorator>();
        // Picturify.Instance.UseDecorator<ProcessorLoggerPipelineDecorator>();
        // var image = FastImageFactory.FromFile(@"D:\dev\dotnet\libs\picturify-examples\images.jpg");
        // var kernel = new Pixel[301,301];
        // var value = 1f / (301 * 301);
        // for (var w = 0; w < 301; w++)
        // {
        //     for (var h = 0; h < 301; h++)
        //     {
        //         kernel[w, h] = new Pixel(value, value, value, value);
        //     }
        // }
        // image.Process(new ConvolutionProcessorBase(kernel, ChannelSelector.RGB));
        // image.Save(@"D:\dev\dotnet\libs\picturify-examples\test.jpg");
    }
}
