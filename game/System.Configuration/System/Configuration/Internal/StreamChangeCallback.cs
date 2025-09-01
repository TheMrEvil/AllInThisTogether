using System;

namespace System.Configuration.Internal
{
	/// <summary>Represents a method for hosts to call when a monitored stream has changed.</summary>
	/// <param name="streamName">The name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
	// Token: 0x02000088 RID: 136
	// (Invoke) Token: 0x06000489 RID: 1161
	public delegate void StreamChangeCallback(string streamName);
}
