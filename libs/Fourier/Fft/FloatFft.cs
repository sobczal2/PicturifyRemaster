using PicturifyRemaster.Core;

namespace PicturifyRemaster.Fourier.Fft;

public static class FloatFft
{
    private static int BitReverse(int n, int bits)
    {
        var reversedN = n;
        var count = bits - 1;

        n >>= 1;
        while (n > 0)
        {
            reversedN = (reversedN << 1) | (n & 1);
            count--;
            n >>= 1;
        }

        return ((reversedN << count) & ((1 << bits) - 1));
    }

    public static void FFT(float[] real, float[] imag)
    {
        if (real.Length != imag.Length)
            throw new ArgumentException("real and imag must have the same length");
        var bits = (int)MathF.Log(real.Length, 2);
        for (var j = 1; j < real.Length; j++)
        {
            var swapPos = BitReverse(j, bits);
            if (swapPos <= j)
            {
                continue;
            }

            (real[j], real[swapPos]) = (real[swapPos], real[j]);
            (imag[j], imag[swapPos]) = (imag[swapPos], imag[j]);
        }

        Parallel.For(0, real.Length, Picturify.Instance.ParallelOptions, N => {
            for (var i = 0; i < real.Length; i += N)
            {
                for (var k = 0; k < N / 2; k++)
                {
                    int evenIndex = i + k;
                    int oddIndex = i + k + (N / 2);
                    var evenReal = real[evenIndex];
                    var oddReal = real[oddIndex];
                    var evenImag = imag[evenIndex];
                    var oddImag = imag[oddIndex];

                    var term = -2 * MathF.PI * k / N;
                    var termCos = MathF.Cos(term);
                    var termSin = MathF.Sin(term);
                    var expReal = termSin * oddReal + termCos * oddImag;
                    var expImag = termCos * oddReal - termSin * oddImag;
                    
                    real[evenIndex] = evenReal + expReal;
                    imag[evenIndex] = evenImag + expImag;
                    real[oddIndex] = evenReal - expReal;
                    imag[oddIndex] = evenImag - expImag;
                }
            }
        });
    }
}
