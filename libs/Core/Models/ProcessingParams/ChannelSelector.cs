namespace PicturifyRemaster.Core.Models.ProcessingParams;

public class ChannelSelector
{
    public bool Red { get; set; }
    public bool Green { get; set; }
    public bool Blue { get; set; }
    public bool Alpha { get; set; }

    public ChannelSelector(bool red, bool green, bool blue, bool alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public static ChannelSelector RGB => new ChannelSelector(true, true, true, false);
}
