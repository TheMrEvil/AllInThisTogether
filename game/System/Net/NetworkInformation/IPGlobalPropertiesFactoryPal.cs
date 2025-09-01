using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200070E RID: 1806
	internal static class IPGlobalPropertiesFactoryPal
	{
		// Token: 0x060039CA RID: 14794 RVA: 0x000C8EBC File Offset: 0x000C70BC
		public static IPGlobalProperties Create()
		{
			IPGlobalProperties ipglobalProperties = UnixIPGlobalPropertiesFactoryPal.Create();
			if (ipglobalProperties == null)
			{
				ipglobalProperties = Win32IPGlobalPropertiesFactoryPal.Create();
			}
			if (ipglobalProperties == null)
			{
				throw new NotImplementedException();
			}
			return ipglobalProperties;
		}
	}
}
