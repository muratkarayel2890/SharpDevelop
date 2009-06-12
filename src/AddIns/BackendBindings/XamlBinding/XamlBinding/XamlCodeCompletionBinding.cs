// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Siegfried Pammer" email="sie_pam@gmx.at"/>
//     <version>$Revision: 3731 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using ICSharpCode.XmlEditor;

namespace ICSharpCode.XamlBinding
{
	public class XamlCodeCompletionBinding : ICodeCompletionBinding
	{
		static XamlCodeCompletionBinding instance;
		
		public static XamlCodeCompletionBinding Instance {
			get {
				if (instance == null)
					instance = new XamlCodeCompletionBinding();
				
				return instance;
			}
		}
		
		public CodeCompletionKeyPressResult HandleKeyPress(ITextEditor editor, char ch)
		{
			ParseInformation info = ParserService.GetParseInformation(editor.FileName);
			
			XamlContext context = CompletionDataHelper.ResolveContext(editor, ch);
			ICompletionItemList list;
			
			if (context.Description == XamlContextDescription.InComment)
				return CodeCompletionKeyPressResult.None;
			
			switch (ch) {
				case '<':
					context.Description = (context.Description == XamlContextDescription.None) ? XamlContextDescription.AtTag : context.Description;
					list = CompletionDataHelper.CreateListForContext(editor, context);
					editor.ShowCompletionWindow(list);
					return CodeCompletionKeyPressResult.Completed;
				case '>':
					return CodeCompletionKeyPressResult.None;
				case '"':
					if (!XmlParser.IsInsideAttributeValue(editor.Document.Text, editor.Caret.Offset)) {
						int search = editor.Caret.Offset + 1;
						while (search < editor.Document.TextLength - 1 && char.IsWhiteSpace(editor.Document.GetCharAt(search)))
							search++;
						
						if (editor.Document.GetCharAt(search) != '"') {
							editor.Document.Insert(editor.Caret.Offset, "\"\"");
							editor.Caret.Offset--;
							this.CtrlSpace(editor);
							return CodeCompletionKeyPressResult.EatKey;
						}
					}
					break;
				case '{': // starting point for Markup Extension Completion
					if (!string.IsNullOrEmpty(context.AttributeName) && XmlParser.IsInsideAttributeValue(editor.Document.Text, editor.Caret.Offset)) {
						string valueBeforeCaret = context.RawAttributeValue.Substring(0, context.ValueStartOffset);
						if (valueBeforeCaret.StartsWith("{}", StringComparison.OrdinalIgnoreCase))
							return CodeCompletionKeyPressResult.None;
						
						editor.Document.Insert(editor.Caret.Offset, "{}");
						editor.Caret.Offset--;
						
						DoMarkupExtensionCompletion(editor, info, CompletionDataHelper.ResolveContext(editor, '{'));
						return CodeCompletionKeyPressResult.EatKey;
					}
					break;
				case '.':
					if (context.Path != null && !XmlParser.IsInsideAttributeValue(editor.Document.Text, editor.Caret.Offset)) {
						context.Description = XamlContextDescription.AtTag;
						list = CompletionDataHelper.CreateListForContext(editor, context);
						editor.ShowCompletionWindow(list);
						return CodeCompletionKeyPressResult.Completed;
					}
					break;
				case ':':
					if (context.Path != null && XmlParser.GetQualifiedAttributeNameAtIndex(editor.Document.Text, editor.Caret.Offset) == null) {
						if (!context.AttributeName.StartsWith("xmlns")) {
							list = CompletionDataHelper.CreateListForContext(editor, context);
							editor.ShowCompletionWindow(list);
							return CodeCompletionKeyPressResult.CompletedIncludeKeyInCompletion;
						}
					}
					break;
				case '/': // ignore '/' when trying to type '/>'
					return CodeCompletionKeyPressResult.None;
				case '=':
					if (!XmlParser.IsInsideAttributeValue(editor.Document.Text, editor.Caret.Offset)) {
						int searchOffset = editor.Caret.Offset;
						
						while (searchOffset < editor.Document.TextLength - 1) {
							searchOffset++;
							if (!char.IsWhiteSpace(editor.Document.GetCharAt(searchOffset)))
							    break;
						}
						
						if (searchOffset >= editor.Document.TextLength || editor.Document.GetCharAt(searchOffset) != '"') {
							editor.Document.Insert(editor.Caret.Offset, "=\"\"");
							editor.Caret.Offset--;
						} else {
							editor.Document.Insert(editor.Caret.Offset, "=");
							editor.Caret.Offset++;
						}
						
						this.CtrlSpace(editor);
						return CodeCompletionKeyPressResult.EatKey;
					}
					break;
				default:
					if (context.Description != XamlContextDescription.None && !char.IsWhiteSpace(context.PressedKey)) {
						editor.Document.Insert(editor.Caret.Offset, ch.ToString());
						if (!context.AttributeName.StartsWith("xmlns"))
							this.CtrlSpace(editor);
						return CodeCompletionKeyPressResult.EatKey;
					}
					break;
			}
			
			return CodeCompletionKeyPressResult.None;
		}
		
