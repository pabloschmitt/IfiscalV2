namespace IFiscalV2.Common
{
    using System;
    using System.Threading;
    using Xamarin.Forms;

    // https://forums.xamarin.com/discussion/22443/how-to-use-timer-in-xamarin-forms

    public enum TimerTaskExecution
    {
        Single,
        Continuous
    }

    public class TimerTask
    {

        private readonly TimeSpan _timeSpan;
        private readonly Action _callback;
        private readonly TimerTaskExecution _timerTaskExecution;

        private static CancellationTokenSource _cancellationTokenSource;

        public TimerTask(TimeSpan timeSpan, Action callback, TimerTaskExecution isSingle = TimerTaskExecution.Continuous)
        {
            _timeSpan = timeSpan;
            _callback = callback;
            _timerTaskExecution = isSingle;

            _cancellationTokenSource = new CancellationTokenSource();
        }
        public void Start()
        {
            CancellationTokenSource cts = _cancellationTokenSource; // safe copy
            Device.StartTimer(_timeSpan, () =>
            {
                if (cts.IsCancellationRequested)
                {
                    return false;
                }
                _callback.Invoke();
                if (_timerTaskExecution == TimerTaskExecution.Single)
                    return false;

                return true; //true to continuous, false to single use
            });
        }

        public void Stop()
        {
            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();
        }
    }
}
