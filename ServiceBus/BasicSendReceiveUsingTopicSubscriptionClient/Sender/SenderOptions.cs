using System;

namespace Sender
{
    public class SenderOptions
    {
        public string ConnectionString { get; set; } = "<secret>";
        public string TopicName { get; set; } = "<secret>";
        public TimeSpan SendDelay { get; set; } = TimeSpan.Zero;
        public TimeSpan SendPeriod { get; set; } = TimeSpan.FromSeconds(1);
    }
}