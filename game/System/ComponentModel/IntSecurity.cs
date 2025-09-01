using System;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000417 RID: 1047
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class IntSecurity
	{
		// Token: 0x060021CD RID: 8653 RVA: 0x00073AF5 File Offset: 0x00071CF5
		public static string UnsafeGetFullPath(string fileName)
		{
			return Path.GetFullPath(fileName);
		}
	}
}
