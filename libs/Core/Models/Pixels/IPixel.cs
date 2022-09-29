using System.Numerics;

namespace PicturifyRemaster.Core.Models.Pixels;

public interface IPixel
{
    public float Red { get; set; }
    public float Green { get; set; }
    public float Blue { get; set; }
    public float Alpha { get; set; }
    public byte ByteRed { get; set; }
    public byte ByteGreen { get; set; }
    public byte ByteBlue { get; set; }
    public byte ByteAlpha { get; set; }
    public void Transform(Matrix4x4 matrix);
}