		static bool DoXmlTagCompletion(ITextEditor editor)
		{
			int offset = editor.Caret.Offset;
			if (offset > 0) {
				int searchOffset = offset - 1;
				char c = editor.Document.GetCharAt(searchOffset);
				while (char.IsWhiteSpace(c)) {
					searchOffset--;
					c = editor.Document.GetCharAt(searchOffset);
				}
				if (c != '/') {
					string document = editor.Document.Text.Insert(offset, ">");
					XmlElementPath path = XmlParser.GetActiveElementStartPathAtIndex(document, offset);
					if (path != null && path.Elements.Count > 0) {
						QualifiedName last = path.Elements[path.Elements.Count - 1];
						if (!Utils.HasMatchingEndTag(last.Name, document, offset) && !last.Name.StartsWith("/") && !last.Name.StartsWith("!--")) {
							editor.Document.Insert(offset, "></" + last.Name + ">");
							editor.Caret.Offset = offset + 1;
							return true;
						}
					}
				}
			}
			
			return false;
		}

		public bool CtrlSpace(ITextEditor editor)
		{
			XamlContext context = CompletionDataHelper.ResolveContext(editor, ' ');
			context.Forced = true;
			
			var info = ParserService.GetParseInformation(editor.FileName);
			if (context.Path != null) {
				if (!XmlParser.IsInsideAttributeValue(editor.Document.Text, editor.Caret.Offset)) {
					var list = CompletionDataHelper.CreateListForContext(editor, context) as XamlCompletionItemList;
					string starter = editor.GetWordBeforeCaret().Trim('<', '>');
					if (!string.IsNullOrEmpty(starter) && !starter.EndsWith(StringComparison.Ordinal, ' ', '\t', '\n', '\r'))
						list.PreselectionLength = starter.Length;
					editor.ShowCompletionWindow(list);
					return true;
				} else {
					// DO NOT USE CompletionDataHelper.CreateListForContext here!!! might result in endless recursion!!!!
					if (!string.IsNullOrEmpty(context.AttributeName)) {
						string valueBeforeCaret = context.RawAttributeValue.Substring(0, context.ValueStartOffset);
						if (valueBeforeCaret.StartsWith("{}", StringComparison.OrdinalIgnoreCase))
							return false;
						
						if (!DoMarkupExtensionCompletion(editor, info, context)) {
							XamlResolver resolver = new XamlResolver();
							var completionList = new XamlCompletionItemList();
							
							var mrr = resolver.Resolve(new ExpressionResult(context.AttributeName, new XamlExpressionContext(context.Path, context.AttributeName, false)),
							                           info, editor.Document.Text) as MemberResolveResult;
							if (mrr != null && mrr.ResolvedType != null) {
								var c = mrr.ResolvedType.GetUnderlyingClass();
								
								completionList.Items.AddRange(CompletionDataHelper.MemberCompletion(editor, mrr.ResolvedType));
								editor.ShowInsightWindow(CompletionDataHelper.MemberInsight(mrr));
							}
							
							if (context.AttributeName == "Property" && context.Path.Elements.LastOrDefault().Name == "Setter") {
								int offset = Utils.GetParentElementStart(editor);
								string[] attributes = Utils.GetListOfExistingAttributeNames(editor.Document.Text, offset);
								
								if (attributes.Contains("TargetType")) {
									AttributeValue value = MarkupExtensionParser.ParseValue(Utils.GetAttributeValue(editor.Document.Text, offset, "TargetType"));
									if (!value.IsString) {
										TypeResolveResult trr = CompletionDataHelper.ResolveMarkupExtensionType(value.ExtensionValue, info, editor, context.Path);
										
										var typeName = CompletionDataHelper.ResolveType(GetTypeNameFromTypeExtension(value.ExtensionValue), context, editor);
										
										if (trr != null && trr.ResolvedClass != null && trr.ResolvedClass.FullyQualifiedName == "System.Windows.Markup.TypeExtension"
										    && typeName != null && typeName != null) {
						/*					completionList.Items.AddRange(
												typeName.ResolvedClass.Properties
												.Where(p => p.IsPublic && p.CanSet)
												.Select(prop => new DefaultCompletionItem(prop.Name))
												.Cast<ICompletionItem>()
											);*/
										}
									}
								}
							}
							
							if (context.AttributeName.StartsWith("xmlns"))
								completionList.Items.AddRange(CompletionDataHelper.CreateListForXmlnsCompletion(info.BestCompilationUnit.ProjectContent));
							
							ICompletionListWindow window = editor.ShowCompletionWindow(completionList);
							
							if (context.AttributeName.StartsWith("xmlns"))
								window.Width = double.NaN;
							
							return true;
						}
					}
				}
			}
			return false;
		}
		
		static string GetTypeNameFromTypeExtension(MarkupExtensionInfo info)
		{
			var item = info.PositionalArguments.FirstOrDefault();
			if (item != null && item.IsString) {
				return item.StringValue;
			} else {
				if (info.NamedArguments.TryGetValue("typename", out item)) {
					if (item.IsString)
						return item.StringValue;
				}
			}
			
			return string.Empty;
		}

		static bool DoMarkupExtensionCompletion(ITextEditor editor, ParseInformation info, XamlContext context)
		{
			if (context.AttributeValue != null && !context.AttributeValue.IsString) {
				var completionList = CompletionDataHelper.CreateMarkupExtensionCompletion(context, info, editor);
				editor.ShowCompletionWindow(completionList);
				var insightList = CompletionDataHelper.CreateMarkupExtensionInsight(context, info, editor);
				//editor.ShowInsightWindow(insightList);
				return true;
			}
			
			return false;
		}
	}
}
