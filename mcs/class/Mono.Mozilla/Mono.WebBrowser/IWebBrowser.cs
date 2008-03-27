// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2007, 2008 Novell, Inc.
//
// Authors:
//	Andreia Gaita (avidigal@novell.com)
//

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Specialized;
using Mono.WebBrowser.DOM;

namespace Mono.WebBrowser
{
	public interface IWebBrowser
	{
		bool Load (IntPtr handle, int width, int height);
		void Shutdown ();
		void FocusIn (FocusOption focus);
		void FocusOut ();
		void Activate ();
		void Deactivate ();
		void Resize (int width, int height);

		void Render (byte[] data);
		void Render (string html);
		void Render (string html, string uri, string contentType);
			
		
		bool Initialized { get; }
		IWindow Window { get; }
		IDocument Document { get; }
		INavigation Navigation { get; }

		event NodeEventHandler KeyDown;
		event NodeEventHandler KeyPress;
		event NodeEventHandler KeyUp;
		event NodeEventHandler MouseClick;
		event NodeEventHandler MouseDoubleClick;
		event NodeEventHandler MouseDown;
		event NodeEventHandler MouseEnter;
		event NodeEventHandler MouseLeave;
		event NodeEventHandler MouseMove;
		event NodeEventHandler MouseUp;
		event EventHandler Focus;
		event CreateNewWindowEventHandler CreateNewWindow;
		event AlertEventHandler Alert;
		event EventHandler Transferring;
		event EventHandler DocumentCompleted;
		event EventHandler Completed;
		
		event EventHandler Generic;		
	}

	public enum ReloadOption : uint
	{
		None = 0,
		Proxy = 1,
		Full = 2
	}

	public enum FocusOption
	{
		None = 0,
		FocusFirstElement = 1,
		FocusLastElement = 2
	}

	[Flags]
	public enum DialogButtonFlags
	{
		BUTTON_POS_0 = 1,
		BUTTON_POS_1 = 256,
		BUTTON_POS_2 = 65536,
		BUTTON_TITLE_OK = 1,
		BUTTON_TITLE_CANCEL = 2,
		BUTTON_TITLE_YES = 3,
		BUTTON_TITLE_NO = 4,
		BUTTON_TITLE_SAVE = 5,
		BUTTON_TITLE_DONT_SAVE = 6,
		BUTTON_TITLE_REVERT = 7,
		BUTTON_TITLE_IS_STRING = 127,
		BUTTON_POS_0_DEFAULT = 0,
		BUTTON_POS_1_DEFAULT = 16777216,
		BUTTON_POS_2_DEFAULT = 33554432,
		BUTTON_DELAY_ENABLE = 67108864,
		STD_OK_CANCEL_BUTTONS = 513
	}

	public enum DialogType
	{
		Alert = 1,
		AlertCheck = 2,
		Confirm = 3,
		ConfirmEx = 4,
		ConfirmCheck = 5,
		Prompt = 6,
		PromptUsernamePassword = 7,
		PromptPassword = 8,
		Select = 9
	}

	public enum Platform
	{
		Unknown = 0,
		Winforms = 1,
		Gtk = 2
	}

	public delegate bool CreateNewWindowEventHandler (object sender, CreateNewWindowEventArgs e);
	public class CreateNewWindowEventArgs : EventArgs
	{
		private bool isModal;

		#region Public Constructors
		public CreateNewWindowEventArgs (bool isModal)
			: base ()
		{
			this.isModal = isModal;
		}
		#endregion	// Public Constructors

		#region Public Instance Properties
		public bool IsModal
		{
			get { return this.isModal; }
		}
		#endregion	// Public Instance Properties
	}


	public delegate void AlertEventHandler (object sender, AlertEventArgs e);
	public class AlertEventArgs : EventArgs
	{
		private DialogType type;
		private string title;
		private string text;
		private string text2;
		
		private string username;
		private string password;
		private string checkMsg; 
		private bool checkState;
		private DialogButtonFlags dialogButtons;
		
		private StringCollection buttons;
		private StringCollection options;

		private object returnValue;

		#region Public Constructors
		/// <summary>
		/// void (STDCALL *OnAlert) (const PRUnichar * title, const PRUnichar * text);
		/// </summary>
		/// <param name="title"></param>
		/// <param name="text"></param>
		public AlertEventArgs ()
			: base ()
		{
		}


#endregion	// Public Constructors

#region Public Instance Properties
		public DialogType Type {
			get { return this.type; }
			set { this.type = value; }
		}
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		public string Text {
			get { return this.text; }
			set { this.text = value; }
		}
		public string Text2 {
			get { return this.text2; }
			set { this.text2 = value; }
		}
		public string CheckMessage
		{
			get { return this.checkMsg; }
			set { this.checkMsg = value; }
		}
		public bool CheckState {
			get { return this.checkState; }
			set { this.checkState = value; }
		}
		public DialogButtonFlags DialogButtons {
			get { return this.dialogButtons; }
			set { this.dialogButtons = value; }
		}
		public StringCollection Buttons {
			get { return buttons; }
			set { buttons = value; }
		}
		public StringCollection Options {
			get { return options; }
			set { options = value; }
		}

		public string Username {
			get { return username; }
			set { username = value; }
		}

		public string Password {
			get { return password; }
			set { password = value; }
		}

		public bool BoolReturn {
			get { 
				if (returnValue is bool)
					return (bool) returnValue;
				return false;
			}
			set { returnValue = value; }
		}

		public int IntReturn
		{
			get { 
				if (returnValue is int)
					return (int) returnValue;
				return -1;
			}
			set { returnValue = value; }
		}

		public string StringReturn
		{
			get { 
				if (returnValue is string)
					return (string) returnValue;
				return String.Empty;
			}
			set { returnValue = value; }
		}

#endregion
	}
}
