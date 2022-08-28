using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using Wpf.Ui.Appearance;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += OnButtonKeyDown;
        }

        private void IncreaseFontSize(object sender, RoutedEventArgs e)
        {
            Utilities.IncreaseFontSize(Editor, 3);
        }
        
        private void DecreaseFontSize(object sender, RoutedEventArgs e)
        {
            Utilities.DecreaseFontSize(Editor, 3);
        }
        
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;

            switch (e.Key)
            {
                case Key.OemPlus:
                    Utilities.IncreaseFontSize(Editor, 1);
                    break;
                case Key.OemMinus:
                    Utilities.DecreaseFontSize(Editor, 1);
                    break;
                case Key.O:
                    Utilities.OpenFile(Editor);
                    break;
            }
        }

        private void SyntaxMenu(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem) sender;
            
            Editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(menuItem.Header.ToString());;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem) sender;

            if (menuItem.Header.ToString() == "Light")
            {
                Editor.Background = Brushes.White;
                Editor.Foreground = Brushes.Black;
                Menu.Foreground = Brushes.Black;
                Theme.Apply(ThemeType.Light, BackgroundType.None);
            }
            else
            {
                Editor.Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#2d2d2d")!;
                Editor.Foreground = Brushes.White;
                Menu.Foreground = Brushes.White;
                Theme.Apply(ThemeType.Dark, BackgroundType.None);
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            Utilities.OpenFile(Editor);
        }
    }
}