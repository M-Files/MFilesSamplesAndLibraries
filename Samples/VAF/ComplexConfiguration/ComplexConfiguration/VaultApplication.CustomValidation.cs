using System;
using System.Collections.Generic;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration
{
	public partial class VaultApplication
	{
		/// <summary>
		/// Provides server-side validation of the current configuration.
		/// </summary>
		/// <param name="myConfiguration">The configuration to validate.</param>
		/// <returns>Validation findings.</returns>
		private IEnumerable<ValidationFinding> CustomValidator(MyConfiguration myConfiguration)
		{
			// Sanity.
			if(null == myConfiguration)
				throw new ArgumentNullException(nameof(myConfiguration));

			// Return an informational validation finding.
			yield return new ValidationFinding(ValidationFindingType.Info,
				string.Empty, 
				"This is an informational finding related to no specific configuration part.");

			// Return something which is okay.
			yield return new ValidationFinding(ValidationFindingType.Ok,
				"/ConfigurationEditors",
				"The configuration editors are not null.");

			// Return an error.
			// NOTE: Errors do not stop the configuration from saving or becoming active!
			if (null == myConfiguration?.ConfigurationEditors?.DateConfigurationEditors?.SimpleDateValue)
			{
				yield return new ValidationFinding(ValidationFindingType.Error,
					"/ConfigurationEditors/DateConfigurationEditors/SimpleDateValue",
					"Simple date value was not provided");
			}

		}

	}
}