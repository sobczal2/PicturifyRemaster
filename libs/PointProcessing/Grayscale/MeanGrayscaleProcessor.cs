using System.Numerics;

using PicturifyRemaster.Core.Models.ProcessingParams;
using PicturifyRemaster.PointProcessing.Common;

namespace PicturifyRemaster.PointProcessing.Grayscale;

public class MeanGrayscaleProcessor : PureTransformProcessorBase
{
    public MeanGrayscaleProcessor(Intensity? intensity = null, ChannelSelector? channelSelector = null) : base(new Matrix4x4(0.333f, 0.333f, 0.333f, 0, 0.333f, 0.333f, 0.333f, 0, 0.333f,
        0.333f, 0.333f, 0, 0, 0, 0, 1), intensity, channelSelector)
    {
    }
}
