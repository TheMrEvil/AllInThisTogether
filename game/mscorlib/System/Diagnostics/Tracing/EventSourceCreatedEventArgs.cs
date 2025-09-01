using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Tracing.EventListener.EventSourceCreated" /> event.</summary>
	// Token: 0x020009FA RID: 2554
	public class EventSourceCreatedEventArgs : EventArgs
	{
		/// <summary>Get the event source that is attaching to the listener.</summary>
		/// <returns>The event source that is attaching to the listener.</returns>
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x00134811 File Offset: 0x00132A11
		// (set) Token: 0x06005B1D RID: 23325 RVA: 0x00134819 File Offset: 0x00132A19
		public EventSource EventSource
		{
			[CompilerGenerated]
			get
			{
				return this.<EventSource>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<EventSource>k__BackingField = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceCreatedEventArgs" /> class.</summary>
		// Token: 0x06005B1E RID: 23326 RVA: 0x0013443C File Offset: 0x0013263C
		public EventSourceCreatedEventArgs()
		{
		}

		// Token: 0x04003837 RID: 14391
		[CompilerGenerated]
		private EventSource <EventSource>k__BackingField;
	}
}
