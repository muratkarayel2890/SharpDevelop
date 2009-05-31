// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <author name="Daniel Grunwald"/>
//     <version>$Revision$</version>
// </file>

using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory;

namespace ICSharpCode.SharpDevelop.Editor
{
	public interface ITextEditorProvider : IFileDocumentProvider
	{
		ITextEditor TextEditor {
			get;
		}
	}
	
	/// <summary>
	/// Interface for text editors.
	/// </summary>
	public interface ITextEditor : IServiceProvider
	{
		IDocument Document { get; }
		ITextEditorCaret Caret { get; }
		ITextEditorOptions Options { get; }
		
		/// <summary>
		/// Gets the start offset of the selection.
		/// </summary>
		int SelectionStart { get; }
		
		/// <summary>
		/// Gets the length of the selection.
		/// </summary>
		int SelectionLength { get; }
		
		/// <summary>
		/// Gets/Sets the selected text.
		/// </summary>
		string SelectedText { get; set; }
		
		/// <summary>
		/// Sets the selection.
		/// </summary>
		/// <param name="selectionStart">Start offset of the selection</param>
		/// <param name="selectionLength">End offset of the selection</param>
		void Select(int selectionStart, int selectionLength);
		
		/// <summary>
		/// Is raised when the selection changes.
		/// </summary>
		event EventHandler SelectionChanged;
		
		/// <summary>
		/// Sets the caret to the specified line/column and brings the caret into view.
		/// </summary>
		void JumpTo(int line, int column);
		
		string FileName { get; }
		
		void ShowCompletionWindow(ICompletionItemList data);
		
		/// <summary>
		/// Open a new insight window showing the specific insight items.
		/// </summary>
		/// <param name="items">The insight items to show in the window.
		/// If this property is null or an empty list, the insight window will not be shown.</param>
		/// <returns>The insight window; or null if no insight window was opened.</returns>
		IInsightWindow ShowInsightWindow(IEnumerable<IInsightItem> items);
		
		/// <summary>
		/// Gets the insight window that is currently open.
		/// </summary>
		IInsightWindow ActiveInsightWindow { get; }
		
		[Obsolete("Use the overload taking IEnumerable<IInsightItem>")]
		void ShowInsightWindow(ICSharpCode.TextEditor.Gui.InsightWindow.IInsightDataProvider provider);
		[Obsolete("Use the overload taking ICompletionItemList")]
		void ShowCompletionWindow(ICSharpCode.TextEditor.Gui.CompletionWindow.ICompletionDataProvider provider, char ch);
	}
	
	public interface ITextEditorOptions
	{
		/// <summary>
		/// Gets the text used for one indentation level.
		/// </summary>
		string IndentationString { get; }
		
		/// <summary>
		/// Gets whether a '}' should automatically be inserted when a block is opened.
		/// </summary>
		bool AutoInsertBlockEnd { get; }
	}
	
	public interface ITextEditorCaret
	{
		/// <summary>
		/// Gets/Sets the caret offset;
		/// </summary>
		int Offset { get; set; }
		
		/// <summary>
		/// Gets/Sets the caret line number.
		/// Line numbers are counted starting from 1.
		/// </summary>
		int Line { get; set; }
		
		/// <summary>
		/// Gets/Sets the caret column number.
		/// Column numbers are counted starting from 1.
		/// </summary>
		int Column { get; set; }
		
		/// <summary>
		/// Gets/sets the caret position.
		/// </summary>
		Location Position { get; set; }
	}
}
