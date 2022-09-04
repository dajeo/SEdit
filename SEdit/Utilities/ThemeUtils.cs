using System;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Wpf.Ui.Appearance;

namespace SEdit.Utilities;

public static class ThemeUtils
{
    public static void ApplyTheme(TextEditor editor, Menu menu, Enums.Theme theme)
    {
        // Foreground - font color

        switch (theme)
        {
            case Enums.Theme.Light:
                editor.Background = Brushes.White;
                editor.Foreground = Brushes.Black;
                menu.Foreground = Brushes.Black;
                Theme.Apply(ThemeType.Light, BackgroundType.None);
                break;
            case Enums.Theme.Dark:
                editor.Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#2d2d2d")!;
                editor.Foreground = Brushes.White;
                menu.Foreground = Brushes.White;
                Theme.Apply(ThemeType.Dark, BackgroundType.None);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
        }
    }
}
