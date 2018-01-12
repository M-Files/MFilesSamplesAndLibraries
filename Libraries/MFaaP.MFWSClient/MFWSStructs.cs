// M-Files Web Service data types.
//
// These M-Files Web Service data types are plain types with no decorators.
// They can be used with third party serializers without other dependncies
// within .NET Framework.


// Copyright 2012 M-Files Corporation
// http://www.m-files.com/
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

//
//
//

using System;
using System.Collections.Generic;
using System.Linq;
#if !WINDOWS_UWP
using System.Web;
#endif
// ReSharper disable EmptyConstructor

// Requires System.Runtime.Serialization reference.

namespace MFaaP.MFWSClient
{



    /// <summary>
    /// Specifies the information required when creating a new object.
    /// </summary>
    public class ObjectCreationInfo
    {

        /// <summary>
        /// Properties for the new object.
        /// </summary>
        public PropertyValue[] PropertyValues { get; set; }

        /// <summary>
        /// References previously uploaded files.
        /// </summary>
        public UploadInfo[] Files { get; set; }

    }



    /// <summary>
    /// Contains the information on a temporary upload.
    /// </summary>
    public class UploadInfo
    {
        public UploadInfo()
        {
        }

        /// <summary>
        /// Temporary upload ID given by the server.
        /// </summary>
        public int UploadID { get; set; }

        /// <summary>
        /// File name without extension.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// File extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// File size.
        /// </summary>
        public long Size { get; set; }

    }



    /// <summary>
    /// A &#39;typed value&#39; represents a value, such as text, number, date or lookup item.
    /// </summary>
    public class TypedValue
    {

        public TypedValue()
        {
        }

        /// <summary>
        /// Specifies the type of the value.
        /// </summary>
        public MFDataType DataType { get; set; }

        /// <summary>
        /// Specifies whether the typed value contains a real value.
        /// </summary>
        public bool HasValue { get; set; }

        /// <summary>
        /// Specifies the string, number or boolean value when the DataType is not a lookup type.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Specifies the lookup value when the DataType is Lookup.
        /// </summary>
        public Lookup Lookup { get; set; }

        /// <summary>
        /// Specifies the collection of \type{Lookup}s when the DataType is MultiSelectLookup.
        /// </summary>
        public List<Lookup> Lookups { get; set; }

        /// <summary>
        /// Provides the value formatted for display.
        /// </summary>
        public string DisplayValue { get; set; }

        /// <summary>
        /// Provides a key that can be used to sort \type{TypedValue}s
        /// </summary>
        public string SortingKey { get; set; }

        /// <summary>
        /// Provides the typed value in a serialized format suitable to be used in URIs.
        /// </summary>
        public string SerializedValue { get; set; }

    }



    /// <summary>
    /// An object version with extended properties. Inherits from ObjectVersion.
    /// </summary>
    public class ExtendedObjectVersion
        : ObjectVersion
    {
        public ExtendedObjectVersion()
        {
        }

        /// <summary>
        /// Object properties
        /// </summary>
        public List<PropertyValue> Properties { get; set; }

        /// <summary>
        /// The object properties to display.
        /// </summary>
        public List<PropertyValue> PropertiesForDisplay { get; set; }

        /// <summary>
        /// Files that were added.
        /// </summary>
        public List<FileVer> AddedFiles { get; set; }

    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class FileVer
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Version { get; set; }
    }



    /// <summary>
    /// Authentication details.
    /// </summary>
    public class Authentication
    {

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool WindowsUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ComputerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? VaultGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string URL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Method { get; set; }

    }



    /// <summary>
    /// Vault information.
    /// </summary>
    public class Vault
    {

        /// <summary>
        /// Vault name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vault GUID.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string GUID { get; set; }

        /// <summary>
        /// Vault-specific authentication token.
        /// </summary>
        public string Authentication { get; set; }

    }



    /// <summary>
    /// Information required for changing password.
    /// </summary>
    public class PasswordChange
    {

        /// <summary>
        /// The current password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// The new password.
        /// </summary>
        public string NewPassword { get; set; }

    }



