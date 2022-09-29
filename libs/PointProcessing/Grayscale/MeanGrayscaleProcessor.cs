using System.Numerics;
using PicturifyRemaster.Core;
using PicturifyRemaster.Core.Models.Pixels;
using PicturifyRemaster.Core.VirtualAccess;

namespace PicturifyRemaster.PointProcessing.Grayscale;

public class MeanGrayscaleProcessor : IProcessorBase
{
    public void Process(IPixel[,] pixels, IVirtualAccess virtualAccess)
    {
        var width = pixels.GetLength(0);
        var height = pixels.GetLength(1);
        var transpositionMatrix = new Matrix4x4(0.333f,0.333f,0.333f,0,0.333f,0.333f,0.333f,0,0.333f,0.333f,0.333f,0,0,0,0,1);
        Parallel.For(0, height, h =>
        {
            for (var w = 0; w < width; w++)
            {
                pixels[w, h].Transform(transpositionMatrix);
            }
        });
    }
}