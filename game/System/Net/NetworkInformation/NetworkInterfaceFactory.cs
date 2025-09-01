using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000719 RID: 1817
	internal abstract class NetworkInterfaceFactory
	{
		// Token: 0x06003A12 RID: 14866
		public abstract NetworkInterface[] GetAllNetworkInterfaces();

		// Token: 0x06003A13 RID: 14867
		public abstract int GetLoopbackInterfaceIndex();

		// Token: 0x06003A14 RID: 14868
		public abstract IPAddress GetNetMask(IPAddress address);

		// Token: 0x06003A15 RID: 14869 RVA: 0x000C997D File Offset: 0x000C7B7D
		public static NetworkInterfaceFactory Create()
		{
			return NetworkInterfaceFactoryPal.Create();
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x0000219B File Offset: 0x0000039B
		protected NetworkInterfaceFactory()
		{
		}
	}
}
