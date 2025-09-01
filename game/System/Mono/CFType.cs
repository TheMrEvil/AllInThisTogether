using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000025 RID: 37
	internal class CFType
	{
		// Token: 0x0600002C RID: 44
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", EntryPoint = "CFGetTypeID")]
		public static extern IntPtr GetTypeID(IntPtr typeRef);

		// Token: 0x0600002D RID: 45 RVA: 0x0000219B File Offset: 0x0000039B
		public CFType()
		{
		}
	}
}
