using System;
using System.IO;
using TubeSniper.Core.Domain.Models;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Factories
{
    public class CampaignSessionFactory : ICampaignSessionFactory
    {
        public CampaignSessionFactory()
        {
        }

        public void Create()
        {
            var sessionDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sessions", Environment.TickCount.ToString());
            Directory.CreateDirectory(sessionDir);
        }
    }
}