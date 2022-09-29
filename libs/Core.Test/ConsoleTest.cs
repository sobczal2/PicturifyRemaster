using System.Drawing;
using PicturifyRemaster.Core.Models.Images;

namespace PicturifyRemaster.Core.Test;

public class ConsoleTest
{
    [Fact]
    public void Test()
    {
        var image = FastImageFactory.Random(new Size(500, 500), Random.Shared);
        image.Save(@"C:\dev\dotnet\libs\picturify-examples\test.png");
    }
}