    /// <summary>
    /// Server public key information.
    /// </summary>
    public class PublicKey
    {

        /// <summary>
        /// Base64URL encoded exponent.
        /// </summary>
        public string Exponent { get; set; }

        /// <summary>
        /// Base64URL encoded modulus.
        /// </summary>
        public string Modulus { get; set; }

    }



    /// <summary>
    /// Response for status requests.
    /// </summary>
    public class StatusResponse
    {

        /// <summary>
        /// The result of the status request.
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// Display message for the status.
        /// </summary>
        public string Message { get; set; }

    }



    /// <summary>
    /// An object class with extended properties. Inherits from ObjectClass.
    /// </summary>
    public class ExtendedObjectClass
        : ObjectClass
    {
        public ExtendedObjectClass()
        {
        }

        /// <summary>
        /// Property definitions associated with this class.
        /// </summary>
        public List<AssociatedPropertyDef> AssociatedPropertyDefs { get; set; }

        /// <summary>
        /// Templates available for use with this class.
        /// </summary>
        public List<ObjectVersion> Templates { get; set; }

    }



    /// <summary>
    /// Workflow state information.
    /// </summary>
    public class WorkflowState
    {

        /// <summary>
        /// Workflow state name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Workflow state ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Defines whether this state is selectable for the current object.
        /// </summary>
        public bool Selectable { get; set; }

    }



    /// <summary>
    /// An object version with extended properties. Inherits from ObjectVersion.
    /// </summary>
    public class FolderContentItems
    {
        public FolderContentItems()
        {
        }

        /// <summary>
        /// The path to the current folder.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Specifies whether there are more results in the folder.
        /// </summary>
        public bool MoreResults { get; set; }

