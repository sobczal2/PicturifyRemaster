using System.Drawing;

namespace PicturifyRemaster.Core.Models.Images;

public class FastImageData
{
    public Size Size { get; set; }
    public float[,] RedArray { get; set; }
    public float[,] GreenArray { get; set; }
    public float[,] BlueArray { get; set; }
    public float[,] AlphaArray { get; set; }

    public FastImageData(Size size)
    {
        Size = size;
        RedArray = new float[size.Width, size.Height];
        GreenArray = new float[size.Width, size.Height];
        BlueArray = new float[size.Width, size.Height];
        AlphaArray = new float[size.Width, size.Height];
    }
}
