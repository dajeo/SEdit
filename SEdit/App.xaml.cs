using System.Windows;
using Prop = SEdit.Properties;

namespace SEdit;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private void OnExit(object sender, ExitEventArgs e)
    {
        Prop.Settings.Default.Save();
    }
}
