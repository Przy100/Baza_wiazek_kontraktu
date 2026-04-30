using System.Windows;

namespace Baza_wiazek_przyciskow_20240205
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.ApplyTheme(global::Baza_wiazek_przyciskow_20240205.Properties.Settings.Default.ColorTheme);
            base.OnStartup(e);
        }
    }
}
