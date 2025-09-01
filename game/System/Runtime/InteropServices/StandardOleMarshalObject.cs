using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Replaces the standard common language runtime (CLR) free-threaded marshaler with the standard OLE STA marshaler.</summary>
	// Token: 0x02000186 RID: 390
	[MonoLimitation("The runtime does nothing special apart from what it already does with marshal-by-ref objects")]
	[ComVisible(true)]
	public class StandardOleMarshalObject : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.StandardOleMarshalObject" /> class.</summary>
		// Token: 0x06000A6F RID: 2671 RVA: 0x0002D758 File Offset: 0x0002B958
		protected StandardOleMarshalObject()
		{
		}
	}
}
