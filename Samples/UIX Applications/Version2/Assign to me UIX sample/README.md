# Creating assignments for selected objects

## Overview
This sample creates a basic User Interface Extensibility Framework application consisting of one ShellUI module which allows the user to select objects within the M-Files vault and assign them to themselves via a button in the main menu.

This sample does not show how to create a local development folder or to deploy the code to the M-Files server. It is assumed that a local development folder already exists, and that is the location in which the development is occurring.

## Creating the application structure

### Creating the application definition file

Into this folder we will create an application definition file. This file must be named `appdef.xml`. The application will use version 5 of the client schema (as we are only targeting newer M-Files versions). The application will declare a single Shell UI module (with its code in `main.js`), and no dashboards.

`appdef.xml`
<Tabs>
<TabItem value="xml" label="XML">

```xml
<?xml version="1.0"?>
<application xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="http://www.m-files.com/schemas/appdef-client-v5.xsd">
    <guid>C311B570-40F4-4893-96C8-05110A30743C</guid>
    <name>Assign to me UIX sample</name>
    <version>0.1</version>
    <description>A demonstration application for reacting to selected items and assigning them via a command.</description>
    <publisher>M-Files Corporation</publisher>
    <enabled-by-default>true</enabled-by-default>
    <modules>
        <module environment="shellui">
        <file>main.js</file>
        </module>
    </modules>
</application>

```

</TabItem>
</Tabs>

> Ensure that your application has a unique GUID by using a GUID generator.

### Creating the module
Next we will create a module file to contain our actual application logic. Initially:

- We will declare a default entry point for the ShellUI module.
- We will react to the `NewShellFrame` event and obtain a reference to the shell frame.
- We will react to the shell frame’s `Started` event (as using the shell frame before this point will result in an exception).
- Create a command (button, place it into the menu area, and hide it.
- React to the shellFrame’s `CustomCommand` event and add some placeholder code to execute when the command is clicked.

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

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

		// Register to respond to commands being clicked.
		shellFrame.Commands.Events.Register(
			MFiles.Event.CustomCommand,
			async ( command ) => {

				// We only care about our command.
				// If the command being clicked is something else then return.
				if( command !== assignCommandId ) {
					return;
				}

				// TODO: Ensure we have items to process.

				// TODO: Create the assignment object.
			} );
	};
}
```

</TabItem>
</Tabs>

### Reacting to shell listing selection changes

We must be able to react to selection changes in the listing.

We will:

- Declare a global variable to hold the currently-selected items (`currentlySelectedItems`).
- React to the shell frame’s `NewShellListing` event to attach event handlers to each shell listing.
- Alter the visibility of the command depending on whether or not any objects are currently selected.
- React to each shell listing’s `SelectionChanged` event, saving the currently-selected items.

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

// The currently-selected items in the active listing.
let currentlySelectedItems = null;

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

			    // TODO: Create the assignment object.
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
```

</TabItem>
</Tabs>

### Creating the assignment object

We will:

- Create a function that creates the assignment object (`createAssigmentObject`). Specifically, it will:
    - Create property values for the built-in properties used by the `Assignment` object type:
        - Class.
        - Name or title.
        - Single file document.
        - Assigned to.
    - Create properties to establish relationships between the assignment object and the currently-selected items.
    - Create the object and check it in immediately.
- Call the function when the command is clicked.

`main.js`
<Tabs>
<TabItem value="js" label="JavaScript">

```js
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
```

</TabItem>
</Tabs>

 ## Testing the application

 The command/button appears in the menu area, and is shown/hidden as items are selected. Selecting items and clicking `Assign to me` shows a message that the operation was successful.

![ alt text ]( ./screenshots/CreateAssignment_1.png "Create assignment" )

Once the button has been clicked, an assignment is created for the current user and is related to the objects that were previously selected.

![ alt text ]( ./screenshots/CreateAssignment_2.png "Assignment" )
