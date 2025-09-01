using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies the type of Windows account used.</summary>
	// Token: 0x020004EA RID: 1258
	[ComVisible(true)]
	[Serializable]
	public enum WindowsAccountType
	{
		/// <summary>A standard user account.</summary>
		// Token: 0x0400232E RID: 9006
		Normal,
		/// <summary>A Windows guest account.</summary>
		// Token: 0x0400232F RID: 9007
		Guest,
		/// <summary>A Windows system account.</summary>
		// Token: 0x04002330 RID: 9008
		System,
		/// <summary>An anonymous account.</summary>
		// Token: 0x04002331 RID: 9009
		Anonymous
	}
}
