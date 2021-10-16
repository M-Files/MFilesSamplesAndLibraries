using MFiles.VAF.Common;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Configuration.JsonEditor;
using MFiles.VAF.Core;

using MultiLingualConfiguration.Properties;

using System;

namespace MultiLingualConfiguration
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication
        : ConfigurableVaultApplicationBase<Configuration>
    {
        /// <summary>
        /// Override for setting resource manager before running the logic of the base class
        /// </summary>
        /// <param name="context">The context for the request</param>
        /// <returns>The schema for the application's configuration.</returns>
        public override Schema GetConfigurationSchema(IConfigurationRequestContext context)
        {
            // Sanity
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // ConfManager should be initialized here if standard logic was not changed
            ConfManager.ResourceManager = Resources.ResourceManager;

            // Call base class
            return base.GetConfigurationSchema(context);
        }

        /// <summary>
        /// Respond to changes in my configuration
        /// </summary>
        /// <param name="oldConfiguration">The configuration that was just replaced.</param>
        /// <param name="updateExternals">see base class</param>
        protected override void OnConfigurationUpdated(Configuration oldConfiguration, bool updateExternals)
        {
            if (oldConfiguration.Enabled != Configuration.Enabled)
            {
                if (Configuration.Enabled)
                    SysUtils.ReportInfoToEventLog(Resources.Message_VAF_Enabled);
                else
                    SysUtils.ReportInfoToEventLog(Resources.Message_VAF_Disabled);
            }

            if (!oldConfiguration.Selections.Equals(Configuration.Selections))
            {
                string selections = "";
                foreach (SelectionOption selection in Configuration.Selections)
                {
                    switch (selection.SelectedOption)
                    {
                        case SelectionOptions.First:
                            selections += $"\n- {Resources.Label_SelectionOptions_First}";
                            break;
                        case SelectionOptions.Second:
                            selections += $"\n- {Resources.Label_SelectionOptions_Second}";
                            break;
                        case SelectionOptions.Third:
                            selections += $"\n- {Resources.Label_SelectionOptions_Third}";
                            break;
                        default:
                            selections += $"\n- {Resources.Label_SelectionOptions_Invalid}";
                            break;
                    }
                }

                SysUtils.ReportInfoToEventLog(
                    Resources.Message_Selection_Changed 
                    + selections == "" ? $"\n- {Resources.Message_No_Selection_Option}" : selections);
            }

            base.OnConfigurationUpdated(oldConfiguration, updateExternals);
        }
    }
}