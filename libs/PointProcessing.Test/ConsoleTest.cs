using System.Diagnostics;
using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.PointProcessing.Grayscale;
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
        var image = FastImageFactory.FromFile(@"C:\dev\dotnet\libs\picturify-examples\images.jpg");
        var sw = new Stopwatch();
        sw.Start();
        image.Execute(new MeanGrayscaleProcessor());
        sw.Stop();
        _output.WriteLine($"Processing took: {sw.ElapsedTicks} ticks({sw.ElapsedMilliseconds} ms).");
        image.Save(@"C:\dev\dotnet\libs\picturify-examples\test.png");
    }
}