using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using MFiles.VAF;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;

namespace XmlImporter
{
	/// <summary>
	/// The entry point for this Vault Application Framework application.
	/// </summary>
	/// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
	public partial class VaultApplication
		: ConfigurableVaultApplicationBase<Configuration>
	{
	}
}