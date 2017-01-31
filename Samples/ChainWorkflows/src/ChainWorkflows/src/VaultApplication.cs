using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace ChainWorkflows
{
	public class Configuration
	{
		internal const string WorkflowOneAlias = "MFiles.Workflows.Workflow1";
		internal const string WorkflowTwoAlias = "MFiles.Workflows.Workflow2";
		internal const string WorkflowOneStateFinalAlias = "MFiles.WorkflowStates.Workflow1.Final";
		internal const string WorkflowTwoStateInitialAlias = "MFiles.WorkflowStates.Workflow2.Initial";

		[MFWorkflow(Required = false)]
		public MFIdentifier WorkflowOne = WorkflowOneAlias;

		[MFWorkflow(Required = false)]
		public MFIdentifier WorkflowTwo = WorkflowTwoAlias;

		[MFState(Required = false)]
		public MFIdentifier WorkflowOneStateFinal = WorkflowOneStateFinalAlias;

		[MFState(Required = false)]
		public MFIdentifier WorkflowTwoStateInitial= WorkflowTwoStateInitialAlias;

	}

	/// <summary>
	/// Simple vault application to demonstrate VAF.
	/// </summary>
	public class VaultApplication : VaultApplicationBase
	{
		[MFConfiguration("ChainWorkflows", "config")]
		private Configuration config = new Configuration();



		[StateAction(Configuration.WorkflowOneStateFinalAlias)]
		public void HandleWorkflowOneFinalState(StateEnvironment env)
		{
			// Move it to the next workflow.
			env.ObjVerEx.SetProperty((int) MFBuiltInPropertyDef.MFBuiltInPropertyDefWorkflow,
				MFDataType.MFDatatypeLookup,
				this.config.WorkflowTwo);

			// Move it to the next state.
			env.ObjVerEx.SetProperty((int) MFBuiltInPropertyDef.MFBuiltInPropertyDefState,
				MFDataType.MFDatatypeLookup,
				this.config.WorkflowTwoStateInitial);

			// Save the properties.
			env.ObjVerEx.SaveProperties();

			// Ensure the last modified is correct.
			var lastModifiedBy = new TypedValue();
			lastModifiedBy.SetValue(MFDataType.MFDatatypeLookup, env.CurrentUserID);
			env.Vault.ObjectPropertyOperations.SetLastModificationInfoAdmin(env.ObjVer,
				UpdateLastModified: false,
				UpdateLastModifiedBy: true,
				LastModifiedBy: lastModifiedBy,
				LastModifiedUtc: null);
		}


	}
}