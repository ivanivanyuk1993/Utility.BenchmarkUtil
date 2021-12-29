using System.Diagnostics;
using System.Reactive.Concurrency;

namespace BenchmarkUtilNS;

/// <summary>
///     BenchmarkUtil is a class for very approximate benchmarking, times are all over the place, but after spending time
///     trying to find/create better solution for benchmarking concurrent code, this was considered the best alternative
///     (dotnet test -c Release --logger "console;verbosity=detailed" --filter ShouldSpendLessProcessorTimeThanAlternatives
///     didn't change anything)
/// </summary>
public static class BenchmarkUtil
{
    public static Task<BenchmarkResult<T>> BenchmarkOnScheduler<T>(Func<T> benchmark, IScheduler scheduler)
    {
        var taskCompletionSource = new TaskCompletionSource<BenchmarkResult<T>>();
        var stopwatch = new Stopwatch();
        scheduler.Schedule(action: () =>
        {
            stopwatch.Start();
            var result = benchmark();
            stopwatch.Stop();
            taskCompletionSource.SetResult(
                result: new BenchmarkResult<T>(
                    result: result,
                    timePassed: stopwatch.Elapsed
                )
            );
        });
        return taskCompletionSource.Task;
    }
}