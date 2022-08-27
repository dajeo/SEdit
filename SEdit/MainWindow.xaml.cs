using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using Wpf.Ui.Appearance;

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

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) return;
            
            if (e.Key == Key.OemPlus)
            {
                Editor.FontSize += 1;
            }
            else if (e.Key == Key.OemMinus)
            {
                Editor.FontSize -= 1;
            }
        }

        private void SyntaxMenu(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem) sender;
            
            var syntax = HighlightingManager.Instance.GetDefinition(menuItem.Header.ToString());

            if (syntax == null)
            {
                throw new Exception("Syntax not found.");
            }
            
            Editor.SyntaxHighlighting = syntax;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            if (Theme.GetAppTheme() == ThemeType.Dark)
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
    }
}