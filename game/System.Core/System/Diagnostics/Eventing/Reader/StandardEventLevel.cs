using System;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Defines the standard event levels that are used in the Event Log service. The level defines the severity of the event. Custom event levels can be defined beyond these standard levels. For more information about levels, see <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" />.</summary>
	// Token: 0x020003B6 RID: 950
	public enum StandardEventLevel
	{
		/// <summary>This level corresponds to critical errors, which is a serious error that has caused a major failure. </summary>
		// Token: 0x04000D63 RID: 3427
		Critical = 1,
		/// <summary>This level corresponds to normal errors that signify a problem. </summary>
		// Token: 0x04000D64 RID: 3428
		Error,
		/// <summary>This level corresponds to informational events or messages that are not errors. These events can help trace the progress or state of an application.</summary>
		// Token: 0x04000D65 RID: 3429
		Informational = 4,
		/// <summary>This value indicates that not filtering on the level is done during the event publishing.</summary>
		// Token: 0x04000D66 RID: 3430
		LogAlways = 0,
		/// <summary>This level corresponds to lengthy events or messages. </summary>
		// Token: 0x04000D67 RID: 3431
		Verbose = 5,
		/// <summary>This level corresponds to warning events. For example, an event that gets published because a disk is nearing full capacity is a warning event.</summary>
		// Token: 0x04000D68 RID: 3432
		Warning = 3
	}
}
