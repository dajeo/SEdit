using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;
using SEdit.Utilities;
using Wpf.Ui.Appearance;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SEdit;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private string _currentFileName = "";
    
    public MainWindow()
    {
        InitializeComponent();
        KeyDown += OnButtonKeyDown;
    }

    private void IncreaseFontSize(object sender, RoutedEventArgs e)
    {
        EditorUtils.IncreaseFontSize(Editor, 3);
    }
        
    private void DecreaseFontSize(object sender, RoutedEventArgs e)
    {
        EditorUtils.DecreaseFontSize(Editor, 3);
    }

    private void OnButtonKeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers != ModifierKeys.Control) return;

        switch (e.Key)
        {
            case Key.OemPlus:
                EditorUtils.IncreaseFontSize(Editor, 1);
                break;
            case Key.OemMinus:
                EditorUtils.DecreaseFontSize(Editor, 1);
                break;
            case Key.O:
                FileUtils.OpenFile(this, Editor, ref _currentFileName);
                break;
            case Key.S:
                FileUtils.SaveFile(this, StatusBlock, ref _currentFileName, Editor.Text);
                break;
        }
    }

    private void SyntaxMenu(object sender, RoutedEventArgs e)
    {
        var menuItem = (MenuItem) sender;

        Editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(menuItem.Header.ToString());
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
        FileUtils.OpenFile(this, Editor, ref _currentFileName);
    }

    private void SaveFile(object sender, RoutedEventArgs e)
    {
        FileUtils.SaveFile(this, StatusBlock, ref _currentFileName, Editor.Text);
    }

    private void OnStatusBarClick(object sender, MouseButtonEventArgs e)
    {
        if (!string.IsNullOrEmpty(StatusBlock.Text))
        {
            StatusBlock.Text = "";
        }
    }

    private void SaveAsFile(object sender, RoutedEventArgs e)
    {
        FileUtils.SaveAsFile(this, StatusBlock, Editor.Text);
    }

    private void StatusBarAction(object sender, RoutedEventArgs e)
    {
        var menuItem = (MenuItem) sender;

        StatusBar.Visibility = menuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;
    }

    private void WordWrapAction(object sender, RoutedEventArgs e)
    {
        var menuItem = (MenuItem) sender;

        Editor.WordWrap = menuItem.IsChecked;
    }
}