        /// <summary>
        /// The actual folder contents.
        /// </summary>
        public List<FolderContentItem> Items { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public FolderUIState FolderUIState { get; set; }

    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    /// <remarks>
    /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~FolderUIState.html
    /// </remarks>
    public class FolderUIState
    {
        public FolderUIState()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? BottomPaneBarMinimized { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? HitHighlightingEnabled { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? MetadataEditorInRightPane { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? RelativeBottomPaneHeight { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? RelativeRightPaneWidth { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? RightPaneBarMinimized { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? ShowBottomPaneBar { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool? ShowRightPaneBar { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public FolderListingUIState ListingUIState { get; set; }
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    /// <remarks>
    /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~FolderListingUIState.html
    /// </remarks>
    public class FolderListingUIState
    {
        public FolderListingUIState()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public List<FolderListingColumnSorting> ColumnSortings { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public List<FolderListingColumn> Columns { get; set; }
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    /// <remarks>
    /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~FolderListingColumn.html
    /// </remarks>
    public class FolderListingColumn
    {
        public FolderListingColumn()
        {
        }

        /// <summary>
        /// The column ID to be sorted; the property definition ID or a pseudo-ID.
        /// </summary>
        /// <remarks>
        /// The column ID is either a property definition ID (positive values) or a value in <see cref="MFFolderColumnId" /> Enumeration (negative values).
        /// </remarks>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public MFFolderListingColumnFlags Flags { get; set; }

        /// <summary>
        /// The column display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies the position for a visible column.
        /// </summary>
        /// <remarks>
        /// Each column in the FolderListingColumns Collection should have a unique and linear position number for visible columns. The position for the leftmost column is 0.
        /// For invisible columns, the position number is -1.
        /// </remarks>
        public int Position { get; set; }

        /// <summary>
        /// Specifies the column width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Specifies whether the column is visible.
        /// </summary>
        public bool Visible { get; set; }

    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    /// <remarks>
    /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFFolderListingColumnFlags.html
    /// </remarks>
    [Flags]
    public enum MFFolderListingColumnFlags
    {
        None = 0,

        /// <summary>
        /// Displays the column with selected background color if the column on the left is selected.
        /// </summary>
        MFFolderListingColumnFlagsSelectIfLeftOfSelectedMainColumn = 1,

        /// <summary>
        /// Hides the column text.
        /// </summary>
        MFFolderListingColumnFlagsHideColumnText = 2
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    /// <remarks>
    /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~FolderListingColumnSorting.html
    /// </remarks>
    public class FolderListingColumnSorting
    {
        public FolderListingColumnSorting()
        {
        }

        /// <summary>
        /// The column ID to be sorted; the property definition ID or a pseudo-ID.
        /// </summary>
        /// <remarks>
        /// The column ID is either a property definition ID (positive values) or a value in <see cref="MFFolderColumnId" /> Enumeration (negative values).
        /// </remarks>
        public int ID { get; set; }

        /// <summary>
        /// The zero-based column index to be sorted.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// True to sort ascending, false to sort descending.
        /// </summary>
        public bool SortAscending { get; set; }
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    /// <remarks>
    /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFFolderColumnId.html
    /// </remarks>
    public enum MFFolderColumnId
    {
        /// <summary>The identifier of the 'Date-Time' column.</summary>
        MFFolderColumnIdCheckOutAt = -6,

        /// <summary>The identifier of the 'Checked Out To' column.</summary>
        MFFolderColumnIdCheckoutTo = -5,

        /// <summary>The identifier of the 'Date Created' column.</summary>
        MFFolderColumnIdDateCreated = -13,

        /// <summary>The identifier of the 'Date Modified' column.</summary>
        MFFolderColumnIdDateModified = -14,

        /// <summary>The identifier of the 'Exported File' column.</summary>
        MFFolderColumnIdExportedFile = -1000000,

        /// <summary>The identifier of the 'Has Assignments' column.</summary>
        MFFolderColumnIdHasAssignments = -22,

        /// <summary>The identifier of the 'Has Relationships' column.</summary>
        MFFolderColumnIdHasRelationships = -17,

        /// <summary>The identifier of the ID Segment column.</summary>
        MFFolderColumnIdIdSegment = -1000001,

        /// <summary>The identifier of the 'Location' column.</summary>
        MFFolderColumnIdLocation = -11,

        /// <summary>The identifier of the 'Name' column.</summary>
        MFFolderColumnIdName = -1,

        /// <summary>The identifier of the 'ID' column (for display purposes).</summary>
        MFFolderColumnIdObjectDispId = -7,

        /// <summary>The identifier of the 'Permissions' column.</summary>
        MFFolderColumnIdObjectPermissions = -20,

        /// <summary>The identifier of the 'Object Type' column.</summary>
        MFFolderColumnIdObjectType = -19,

        /// <summary>The identifier of the 'Version' column.</summary>
        MFFolderColumnIdObjectVersion = -8,

        /// <summary>The identifier of the 'Description' column of a relationships view.</summary>
        MFFolderColumnIdRelationshipDesc = -16,

        /// <summary>The identifier of the 'Target Version' column of a relationships view.</summary>
        MFFolderColumnIdRelationshipTargetVer = -18,

        /// <summary>The identifier of the 'Relative Location' column. Used for special purposes only.</summary>
        MFFolderColumnIdRelLocation = -12,

        /// <summary>The identifier of the 'Score' column.</summary>
        MFFolderColumnIdScore = -15,

        /// <summary>The identifier of the 'User' column. Available in History view only.</summary>
        MFFolderColumnIdShLastModifiedBy = -10,

        /// <summary>The identifier of the 'Date-Time' column. Available in History view only.</summary>
        MFFolderColumnIdShStatusChanged = -9,

        /// <summary>The identifier of the 'Size' column.</summary>
        MFFolderColumnIdSize = -3,

        /// <summary>The identifier of the 'Status' column.</summary>
        MFFolderColumnIdStatus = -4,

        /// <summary>The identifier of the 'Type' column, basic type info.</summary>
        MFFolderColumnIdType = -2,

        /// <summary>The identifier of the 'Type' column, extended type info.</summary>
        MFFolderColumnIdTypeEx = -21
    }

    /// <summary>
    /// A workflow state on an object.
    /// </summary>
    public class ObjectWorkflowState
    {

        /// <summary>
        /// The workflow state defined as a property value.
        /// </summary>
        public PropertyValue State { get; set; }

        /// <summary>
        /// The workflow state ID.
        /// </summary>
        public int StateID { get; set; }

        /// <summary>
        /// The workflow state name.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// The workflow defined as a property value.
        /// </summary>
        public PropertyValue Workflow { get; set; }

        /// <summary>
        /// The workflow ID.
        /// </summary>
        public int WorkflowID { get; set; }

        /// <summary>
        /// The workflow name.
        /// </summary>
        public string WorkflowName { get; set; }

        /// <summary>
        /// Version comment defined on the workflow transition.
        /// </summary>
        public string VersionComment { get; set; }

    }



    /// <summary>
    /// M-Files Web Service error object.
    /// </summary>
    public class WebServiceError
    {
        public WebServiceError()
        {
        }

        /// <summary>
        /// HTTP Status code
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// The request URL which caused this error.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string URL { get; set; }

        /// <summary>
        /// The request method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Detailed information on the exception.
        /// </summary>
        public ExceptionInfo Exception { get; set; }

        /// <summary>
        /// Converts a web service error into something that can be thrown in .NET-land.
        /// </summary>
        /// <param name="info"></param>
        public static explicit operator Exception(WebServiceError info)
        {
            // Sanity.
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            // Convert.
#if WINDOWS_UWP
            return new Exception( info.ToString( ),
                info.Exception == null ?  null : (Exception) info.Exception );
#else
            return new HttpException(info.Status, info.ToString(),
                info.Exception == null ? null : (Exception)info.Exception);
#endif
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Method} request to {this.URL} returned a status code of {this.Status}.";
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class ExceptionInfo
    {
        public ExceptionInfo()
        {
        }

        /// <summary>
        /// Error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Underlying error that caused this one.
        /// </summary>
        public ExceptionInfo InnerException { get; set; }

        /// <summary>
        /// M-Files Web Service server-side stack trace.
        /// </summary>
        public StackTraceElement[] Stack { get; set; }

        /// <summary>
        /// Converts information about an exception into something that can be thrown in .NET-land.
        /// </summary>
        /// <param name="info"></param>
        public static explicit operator Exception(ExceptionInfo info)
        {
            // Sanity.
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            // Convert.
            return new ExceptionInfoException(info);
        }

    }

    /// <summary>
    /// Allows a <see cref="ExceptionInfo"/> object to be expressed as a <see cref="Exception"/>.
    /// </summary>
#if !WINDOWS_UWP
    [Serializable]
#endif
    internal class ExceptionInfoException
        : Exception
    {

        private readonly ExceptionInfo exceptionInfo;

        /// <inheritdoc />
        public override string StackTrace => string.Join("\r\n", this.exceptionInfo.Stack?.Select(e => e.ToString()) ?? new string[0]);

        public ExceptionInfoException(ExceptionInfo info)
            : base(info.Message, info.InnerException == null ? null : new ExceptionInfoException(info.InnerException))
        {
            this.exceptionInfo = info;
        }
    }



    /// <summary>
    /// M-Files Web Service error stack trace element.
    /// </summary>
    public class StackTraceElement
    {
        public StackTraceElement()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.ClassName}.{this.MethodName} ({this.FileName}, line {this.LineNumber}";
        }
    }



    /// <summary>
    /// Results of a query which might leave only a partial set of items.
    /// </summary>
    public class Results<T>
    {
        public Results()
        {
        }

        /// <summary>
        /// Contains results of a query
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// True if there were more results which were left out.
        /// </summary>
        public bool MoreResults { get; set; }

    }



    /// <summary>
    /// 
    /// </summary>
    public class PrimitiveType<T>
    {
        public PrimitiveType()
        {
        }

        /// <summary>
        /// Primitive value.
        /// </summary>
        public T Value { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ObjectVersion
    {

        public ObjectVersion()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public DateTime AccessedByMeUtc { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public DateTime CheckedOutAtUtc { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int CheckedOutTo { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        /// <remarks>
        /// Called CheckedOutToHostName in API.
        /// </remarks>
        public string CheckedOutFrom { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string CheckedOutToUserName { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Class { get; set; }

        /// <summary>
        /// The score of the item, if returned in a search.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string DisplayID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public List<ObjectFile> Files { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool HasAssignments { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool HasRelationshipsFromThis { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool HasRelationshipsToThis { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool IsStub { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public DateTime LastModifiedUtc { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool ObjectCheckedOut { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool ObjectCheckedOutToThisUser { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public MFObjectVersionFlag ObjectVersionFlags { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public ObjVer ObjVer { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool SingleFile { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool ThisVersionLatestToThisUser { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The title that is compatible with file systems, including the ID.
        /// </summary>
        /// <remarks>
        /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~ObjectVersion~GetNameForFileSystem.html
        /// </remarks>
        public string EscapedTitleWithID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool VisibleAfterOperation { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string ObjectGUID { get; set; }

    }


    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ObjectFile
    {

        public ObjectFile()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public DateTime ChangeTimeUtc { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        /// <remarks>
        /// When returned from the server it does not include the "." prefix.
        /// </remarks>
        public string Extension { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        /// <remarks>
        /// ref: https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~ObjectFile~GetNameForFileSystem.html
        /// </remarks>
        public string EscapedName { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        /// <remarks>M-Files API uses LogicalSize.</remarks>
        public long Size { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string FileGUID { get; set; }
    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ObjVer
    {

        public ObjVer()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Version { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class PropertyValue
    {
        public PropertyValue()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int PropertyDef { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public TypedValue TypedValue { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class SessionInfo
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public MFACLMode ACLMode { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public MFAuthType AuthenticationType { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanForceUndoCheckout { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool CanManageCommonUISettings { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanManageCommonViews { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanManageTraditionalFolders { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanMaterializeViews { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanSeeAllObjects { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanSeeDeletedObjects { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool InternalUser { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool LicenseAllowsModifications { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int UserID { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ObjType
    {
        public ObjType()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool AllowAdding { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool CanHaveFiles { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int DefaultPropertyDef { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool External { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string NamePlural { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int OwnerPropertyDef { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public List<int> ReadOnlyPropertiesDuringInsert { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public List<int> ReadOnlyPropertiesDuringUpdate { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool RealObjectType { get; set; }

        /// <summary>
        /// An overview of effective permissions.
        /// </summary>
        public Permission Permission { get; set; }

        public bool Hierarchial { get; set; }

    }

    public class Permission
    {
        public Permission()
        {
        }

        public bool CanRead { get; set; }
        public bool CanEdit { get; set; }
        public bool CanAttachObjects { get; set; }
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class PropertyDef
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool AllObjectTypes { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string AutomaticValue { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public MFAutomaticValueType AutomaticValueType { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool BasedOnValueList { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public MFDataType DataType { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ObjectType { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ValueList { get; set; }

    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ExtendedPropertyDef : PropertyDef
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public ExtendedPropertyDef() { }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool IsCreatePermissionAllowed { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public Permissions Permissions { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ContentType { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int DependencyPD { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int DependencyRelation { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public Ownerpropertydef OwnerPropertyDef { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool SortAscending { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int UpdateType { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ValueListSortingType { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool IsOwnerPropertyDef { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int SortingType { get; set; }
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class Permissions : Permission
    {
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class Ownerpropertydef
    {
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public Ownerpropertydef() { }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int DependencyRelation { get; set; }
        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool IsRelationFiltering { get; set; }
    }

    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class Workflow
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ObjectClass { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ValueListItem
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string DisplayID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool HasOwner { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool HasParent { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int OwnerID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ValueListID { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ObjID
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Type { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class Lookup
    {

        public Lookup()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string DisplayValue { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Item { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int? Version { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class VersionComment
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public PropertyValue LastModifiedBy { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public ObjVer ObjVer { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public PropertyValue StatusChanged { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public PropertyValue Comment { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class AssociatedPropertyDef
    {
        public AssociatedPropertyDef()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int PropertyDef { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool Required { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class FolderContentItem
    {

        public FolderContentItem()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public MFFolderContentItemType FolderContentItemType { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public ObjectVersion ObjectVersion { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public TypedValue PropertyFolder { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public Lookup TraditionalFolder { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public View View { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class View
    {

        public View()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool Common { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Parent { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public ViewLocation ViewLocation { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ViewLocation
    {

        public ViewLocation()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public TypedValue OverlappedFolder { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public bool Overlapping { get; set; }

    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ObjectClass
    {
        public ObjectClass()
        {
        }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int NamePropertyDef { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int Workflow { get; set; }

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public int ObjType { get; set; }
    }



    /// <summary>
    /// Based on M-Files API.
    /// </summary>
    public class ClassGroup
    {

        /// <summary>
        /// Based on M-Files API.
        /// </summary>
        public string Name { get; set; }

    }




    /// <summary>
    /// 
    /// </summary>
    public enum MFCheckOutStatus
    {


        /// <summary>
        /// Object is checked in.
        /// </summary>
        CheckedIn = 0,


        /// <summary>
        /// Object is checked out to someone else.
        /// </summary>
        CheckedOut = 1,


        /// <summary>
        /// Object is checked out to the current user.
        /// </summary>
        CheckedOutToMe = 2,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFRefreshStatus
    {


        /// <summary>
        /// 
        /// </summary>
        None = 0,


        /// <summary>
        /// 
        /// </summary>
        Quick = 1,


        /// <summary>
        /// 
        /// </summary>
        Full = 2,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFObjectVersionFlag
    {

        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        Completed = 1,

        /// <summary>
        /// 
        /// </summary>
        HasRelatedObjects = 2,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFAuthType
    {


        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,


        /// <summary>
        /// 
        /// </summary>
        LoggedOnWindowsUser = 1,


        /// <summary>
        /// 
        /// </summary>
        SpecificWindowsUser = 2,


        /// <summary>
        /// 
        /// </summary>
        SpecificMFilesUser = 3,

    }



    /// <summary>
    /// 
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public enum MFACLMode
    {


        /// <summary>
        /// 
        /// </summary>
        Simple = 0,


        /// <summary>
        /// 
        /// </summary>
        AutomaticPermissionsWithComponents = 1,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFDataType
    {


        /// <summary>
        /// 
        /// </summary>
        Uninitialized = 0,


        /// <summary>
        /// 
        /// </summary>
        Text = 1,


        /// <summary>
        /// 
        /// </summary>
        Integer = 2,


        /// <summary>
        /// 
        /// </summary>
        Floating = 3,


        /// <summary>
        /// 
        /// </summary>
        Date = 5,


        /// <summary>
        /// 
        /// </summary>
        Time = 6,


        /// <summary>
        /// 
        /// </summary>
        Timestamp = 7,


        /// <summary>
        /// 
        /// </summary>
        Boolean = 8,


        /// <summary>
        /// 
        /// </summary>
        Lookup = 9,


        /// <summary>
        /// 
        /// </summary>
        MultiSelectLookup = 10,


        /// <summary>
        /// 
        /// </summary>
        Integer64 = 11,


        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        FILETIME = 12,


        /// <summary>
        /// 
        /// </summary>
        MultiLineText = 13,


        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        ACL = 14,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFAutomaticValueType
    {


        /// <summary>
        /// 
        /// </summary>
        None = 0,


        /// <summary>
        /// 
        /// </summary>
        CalculatedWithPlaceholders = 1,


        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        CalculatedWithVBScript = 2,


        /// <summary>
        /// 
        /// </summary>
        AutoNumberSimple = 3,


        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        WithVBScript = 4,

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MFFolderContentItemType
    {


        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,


        /// <summary>
        /// 
        /// </summary>
        ViewFolder = 1,


        /// <summary>
        /// 
        /// </summary>
        PropertyFolder = 2,


        /// <summary>
        /// 
        /// </summary>
        TraditionalFolder = 3,


        /// <summary>
        /// 
        /// </summary>
        ObjectVersion = 4,

    }


}
