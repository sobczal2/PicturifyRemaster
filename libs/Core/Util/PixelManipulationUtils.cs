namespace PicturifyRemaster.Core.Util;

public static class PixelManipulationUtils
{
    public static byte ToPixelByte(this float value)
    {
        return (byte)MathF.Min(255f, MathF.Max(0, value));
    }
}
