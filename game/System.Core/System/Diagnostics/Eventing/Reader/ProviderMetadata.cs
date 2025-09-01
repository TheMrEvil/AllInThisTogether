using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains static information about an event provider, such as the name and id of the provider, and the collection of events defined in the provider.</summary>
	// Token: 0x020003B4 RID: 948
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ProviderMetadata : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.ProviderMetadata" /> class by specifying the name of the provider that you want to retrieve information about.</summary>
		/// <param name="providerName">The name of the event provider that you want to retrieve information about.</param>
		// Token: 0x06001C47 RID: 7239 RVA: 0x0000235B File Offset: 0x0000055B
		public ProviderMetadata(string providerName)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.ProviderMetadata" /> class by specifying the name of the provider that you want to retrieve information about, the event log service that the provider is registered with, and the language that you want to return the information in.</summary>
		/// <param name="providerName">The name of the event provider that you want to retrieve information about.</param>
		/// <param name="session">The <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object that specifies whether to get the provider information from a provider on the local computer or a provider on a remote computer.</param>
		/// <param name="targetCultureInfo">The culture that specifies the language that the information should be returned in.</param>
		// Token: 0x06001C48 RID: 7240 RVA: 0x0000235B File Offset: 0x0000055B
		public ProviderMetadata(string providerName, EventLogSession session, CultureInfo targetCultureInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name of the event provider.</summary>
		/// <returns>Returns a string that contains the localized name of the event provider.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0005A05A File Offset: 0x0005825A
		public string DisplayName
		{
			[SecurityCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventMetadata" /> objects, each of which represents an event that is defined in the provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventMetadata" /> objects.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IEnumerable<EventMetadata> Events
		{
			[SecurityCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the base of the URL used to form help requests for the events in this event provider.</summary>
		/// <returns>Returns a <see cref="T:System.Uri" /> value.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0005A05A File Offset: 0x0005825A
		public Uri HelpLink
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the globally unique identifier (GUID) for the event provider.</summary>
		/// <returns>Returns the GUID value for the event provider.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0005AA7C File Offset: 0x00058C7C
		public Guid Id
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" /> objects, each of which represent an event keyword that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" /> objects.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<EventKeyword> Keywords
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" /> objects, each of which represent a level that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" /> objects.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<EventLevel> Levels
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLogLink" /> objects, each of which represent a link to an event log that is used by the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLogLink" /> objects.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<EventLogLink> LogLinks
		{
			[SecurityCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the path of the file that contains the message table resource that has the strings associated with the provider metadata.</summary>
		/// <returns>Returns a string that contains the path of the provider message file.</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0005A05A File Offset: 0x0005825A
		public string MessageFilePath
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the unique name of the event provider.</summary>
		/// <returns>Returns a string that contains the unique name of the event provider.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0005A05A File Offset: 0x0005825A
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" /> objects, each of which represent an opcode that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" /> objects.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<EventOpcode> Opcodes
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the path of the file that contains the message table resource that has the strings used for parameter substitutions in event descriptions.</summary>
		/// <returns>Returns a string that contains the path of the file that contains the message table resource that has the strings used for parameter substitutions in event descriptions.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0005A05A File Offset: 0x0005825A
		public string ParameterFilePath
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the path to the file that contains the metadata associated with the provider.</summary>
		/// <returns>Returns a string that contains the path to the file that contains the metadata associated with the provider.</returns>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0005A05A File Offset: 0x0005825A
		public string ResourceFilePath
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventTask" /> objects, each of which represent a task that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventTask" /> objects.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<EventTask> Tasks
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Releases all the resources used by this object.</summary>
		// Token: 0x06001C56 RID: 7254 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001C57 RID: 7255 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
