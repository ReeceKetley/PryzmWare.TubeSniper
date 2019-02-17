using System;
using System.Windows.Controls;

namespace TubeSniper.Presentation.Wpf.Navigation
{
    public static class Navigation
    {

		public static Frame Frame { get; set; }

		public static bool Navigate(Uri sourcePageUri, object extraData = null)
        {
            if (Frame.CurrentSource != sourcePageUri)
            {
                return Frame.Navigate(sourcePageUri, extraData);
            }
            return true;
        }

        public static bool Navigate(object content)
        {
            if (Frame.NavigationService.Content != content)
            {
                return Frame.Navigate(content);
            }
            return true;
        }

        public static void GoBack()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
