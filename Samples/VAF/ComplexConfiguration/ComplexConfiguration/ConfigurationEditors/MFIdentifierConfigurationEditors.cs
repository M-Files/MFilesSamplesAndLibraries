using System.Runtime.Serialization;
using MFiles.VAF.Configuration;

namespace ComplexConfiguration.ConfigurationEditors
{
	/// <summary>
	/// Showcases options for referencing vault structure.
	/// </summary>
	[DataContract]
	// ReSharper disable once InconsistentNaming
	public class MFIdentifierConfigurationEditors
	{

		#region Basic vault structure references

		/// <summary>
		/// Allows the user to choose a single class.
		/// </summary>
		[MFClass]
		[DataMember]
		public MFIdentifier ClassValue { get; set; }

		/// <summary>
		/// Allows the user to choose a single object type.
		/// </summary>
		[MFObjType]
		[DataMember]
		public MFIdentifier ObjectTypeValue { get; set; }

		/// <summary>
		/// Allows the user to choose a single property definition.
		/// </summary>
		[MFPropertyDef]
		[DataMember]
		public MFIdentifier PropertyDefValue { get; set; }

		/// <summary>
		/// Allows the user to choose a single workflow.
		/// </summary>
		[MFWorkflow]
		[DataMember]
		public MFIdentifier WorkflowValue { get; set; }

		/// <summary>
		/// Allows the user to choose a single workflow state.
		/// </summary>
		[MFState]
		[DataMember]
		public MFIdentifier StateValue { get; set; }

		/// <summary>
		/// Allows the user to choose a single user group.
		/// </summary>
		[MFUserGroup]
		[DataMember]
		public MFIdentifier UserGroupValue { get; set; }

		#endregion

		#region Default values

		/// <summary>
		/// A default value for <see cref="PropertyDefValueWithDefault"/>.
		/// </summary>
		private const string defaultValue = "MFiles.PropertyDef.Keywords";

		/// <summary>
		/// A simple identifier that has a default alias.
		/// </summary>
		/// <remarks>If a property definition exists with this alias then the identifier will be automatically resolved to an ID.</remarks>
		[DataMember]
		[MFPropertyDef]
		[JsonConfEditor(DefaultValue = MFIdentifierConfigurationEditors.defaultValue)]
		public MFIdentifier PropertyDefValueWithDefault { get; set; }
			= MFIdentifierConfigurationEditors.defaultValue;

		#endregion

		#region Custom label

		/// <summary>
		/// A simple identifier that has a custom label.
		/// </summary>
		[MFClass]
		[DataMember]
		[JsonConfEditor(Label = "Class selection with a custom label")]
		public MFIdentifier ClassValueWithCustomLabel { get; set; }

		#endregion

		#region Help text
		
		/// <summary>
		/// Allows the user to choose a single object type.
		/// </summary>
		[MFObjType]
		[DataMember]
		[JsonConfEditor(HelpText = "This is shown when the user clicks the small 'i' next to the configuration option.")]
		public MFIdentifier ObjectTypeValueWithHelpText { get; set; }

		#endregion

	}
}
