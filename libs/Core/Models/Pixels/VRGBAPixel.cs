using System.Numerics;

namespace PicturifyRemaster.Core.Models.Pixels;

public class VRGBAPixel : IPixel
{
    private Vector4 _data;

    public VRGBAPixel()
    {
        _data = Vector4.Zero;
    }
    public VRGBAPixel(float red, float green, float blue, float alpha)
    {
        _data = new Vector4(red, green, blue, alpha);
    }

    public float Red
    {
        get => _data.X;
        set => _data.X = value;
    }
    public float Green
    {
        get => _data.Y;
        set => _data.Y = value;
    }
    public float Blue
    {
        get => _data.Z;
        set => _data.Z = value;
    }
    public float Alpha
    {
        get => _data.W;
        set => _data.W = value;
    }

    public byte ByteRed
    {
        get => (byte)MathF.Min(255f, MathF.Max(0, _data.X));
        set => _data.X = value;
    }

    public byte ByteGreen
    {
        get => (byte)MathF.Min(255f, MathF.Max(0, _data.Y));
        set => _data.X = value;
    }
    public byte ByteBlue
    {
        get => (byte)MathF.Min(255f, MathF.Max(0, _data.Z));
        set => _data.X = value;
    }
    public byte ByteAlpha
    {
        get => (byte)MathF.Min(255f, MathF.Max(0, _data.W));
        set => _data.X = value;
    }

    public void Transform(Matrix4x4 matrix)
    {
        _data = Vector4.Transform(_data, matrix);
    }
}