using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072A RID: 1834
	internal static class Win32IPGlobalPropertiesFactoryPal
	{
		// Token: 0x06003A85 RID: 14981 RVA: 0x000CAF4B File Offset: 0x000C914B
		public static IPGlobalProperties Create()
		{
			return new Win32IPGlobalProperties();
		}
	}
}
