using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Identifies the operating system, or platform, supported by an assembly.</summary>
	// Token: 0x02000248 RID: 584
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		/// <summary>The operating system is Win32s. This value is no longer in use.</summary>
		// Token: 0x04001770 RID: 6000
		Win32S,
		/// <summary>The operating system is Windows 95 or Windows 98. This value is no longer in use.</summary>
		// Token: 0x04001771 RID: 6001
		Win32Windows,
		/// <summary>The operating system is Windows NT or later.</summary>
		// Token: 0x04001772 RID: 6002
		Win32NT,
		/// <summary>The operating system is Windows CE. This value is no longer in use.</summary>
		// Token: 0x04001773 RID: 6003
		WinCE,
		/// <summary>The operating system is Unix.</summary>
		// Token: 0x04001774 RID: 6004
		Unix,
		/// <summary>The development platform is Xbox 360. This value is no longer in use.</summary>
		// Token: 0x04001775 RID: 6005
		Xbox,
		/// <summary>The operating system is Macintosh. This value was returned by Silverlight. On .NET Core, its replacement is Unix.</summary>
		// Token: 0x04001776 RID: 6006
		MacOSX
	}
}
