using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace MFaaP.MFilesAPI.Tests.ExtensionMethods
{
	public static class ListParameterExtensionMethods
	{
		public static string GetQuerystringParameter(this List<Parameter> parameters, string name)
		{
			return parameters?
						.Where(p => p.Type == ParameterType.QueryString)
						.Where(p => p.Name == name)
						.Select(p => p.Value as string)
						.FirstOrDefault();
		}

		public static string GetMethodQuerystringParameter(this List<Parameter> parameters)
		{
			return parameters.GetQuerystringParameter("_method");
		}
	}
}
