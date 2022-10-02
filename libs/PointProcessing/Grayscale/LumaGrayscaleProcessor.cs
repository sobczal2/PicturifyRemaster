using System.Numerics;

using PicturifyRemaster.Core.Models.ProcessingParams;
using PicturifyRemaster.PointProcessing.Common;

namespace PicturifyRemaster.PointProcessing.Grayscale;

public class LumaGrayscaleProcessor : PureTransformProcessorBase
{
    public LumaGrayscaleProcessor(Intensity? intensity = null, ChannelSelector? channelSelector = null) : base(new Matrix4x4(0.299f, 0.299f, 0.299f, 0, 0.587f, 0.587f, 0.587f, 0, 0.114f,
        0.114f, 0.114f, 0, 0, 0, 0, 1f), intensity, channelSelector)
    {
    }
}
