using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RestSharp;

namespace MFaaP.MFWSClient
{
	/// <summary>
	/// Event argument for the <see cref="MFWSClientBase.BeforeExecuteRequest"/> event.
	/// </summary>
	public class RestResponseEventArgs
		: EventArgs
	{
		/// <summary>
		/// The response from the request.
		/// </summary>
		public IRestResponse RestResponse { get; private set; }

		/// <summary>
		/// Instantiates a <see cref="RestResponseEventArgs"/> for the supplied response.
		/// </summary>
		/// <param name="restResponse">The response from the request.</param>
		public RestResponseEventArgs(IRestResponse restResponse)
		{
			this.RestResponse = restResponse;
		}
	}
}