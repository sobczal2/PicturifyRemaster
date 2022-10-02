using System.Numerics;

namespace PicturifyRemaster.Core.Models.ProcessingParams;

public class Intensity
{
    private readonly float _value;

    public Intensity(float value)
    {
        if(value < 0 || value > 1)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 1");
        _value = value;
    }
    
    public Matrix4x4 GetMatrix(Matrix4x4 input)
    {
        var matrixM1 = Matrix4x4.Identity * (1f - _value);
        var matrixM2 = input * _value;
        return matrixM1 + matrixM2;
    }
}
