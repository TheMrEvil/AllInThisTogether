using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the tracking of activity start and stop events. You should only use the lower 24 bits. For more information, see <see cref="T:System.Diagnostics.Tracing.EventSourceOptions" /> and <see cref="M:System.Diagnostics.Tracing.EventSource.Write(System.String,System.Diagnostics.Tracing.EventSourceOptions)" />.</summary>
	// Token: 0x020009E6 RID: 2534
	[Flags]
	public enum EventTags
	{
		/// <summary>Specifies no tag and is equal to zero.</summary>
		// Token: 0x040037E7 RID: 14311
		None = 0
	}
}
