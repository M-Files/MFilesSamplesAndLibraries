using MFiles.VAF;
using MFiles.VAF.AppTasks;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFiles.VaultApplications.Logging;
using MFiles.VaultApplications.Logging.Resources;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WithoutVAFExtensionsLibrary
{
    /// <summary>
    /// This shows how to log in the VAF (https://developer.m-files.com/Frameworks/Logging/Vault-Application-Framework/).
    /// This sample does not require the VAF Extensions library.
    /// </summary>
    public class VaultApplication
        : ConfigurableVaultApplicationBase<Configuration>
	{

		/// <summary>
		/// Handles the <see cref="MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize" /> event.
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize)]
        public void BeforeCheckInChangesFinalize(EventHandlerEnvironment env)
		{
			// You can log at different levels: https://developer.m-files.com/Frameworks/Logging/#log-levels
			this.Logger?.Trace("Starting job");
		}

        #region Boilerplate required

		/// <summary>
		/// The logger for this class.
		/// </summary>
        public ILogger Logger { get; private set; }

		/// <inheritdoc />
		protected override void StartApplication()
		{
			base.StartApplication();

#if DEBUG
			// Enable logging to any attached debugger, but do not launch the debugger.
			LogManager.EnableLoggingToAttachedDebugger(launchDebugger: false);
#endif

			// Initialize the log manager and this class' logger.
			LogManager.Initialize(this.PermanentVault, this.Configuration?.LoggingConfiguration);
			this.Logger = LogManager.GetLogger(this.GetType());
			this.Logger?.Info("Logging started");

		}

		/// <inheritdoc />
		protected override void UninitializeApplication(Vault vault)
		{
			// Ensure all logging is stopped.
			this.Logger?.Info("Logging stopping");
			LogManager.Shutdown();
			base.UninitializeApplication(vault);
		}

		/// <inheritdoc />
		protected override void OnConfigurationUpdated(Configuration oldConfiguration, bool updateExternals)
		{
			// Ensure that the logging configuration is updated.
			this.Logger?.Info("Logging configuration updating");
			base.OnConfigurationUpdated(oldConfiguration, updateExternals);
			LogManager.UpdateConfiguration(this.Configuration?.LoggingConfiguration);
			this.Logger?.Info("Logging configuration updated");
		}

		/// <inheritdoc />
		// Sets up the combined resource manager so that the strings from the logging
		// exceptions and configuration work.
		protected override SecureConfigurationManager<Configuration> GetConfigurationManager()
		{
			var configurationManager = base.GetConfigurationManager();

			// Set the resource manager for the configuration manager.
			var combinedResourceManager = new CombinedResourceManager(true, configurationManager.ResourceManager);

			// Set the resource manager for the configuration.
			configurationManager.ResourceManager = combinedResourceManager;
			return configurationManager;
		}

		/// <inheritdoc />
		protected override IEnumerable<ValidationFinding> CustomValidation(Vault vault, Configuration config)
		{
			// Return any base validation errors.
			foreach (var finding in base.CustomValidation(vault, config) ?? new ValidationFinding[0])
				yield return finding;

			// Add in the logging validation errors.
			foreach (var finding in config?.LoggingConfiguration?.GetValidationFindings() ?? new ValidationFinding[0])
				yield return finding;
		}

		#endregion
	}
}