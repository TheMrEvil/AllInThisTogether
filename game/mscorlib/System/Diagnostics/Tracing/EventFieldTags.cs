using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the user-defined tag that is placed on fields of user-defined types that are passed as <see cref="T:System.Diagnostics.Tracing.EventSource" /> payloads through the <see cref="T:System.Diagnostics.Tracing.EventFieldAttribute" />.</summary>
	// Token: 0x020009F3 RID: 2547
	[Flags]
	public enum EventFieldTags
	{
		/// <summary>Specifies no tag and is equal to zero.</summary>
		// Token: 0x04003826 RID: 14374
		None = 0
	}
}
