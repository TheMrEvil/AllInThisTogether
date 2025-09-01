using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Used to access the Event Log service on the local computer or a remote computer so you can manage and gather information about the event logs and event providers on the computer.</summary>
	// Token: 0x0200039B RID: 923
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogSession : IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object, establishes a connection with the local Event Log service.</summary>
		// Token: 0x06001B98 RID: 7064 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public EventLogSession()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object, and establishes a connection with the Event Log service on the specified computer. The credentials (user name and password) of the user who calls the method is used for the credentials to access the remote computer.</summary>
		/// <param name="server">The name of the computer on which to connect to the Event Log service.</param>
		// Token: 0x06001B99 RID: 7065 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogSession(string server)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object, and establishes a connection with the Event Log service on the specified computer. The specified credentials (user name and password) are used for the credentials to access the remote computer.</summary>
		/// <param name="server">The name of the computer on which to connect to the Event Log service.</param>
		/// <param name="domain">The domain of the specified user.</param>
		/// <param name="user">The user name used to connect to the remote computer.</param>
		/// <param name="password">The password used to connect to the remote computer.</param>
		/// <param name="logOnType">The type of connection to use for the connection to the remote computer.</param>
		// Token: 0x06001B9A RID: 7066 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public EventLogSession(string server, string domain, string user, SecureString password, SessionAuthentication logOnType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a static predefined session object that is connected to the Event Log service on the local computer.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object that is a predefined session object that is connected to the Event Log service on the local computer.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x0005A05A File Offset: 0x0005825A
		public static EventLogSession GlobalSession
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Cancels any operations (such as reading an event log or subscribing to an event log) that are currently active for the Event Log service that this session object is connected to.</summary>
		// Token: 0x06001B9C RID: 7068 RVA: 0x0000235B File Offset: 0x0000055B
		public void CancelCurrentOperations()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Clears events from the specified event log.</summary>
		/// <param name="logName">The name of the event log to clear all the events from.</param>
		// Token: 0x06001B9D RID: 7069 RVA: 0x0000235B File Offset: 0x0000055B
		public void ClearLog(string logName)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Clears events from the specified event log, and saves the cleared events to the specified file.</summary>
		/// <param name="logName">The name of the event log to clear all the events from.</param>
		/// <param name="backupPath">The path to the file in which the cleared events will be saved. The file should end in .evtx.</param>
		// Token: 0x06001B9E RID: 7070 RVA: 0x0000235B File Offset: 0x0000055B
		public void ClearLog(string logName, string backupPath)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases all the resources used by this object.</summary>
		// Token: 0x06001B9F RID: 7071 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001BA0 RID: 7072 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Exports events into an external log file. The events are stored without the event messages.</summary>
		/// <param name="path">The name of the event log to export events from, or the path to the event log file to export events from.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		/// <param name="query">The query used to select the events to export.  Only the events returned from the query will be exported.</param>
		/// <param name="targetFilePath">The path to the log file (ends in .evtx) in which the exported events will be stored after this method is executed.</param>
		// Token: 0x06001BA1 RID: 7073 RVA: 0x0000235B File Offset: 0x0000055B
		public void ExportLog(string path, PathType pathType, string query, string targetFilePath)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Exports events into an external log file. A flag can be set to indicate that the method will continue exporting events even if the specified query fails for some logs. The events are stored without the event messages.</summary>
		/// <param name="path">The name of the event log to export events from, or the path to the event log file to export events from.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		/// <param name="query">The query used to select the events to export. Only the events returned from the query will be exported.</param>
		/// <param name="targetFilePath">The path to the log file (ends in .evtx) in which the exported events will be stored after this method is executed.</param>
		/// <param name="tolerateQueryErrors">
		///       <see langword="true" /> indicates that the method will continue exporting events even if the specified query fails for some logs, and <see langword="false" /> indicates that this method will not continue to export events when the specified query fails.</param>
		// Token: 0x06001BA2 RID: 7074 RVA: 0x0000235B File Offset: 0x0000055B
		public void ExportLog(string path, PathType pathType, string query, string targetFilePath, bool tolerateQueryErrors)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Exports events and their messages into an external log file.</summary>
		/// <param name="path">The name of the event log to export events from, or the path to the event log file to export events from.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		/// <param name="query">The query used to select the events to export.  Only the events returned from the query will be exported.</param>
		/// <param name="targetFilePath">The path to the log file (ends in .evtx) in which the exported events will be stored after this method is executed.</param>
		// Token: 0x06001BA3 RID: 7075 RVA: 0x0000235B File Offset: 0x0000055B
		public void ExportLogAndMessages(string path, PathType pathType, string query, string targetFilePath)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Exports events and their messages into an external log file. A flag can be set to indicate that the method will continue exporting events even if the specified query fails for some logs. The event messages are exported in the specified language.</summary>
		/// <param name="path">The name of the event log to export events from, or the path to the event log file to export events from.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		/// <param name="query">The query used to select the events to export.  Only the events returned from the query will be exported.</param>
		/// <param name="targetFilePath">The path to the log file (ends in .evtx) in which the exported events will be stored after this method is executed.</param>
		/// <param name="tolerateQueryErrors">
		///       <see langword="true" /> indicates that the method will continue exporting events even if the specified query fails for some logs, and <see langword="false" /> indicates that this method will not continue to export events when the specified query fails.</param>
		/// <param name="targetCultureInfo">The culture that specifies which language that the exported event messages will be in.</param>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x0000235B File Offset: 0x0000055B
		public void ExportLogAndMessages(string path, PathType pathType, string query, string targetFilePath, bool tolerateQueryErrors, CultureInfo targetCultureInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an object that contains runtime information for the specified event log.</summary>
		/// <param name="logName">The name of the event log to get information about, or the path to the event log file to get information about.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInformation" /> object that contains information about the specified log.</returns>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventLogInformation GetLogInformation(string logName, PathType pathType)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets an enumerable collection of all the event log names that are registered with the Event Log service.</summary>
		/// <returns>Returns an enumerable collection of strings that contain the event log names.</returns>
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0005A6D7 File Offset: 0x000588D7
		[SecurityCritical]
		public IEnumerable<string> GetLogNames()
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets an enumerable collection of all the event provider names that are registered with the Event Log service. An event provider is an application that publishes events to an event log.</summary>
		/// <returns>Returns an enumerable collection of strings that contain the event provider names.</returns>
		// Token: 0x06001BA7 RID: 7079 RVA: 0x0005A6D7 File Offset: 0x000588D7
		[SecurityCritical]
		public IEnumerable<string> GetProviderNames()
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
