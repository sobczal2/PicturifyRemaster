using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using PicturifyRemaster.Core.Processing;
using PicturifyRemaster.Core.Util;
using PicturifyRemaster.Core.VirtualAccess;

namespace PicturifyRemaster.Core.Models.Images;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility",
    Justification = "This is a Windows only application.")]
public class FastImage : IFastImage
{
    private FastImageData _fastImageData;
    private IVirtualAccess _virtualAccess;
    public Size Size => _fastImageData.Size;

    internal FastImage(Size size)
    {
        _fastImageData = new FastImageData(size);
        _virtualAccess = new BlackVirtualAccess(_fastImageData);
    }

    internal FastImage(FastImageData fastImageData)
    {
        _fastImageData = fastImageData;
    }

    internal FastImage(Image image) : this(image.Size)
    {
        Picturify.Instance.Log.Information($"{GetType().Name} started reading image.");
        var sw = Stopwatch.StartNew();
        var bitmap = new Bitmap(image);
        var widthInBytes = Size.Width * 4;
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, Size.Width, Size.Height), ImageLockMode.ReadOnly,
            bitmap.PixelFormat);
        unsafe
        {
            var ptr = (byte*)bitmapData.Scan0;
            Parallel.For(0, Size.Height, Picturify.Instance.ParallelOptions, h => {
                for (var w = 0; w < Size.Width; w++)
                {
                    _fastImageData.AlphaArray[w,h] = ptr[h * widthInBytes + w * 4 + 3];
                    _fastImageData.RedArray[w,h] = ptr[h * widthInBytes + w * 4 + 2];
                    _fastImageData.GreenArray[w,h] = ptr[h * widthInBytes + w * 4 + 1];
                    _fastImageData.BlueArray[w,h] = ptr[h * widthInBytes + w * 4];
                }
            });
        }
        bitmap.UnlockBits(bitmapData);
        sw.Stop();
        Picturify.Instance.Log.Information($"{GetType().Name} reading image took {sw.ElapsedMilliseconds} ms.");
    }

    internal FastImage(Stream stream) : this(Image.FromStream(stream))
    {
    }

    internal FastImage(string path) : this(Image.FromFile(path))
    {
    }

    public void Save(Stream stream, ImageFormat format)
    {
        var bitmap = GetBitmap();
        bitmap.Save(stream, format);
    }

    public void Save(string path)
    {
        var bitmap = GetBitmap();
        var format = GetImageFormat(path);
        bitmap.Save(path, format);
    }

    public void Execute(IProcessorBase processorBase)
    {
        processorBase.Process(_fastImageData, _virtualAccess);
    }

    private Bitmap GetBitmap()
    {
        Picturify.Instance.Log.Information($"{GetType().Name} started writing image.");
        var sw = Stopwatch.StartNew();
        var widthInBytes = Size.Width * 4;
        var bitmap = new Bitmap(Size.Width, Size.Height);
        var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly,
            bitmap.PixelFormat);
        unsafe
        {
            var ptr = (byte*)bitmapData.Scan0;
            Parallel.For(0, Size.Height, Picturify.Instance.ParallelOptions, h => {
                for (var w = 0; w < Size.Width; w++)
                {
                    ptr[h * widthInBytes + w * 4 + 3] = _fastImageData.AlphaArray[w, h].ToPixelByte();
                    ptr[h * widthInBytes + w * 4 + 2] = _fastImageData.RedArray[w, h].ToPixelByte();
                    ptr[h * widthInBytes + w * 4 + 1] = _fastImageData.GreenArray[w, h].ToPixelByte();
                    ptr[h * widthInBytes + w * 4 + 0] = _fastImageData.BlueArray[w, h].ToPixelByte();
                }
            });
        }
        bitmap.UnlockBits(bitmapData);
        sw.Stop();
        Picturify.Instance.Log.Information($"{GetType().Name} writing image took {sw.ElapsedMilliseconds} ms.");
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
