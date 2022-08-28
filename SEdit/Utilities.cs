using System.IO;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;

namespace SEdit;

public static class Utilities
{
    public static void OpenFile(MainWindow mainWindow, TextEditor editor, ref string currentFileName)
    {
        var openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() != true) return;
        
        currentFileName = openFileDialog.FileName;
        mainWindow.Title = currentFileName + " - SEdit";
        editor.Text = File.ReadAllText(currentFileName);
    }

    public static void SaveFile(MainWindow mainWindow, TextBlock statusBlock, ref string fileName, string content)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            var saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                fileName = saveFileDialog.FileName;
            }
            else return;
        }
        
        statusBlock.Text = "File saved";
        mainWindow.Title = fileName + " - SEdit";
        File.WriteAllText(fileName, content);
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
