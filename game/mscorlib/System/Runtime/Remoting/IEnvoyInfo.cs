using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	/// <summary>Provides envoy information.</summary>
	// Token: 0x0200055D RID: 1373
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		/// <summary>Gets or sets the list of envoys that were contributed by the server context and object chains when the object was marshaled.</summary>
		/// <returns>A chain of envoy sinks.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060035E2 RID: 13794
		// (set) Token: 0x060035E3 RID: 13795
		IMessageSink EnvoySinks { get; set; }
	}
}
