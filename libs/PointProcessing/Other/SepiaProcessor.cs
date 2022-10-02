using System.Numerics;

using PicturifyRemaster.Core.Models.ProcessingParams;
using PicturifyRemaster.PointProcessing.Common;

namespace PicturifyRemaster.PointProcessing.Other;

public class SepiaProcessor : PureTransformProcessorBase
{
    public SepiaProcessor(Intensity? intensity = null, ChannelSelector? channelSelector = null) : base(
        new Matrix4x4(0.393f, 0.349f, 0.272f, 0, 0.769f, 0.686f, 0.534f, 0, 0.189f, 0.168f, 0.131f, 0, 0, 0, 0, 1),
        intensity, channelSelector)
    {
    }
}
