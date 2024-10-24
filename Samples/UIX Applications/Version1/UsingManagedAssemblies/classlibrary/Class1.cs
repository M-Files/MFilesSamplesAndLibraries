using System;
using System.Windows.Forms;

namespace MyClassLibrary
{
	/// <summary>
	/// The class which will be instantiated from the UIX application.
	/// </summary>
    public class Class1
    {
		/// <summary>
		/// Shows a message from within the managed assembly.
		/// </summary>
		/// <param name="parentHWND">A handle for the parent window (see https://www.m-files.com/UI_Extensibility_Framework/index.html#MFClientScript~IWindow~Handle.html).</param>
		/// <param name="message">The message to show, which will be prefixed by the vault name.</param>
		/// <param name="vault">The M-Files API Vault object (see https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~Vault.html).</param>
		public void ShowMessage(Int32 parentHWND, string message, dynamic vault)
		{
			// Get a reference to the window from the provided handle.
			IWin32Window parentWindow = Control.FromHandle((IntPtr)parentHWND);

			// Show the message.
			MessageBox.Show(parentWindow, $"{vault.Name} says {message}");
		}
	}
}
