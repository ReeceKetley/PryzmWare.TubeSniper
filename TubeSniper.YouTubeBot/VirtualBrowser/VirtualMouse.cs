﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using EO.WebBrowser;
using TubeSniper.YouTubeBot.Extensions;

namespace TubeSniper.YouTubeBot.VirtualBrowser
{
    public class VirtualMouse
    {
        public WebView WebView { get; }
        public Point Position { get; set; }

        public VirtualMouse(WebView webView)
        {
            WebView = webView;
        }

        public void LeftClick()
        {
            WebView.SendMouseEvent(MouseEventType.Click, new MouseEventArgs(MouseButtons.Left, 1, Position.X, Position.Y, 0));
        }

        public void RightClick()
        {
            WebView.SendMouseEvent(MouseEventType.Click, new MouseEventArgs(MouseButtons.Right, 1, Position.X, Position.Y, 0));
        }

        public bool MoveToAndClickElement(string selector)
        {
            var isDone = false;
	        WebView.Send(() =>
            {
                WebView.InjectJQuery();
                if (!WebView.ElementExists(selector, TimeSpan.FromSeconds(5)))
                {
                    return;
                }

                if (MoveToElement(selector).Code != MoveToElementResultCode.Success)
                {
                    return;
                }

                LeftClick();
                isDone = true;
            });

            return isDone;
        }
        public bool FocusElement(string selector)
        {
	        WebView.Send(() =>
            {
                WebView.InjectJQuery();
	            WebView.EvalScript("$(\"" + selector + "\").focus();");
            });

	        return true;
        }

        public MoveToElementResult MoveToElement(string selector)
        {
            object objX;
            object objY;
            try
            {
                objX = WebView.EvalScript("$(\"" + selector + "\").offset().left + ($(\"" + selector +
                                          "\").width() / 2);");
                objY = WebView.EvalScript("$(\"" + selector + "\").offset().top + ($(\"" + selector +
                                          "\").height() / 2);");
            }
            catch
            {
                return new MoveToElementResult(MoveToElementResultCode.ObjectNotFound);
            }

            int offsetX;
            int offsetY;

            if (objX is int i)
            {
                offsetX = i;
            }
            else if (objX is double d)
            {
                offsetX = (int)d;
            }
            else
            {
                return new MoveToElementResult();
            }

            if (objY is int i1)
            {
                offsetY = i1;
            }
            else if (objY is double d)
            {
                offsetY = (int)d;
            }
            else
            {
                return new MoveToElementResult();
            }




            var buttonPosition = new Point(offsetX, offsetY);
            buttonPosition.X += 25;
            buttonPosition.Y += 10;

            LinearSmoothMove(buttonPosition, new TimeSpan(0, 0, 0, 0, 700));
            return new MoveToElementResult();
        }

        public void LinearSmoothMove(Point newPosition, TimeSpan duration)
        {
            Point currentPosition = Position;

            // Find the vector between start and newPosition
            double deltaX = newPosition.X - currentPosition.X;
            double deltaY = newPosition.Y - currentPosition.Y;

            // start a timer
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double timeFraction;

            do
            {
                timeFraction = (double)stopwatch.Elapsed.Ticks / duration.Ticks;
                if (timeFraction > 1.0)
                    timeFraction = 1.0;

                PointF curPoint = new PointF((float)(currentPosition.X + timeFraction * deltaX), (float)(currentPosition.Y + timeFraction * deltaY));
                Position = Point.Round(curPoint);
                WebView.SendMouseEvent(MouseEventType.Move, new MouseEventArgs(MouseButtons.None, 0, Position.X, Position.Y, 0));
                Thread.Sleep(20);
            } while (timeFraction < 1.0);
        }

	   
    }
}
