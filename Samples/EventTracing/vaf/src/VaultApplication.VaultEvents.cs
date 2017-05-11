using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace EventTracing
{
	/// <summary>
	/// A sample vault application which aids in tracing events and VAF lifecycle.
	/// </summary>
	public partial class VaultApplication
	{
		/// <summary>
		/// Generates a string that can be logged to the event log, detailing the event handler environment.
		/// </summary>
		/// <param name="type">The type of the event that was thrown.</param>
		/// <param name="env">The environment passed to the event.</param>
		/// <returns>A textual representation of the type and environment.</returns>
		protected string GenerateEventLogInfo(EventHandlerEnvironment env)
		{
			// Sanity.
			if (null == env)
				return "The environment provided was null.";

			// Create our string builder.
			StringBuilder sb = new StringBuilder();

			// What was the event type?
			sb.AppendLine($"Event type: {env.Type}");
			sb.AppendLine();

			// Transactional data.
			if(null != env.ActivityID)
				sb.AppendLine($"Activity ID: {env.ActivityID.GetValueAsUnlocalizedText()}");
			if (null != env.ActivityID)
				sb.AppendLine($"Current Transaction ID: {env.CurrentTransactionID.GetValueAsUnlocalizedText()}");
			if (null != env.ActivityID)
				sb.AppendLine($"Parent Transaction ID: {env.ParentTransactionID.GetValueAsUnlocalizedText()}");
			if (null != env.ActivityID)
				sb.AppendLine($"Master Transaction ID: {env.MasterTransactionID.GetValueAsUnlocalizedText()}");
			sb.AppendLine();

			// Login/logout data.
			if(env.CurrentUserID > 0)
				sb.AppendLine($"Current user Id: {env.CurrentUserID}");
			if(env.LoggedOutUserID > 0)
				sb.AppendLine($"The user that logged out had Id: {env.LoggedOutUserID}");
			if (null != env.LoginAccount)
				sb.AppendLine($"Login account: {env.LoginAccount.AccountName}, {env.LoginAccount.AccountType}");
			if(null != env.UserAccount)
				sb.AppendLine($"User account: {env.UserAccount.ID}, Enabled: {env.UserAccount.Enabled}, Is internal user: {env.UserAccount.InternalUser}");
			sb.AppendLine();

			// Extension method details.
			if (false == string.IsNullOrWhiteSpace(env.VaultExtensionMethodName))
			{
				sb.AppendLine($"Vault Extension Method: {env.VaultExtensionMethodName}, Input: {env.Input}");
				sb.AppendLine();
			}

			// File data.
			if (env.FileTransferSessionID > 0)
				sb.AppendLine($"FileTransferSessionID: {env.FileTransferSessionID}");
			if (null != env.FileVer)
			{
				sb.AppendLine($"FileVer: ({env.FileVer.ID}-{env.FileVer.Version})");
				sb.AppendLine();
			}

			// Basic object details.
			if (env.IsObjectEvent)
				sb.AppendLine($"Is Object Event: {env.IsObjectEvent}");
			if (null != env.ObjVer)
				sb.AppendLine($"ObjVer: ({env.ObjVer.ID}-{env.ObjVer.ID}-{env.ObjVer.Version})");

			// After the object has been destroyed we can't get its data!
			if (env.Type != MFEventHandlerType.MFEventHandlerAfterDestroyObject)
			{
				// Additional object data.
				if (env.IsRecordObject)
					sb.AppendLine($"Is Record Object: {env.IsRecordObject}");
				if (env.IsTemplateObject)
					sb.AppendLine($"Is Template Object: {env.IsTemplateObject}");
				sb.AppendLine();

				// Files
				var files = env.ObjVerEx?.Info?.Files;
				if (null != files)
				{
					sb.AppendLine($"Files has {files.Count} items:");
					foreach (ObjectFile file in files.Cast<ObjectFile>())
					{
						sb.AppendLine($"\tID: {file.ID}, Title: {file.Title}, Size (b): {file.LogicalSize}");
					}
					sb.AppendLine();
				}
			}

			// Properties
			if (null != env.PropertyValues)
			{
				sb.AppendLine($"PropertyValues has {env.PropertyValues.Count} items:");
				foreach (PropertyValue pv in env.PropertyValues.Cast<PropertyValue>())
				{
					sb.AppendLine($"\tID: {pv.PropertyDef}, Value: {pv.GetValueAsUnlocalizedText()}");
				}
				sb.AppendLine();
			}

			// Value list item
			if (null != env.ValueListItem)
			{
				sb.AppendLine($"Value list item: {env.ValueListItem.Name} ({env.ValueListItem.ID})");
				sb.AppendLine();
			}

			// Return the string.
			return sb.ToString();
		}

		/// <summary>
		/// Handles all events in the vault.
		/// </summary>
		/// <param name="env">The event environment.</param>
		public void HandleEvent(EventHandlerEnvironment env)
		{
			try
			{
				// Generate a set of data for the log.
				var logInfo = this.GenerateEventLogInfo(env);

				// Log it as information.
				SysUtils.ReportInfoToEventLog(logInfo);
			}
			catch (Exception e)
			{
				SysUtils.ReportErrorMessageToEventLog($"Exception generating log data for event {env.Type}",
					e);
			}
		}

		/// <inheritdoc />
		/// <remarks>Adds some dynamic events allowing us to log events as they occur.</remarks>
		protected override void LoadHandlerMethods(Vault vault)
		{
			// Register everything as normal.
			base.LoadHandlerMethods(vault);

			// Generate method info for our handler method.
			var methodInfo = this.GetType().GetMethod(nameof(VaultApplication.HandleEvent));

			// Iterate over each type of event to register handlers.
			foreach (MFEventHandlerType type in Enum.GetValues(typeof(MFEventHandlerType)))
			{
				// Sanity.
				if (type == MFEventHandlerType.MFEventHandlerTypeUndefined)
					continue;

				//// Certain events can't be registered, so ignore them.
				if (type == MFEventHandlerType.MFEventHandlerReplication_AfterCheckInChanges
					|| type == MFEventHandlerType.MFEventHandlerReplication_AfterCreateNewObjectFinalize
					|| type == MFEventHandlerType.MFEventHandlerAfterCreateLoginAccount
					|| type == MFEventHandlerType.MFEventHandlerAfterModifyLoginAccount
					|| type == MFEventHandlerType.MFEventHandlerAfterRemoveLoginAccount
					|| type == MFEventHandlerType.MFEventHandlerBeforeCreateLoginAccount
					|| type == MFEventHandlerType.MFEventHandlerBeforeModifyLoginAccount
					|| type == MFEventHandlerType.MFEventHandlerBeforeRemoveLoginAccount
					|| type == MFEventHandlerType.MFEventHandlerBeforeModifyMFilesCredentials
					|| type == MFEventHandlerType.MFEventHandlerAfterModifyMFilesCredentials
					|| type == MFEventHandlerType.MFEventHandlerAfterRunScheduledJob
					|| type == MFEventHandlerType.MFEventHandlerBeforeRunScheduledJob)
					continue;

				// Certain events will flood the log, so ignore them.
				if (type == MFEventHandlerType.MFEventHandlerBeforeCommitTransaction
					|| type == MFEventHandlerType.MFEventHandlerAfterBeginTransaction)
					continue;

				// Certain events will throw exceptions, so ignore them.
				if (type == MFEventHandlerType.MFEventHandlerBeforeRollbackTransaction
					|| type == MFEventHandlerType.MFEventHandlerAfterBringOnline)
					continue;

				// Create the event handler information.
				var eventHandlerMethodInfo = new EventHandlerMethodInfo(methodInfo,
					this,
					new EventHandlerAttribute(type));

				// Ensure we have a dictionary to add to.
				if (!this.eventHandlerMethods.ContainsKey(type))
				{
					this.eventHandlerMethods.Add(type, new List<IEventHandlerMethodInfo>());
				}

				// Register the dynamic method for this event type.
				base.eventHandlerMethods[type].Add(eventHandlerMethodInfo);
			}
		}
	}
}