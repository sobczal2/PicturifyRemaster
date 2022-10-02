using System.Drawing;

using BenchmarkDotNet.Attributes;

using PicturifyRemaster.Core.Models.Images;

namespace PicturifyRemaster.Core.Benchmark.Models.Images;

public class FastImageDataBenchmark
{
    public class CtorBenchmark
    {
        [ParamsSource(nameof(Sizes))]
        public Size Size { get; set; }

        public IEnumerable<Size> Sizes => new[] {
            new Size(100, 100), new Size(1000, 1000)
        };

        [Benchmark]
        public void FastImageDataCreation() => new FastImageData(Size);
    }
}
