using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the properties of an event instance for an event that is received from an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> object. The event properties provide information about the event such as the name of the computer where the event was logged and the time that the event was created.</summary>
	// Token: 0x020003AE RID: 942
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogRecord : EventRecord
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x0000235B File Offset: 0x0000055B
		internal EventLogRecord()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the globally unique identifier (GUID) for the activity in process for which the event is involved. This allows consumers to group related activities.</summary>
		/// <returns>Returns a GUID value.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0005A84C File Offset: 0x00058A4C
		public override Guid? ActivityId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a placeholder (bookmark) that corresponds to this event. This can be used as a placeholder in a stream of events.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventBookmark" /> object.</returns>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0005A05A File Offset: 0x0005825A
		public override EventBookmark Bookmark
		{
			[SecuritySafeCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the event log or the event log file in which the event is stored.</summary>
		/// <returns>Returns a string that contains the name of the event log or the event log file in which the event is stored.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0005A05A File Offset: 0x0005825A
		public string ContainerLog
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the identifier for this event. All events with this identifier value represent the same type of event.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0005A868 File Offset: 0x00058A68
		public override int Id
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the keyword mask of the event. Get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.KeywordsDisplayNames" /> property to get the name of the keywords used in this mask.</summary>
		/// <returns>Returns a long value. This value can be null.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0005A884 File Offset: 0x00058A84
		public override long? Keywords
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display names of the keywords used in the keyword mask for this event.</summary>
		/// <returns>Returns an enumerable collection of strings that contain the display names of the keywords used in the keyword mask for this event.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public override IEnumerable<string> KeywordsDisplayNames
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the level of the event. The level signifies the severity of the event. For the name of the level, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.LevelDisplayName" /> property.</summary>
		/// <returns>Returns a byte value. This value can be null.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0005A8A0 File Offset: 0x00058AA0
		public override byte? Level
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display name of the level for this event.</summary>
		/// <returns>Returns a string that contains the display name of the level for this event.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string LevelDisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the event log where this event is logged.</summary>
		/// <returns>Returns a string that contains a name of the event log that contains this event.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string LogName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the computer on which this event was logged.</summary>
		/// <returns>Returns a string that contains the name of the computer on which this event was logged.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string MachineName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a list of query identifiers that this event matches. This event matches a query if the query would return this event.</summary>
		/// <returns>Returns an enumerable collection of integer values.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IEnumerable<int> MatchedQueryIds
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the opcode of the event. The opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event. For the name of the opcode, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.OpcodeDisplayName" /> property.</summary>
		/// <returns>Returns a short value. This value can be null.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0005A8BC File Offset: 0x00058ABC
		public override short? Opcode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display name of the opcode for this event.</summary>
		/// <returns>Returns a string that contains the display name of the opcode for this event.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string OpcodeDisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the process identifier for the event provider that logged this event.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0005A8D8 File Offset: 0x00058AD8
		public override int? ProcessId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the user-supplied properties of the event.</summary>
		/// <returns>Returns a list of <see cref="T:System.Diagnostics.Eventing.Reader.EventProperty" /> objects.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public override IList<EventProperty> Properties
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the globally unique identifier (GUID) of the event provider that published this event.</summary>
		/// <returns>Returns a GUID value. This value can be null.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		public override Guid? ProviderId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the event provider that published this event.</summary>
		/// <returns>Returns a string that contains the name of the event provider that published this event.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string ProviderName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets qualifier numbers that are used for event identification.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0005A910 File Offset: 0x00058B10
		public override int? Qualifiers
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the event record identifier of the event in the log.</summary>
		/// <returns>Returns a long value. This value can be null.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0005A92C File Offset: 0x00058B2C
		public override long? RecordId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a globally unique identifier (GUID) for a related activity in a process for which an event is involved.</summary>
		/// <returns>Returns a GUID value. This value can be null.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0005A948 File Offset: 0x00058B48
		public override Guid? RelatedActivityId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a task identifier for a portion of an application or a component that publishes an event. A task is a 16-bit value with 16 top values reserved. This type allows any value between 0x0000 and 0xffef to be used. For the name of the task, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.TaskDisplayName" /> property.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0005A964 File Offset: 0x00058B64
		public override int? Task
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display name of the task for the event.</summary>
		/// <returns>Returns a string that contains the display name of the task for the event.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string TaskDisplayName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the thread identifier for the thread that the event provider is running in.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0005A980 File Offset: 0x00058B80
		public override int? ThreadId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the time, in <see cref="T:System.DateTime" /> format, that the event was created.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> value. The value can be null.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0005A99C File Offset: 0x00058B9C
		public override DateTime? TimeCreated
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the security descriptor of the user whose context is used to publish the event.</summary>
		/// <returns>Returns a <see cref="T:System.Security.Principal.SecurityIdentifier" /> value.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0005A05A File Offset: 0x0005825A
		public override SecurityIdentifier UserId
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the version number for the event.</summary>
		/// <returns>Returns a byte value. This value can be null.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0005A9B8 File Offset: 0x00058BB8
		public override byte? Version
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001C22 RID: 7202 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the event message in the current locale.</summary>
		/// <returns>Returns a string that contains the event message in the current locale.</returns>
		// Token: 0x06001C23 RID: 7203 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string FormatDescription()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the event message, replacing variables in the message with the specified values.</summary>
		/// <param name="values">The values used to replace variables in the event message. Variables are represented by %n, where n is a number.</param>
		/// <returns>Returns a string that contains the event message in the current locale.</returns>
		// Token: 0x06001C24 RID: 7204 RVA: 0x0005A05A File Offset: 0x0005825A
		public override string FormatDescription(IEnumerable<object> values)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the enumeration of the values of the user-supplied event properties, or the results of XPath-based data if the event has XML representation.</summary>
		/// <param name="propertySelector">Selects the property values to return.</param>
		/// <returns>Returns a list of objects.</returns>
		// Token: 0x06001C25 RID: 7205 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<object> GetPropertyValues(EventLogPropertySelector propertySelector)
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets the XML representation of the event. All of the event properties are represented in the event's XML. The XML conforms to the event schema.</summary>
		/// <returns>Returns a string that contains the XML representation of the event.</returns>
		// Token: 0x06001C26 RID: 7206 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public override string ToXml()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
