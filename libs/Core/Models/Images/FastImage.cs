using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using PicturifyRemaster.Core.Models.Pixels;
using PicturifyRemaster.Core.VirtualAccess;

namespace PicturifyRemaster.Core.Models.Images;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility",
    Justification = "This is a Windows only application.")]
public class FastImage : IFastImage
{
    private IPixel[,] _pixels;
    private Size _size;
    private IVirtualAccess _virtualAccess;

    internal FastImage(Size size)
    {
        InitializePixels(new IPixel[size.Width, size.Height]);
    }

    internal FastImage(IPixel[,] pixels)
    {
        InitializePixels(pixels);
    }

    internal FastImage(Image image)
    {
        InitializePixels(new IPixel[image.Width,image.Height]);
        var bitmap = new Bitmap(image);
        var widthInBytes = _size.Width * 4;
        var arr = new byte[widthInBytes * _size.Height];
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, _size.Width, _size.Height), ImageLockMode.ReadOnly,
            bitmap.PixelFormat);
        var ptr = bitmapData.Scan0;
        Marshal.Copy(ptr, arr, 0, arr.Length);
        bitmap.UnlockBits(bitmapData);
        Parallel.For(0, _size.Height, h =>
        {
            for (var w = 0; w < _size.Width; w++)
            {
                _pixels![w, h] = new VRGBAPixel();
                _pixels[w, h].Red = arr[h * widthInBytes + w * 4 + 0];
                _pixels[w, h].Green = arr[h * widthInBytes + w * 4 + 1];
                _pixels[w, h].Blue = arr[h * widthInBytes + w * 4 + 2];
                _pixels[w, h].Alpha = arr[h * widthInBytes + w * 4 + 3];
            }
        });
    }

    internal FastImage(Stream stream) : this(Image.FromStream(stream))
    {
    }

    internal FastImage(string path) : this(Image.FromFile(path))
    {
    }

    public void Save(Stream stream, ImageFormat format)
    {
        var bitmap = GetBitmap(CancellationToken.None);
        bitmap.Save(stream, format);
    }

    public void Save(string path)
    {
        var bitmap = GetBitmap(CancellationToken.None);
        var format = GetImageFormat(path);
        bitmap.Save(path, format);
    }

    public void Execute(IProcessorBase processorBase)
    {
        processorBase.Process(_pixels, _virtualAccess);
    }
    
    private void InitializePixels(IPixel[,] pixels)
    {
        _pixels = pixels;
        _size = new Size(pixels.GetLength(0), pixels.GetLength(1));
        _virtualAccess = new BlackVirtualAccess(_pixels);
    }

    private Bitmap GetBitmap(CancellationToken cancellationToken)
    {
        var widthInBytes = _size.Width * 4;
        var arr = new byte[widthInBytes * _size.Height];
        var po = new ParallelOptions();
        po.CancellationToken = cancellationToken;
        Parallel.For(0, _size.Height, po, h =>
        {
            for (var w = 0; w < _size.Width; w++)
            {
                arr[h * widthInBytes + w * 4 + 0] = _pixels[w, h].ByteRed;
                arr[h * widthInBytes + w * 4 + 1] = _pixels[w, h].ByteGreen;
                arr[h * widthInBytes + w * 4 + 2] = _pixels[w, h].ByteBlue;
                arr[h * widthInBytes + w * 4 + 3] = _pixels[w, h].ByteAlpha;
            }
        });
        var bitmap = new Bitmap(_size.Width, _size.Height);
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly,
            bitmap.PixelFormat);
        var ptr = bitmapData.Scan0;
        Marshal.Copy(arr, 0, ptr, arr.Length);
        bitmap.UnlockBits(bitmapData);
        return bitmap;
    }

    private ImageFormat GetImageFormat(string path)
    {
        ImageFormat format;
        switch (Path.GetExtension(path))
        {
            case ".jpg":
                format = ImageFormat.Jpeg;
                break;
            case ".jpeg":
                format = ImageFormat.Jpeg;
                break;
            case ".png":
                format = ImageFormat.Png;
                break;
            case ".tiff":
                format = ImageFormat.Tiff;
                break;
            case ".bmp":
                format = ImageFormat.Bmp;
                break;
            case ".gif":
                format = ImageFormat.Gif;
                break;
            case ".icon":
                format = ImageFormat.Icon;
                break;
            case ".ico":
                format = ImageFormat.Icon;
                break;
            default:
                format = ImageFormat.Png;
                break;
        }

        return format;
    }
}