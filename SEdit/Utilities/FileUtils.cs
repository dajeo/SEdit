using System.IO;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;

namespace SEdit.Utilities;

public static class FileUtils
{
    private static readonly string[] Filters =
    {
        FormatFilter("Plain text", "txt"),
        FormatFilter("Python", "py"),
        FormatFilter("Java", "java"),
        FormatFilter("C#", "cs")
    };

    private static readonly string FormattedFilters = string.Join("|", Filters);
    
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
            var tempFileName = SaveAsFile(mainWindow, statusBlock, content);
            if (tempFileName == null) return;
            fileName = tempFileName;
            return;
        }
        
        statusBlock.Text = "File saved";
        mainWindow.Title = fileName + " - SEdit";
        File.WriteAllText(fileName, content);
    }

    public static string? SaveAsFile(MainWindow mainWindow, TextBlock statusBlock, string content)
    {
        string fileName;
        
        var saveFileDialog = new SaveFileDialog
        {
            Filter = FormattedFilters
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            fileName = saveFileDialog.FileName;
        }
        else return null;

        statusBlock.Text = "File saved";
        mainWindow.Title = fileName + " - SEdit";
        File.WriteAllText(fileName, content);
        
        return fileName;
    }

    private static string FormatFilter(string description, string format)
    {
        return string.Format("{0} (*.{1})|*.{1}", description, format);
    }
}
