﻿namespace TubeSniper.Domain.Youtube
{
    public class StatusChangedEventArgs
    {
        public string Status { get; }

        public StatusChangedEventArgs(string status)
        {
            Status = status;
        }
    }
}