using System;
using TubeSniper.Presentation.Views.Nested;

namespace TubeSniper.Presentation.Interfaces
{
    public interface IMainView
    {
        ISideMenuView SideMenuView { get; }
        IContentView ContentView { get; }
        event EventHandler TestClicked;
        void Show();
    }
}