using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies additional event schema information for an event.</summary>
	// Token: 0x020009EC RID: 2540
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> class with the specified event identifier.</summary>
		/// <param name="eventId">The event identifier for the event.</param>
		// Token: 0x06005A9F RID: 23199 RVA: 0x00134383 File Offset: 0x00132583
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
		}

		/// <summary>Gets or sets the identifier for the event.</summary>
		/// <returns>The event identifier. This value should be between 0 and 65535.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x00134392 File Offset: 0x00132592
		// (set) Token: 0x06005AA1 RID: 23201 RVA: 0x0013439A File Offset: 0x0013259A
		public int EventId
		{
			[CompilerGenerated]
			get
			{
				return this.<EventId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EventId>k__BackingField = value;
			}
		}

		/// <summary>Specifies the behavior of the start and stop events of an activity. An activity is the region of time in an app between the start and the stop.</summary>
		/// <returns>Returns <see cref="T:System.Diagnostics.Tracing.EventActivityOptions" />.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x001343A3 File Offset: 0x001325A3
		// (set) Token: 0x06005AA3 RID: 23203 RVA: 0x001343AB File Offset: 0x001325AB
		public EventActivityOptions ActivityOptions
		{
			[CompilerGenerated]
			get
			{
				return this.<ActivityOptions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ActivityOptions>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the level for the event.</summary>
		/// <returns>One of the enumeration values that specifies the level for the event.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x001343B4 File Offset: 0x001325B4
		// (set) Token: 0x06005AA5 RID: 23205 RVA: 0x001343BC File Offset: 0x001325BC
		public EventLevel Level
		{
			[CompilerGenerated]
			get
			{
				return this.<Level>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Level>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the keywords for the event.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005AA6 RID: 23206 RVA: 0x001343C5 File Offset: 0x001325C5
		// (set) Token: 0x06005AA7 RID: 23207 RVA: 0x001343CD File Offset: 0x001325CD
		public EventKeywords Keywords
		{
			[CompilerGenerated]
			get
			{
				return this.<Keywords>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Keywords>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the operation code for the event.</summary>
		/// <returns>One of the enumeration values that specifies the operation code.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x001343D6 File Offset: 0x001325D6
		// (set) Token: 0x06005AA9 RID: 23209 RVA: 0x001343DE File Offset: 0x001325DE
		public EventOpcode Opcode
		{
			[CompilerGenerated]
			get
			{
				return this.<Opcode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Opcode>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets an additional event log where the event should be written.</summary>
		/// <returns>An additional event log where the event should be written.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005AAA RID: 23210 RVA: 0x001343E7 File Offset: 0x001325E7
		// (set) Token: 0x06005AAB RID: 23211 RVA: 0x001343EF File Offset: 0x001325EF
		public EventChannel Channel
		{
			[CompilerGenerated]
			get
			{
				return this.<Channel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Channel>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x001343F8 File Offset: 0x001325F8
		// (set) Token: 0x06005AAD RID: 23213 RVA: 0x00134400 File Offset: 0x00132600
		public string Message
		{
			[CompilerGenerated]
			get
			{
				return this.<Message>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Message>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x00134409 File Offset: 0x00132609
		// (set) Token: 0x06005AAF RID: 23215 RVA: 0x00134411 File Offset: 0x00132611
		public EventTask Task
		{
			[CompilerGenerated]
			get
			{
				return this.<Task>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Task>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Diagnostics.Tracing.EventTags" /> value for this <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> object. An event tag is a user-defined value that is passed through when the event is logged.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.Tracing.EventTags" /> value for this <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> object. An event tag is a user-defined value that is passed through when the event is logged.</returns>
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005AB0 RID: 23216 RVA: 0x0013441A File Offset: 0x0013261A
		// (set) Token: 0x06005AB1 RID: 23217 RVA: 0x00134422 File Offset: 0x00132622
		public EventTags Tags
		{
			[CompilerGenerated]
			get
			{
				return this.<Tags>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tags>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005AB2 RID: 23218 RVA: 0x0013442B File Offset: 0x0013262B
		// (set) Token: 0x06005AB3 RID: 23219 RVA: 0x00134433 File Offset: 0x00132633
		public byte Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x0400380E RID: 14350
		[CompilerGenerated]
		private int <EventId>k__BackingField;

		// Token: 0x0400380F RID: 14351
		[CompilerGenerated]
		private EventActivityOptions <ActivityOptions>k__BackingField;

		// Token: 0x04003810 RID: 14352
		[CompilerGenerated]
		private EventLevel <Level>k__BackingField;

		// Token: 0x04003811 RID: 14353
		[CompilerGenerated]
		private EventKeywords <Keywords>k__BackingField;

		// Token: 0x04003812 RID: 14354
		[CompilerGenerated]
		private EventOpcode <Opcode>k__BackingField;

		// Token: 0x04003813 RID: 14355
		[CompilerGenerated]
		private EventChannel <Channel>k__BackingField;

		// Token: 0x04003814 RID: 14356
		[CompilerGenerated]
		private string <Message>k__BackingField;

		// Token: 0x04003815 RID: 14357
		[CompilerGenerated]
		private EventTask <Task>k__BackingField;

		// Token: 0x04003816 RID: 14358
		[CompilerGenerated]
		private EventTags <Tags>k__BackingField;

		// Token: 0x04003817 RID: 14359
		[CompilerGenerated]
		private byte <Version>k__BackingField;
	}
}
