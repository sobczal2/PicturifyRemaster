using System.Drawing;

using BenchmarkDotNet.Attributes;

using PicturifyRemaster.Core.Models.Images;

namespace PicturifyRemaster.Core.Benchmark.Models.Images;

public class FastImageFactoryBenchmark
{
    public class FromImageDataBenchmark
    {
        [ParamsSource(nameof(DataSamples))]
        public FastImageData FastImageData { get; set; }

        public static IEnumerable<FastImageData> DataSamples => new[] {
            new FastImageData(new Size(100, 100)), new FastImageData(new Size(1000, 1000))
        };

        [Benchmark]
        public void FromImageData() => FastImageFactory.FromImageData(FastImageData);
    }

    public class FromFileBenchmark
    {
        private const string FilePrefix = "TestImage";
        [GlobalSetup]
        public void GlobalSetup()
        {
            var sizes = Enumerable.Range(1, 5);
            foreach (var size in sizes)
            {
                var fastImage = FastImageFactory.Random(new Size(size, size));
                fastImage.Save($"../{FilePrefix}{size}.png");
            }
        }

        [GlobalCleanup]
        public void GlobalClenup()
        {
            foreach (var file in Directory.EnumerateFiles("../", FilePrefix))
            {
                File.Delete(file);
            }
        }
        [ParamsSource(nameof(FileNames))]
        public string Path { get; set; }

        public static IEnumerable<string> FileNames => Directory.EnumerateFiles("../", FilePrefix);

        [Benchmark]
        public void FromFile() => FastImageFactory.FromFile(Path);
    }
}
