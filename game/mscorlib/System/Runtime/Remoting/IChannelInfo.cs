using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Provides custom channel information that is carried along with the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
	// Token: 0x0200055C RID: 1372
	[ComVisible(true)]
	public interface IChannelInfo
	{
		/// <summary>Gets or sets the channel data for each channel.</summary>
		/// <returns>The channel data for each channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060035E0 RID: 13792
		// (set) Token: 0x060035E1 RID: 13793
		object[] ChannelData { get; set; }
	}
}
