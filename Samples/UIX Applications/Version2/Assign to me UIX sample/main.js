// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

// The currently-selected items in the active listing.
let currentlySelectedItems = null;

// Built in property defs.
const BuiltInPropertyDefs = {
    Class: 100,
    NameOrTitle: 0,
    SingleFlie: 22,
    AssignedTo: 44
};

// Built in object types.
const BuiltInObjectType = {
    Assignment: 10
};

// Null value of GUID.
const GUID_NULL = "{00000000-0000-0000-0000-000000000000}";

function OnNewShellUI( shellUI ) {

	/// <summary>The entry point of ShellUI module.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The new shell UI object.</param> 
	
	// Register to be notified when a new normal shell frame (Event_NewShellFrame) is created.
	// We use Event_NewShellFrame rather than Event_NewShellFrame as this won't fire for history (etc.) dialogs.
	shellUI.Events.Register(
		MFiles.Event.NewShellFrame,
		handleNewShellFrame );
}

function handleNewShellFrame( shellFrame ) {

	/// <summary>Handles the OnNewShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param> 

	// Register to listen to the started event.
	shellFrame.Events.Register(
		MFiles.Event.Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler( shellFrame ) {

	/// <summary>Gets a function to handle the Started event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnStarted">The event handler.</returns>

	// Return the handler function for ShellFrame's Started event.
	return async () => {

		// Create a command for "assign to me".
		const assignCommandId = await shellFrame.Commands.CreateCustomCommand( "Assign to me" );

		// Add the command to the main menu.
		await shellFrame.Commands.AddCustomCommandToMenu( assignCommandId, MFiles.MenuLocation.MenuLocation_TopPaneMenu, 1 );

		// Hide the command.  We will show it when the selected items change.
		await shellFrame.Commands.SetCommandState( assignCommandId, MFiles.CommandLocation.All, MFiles.CommandState.CommandState_Hidden );

		// Register to listen to when new shell listings are created.
		shellFrame.Events.Register(
			MFiles.Event.NewShellListing,
			getNewShellListingHandler( shellFrame, assignCommandId ) );

		// Is there already a listing?  If so then we need to hook into it as well.
		if( shellFrame.Listing ) {
			getNewShellListingHandler( shellFrame, assignCommandId )( shellFrame.Listing );
		}

		// Register to respond to commands being clicked.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( command ) => {

				// We only care about our command.
				// If the command being clicked is something else then return.
				if( command !== assignCommandId ) {
					return;
				}

				// Ensure we have items to process.
				if( !currentlySelectedItems ) {
					return;
				}

				// Create the assignment object.
				await createAssignmentObject( shellFrame );
			} );
	};
}

function getNewShellListingHandler( shellFrame, assignCommandId ) {

	/// <summary>Gets a function to handle the NewShellListing event for shell frame.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.Events.OnNewShellListing">The event handler.</returns>

	// Return the handler function for NewShellListing event.
	return ( shellListing ) => {

		// Listen for selection change events on the listing.
		shellListing.Events.Register(
			MFiles.Event.SelectionChanged,
			async ( selectedItems ) => {

				// Sanity.
				if( shellListing.IsActive === false ) {
					return false;
				}

				// Set the currently-selected items to null (assume nothing selected).
				currentlySelectedItems = null;

				// Has the user got any object versions selected?
				if( selectedItems.ObjectVersions.length === 0 ) {

					// Hide the menu item - there's nothing selected.
					await shellFrame.Commands.SetCommandState( assignCommandId, MFiles.CommandLocation.All, MFiles.CommandState.CommandState_Hidden );
					return false;
				}

				// Show the menu item.
				await shellFrame.Commands.SetCommandState( assignCommandId, MFiles.CommandLocation.All, MFiles.CommandState.CommandState_Active );

				// Store the selected items.
				currentlySelectedItems = selectedItems;

				// We succeeded; return true.
				return true;
			} );
	};
}

async function retrieveRelationshipPropertyValues( shellFrame, selectedItems ) {

	/// <summary>Retrieves property values representing relationships to the selecteed items.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <param name="selectedItems" type="MFiles.IShellItems">The items that are selected.</param> 
	/// <param name="Promise">A promise to be resolved with the property values for the relationship.</param> 

    // Create an array to store the property values.
	const relationshipPropertyValues = [];

    try {

        // Get the vault reference.
        const iVault = shellFrame.ShellUI.Vault;

        // Get all the object types.
        const allObjectTypes = await iVault.objectTypes.getObjectTypes( {
            call_importance: 1  // CallImportance.CALL_IMPORTANCE_NORMAL
        } );

        // Selected object types.
        const selectedObjectTypes = [];

        // Iterate over the objects and populate the properties for the assignment.
        for( let i = 0; i < selectedItems.ObjectVersions.length; i++ ) {

            // Get the item.
            const selectedItem = selectedItems.ObjectVersions[ i ];
            const objectType = allObjectTypes.object_types.find( ( item ) => item.id === selectedItem.object_info.obj_id.type );
            selectedObjectTypes.push( objectType );
        }

        // Get the default property defs of the selected objects.
        const defaultPropertyDefs = selectedObjectTypes.map( ( result ) => result.default_property_def );

        // Do we have a property value already?
        // Will happen if they select two of the same object type.
        for( let i = 0; i < defaultPropertyDefs.length; i++ ) {
            const defaultPropertyDef = defaultPropertyDefs[ i ];
            let propertyValue = relationshipPropertyValues
                .find( ( relationshipPropertyValue ) => relationshipPropertyValue.property_def === defaultPropertyDef );

            // If there isn't already a property value for this object type, we need to create it.
            if( !propertyValue ) {
                propertyValue = {
                    property_def: defaultPropertyDef,
                    value: {
                        type: 10,  // Datatype.DATATYPE_MULTI_SELECT_LOOKUP,
                        is_null: false,
                        data:  {
                            multi_select_lookup: {
                                values: []
                            }
                        }
                    }
                };

                // Push it to array.
                relationshipPropertyValues.push( propertyValue );
            }

            // Add this item to the lookup.
            const lookup = {
                value_list_item_info: selectedItems.ObjectVersions[ i ].object_info,
                version: selectedItems.ObjectVersions[ i ].version_info.version
            };
            propertyValue.value.data.multi_select_lookup.values.push( lookup );
        }

    // Handle the error.
    } catch ( exception ) {
        console.error( exception );

    // Return the prepared property values.
    } finally {
        return relationshipPropertyValues;
    }
}

