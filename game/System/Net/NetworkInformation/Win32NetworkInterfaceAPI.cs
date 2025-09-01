using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000739 RID: 1849
	internal class Win32NetworkInterfaceAPI : NetworkInterfaceFactory
	{
		// Token: 0x06003B04 RID: 15108
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersAddresses(uint family, uint flags, IntPtr reserved, IntPtr info, ref int size);

		// Token: 0x06003B05 RID: 15109
		[DllImport("iphlpapi.dll")]
		private static extern uint GetBestInterfaceEx(byte[] ipAddress, out int index);

		// Token: 0x06003B06 RID: 15110 RVA: 0x000CB948 File Offset: 0x000C9B48
		private static Win32_IP_ADAPTER_ADDRESSES[] GetAdaptersAddresses()
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = 0;
			uint flags = 192U;
			Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, flags, IntPtr.Zero, intPtr, ref num);
			if (Marshal.SizeOf(typeof(Win32_IP_ADAPTER_ADDRESSES)) > num)
			{
				throw new NetworkInformationException();
			}
			intPtr = Marshal.AllocHGlobal(num);
			int adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, flags, IntPtr.Zero, intPtr, ref num);
			if (adaptersAddresses != 0)
			{
				throw new NetworkInformationException(adaptersAddresses);
			}
			List<Win32_IP_ADAPTER_ADDRESSES> list = new List<Win32_IP_ADAPTER_ADDRESSES>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ADDRESSES win32_IP_ADAPTER_ADDRESSES = Marshal.PtrToStructure<Win32_IP_ADAPTER_ADDRESSES>(intPtr2);
				list.Add(win32_IP_ADAPTER_ADDRESSES);
				intPtr2 = win32_IP_ADAPTER_ADDRESSES.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000CB9E8 File Offset: 0x000C9BE8
		public override NetworkInterface[] GetAllNetworkInterfaces()
		{
			Win32_IP_ADAPTER_ADDRESSES[] adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses();
			NetworkInterface[] array = new NetworkInterface[adaptersAddresses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Win32NetworkInterface2(adaptersAddresses[i]);
			}
			return array;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000CBA24 File Offset: 0x000C9C24
		private static int GetBestInterfaceForAddress(IPAddress addr)
		{
			int result;
			int bestInterfaceEx = (int)Win32NetworkInterfaceAPI.GetBestInterfaceEx(new SocketAddress(addr).m_Buffer, out result);
			if (bestInterfaceEx != 0)
			{
				throw new NetworkInformationException(bestInterfaceEx);
			}
			return result;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000CBA4F File Offset: 0x000C9C4F
		public override int GetLoopbackInterfaceIndex()
		{
			return Win32NetworkInterfaceAPI.GetBestInterfaceForAddress(IPAddress.Loopback);
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x0000829A File Offset: 0x0000649A
		public override IPAddress GetNetMask(IPAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000CBA5B File Offset: 0x000C9C5B
		public Win32NetworkInterfaceAPI()
		{
		}

		// Token: 0x040022A6 RID: 8870
		private const string IPHLPAPI = "iphlpapi.dll";
	}
}
