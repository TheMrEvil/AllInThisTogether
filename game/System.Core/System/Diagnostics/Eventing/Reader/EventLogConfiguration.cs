using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains static information and configuration settings for an event log. Many of the configurations settings were defined by the event provider that created the log.</summary>
	// Token: 0x0200039A RID: 922
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogConfiguration : IDisposable
	{
		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogConfiguration" /> object by specifying the local event log for which to get information and configuration settings. </summary>
		/// <param name="logName">The name of the local event log for which to get information and configuration settings.</param>
		// Token: 0x06001B7A RID: 7034 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogConfiguration(string logName)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Eventing.Reader.EventLogConfiguration" /> object by specifying the name of the log for which to get information and configuration settings. The log can be on the local computer or a remote computer, based on the event log session specified.</summary>
		/// <param name="logName">The name of the event log for which to get information and configuration settings.</param>
		/// <param name="session">The event log session used to determine the event log service that the specified log belongs to. The session is either connected to the event log service on the local computer or a remote computer.</param>
		// Token: 0x06001B7B RID: 7035 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public EventLogConfiguration(string logName, EventLogSession session)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the flag that indicates if the event log is a classic event log. A classic event log is one that has its events defined in a .mc file instead of a manifest (.xml file) used by the event provider.</summary>
		/// <returns>Returns <see langword="true" /> if the event log is a classic log, and returns <see langword="false" /> if the event log is not a classic log.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0005A56C File Offset: 0x0005876C
		public bool IsClassicLog
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the event log is enabled or disabled. An enabled log is one in which events can be logged, and a disabled log is one in which events cannot be logged.</summary>
		/// <returns>Returns <see langword="true" /> if the log is enabled, and returns <see langword="false" /> if the log is disabled.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0005A588 File Offset: 0x00058788
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0000235B File Offset: 0x0000055B
		public bool IsEnabled
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the file directory path to the location of the file where the events are stored for the log.</summary>
		/// <returns>Returns a string that contains the path to the event log file.</returns>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x0000235B File Offset: 0x0000055B
		public string LogFilePath
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogIsolation" /> value that specifies whether the event log is an application, system, or custom event log. </summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogIsolation" /> value.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0005A5A4 File Offset: 0x000587A4
		public EventLogIsolation LogIsolation
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return EventLogIsolation.Application;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogMode" /> value that determines how events are handled when the event log becomes full.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogMode" /> value.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0005A5C0 File Offset: 0x000587C0
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogMode LogMode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return EventLogMode.Circular;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the name of the event log.</summary>
		/// <returns>Returns a string that contains the name of the event log.</returns>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0005A05A File Offset: 0x0005825A
		public string LogName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogType" /> value that determines the type of the event log.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogType" /> value.</returns>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0005A5DC File Offset: 0x000587DC
		public EventLogType LogType
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return EventLogType.Administrative;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, that the event log file is allowed to be. When the file reaches this maximum size, it is considered full.</summary>
		/// <returns>Returns a long value that represents the maximum size, in bytes, that the event log file is allowed to be.</returns>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0005A5F8 File Offset: 0x000587F8
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0000235B File Offset: 0x0000055B
		public long MaximumSizeInBytes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the name of the event provider that created this event log.</summary>
		/// <returns>Returns a string that contains the name of the event provider that created this event log.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0005A05A File Offset: 0x0005825A
		public string OwningProviderName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the size of the buffer that the event provider uses for publishing events to the log.</summary>
		/// <returns>Returns an integer value that can be null.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0005A614 File Offset: 0x00058814
		public int? ProviderBufferSize
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the control globally unique identifier (GUID) for the event log if the log is a debug log. If this log is not a debug log, this value will be null. </summary>
		/// <returns>Returns a GUID value or null.</returns>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0005A630 File Offset: 0x00058830
		public Guid? ProviderControlGuid
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets keyword mask used by the event provider.</summary>
		/// <returns>Returns a long value that can be null if the event provider did not define any keywords.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0005A64C File Offset: 0x0005884C
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x0000235B File Offset: 0x0000055B
		public long? ProviderKeywords
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the maximum latency time used by the event provider when publishing events to the log.</summary>
		/// <returns>Returns an integer value that can be null if no latency time was specified by the event provider.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0005A668 File Offset: 0x00058868
		public int? ProviderLatency
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the maximum event level (which defines the severity of the event) that is allowed to be logged in the event log. This value is defined by the event provider.</summary>
		/// <returns>Returns an integer value that can be null if the maximum event level was not defined in the event provider.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0005A684 File Offset: 0x00058884
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0000235B File Offset: 0x0000055B
		public int? ProviderLevel
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the maximum number of buffers used by the event provider to publish events to the event log.</summary>
		/// <returns>Returns an integer value that is the maximum number of buffers used by the event provider to publish events to the event log. This value can be null.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0005A6A0 File Offset: 0x000588A0
		public int? ProviderMaximumNumberOfBuffers
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the minimum number of buffers used by the event provider to publish events to the event log.</summary>
		/// <returns>Returns an integer value that is the minimum number of buffers used by the event provider to publish events to the event log. This value can be null.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0005A6BC File Offset: 0x000588BC
		public int? ProviderMinimumNumberOfBuffers
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of the names of all the event providers that can publish events to this event log.</summary>
		/// <returns>Returns an enumerable collection of strings that contain the event provider names.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IEnumerable<string> ProviderNames
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets the security descriptor of the event log. The security descriptor defines the users and groups of users that can read and write to the event log.</summary>
		/// <returns>Returns a string that contains the security descriptor for the event log.</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0000235B File Offset: 0x0000055B
		public string SecurityDescriptor
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Releases all the resources used by this object.</summary>
		// Token: 0x06001B95 RID: 7061 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001B96 RID: 7062 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the configuration settings that </summary>
		// Token: 0x06001B97 RID: 7063 RVA: 0x0000235B File Offset: 0x0000055B
		public void SaveChanges()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
