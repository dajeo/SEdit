using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using SEdit.Utilities;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Media = System.Windows.Media;

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

        // Theme initializing        
        var themeProp = Properties.Settings.Default.Theme;

        if (string.IsNullOrEmpty(themeProp))
        {
            using var key = Registry.CurrentUser
                .OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")!;
            var currentTheme = (int) key.GetValue("AppsUseLightTheme")! == 1 ? "Light" : "Dark";
            Properties.Settings.Default.Theme = currentTheme;
            themeProp = currentTheme;
        }
        
        ThemeUtils.ApplyTheme(Editor, Menu, (Enums.Theme) Enum.Parse(typeof(Enums.Theme), themeProp));
        
        // Word wrap initializing
        var wordWrapProp = Properties.Settings.Default.WordWrap;

        Editor.WordWrap = wordWrapProp;
        var wordWrap = Menu.FindName("WordWrapItem");
        if (wordWrap is MenuItem wordWrapItem)
        {
            wordWrapItem.IsChecked = wordWrapProp;   
        }

        // Status bar initializing
        var statusBarProp = Properties.Settings.Default.StatusBar;

        StatusBar.Visibility = statusBarProp ? Visibility.Visible : Visibility.Collapsed;
        var statusBar = Menu.FindName("StatusBarItem");
        if (statusBar is MenuItem statusBarItem)
        {
            statusBarItem.IsChecked = statusBarProp;
        }
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
        if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift))
        {
            Editor.IsEnabled = false;
            
            switch (e.Key)
            {
                case Key.S:
                    FileUtils.SaveAsFile(this, StatusBlock, Editor.Text);
                    break;
            }
        }
        else if (Keyboard.Modifiers == ModifierKeys.Control)
        {
            Editor.IsEnabled = false;
            
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
        
        Editor.IsEnabled = true;
    }

    private void SyntaxMenu(object sender, RoutedEventArgs e)
    {
        var menuItem = (MenuItem) sender;

        Editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(menuItem.Header.ToString());
    }

    private void ChangeTheme(object sender, RoutedEventArgs e)
    {
        var menuItem = (MenuItem) sender;
        var selectedTheme = menuItem.Header.ToString()!;

        ThemeUtils.ApplyTheme(Editor, Menu, (Enums.Theme) Enum.Parse(typeof(Enums.Theme), selectedTheme));
        Properties.Settings.Default.Theme = selectedTheme; 
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
        var isChecked = ((MenuItem)sender).IsChecked;

        Properties.Settings.Default.StatusBar = isChecked;
        StatusBar.Visibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
    }

    private void WordWrapAction(object sender, RoutedEventArgs e)
    {
        var isChecked = ((MenuItem) sender).IsChecked;

        Properties.Settings.Default.WordWrap = isChecked;
        Editor.WordWrap = isChecked;
    }

    private void FontDialog(object sender, RoutedEventArgs e)
    {
        var fontDialog = new FontDialog
        {
            Font = new System.Drawing.Font(Editor.FontFamily.Source, (float)Editor.FontSize),
            ShowEffects = false
        };

        if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            Editor.FontFamily = new Media.FontFamily(fontDialog.Font.FontFamily.Name);
            Editor.FontSize = fontDialog.Font.Size;
        }
    }
}
