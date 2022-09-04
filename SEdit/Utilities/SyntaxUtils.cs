using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;

namespace SEdit.Utilities;

public static class SyntaxUtils
{
    public static void ApplySyntax(TextEditor editor, string syntax)
    {
        editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition(syntax);
    }
}
