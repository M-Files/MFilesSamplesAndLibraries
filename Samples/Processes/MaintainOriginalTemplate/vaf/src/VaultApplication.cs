using System;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace MaintainOriginalTemplate
{
	/// <summary>
	/// A class containing configuration that can be altered by the administrator
	/// using the Named Value Manager.
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// The property definition to use to store the "Original Template".
		/// </summary>
		[MFPropertyDef(Required = false)]
		public MFIdentifier OriginalTemplatePropertyDef
			= "MFiles.PropertyDef.OriginalTemplate";
	}
	
	public class VaultApplication : VaultApplicationBase
	{
		/// <summary>
		/// The current running configuration.
		/// </summary>
		[MFConfiguration("MaintainOriginalTemplate", "config")]
		private readonly Configuration config = new Configuration();

		/// <summary>
		/// Ensures that the "Original Template" property is maintained on templates.
		/// </summary>
		/// <param name="env">The vault/object environment.</param>
		[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize,
			ObjectType=(int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
		[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize,
			ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
		public void MaintainOriginalTemplateProperty(EventHandlerEnvironment env)
		{
			// Sanity.
			if (null == env?.ObjVerEx)
				return;

			// Only do this for objects that have the "Is template" property set to true!
			var isTemplatePropertyValue = env
				.ObjVerEx
				.Properties
				.SearchForPropertyEx((int) MFBuiltInPropertyDef.MFBuiltInPropertyDefIsTemplate, true);
			if (null == isTemplatePropertyValue?.Value
				|| true == isTemplatePropertyValue.Value.IsEmpty()
				|| true == isTemplatePropertyValue.Value.IsNULL()
				|| true == isTemplatePropertyValue.Value.IsUninitialized())
				return; // The property was not on the object or was empty in some way.
			if (true != (bool) isTemplatePropertyValue.Value.Value)
				return; // "Is template" was not true.

			// Retrieve (and remove) the current "Original Template" property value,
			// or create one if it's not there.
			var originalTemplatePropertyValue =
				env.ObjVerEx.Properties.RemoveProperty(this.config.OriginalTemplatePropertyDef.ID)
				?? new PropertyValue()
				{
					PropertyDef = this.config.OriginalTemplatePropertyDef.ID
				};

			// Create a lookup pointing at the current object.
			// Ensuring that the "Version" is set will mean that this will point
			// to this specific version of the object.
			var lookup = new Lookup
			{
				ObjectType = env.ObjVer.Type,
				Item = env.ObjVer.ID,
				Version = env.ObjVer.Version
			};

			// Set the property value.
			originalTemplatePropertyValue.Value.SetValueToLookup(lookup);

			// Update the object.
			env.ObjVerEx.Properties.Add(-1, originalTemplatePropertyValue);
			env.ObjVerEx.SaveProperties();

			// Audit.
			// ref: http://developer.m-files.com/Built-In/VBScript/Audit-Trail-And-Scripting/
			env.ObjVerEx.SetModifiedBy(env.CurrentUserID);
		}


	}
}