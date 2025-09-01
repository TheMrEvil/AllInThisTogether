using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies the event log channel for the event.</summary>
	// Token: 0x020009EA RID: 2538
	public enum EventChannel : byte
	{
		/// <summary>No channel specified.</summary>
		// Token: 0x040037FE RID: 14334
		None,
		/// <summary>The administrator log channel.</summary>
		// Token: 0x040037FF RID: 14335
		Admin = 16,
		/// <summary>The operational channel.</summary>
		// Token: 0x04003800 RID: 14336
		Operational,
		/// <summary>The analytic channel.</summary>
		// Token: 0x04003801 RID: 14337
		Analytic,
		/// <summary>The debug channel.</summary>
		// Token: 0x04003802 RID: 14338
		Debug
	}
}
