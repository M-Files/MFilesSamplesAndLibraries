using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace ChainWorkflows
{
	/// <summary>
	/// A vault application that moves objects which hit the end of one workflow
	/// to the start of another workflow.
	/// </summary>
	public class VaultApplication 
		: VaultApplicationBase
	{


		/// <summary>
		/// The running configuration, loaded from Name-Value storage.
		/// </summary>
		[MFConfiguration("ChainWorkflows", "config")]
		private readonly Configuration config = new Configuration();
		
		/// <summary>
		/// Handles when the end of the first workflow is hit,
		/// updating the object and moving it to the start of the second workflow.
		/// </summary>
		/// <param name="env">The state environment.</param>
		[StateAction(Configuration.WorkflowOneStateFinalAlias)]
		public void HandleWorkflowOneFinalState(StateEnvironment env)
		{
			// Update the local collection of property values to
			// move it to the next workflow.
			// NOTE: SetProperty only updates the in-memory copy, not the server. The subsequent call to SaveProperties actually updates the object.
			// NOTE: Using SaveProperty instead would update the version on the server immediately. Multiple calls to SaveProperty can slow your code down.
			env.ObjVerEx.SetProperty((int) MFBuiltInPropertyDef.MFBuiltInPropertyDefWorkflow,
				MFDataType.MFDatatypeLookup,
				this.config.WorkflowTwo);

			// Update the local collection of property values to
			// move it to the correct workflow state.
			// NOTE: SetProperty only updates the in-memory copy, not the server. The subsequent call to SaveProperties actually updates the object.
			// NOTE: Using SaveProperty instead would update the version on the server immediately. Multiple calls to SaveProperty can slow your code down.
			env.ObjVerEx.SetProperty((int) MFBuiltInPropertyDef.MFBuiltInPropertyDefState,
				MFDataType.MFDatatypeLookup,
				this.config.WorkflowTwoStateInitial);

			// Save the properties.
			// NOTE: Needed because of the SetProperty calls above.
			env.ObjVerEx.SaveProperties();

			// Ensure the last modified is correct.
			// NOTE: If this was not called then the object would show last modifed by "(M-Files Server)".
			var lastModifiedBy = new TypedValue();
			lastModifiedBy.SetValue(MFDataType.MFDatatypeLookup, env.CurrentUserID);
			env.Vault.ObjectPropertyOperations.SetLastModificationInfoAdmin(env.ObjVer,
				UpdateLastModifiedBy: true,
				LastModifiedBy: lastModifiedBy,
				UpdateLastModified: false,
				LastModifiedUtc: null);
		}
		
	}
}