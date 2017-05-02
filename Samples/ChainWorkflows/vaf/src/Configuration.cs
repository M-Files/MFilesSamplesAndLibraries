using MFiles.VAF.Common;

namespace ChainWorkflows
{
	/// <summary>
	/// Configuration for the ChainWorkflows sample to run.
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// The alias of the first workflow (the one we will move FROM).
		/// </summary>
		internal const string WorkflowOneAlias = "MFiles.Workflows.Workflow1";

		/// <summary>
		/// The alias of the second workflow (the one we will move TO).
		/// </summary>
		internal const string WorkflowTwoAlias = "MFiles.Workflows.Workflow2";

		/// <summary>
		/// The alias of the final state in the first workflow (the one we will move FROM).
		/// </summary>
		internal const string WorkflowOneStateFinalAlias = "MFiles.WorkflowStates.Workflow1.Final";

		/// <summary>
		/// The alias of the initial state in the second workflow (the one we will move TO).
		/// </summary>
		internal const string WorkflowTwoStateInitialAlias = "MFiles.WorkflowStates.Workflow2.Initial";

		/// <summary>
		/// A reference to the first workflow.
		/// </summary>
		/// <remarks>This is initially set to <see cref="WorkflowOneAlias"/>, and is resolved at runtime to the correct Id.</remarks>

		[MFWorkflow(Required = true)]
		public MFIdentifier WorkflowOne = Configuration.WorkflowOneAlias;

		/// <summary>
		/// A reference to the second workflow.
		/// </summary>
		/// <remarks>This is initially set to <see cref="WorkflowTwoAlias"/>, and is resolved at runtime to the correct Id.</remarks>
		[MFWorkflow(Required = true)]
		public MFIdentifier WorkflowTwo = Configuration.WorkflowTwoAlias;

		/// <summary>
		/// A reference to the final state on the first workflow.
		/// </summary>
		/// <remarks>This is initially set to <see cref="WorkflowOneStateFinalAlias"/>, and is resolved at runtime to the correct Id.</remarks>
		[MFState(Required = true)]
		public MFIdentifier WorkflowOneStateFinal = Configuration.WorkflowOneStateFinalAlias;

		/// <summary>
		/// A reference to the initial state on the second workflow.
		/// </summary>
		/// <remarks>This is initially set to <see cref="WorkflowTwoStateInitialAlias"/>, and is resolved at runtime to the correct Id.</remarks>
		[MFState(Required = true)]
		public MFIdentifier WorkflowTwoStateInitial= Configuration.WorkflowTwoStateInitialAlias;

	}
}