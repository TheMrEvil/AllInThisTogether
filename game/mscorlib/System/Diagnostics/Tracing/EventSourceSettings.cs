using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies configuration options for an event source.</summary>
	// Token: 0x020009FB RID: 2555
	[Flags]
	public enum EventSourceSettings
	{
		/// <summary>None of the special configuration options are enabled.</summary>
		// Token: 0x04003839 RID: 14393
		Default = 0,
		/// <summary>The event source throws an exception when an error occurs.</summary>
		// Token: 0x0400383A RID: 14394
		ThrowOnEventWriteErrors = 1,
		/// <summary>The ETW listener should use a manifest-based format when raising events. Setting this option is a directive to the ETW listener should use manifest-based format when raising events. This is the default option when defining a type derived from <see cref="T:System.Diagnostics.Tracing.EventSource" /> using one of the protected <see cref="T:System.Diagnostics.Tracing.EventSource" /> constructors.</summary>
		// Token: 0x0400383B RID: 14395
		EtwManifestEventFormat = 4,
		/// <summary>The ETW listener should use self-describing event format. This is the default option when creating a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> using one of the public <see cref="T:System.Diagnostics.Tracing.EventSource" /> constructors.</summary>
		// Token: 0x0400383C RID: 14396
		EtwSelfDescribingEventFormat = 8
	}
}
