using RestSharp;

namespace MFaaP.MFWSClient
{
	public partial class MFWSClient
		: MFWSClientBase
	{

		/// <summary>
		/// Creates an MFWSClient pointing at the MFWA site.
		/// </summary>
		/// <param name="baseUrl">The base url of the MFWA (web access) site; note that this should be of the form
		/// "http://localhost", not of the form "http://localhost/REST".</param>
		public MFWSClient(string baseUrl)
			: base(baseUrl)
		{
		}

		/// <summary>
		/// Creates an MFWSClient pointing at the MFWA site.
		/// </summary>
		/// <param name="restClient">The <see cref="IRestClient"/> to use for HTTP requests.</param>
		protected MFWSClient(IRestClient restClient)
			: base(restClient)
		{
		}
	}
	
}
