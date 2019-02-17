using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TubeSniper.Core.Domain.Models
{
    internal class CampaignSessionServer
    {
        private Guid _sessionId;

        public CampaignSessionServer()
        {
            _sessionId = Guid.NewGuid();
        }

        public void Start()
        {
            //
            Console.ReadLine();
            var sessionDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sessions", Environment.TickCount.ToString());
            Directory.CreateDirectory(sessionDir);
            var workerCount = 1;
            for (int i = 0; i < workerCount; i++)
            {
                var process = Process.Start(Application.ExecutablePath, "-session " + _sessionId + " -worker " + i);
            }
        }
    }
}
