using BenchmarkDotNet.Running;

namespace Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var summary = BenchmarkRunner.Run<Benchmarks>();
            var summary = BenchmarkRunner.Run<HashAlgoBenchmarks>();
            
        }
    }
}