async function preparePropertyValuesForNewAssignment( shellFrame ) {

    /// <summary>Prepares the property values with the hard coded values.</summary>
    /// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
    /// <returns type="MFiles.PropertyValueArray">The property values array.</returns>

    // Create the property values for the new object.
    const propertyValues = [];

    // Get session info.
    const sessionInfo = await MFiles.GetSessionInfo();

    // Function to create the lookup value.
    const lookupValue = ( value ) => {
        return {
            value_list_item_info: {
                obj_id: {
                    type: -2,
                    item_id: {
                        internal_id: value,
                        external_repository_id: {
                            connection: "",
                            item: ""
                        }
                    }
                },
                name: null,
                external_id_status: 0,  // ExtIDStatus.EXT_IDSTATUS_UNKNOWN,
                external_id: null,
                guid: GUID_NULL,
                options: {
    
                    // Note: Default value(true) enables is_deleted true and deleted flag added to prefilled value
                    // So to handle it false has been set.
                    all: true
                },
                icon_id: null
            },
            version: {
                type: 1,  // ObjVerVersionType.OBJ_VER_VERSION_TYPE_LATEST,
                internal_version: -1,
                external_repository_version: "",
                external_repository_sort_key: 0
            }
        };
    };

	// Class property value.
	const classPropertyValue = {
        property_def: BuiltInPropertyDefs.Class,
        value: {
            type: 9,  // Datatype.DATATYPE_LOOKUP,
            is_null: false,
            data: {
                lookup: lookupValue( -100 )
            }
        }
    };
	propertyValues.push( classPropertyValue );

	// Name or title property.
	const nameOrTitlePropertyValue = {
        property_def: BuiltInPropertyDefs.NameOrTitle,
        value: {
            type: 1,  // Datatype.DATATYPE_TEXT,
            is_null: false,
            data: {
                text: "Assignment"
            }
        }
    };
	propertyValues.push( nameOrTitlePropertyValue );

	// Single-file-document property.
	const singleFileDocumentPropertyValue = {
        property_def: BuiltInPropertyDefs.SingleFlie,
        value: {
            type: 8,  // Datatype.DATATYPE_BOOLEAN,
            is_null: false,
            data: {
                boolean: false
            }
        }
    };
	propertyValues.push( singleFileDocumentPropertyValue );

	// Assigned to property.
	const assignedToPropertyValue = {
        property_def: BuiltInPropertyDefs.AssignedTo,
        value: {
            type: 10,  // Datatype.DATATYPE_MULTI_SELECT_LOOKUP,
            is_null: false,
            data:  {
                multi_select_lookup: {
                    values: [
                        lookupValue( sessionInfo.vault_data?.user_id ) // Assigned to the current login user.
                    ]
                }
            }
        }
    };
	propertyValues.push( assignedToPropertyValue );

    // Return the prepared property values.
    return propertyValues;
}

async function createAssignmentObject( shellFrame ) {

	/// <summary>Creates an assignment for the currently-selected items, assigning it to the current user.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The current shell frame object.</param> 
	/// <returns type="MFiles.ObjectVersionAndProperties">The event handler.</returns>

    try {

        // Get the relationship property values array..
        const relationshipPropertyValues = await retrieveRelationshipPropertyValues( shellFrame, currentlySelectedItems );

        // Prepare the default property values for new assignment.
        propertyValues = await preparePropertyValuesForNewAssignment( shellFrame );

        // Add the relationship property values to the property values for the assignment.
        propertyValues = propertyValues.concat( relationshipPropertyValues );

        // Get the vault instance.
        const iVault = shellFrame.ShellUI.Vault;

        // Create object request.
        const createObjectRequest = {
            object_type_id: BuiltInObjectType.Assignment,
            properties: propertyValues,
            acl: {},
            object_flags: {
                all: true
            },
            check_in: true // Checkin the object immediately.
        };

        // Create new object in the server.
        await iVault.objectOperations.addObjectWithFiles( createObjectRequest );
        await shellFrame.ShowMessage( "Assigned to you." );

    // Handle the exception.
    } catch ( exception) {
        console.error( exception );
    }
}
