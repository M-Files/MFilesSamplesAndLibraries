using System;
namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Enabling or disabling extensions enables specific functionality within the web service.
	/// </summary>
	[Flags]
	// ReSharper disable once InconsistentNaming
	public enum MFWSExtensions
	{
		/// <summary>
		/// No extensions are enabled.
		/// </summary>
		None = 0,

		/// <summary>
		/// Enables extensions commonly used with the M-Files Web Access.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		MFWA = 1,

		/// <summary>
		/// Enables IML (requires server-side version and licensing support).
		/// </summary>
		// ReSharper disable once InconsistentNaming
		IML = 2
	}
}