using System;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Application.Properties;

namespace TubeSniper.Domain.Youtube.Extensions
{
    internal static class WebViewExtensions
    {

	    public static void Send(this WebView view, EO.Base.Action action)
	    {
		    if (view.ThreadRunner != null)
		    {
			    view.ThreadRunner.Send(action);
		    }
		    else
		    {
			    action();
		    }
	    }

		public static void SendString(this WebView view, string text)
        {
            foreach (var c in text)
            {
                view.SendChar(c);
                Thread.Sleep(200);
            }
        }

        public static bool ElementExists(this WebView view, string selector)
        {
	        for (;;)
	        {
		        if (view.CanEvalScript)
		        {
					break;
		        }
	        }
            view.InjectJQuery();
            return (bool)view.EvalScript("$(\"" + selector + "\").length > 0", true);
        }

        public static bool WaitUntilUrlContains(this WebView view, string url, TimeSpan timeout)
        {
            var endTime = DateTime.Now + timeout;
            for (; ; )
            {
                if (view.Url.Contains(url))
                {
                    return true;
                }

                if (DateTime.Now > endTime)
                {
                    return false;
                }
            }
        }

        public static bool ElementExists(this WebView view, string selector, TimeSpan timeout)
        {
            InjectJQuery(view);
            var endTime = DateTime.Now + timeout;
            for (; ; )
            {
                if (ElementExists(view, selector))
                {
                    return true;
                }

                if (DateTime.Now > endTime)
                {
                    return false;
                }
            }
        }

        public static void InjectJQuery(this WebView view)
        {
	        for (;;)
	        {
		        if (!view.IsLoading)
		        {
					break;
		        }
	        }

	        for (;;)
	        {
		        if (view.CanEvalScript)
		        {
			        break;
		        }
	        }
			view.EvalScript(Resources.jquery_min);
        }

        public static bool GetEvalBool(this WebView view, string selector)
        {
            var result = false;
            try
            {
                view.Send(() =>
                {
                    InjectJQuery(view);
	                for (; ; )
	                {
		                if (view.CanEvalScript)
		                {
			                break;
		                }
	                }
					result = (bool)view.EvalScript(selector);
                });
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static string GetEvalString(this WebView view, string selector)
        {
            var result = "";
            try
            {
                view.Send(() =>
                {
                    InjectJQuery(view);
	                for (; ; )
	                {
		                if (view.CanEvalScript)
		                {
			                break;
		                }
	                }
					result = (string)view.EvalScript(selector);
                });
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public static bool ScrollToElement(this WebView view, string selector)
        {
            var result = false;
            try
            {
                view.Send(() =>
                {
                    InjectJQuery(view);
	                for (; ; )
	                {
		                if (view.CanEvalScript)
		                {
			                break;
		                }
	                }
					view.EvalScript(
                        "$(\'html, body\').animate({\r\n    scrollTop: ($(\'" + selector + "\').first().offset().top)\r\n},0);");
                    result = true;
                });
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public static bool ClickElement(this WebView view, string selector)
        {
            var result = false;
            try
            {
                view.Send(() =>
                {
                    InjectJQuery(view);
	                for (; ; )
	                {
		                if (view.CanEvalScript)
		                {
			                break;
		                }
	                }
					view.EvalScript(
                        "$(\""+ selector +"\").click()");
                    result = true;
                });
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
