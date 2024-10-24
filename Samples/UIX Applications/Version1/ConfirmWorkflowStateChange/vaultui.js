// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewVaultUI( vaultUI )
{
	// This is the start point of a VaultUI module.

	// Register to be notified when a VaultEntry starts.
	// REF: https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_NewVaultEntry.html
	vaultUI.Events.Register(
		Event_NewVaultEntry,
		HandleOnNewVaultEntry );
}

function HandleOnNewVaultEntry( vaultEntry )
{
	// Register for the OnSetPropertiesOfObjectVersion event.
	// REF: https://www.m-files.com/UI_Extensibility_Framework/Event_SetPropertiesOfObjectVersion.html
	// NOTE: There is also a OnSetPropertiesOfObjectVersions event when multiple objects are selected; may be worth handling that too.
	// REF: https://www.m-files.com/UI_Extensibility_Framework/Event_SetPropertiesOfObjectVersions.html
	vaultEntry.Events.Register(
		Event_SetPropertiesOfObjectVersion,
		function(setPropertiesParams, singlePropertyUpdate, singlePropertyRemove)
		{
			/// <summary>Handles the OnSetPropertiesOfObjectVersion event for an IVaultEntry.</summary>
			/// <param name="setPropertiesParams" type="MFiles.SetPropertiesParams">
			/// Collection representing the parameters for setting the properties on one object version.
			/// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~SetPropertiesParams.html
			/// </param> 
			/// <param name="singlePropertyUpdate" type="bool">If true, a single property value update is requested. A single value must be specified in PropertyValuesToSet collection. The callee must not specify different change set.</param>
			/// <param name="singlePropertyRemove" type="bool">If true, a single property value remove is requested. A single value must be specified in PropertyValuesToRemove collection. The callee must not specify different change set.</param>
		
			// Attempt to retrieve the state transition property from the list of properties that was set.
			var stateTransitionPropertyValue = setPropertiesParams.PropertyValuesToSet.SearchForPropertyEx(MFBuiltInPropertyDefStateTransition, true);
			if(!(stateTransitionPropertyValue))
			{
				return true; // No state transition provided, so let it work.
			}

			// Retrieve the ID of the state transition.
			// NOTE: This is unused in this code, but should probably not prompt on every state transition; use sparingly!
			// NOTE: Finding the state transition ID can be difficult.  To do this:
			//		 * Right-click on the state transition within the M-Files Admin graphical workflow editor (the line between states).
			//		 * Select "Properties"
			//		 * On the "General" tab, provide the state transition with a name.
			//		 * Save the changes and go to the "Value Lists" section within "Metadata Structure (Flat View)".
			//		 * Select "Show All Value Lists".
			//		 * Right-click on "State Transitions" (ID 17) and select "Contents".
			//		 * Locate the state transition with the name provided earlier and note its ID.
			//	ALTERNATIVELY: GetWorkflowStateTransitionIDByAlias can be used to look up the transition by alias.
			//	REF: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~VaultWorkflowOperations~GetWorkflowStateTransitionIDByAlias.html
			// var stateTransitionID = stateTransitionPropertyValue.Value.GetLookupID();

			// Prompt the user whether to continue or not.
			// REF: https://www.m-files.com/UI_Extensibility_Framework/index.html#ShowingMessageBoxes.html
			var clickedButton = vaultEntry.VaultUI.ShowMessage({
				message: "Are you sure you wish to do this?",
				button1_title: "Yes",
				button2_title: "No",
				defaultButton: 2
			});

			// If they clicked "Yes" then return true (allow the operation).
			// Otherwise disallow the operation.
			// NOTE: The index is one-based, so 1 is the "Yes" button above.
			return clickedButton == 1;
		} );
}