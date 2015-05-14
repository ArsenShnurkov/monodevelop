// 
// CompletionListWindow.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui.Content;
using MonoDevelop.Ide.Editor.Extension;

namespace MonoDevelop.Ide.CodeCompletion
{
	public class CompletionWindowManager
	{
		static CompletionListWindow wnd;
		
		public static bool IsVisible {
			get {
				return wnd != null && wnd.Visible;
			}
		}
		
		public static CompletionListWindow Wnd {
			get { return wnd; }
		}
		
		public static int X {
			get {
				return wnd.X;
			}
		}
		
		public static int Y {
			get {
				return wnd.Y;
			}
		}
		
		public static CodeCompletionContext CodeCompletionContext {
			get {
				return wnd.CodeCompletionContext;
			}
		}

		static CompletionWindowManager ()
		{
			if (IdeApp.Workbench != null)
				IdeApp.Workbench.RootWindow.Destroyed += (sender, e) => DestroyWindow ();
			
			IdeApp.Preferences.ForceSuggestionMode.Changed += (s,a) => {
				if (wnd != null)
					wnd.AutoCompleteEmptyMatch = wnd.AutoSelect = !IdeApp.Preferences.ForceSuggestionMode;
			};
		}
		
		// ext may be null, but then parameter completion don't work
		internal static bool ShowWindow (CompletionTextEditorExtension ext, char firstChar, ICompletionDataList list, ICompletionWidget completionWidget, CodeCompletionContext completionContext)
		{
			try {
				if (ext != null) {
					int inserted = ext.Editor.EnsureCaretIsNotVirtual ();
					if (inserted > 0)
						completionContext.TriggerOffset = ext.Editor.CaretOffset;
				}
				if (wnd == null) {
					wnd = new CompletionListWindow ();
					wnd.WordCompleted += HandleWndWordCompleted;
				}
				if (ext != null) {
					var widget = ext.Editor.GetNativeWidget<Gtk.Widget> ();
					wnd.TransientFor = widget?.Parent?.Toplevel as Gtk.Window;
				}
				wnd.Extension = ext;
				try {
					if (!wnd.ShowListWindow (firstChar, list, completionWidget, completionContext)) {
						if (list is IDisposable)
							((IDisposable)list).Dispose ();
						HideWindow ();
						return false;
					}
					
					if (IdeApp.Preferences.ForceSuggestionMode)
						wnd.AutoSelect = false;
					wnd.Show ();
					DesktopService.RemoveWindowShadow (wnd);
					OnWindowShown (EventArgs.Empty);
					return true;
				} catch (Exception ex) {
					LoggingService.LogError (ex.ToString ());
					return false;
				}
			} finally {
				ParameterInformationWindowManager.UpdateWindow (ext, completionWidget);
			}
		}

		static void HandleWndWordCompleted (object sender, CodeCompletionContextEventArgs e)
		{
			EventHandler<CodeCompletionContextEventArgs> handler = WordCompleted;
			if (handler != null)
				handler (sender, e);
		}
		
		public static event EventHandler<CodeCompletionContextEventArgs> WordCompleted;

		static void DestroyWindow ()
		{
			if (wnd != null) {
				wnd.Destroy ();
				wnd = null;
			}
			OnWindowClosed (EventArgs.Empty);
		}
		
		public static bool PreProcessKeyEvent (KeyDescriptor descriptor)
		{
			if (!IsVisible)
				return false;
			if (descriptor.KeyChar != '\0') {
				wnd.EndOffset = wnd.StartOffset + wnd.CurrentPartialWord.Length + 1;
			}
			return wnd.PreProcessKeyEvent (descriptor);
		}

		public static void UpdateCursorPosition ()
		{
			if (!IsVisible)
				return;
			if (wnd.IsInCompletion)
				return;
			var caretOffset = wnd.CompletionWidget.CaretOffset;
			if (caretOffset < wnd.StartOffset || caretOffset > wnd.EndOffset)
				HideWindow ();
		}

		public static void UpdateWordSelection (string text)
		{
			if (IsVisible) {
				wnd.List.CompletionString = text;
				wnd.UpdateWordSelection ();
			}
		}

		public static void PostProcessKeyEvent (KeyDescriptor descriptor)
		{
			if (!IsVisible)
				return;
			wnd.PostProcessKeyEvent (descriptor);
		}

		public static void RepositionWindow ()
		{
			if (!IsVisible)
				return;
			wnd.RepositionWindow ();
		}
		
		public static void HideWindow ()
		{
			if (!IsVisible)
				return;
			ParameterInformationWindowManager.UpdateWindow (wnd.Extension, wnd.CompletionWidget);
			if (wnd.Extension != null)
				wnd.Extension.Editor.FixVirtualIndentation ();
			wnd.HideWindow ();
			OnWindowClosed (EventArgs.Empty);
			//DestroyWindow ();
		}
		
		
		static void OnWindowClosed (EventArgs e)
		{
			var handler = WindowClosed;
			if (handler != null)
				handler (null, e);
		}

		public static event EventHandler WindowClosed;
		
		static void OnWindowShown (EventArgs e)
		{
			var handler = WindowShown;
			if (handler != null)
				handler (null, e);
		}
		
		public static event EventHandler WindowShown;
	}
}
