using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Benchmark;
public class InProcessConfig : ManualConfig
{
    public InProcessConfig()
    {
        AddJob(Job.Default
            .WithToolchain(InProcessEmitToolchain.Instance));

        // Add a job with specific settings
        AddJob(Job.Default
            .WithWarmupCount(3)  // Number of warmup iterations
            .WithIterationCount(10)  // Number of benchmark iterations
            .WithLaunchCount(1));  // Number of separate process launches

        // Add exporters (e.g., Markdown, CSV, etc.)
        AddExporter(MarkdownExporter.Default);
        AddExporter(CsvExporter.Default);

    }
}
