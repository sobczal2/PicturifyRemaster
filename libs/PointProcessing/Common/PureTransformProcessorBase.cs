using System.Numerics;

using PicturifyRemaster.Core;
using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.Models.ProcessingParams;
using PicturifyRemaster.Core.Processing;
using PicturifyRemaster.Core.VirtualAccess;

namespace PicturifyRemaster.PointProcessing.Common;

public abstract class PureTransformProcessorBase : IProcessorBase
{
    private readonly Matrix4x4 _transpositionMatrix;
    private readonly ChannelSelector _channelSelector;

    public PureTransformProcessorBase(Matrix4x4 transpositionMatrix, Intensity? intensity = null,
        ChannelSelector? channelSelector = null)
    {
        intensity ??= new Intensity(1);
        _channelSelector = channelSelector ?? ChannelSelector.RGB;
        _transpositionMatrix = intensity.GetMatrix(transpositionMatrix);
    }

    public void Process(FastImageData imageData, IVirtualAccess virtualAccess)
    {
        var width = imageData.Size.Width;
        var height = imageData.Size.Height;
        Parallel.For(0, width, Picturify.Instance.ParallelOptions, w => {
            for (var h = 0; h < height; h++)
            {
                var red = imageData.RedArray[w, h];
                var green = imageData.GreenArray[w, h];
                var blue = imageData.BlueArray[w, h];
                var alpha = imageData.AlphaArray[w, h];
                if (_channelSelector.Red)
                    imageData.RedArray[w, h] = red * _transpositionMatrix.M11 +
                                              green * _transpositionMatrix.M21 +
                                              blue * _transpositionMatrix.M31 +
                                              imageData.AlphaArray[w, h] * _transpositionMatrix.M41;
                if (_channelSelector.Green)
                    imageData.GreenArray[w, h] = red * _transpositionMatrix.M12 +
                                                green * _transpositionMatrix.M22 +
                                                blue * _transpositionMatrix.M32 +
                                                imageData.AlphaArray[w, h] * _transpositionMatrix.M42;
                if (_channelSelector.Blue)
                    imageData.BlueArray[w, h] = red * _transpositionMatrix.M13 +
                                               green * _transpositionMatrix.M23 +
                                               blue * _transpositionMatrix.M33 +
                                               imageData.AlphaArray[w, h] * _transpositionMatrix.M43;
                if (_channelSelector.Alpha)
                    imageData.AlphaArray[w, h] = red * _transpositionMatrix.M14 +
                                                green * _transpositionMatrix.M24 +
                                                blue * _transpositionMatrix.M34 +
                                                alpha * _transpositionMatrix.M44;
            }
        });
    }
}
