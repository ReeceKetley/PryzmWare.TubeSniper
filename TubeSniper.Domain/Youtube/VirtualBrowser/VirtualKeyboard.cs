using System;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Domain.Youtube.Extensions;

namespace TubeSniper.Domain.Youtube.VirtualBrowser
{
    public class VirtualKeyboard
    {
        public WebView View { get; }
        private readonly Random _random = new Random();

        public VirtualKeyboard(WebView view)
        {
            View = view;
        }

        public void KeyPress(KeyCode key)
        {
            View.Send(() =>
            {
                View.SendKeyEvent(true, key);
                Thread.Sleep(_random.Next(50, 250));
                View.SendKeyEvent(false, key);
            });
        }

        public void TypeString(string text)
        {
            View.Send(() =>
            {
                for (var index = 0; index < text.Length; index++)
                {
                    var c = text[index];
                    if (index > 0)
                    {
                        Thread.Sleep(_random.Next(50, 500));
                    }

                    View.SendChar(c);
                }
			});
        }

        public void SubmitString(string text)
        {
            View.Send(() =>
            {
                TypeString(text);
                Thread.Sleep(_random.Next(0, 500));
                PressSubmit();
            });
        }

        public void PressSubmit()
        {
            View.Send(() =>
            {
                View.SendKeyEvent(true, KeyCode.Enter);
                Thread.Sleep(_random.Next(50, 500));
                View.SendKeyEvent(false, KeyCode.Enter);
            });
        }

        public void PressTab()
        {
            View.Send(() =>
            {
                View.SendKeyEvent(true, KeyCode.Tab);
                Thread.Sleep(_random.Next(50, 500));
                View.SendKeyEvent(false, KeyCode.Tab);
            });
        }

        public void PressBackspace()
        {
            View.Send(() =>
            {
                View.SendKeyEvent(true, KeyCode.Back);
                Thread.Sleep(_random.Next(0, 500));
                View.SendKeyEvent(false, KeyCode.Back);
            });
        }


	    public void SelectAll()
	    {
		    View.Send(() =>
		    {
			    View.SendKeyEvent(true, KeyCode.LControlKey);
			    Thread.Sleep(_random.Next(0, 500));
				View.SendKeyEvent(true, KeyCode.A);
			    Thread.Sleep(_random.Next(50, 500));
			    View.SendKeyEvent(false, KeyCode.A);
			    Thread.Sleep(_random.Next(0, 250));
			    View.SendKeyEvent(false, KeyCode.LControlKey);
		    });
		}

	    public void SelectAllBackspace()
	    {
			SelectAll();
			Thread.Sleep(_random.Next(0, 250));
			PressBackspace();
		}
    }
}
