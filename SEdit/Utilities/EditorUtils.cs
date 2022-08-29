using ICSharpCode.AvalonEdit;

namespace SEdit.Utilities;

public static class EditorUtils
{
    public static void IncreaseFontSize(TextEditor editor, double increaseOn)
    {
        editor.FontSize += increaseOn;
    }
        
    public static void DecreaseFontSize(TextEditor editor, double decreaseOn)
    {
        editor.FontSize -= decreaseOn;
    }
}
