using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the standard opcodes that are attached to events by the event provider. For more information about opcodes, see <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" />.</summary>
	// Token: 0x020003B7 RID: 951
	public enum StandardEventOpcode
	{
		/// <summary>An event with this opcode is a trace collection start event.</summary>
		// Token: 0x04000D6A RID: 3434
		DataCollectionStart = 3,
		/// <summary>An event with this opcode is a trace collection stop event.</summary>
		// Token: 0x04000D6B RID: 3435
		DataCollectionStop,
		/// <summary>An event with this opcode is an extension event.</summary>
		// Token: 0x04000D6C RID: 3436
		Extension,
		/// <summary>An event with this opcode is an informational event.</summary>
		// Token: 0x04000D6D RID: 3437
		Info = 0,
		/// <summary>An event with this opcode is published when one activity in an application receives data.</summary>
		// Token: 0x04000D6E RID: 3438
		Receive = 240,
		/// <summary>An event with this opcode is published after an activity in an application replies to an event.</summary>
		// Token: 0x04000D6F RID: 3439
		Reply = 6,
		/// <summary>An event with this opcode is published after an activity in an application resumes from a suspended state. The event should follow an event with the Suspend opcode.</summary>
		// Token: 0x04000D70 RID: 3440
		Resume,
		/// <summary>An event with this opcode is published when one activity in an application transfers data or system resources to another activity. </summary>
		// Token: 0x04000D71 RID: 3441
		Send = 9,
		/// <summary>An event with this opcode is published when an application starts a new transaction or activity. This can be embedded into another transaction or activity when multiple events with the Start opcode follow each other without an event with a Stop opcode.</summary>
		// Token: 0x04000D72 RID: 3442
		Start = 1,
		/// <summary>An event with this opcode is published when an activity or a transaction in an application ends. The event corresponds to the last unpaired event with a Start opcode.</summary>
		// Token: 0x04000D73 RID: 3443
		Stop,
		/// <summary>An event with this opcode is published when an activity in an application is suspended. </summary>
		// Token: 0x04000D74 RID: 3444
		Suspend = 8
	}
}
