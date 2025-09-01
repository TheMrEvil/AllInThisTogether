using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Describes the command (<see cref="P:System.Diagnostics.Tracing.EventCommandEventArgs.Command" /> property) that is passed to the <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> callback.</summary>
	// Token: 0x020009ED RID: 2541
	public enum EventCommand
	{
		/// <summary>Update the event.</summary>
		// Token: 0x04003819 RID: 14361
		Update,
		/// <summary>Send the manifest.</summary>
		// Token: 0x0400381A RID: 14362
		SendManifest = -1,
		/// <summary>Enable the event.</summary>
		// Token: 0x0400381B RID: 14363
		Enable = -2,
		/// <summary>Disable the event.</summary>
		// Token: 0x0400381C RID: 14364
		Disable = -3
	}
}
