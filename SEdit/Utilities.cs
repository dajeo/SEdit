using System.IO;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;

namespace SEdit;

public static class Utilities
{
    public static void OpenFile(TextEditor editor)
    {
        var openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            editor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }
    
    public static void IncreaseFontSize(TextEditor editor, double increaseOn)
    {
        editor.FontSize += increaseOn;
    }
        
    public static void DecreaseFontSize(TextEditor editor, double decreaseOn)
    {
        editor.FontSize -= decreaseOn;
    }
}