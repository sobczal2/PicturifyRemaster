namespace PicturifyRemaster.Core.VirtualAccess;

public interface IVirtualAccess
{
    float GetRed(int x, int y);
    float GetGreen(int x, int y);
    float GetBlue(int x, int y);
    float GetAlpha(int x, int y);
}
