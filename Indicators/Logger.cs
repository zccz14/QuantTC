using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// Logger Indicator
    /// </summary>
    public class Logger: Indicator<string>
    {
        public Logger(IIndicator timer)
        {
            Timer = timer;
            Timer.Update += Timer_Update;
        }

        private void Timer_Update()
        {
            Data.Add(string.Join("", Words.Select(s => $"[{s}]")));
            Words.Clear();
            FollowUp();
        }

        public void Log(string s)
        {
            Words.Add(s);
        }

        private List<string> Words { get; } = new List<string>();

        private IIndicator Timer { get; }
    }
}
