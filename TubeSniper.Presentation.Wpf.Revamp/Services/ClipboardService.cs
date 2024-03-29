﻿using System;
using System.Windows;
using TubeSniper.Application.Services;

namespace TubeSniper.Presentation.Wpf.Services
{
    public class ClipboardService : IClipboardService
    {
        public bool SetText(string text)
        {
            try
            {
                Clipboard.SetText(text);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public string GetText()
        {
            return Clipboard.GetText();
        }
    }
}
