using System;
using System.Windows.Forms;

namespace TubeSniper.Presentation.Controls
{
    public class StackPanel : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328)
            {
                if (!DesignMode || HotTrack)
                {
                    m.Result = (IntPtr)1;
                    return;
                }
            }
            base.WndProc(ref m);
            //if (m.Msg == 0x1328 && (HotTrack && !DesignMode)) m.Result = (IntPtr)1;
            //else base.WndProc(ref m);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Focused)
            {
                if (keyData == (Keys.Control | Keys.Tab) || keyData == (Keys.Control | Keys.Shift | Keys.Tab) || keyData == Keys.Left ||
                    keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Home || keyData == Keys.End)
                {
                    return true;
                }
            }
            else
            {
                if (keyData == (Keys.Control | Keys.Tab))
                {
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
