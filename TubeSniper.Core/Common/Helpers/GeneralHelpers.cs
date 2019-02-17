using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;

namespace TubeSniper.Core.Common.Helpers
{
    public static class GeneralHelpers
    {
	    public static int RandomNumber(int min = 0, int max = 1000)
	    {
			var random = new Random();
		    return random.Next(min, max);
	    }

        public static void ConsoleLogMethod(string className, string message)
        {
            //Console.WriteLine("[{0}]:[{1}] - {2}", DateTime.Now.ToLongTimeString(), className, message);
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }

            }

            return inUse;
        }

        public static string MakeValidFileName(string name)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            string escapedInvalidChars = Regex.Escape(name);
            string invalidRegex = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(name, invalidRegex, "_");
        }

	    public static Bitmap GetBitmap(string url)
	    {
		    return null;
	    }

        public static DateTime? YouTubeDateToDateTime(string input)
        {
            var match = Regex.Match(input, @"(\d+) (second|minute|hour|day|week|month|year)");
            if (!match.Success)
            {
                return null;
            }
            var number = Int32.Parse(match.Groups[1].Value);
            var type = match.Groups[2].Value;
            TimeSpan added;
            if (type == "minute")
            {
                added = TimeSpan.FromMinutes(number);
            }
            else if (type == "hour")
            {
                added = TimeSpan.FromHours(number);

            }
            else if (type == "day")
            {
                added = TimeSpan.FromDays(number);

            }
            else if (type == "week")
            {
                added = TimeSpan.FromDays(number * 7);

            }
            else if (type == "month")
            {
                added = TimeSpan.FromDays(number * 30);

            }
            else if (type == "year")
            {
                added = TimeSpan.FromDays(number * 365);

            }
            else
            {
                return null;
            }

            return DateTime.Now - added;
        }
    }

    public static class Retry
    {
        public static void Do(
            Action action,
            TimeSpan retryInterval,
            int maxAttemptCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, maxAttemptCount);
        }

        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int maxAttemptCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
