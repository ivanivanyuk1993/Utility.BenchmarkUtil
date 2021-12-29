namespace BenchmarkUtilNS;

public class BenchmarkResult<T>
{
    public readonly T Result;

    public readonly TimeSpan TimePassed;

    public BenchmarkResult(T result, TimeSpan timePassed)
    {
        Result = result;
        TimePassed = timePassed;
    }
}