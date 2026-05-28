
public class Worker
{
    private CancellationTokenSource? _cts;
    private Func<Task> async_work { get; set; }
    public bool is_running { get; private set; } = false;
    public int Speed = 1000;

    public void Start()
    {
        if (_cts != null) return;
        _cts = new CancellationTokenSource();
        _ = LoopAsync(_cts.Token);
        is_running = true;
    }

    public void Stop()
    {
        if(_cts == null) return;
        _cts.Cancel();
        _cts = null;
        is_running = false;
    }

    private async Task LoopAsync(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await async_work();
                await Task.Delay(Speed, token);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Worker has been canceled successfully");
        }
    }

    public Worker(Func<Task> async_method_to_loop)
    {
        async_work = async_method_to_loop;
    }
}

