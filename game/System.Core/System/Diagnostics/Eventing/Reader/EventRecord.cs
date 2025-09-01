using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Principal;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the properties of an event instance for an event that is received from an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> object. The event properties provide information about the event such as the name of the computer where the event was logged and the time the event was created. This class is an abstract class. The <see cref="T:System.Diagnostics.Eventing.Reader.EventLogRecord" /> class implements this class.</summary>
	// Token: 0x020003AB RID: 939
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class EventRecord : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> class.</summary>
		// Token: 0x06001BE3 RID: 7139 RVA: 0x0000235B File Offset: 0x0000055B
		protected EventRecord()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the globally unique identifier (GUID) for the activity in process for which the event is involved. This allows consumers to group related activities.</summary>
		/// <returns>Returns a GUID value.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001BE4 RID: 7140
		public abstract Guid? ActivityId { get; }

		/// <summary>Gets a placeholder (bookmark) that corresponds to this event. This can be used as a placeholder in a stream of events.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventBookmark" /> object.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001BE5 RID: 7141
		public abstract EventBookmark Bookmark { get; }

		/// <summary>Gets the identifier for this event. All events with this identifier value represent the same type of event.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001BE6 RID: 7142
		public abstract int Id { get; }

		/// <summary>Gets the keyword mask of the event. Get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventRecord.KeywordsDisplayNames" /> property to get the name of the keywords used in this mask.</summary>
		/// <returns>Returns a long value. This value can be null.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001BE7 RID: 7143
		public abstract long? Keywords { get; }

		/// <summary>Gets the display names of the keywords used in the keyword mask for this event. </summary>
		/// <returns>Returns an enumerable collection of strings that contain the display names of the keywords used in the keyword mask for this event.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001BE8 RID: 7144
		public abstract IEnumerable<string> KeywordsDisplayNames { get; }

		/// <summary>Gets the level of the event. The level signifies the severity of the event. For the name of the level, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventRecord.LevelDisplayName" /> property.</summary>
		/// <returns>Returns a byte value. This value can be null.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001BE9 RID: 7145
		public abstract byte? Level { get; }

		/// <summary>Gets the display name of the level for this event.</summary>
		/// <returns>Returns a string that contains the display name of the level for this event.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001BEA RID: 7146
		public abstract string LevelDisplayName { get; }

		/// <summary>Gets the name of the event log where this event is logged.</summary>
		/// <returns>Returns a string that contains a name of the event log that contains this event.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001BEB RID: 7147
		public abstract string LogName { get; }

		/// <summary>Gets the name of the computer on which this event was logged.</summary>
		/// <returns>Returns a string that contains the name of the computer on which this event was logged.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001BEC RID: 7148
		public abstract string MachineName { get; }

		/// <summary>Gets the opcode of the event. The opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event. For the name of the opcode, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventRecord.OpcodeDisplayName" /> property.</summary>
		/// <returns>Returns a short value. This value can be null.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001BED RID: 7149
		public abstract short? Opcode { get; }

		/// <summary>Gets the display name of the opcode for this event.</summary>
		/// <returns>Returns a string that contains the display name of the opcode for this event.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001BEE RID: 7150
		public abstract string OpcodeDisplayName { get; }

		/// <summary>Gets the process identifier for the event provider that logged this event.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001BEF RID: 7151
		public abstract int? ProcessId { get; }

		/// <summary>Gets the user-supplied properties of the event.</summary>
		/// <returns>Returns a list of <see cref="T:System.Diagnostics.Eventing.Reader.EventProperty" /> objects.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001BF0 RID: 7152
		public abstract IList<EventProperty> Properties { get; }

		/// <summary>Gets the globally unique identifier (GUID) of the event provider that published this event.</summary>
		/// <returns>Returns a GUID value. This value can be null.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001BF1 RID: 7153
		public abstract Guid? ProviderId { get; }

		/// <summary>Gets the name of the event provider that published this event.</summary>
		/// <returns>Returns a string that contains the name of the event provider that published this event.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001BF2 RID: 7154
		public abstract string ProviderName { get; }

		/// <summary>Gets qualifier numbers that are used for event identification.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001BF3 RID: 7155
		public abstract int? Qualifiers { get; }

		/// <summary>Gets the event record identifier of the event in the log.</summary>
		/// <returns>Returns a long value. This value can be null.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001BF4 RID: 7156
		public abstract long? RecordId { get; }

		/// <summary>Gets a globally unique identifier (GUID) for a related activity in a process for which an event is involved.</summary>
		/// <returns>Returns a GUID value. This value can be null.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001BF5 RID: 7157
		public abstract Guid? RelatedActivityId { get; }

		/// <summary>Gets a task identifier for a portion of an application or a component that publishes an event. A task is a 16-bit value with 16 top values reserved. This type allows any value between 0x0000 and 0xffef to be used. To obtain the task name, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventRecord.TaskDisplayName" /> property.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001BF6 RID: 7158
		public abstract int? Task { get; }

		/// <summary>Gets the display name of the task for the event.</summary>
		/// <returns>Returns a string that contains the display name of the task for the event.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001BF7 RID: 7159
		public abstract string TaskDisplayName { get; }

		/// <summary>Gets the thread identifier for the thread that the event provider is running in.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001BF8 RID: 7160
		public abstract int? ThreadId { get; }

		/// <summary>Gets the time, in <see cref="T:System.DateTime" /> format, that the event was created.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> value. The value can be null.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001BF9 RID: 7161
		public abstract DateTime? TimeCreated { get; }

		/// <summary>Gets the security descriptor of the user whose context is used to publish the event.</summary>
		/// <returns>Returns a <see cref="T:System.Security.Principal.SecurityIdentifier" /> value.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001BFA RID: 7162
		public abstract SecurityIdentifier UserId { get; }

		/// <summary>Gets the version number for the event.</summary>
		/// <returns>Returns a byte value. This value can be null.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001BFB RID: 7163
		public abstract byte? Version { get; }

		/// <summary>Releases all the resources used by this object.</summary>
		// Token: 0x06001BFC RID: 7164 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001BFD RID: 7165 RVA: 0x0000235B File Offset: 0x0000055B
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the event message in the current locale.</summary>
		/// <returns>Returns a string that contains the event message in the current locale.</returns>
		// Token: 0x06001BFE RID: 7166
		public abstract string FormatDescription();

		/// <summary>Gets the event message, replacing variables in the message with the specified values.</summary>
		/// <param name="values">The values used to replace variables in the event message. Variables are represented by %n, where n is a number.</param>
		/// <returns>Returns a string that contains the event message in the current locale.</returns>
		// Token: 0x06001BFF RID: 7167
		public abstract string FormatDescription(IEnumerable<object> values);

		/// <summary>Gets the XML representation of the event. All of the event properties are represented in the event XML. The XML conforms to the event schema.</summary>
		/// <returns>Returns a string that contains the XML representation of the event.</returns>
		// Token: 0x06001C00 RID: 7168
		public abstract string ToXml();
	}
}
