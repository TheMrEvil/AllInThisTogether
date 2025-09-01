using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000718 RID: 1816
	internal static class SystemNetworkInterface
	{
		// Token: 0x06003A0C RID: 14860 RVA: 0x000C9920 File Offset: 0x000C7B20
		public static NetworkInterface[] GetNetworkInterfaces()
		{
			NetworkInterface[] result;
			try
			{
				result = SystemNetworkInterface.nif.GetAllNetworkInterfaces();
			}
			catch
			{
				result = new NetworkInterface[0];
			}
			return result;
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0000390E File Offset: 0x00001B0E
		public static bool InternalGetIsNetworkAvailable()
		{
			return true;
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000C9958 File Offset: 0x000C7B58
		public static int InternalLoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.nif.GetLoopbackInterfaceIndex();
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x0000829A File Offset: 0x0000649A
		public static int InternalIPv6LoopbackInterfaceIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000C9964 File Offset: 0x000C7B64
		public static IPAddress GetNetMask(IPAddress address)
		{
			return SystemNetworkInterface.nif.GetNetMask(address);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000C9971 File Offset: 0x000C7B71
		// Note: this type is marked as 'beforefieldinit'.
		static SystemNetworkInterface()
		{
		}

		// Token: 0x04002228 RID: 8744
		private static readonly NetworkInterfaceFactory nif = NetworkInterfaceFactory.Create();
	}
}
