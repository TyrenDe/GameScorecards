using MvvmBlazor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace GameScorecardsClient.ViewModels.Games
{
    public class GamesViewModel : ViewModelBase
    {
        private int m_CurrentCount = 0;
        public int CurrentCount
        {
            get => m_CurrentCount;
            private set => Set(ref m_CurrentCount, value);
        }

        public void IncrementCount()
        {
            CurrentCount++;
        }

        private readonly Timer _timer;
        private DateTime m_DateTime = DateTime.Now;

        public GamesViewModel()
        {
            _timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        public DateTime DateTime
        {
            get => m_DateTime;
            set => Set(ref m_DateTime, value);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            DateTime = DateTime.Now;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _timer.Dispose();
        }
    }
}
