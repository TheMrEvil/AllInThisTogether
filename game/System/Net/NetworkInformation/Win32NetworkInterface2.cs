using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073A RID: 1850
	internal sealed class Win32NetworkInterface2 : NetworkInterface
	{
		// Token: 0x06003B0C RID: 15116
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersInfo(IntPtr info, ref int size);

		// Token: 0x06003B0D RID: 15117
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetIfEntry(ref Win32_MIB_IFROW row);

		// Token: 0x06003B0E RID: 15118 RVA: 0x000CBA64 File Offset: 0x000C9C64
		private static Win32_IP_ADAPTER_INFO[] GetAdaptersInfo()
		{
			int cb = 0;
			Win32NetworkInterface2.GetAdaptersInfo(IntPtr.Zero, ref cb);
			IntPtr intPtr = Marshal.AllocHGlobal(cb);
			int adaptersInfo = Win32NetworkInterface2.GetAdaptersInfo(intPtr, ref cb);
			if (adaptersInfo != 0)
			{
				throw new NetworkInformationException(adaptersInfo);
			}
			List<Win32_IP_ADAPTER_INFO> list = new List<Win32_IP_ADAPTER_INFO>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_INFO win32_IP_ADAPTER_INFO = Marshal.PtrToStructure<Win32_IP_ADAPTER_INFO>(intPtr2);
				list.Add(win32_IP_ADAPTER_INFO);
				intPtr2 = win32_IP_ADAPTER_INFO.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000CBAD0 File Offset: 0x000C9CD0
		internal Win32NetworkInterface2(Win32_IP_ADAPTER_ADDRESSES addr)
		{
			this.addr = addr;
			this.mib4 = default(Win32_MIB_IFROW);
			this.mib4.Index = addr.Alignment.IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib4) != 0)
			{
				this.mib4.Index = -1;
			}
			this.mib6 = default(Win32_MIB_IFROW);
			this.mib6.Index = addr.Ipv6IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib6) != 0)
			{
				this.mib6.Index = -1;
			}
			this.ip4stats = new Win32IPv4InterfaceStatistics(this.mib4);
			this.ip_if_props = new Win32IPInterfaceProperties2(addr, this.mib4, this.mib6);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000CBB84 File Offset: 0x000C9D84
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.ip_if_props;
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000CBB8C File Offset: 0x000C9D8C
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return this.ip4stats;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000CBB94 File Offset: 0x000C9D94
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addr.PhysicalAddressLength];
			Array.Copy(this.addr.PhysicalAddress, 0, array, 0, array.Length);
			return new PhysicalAddress(array);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000CBBCE File Offset: 0x000C9DCE
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			if (networkInterfaceComponent != NetworkInterfaceComponent.IPv4)
			{
				return networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && this.mib6.Index >= 0;
			}
			return this.mib4.Index >= 0;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000CBBFE File Offset: 0x000C9DFE
		public override string Description
		{
			get
			{
				return this.addr.Description;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000CBC0B File Offset: 0x000C9E0B
		public override string Id
		{
			get
			{
				return this.addr.AdapterName;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003B16 RID: 15126 RVA: 0x000CBC18 File Offset: 0x000C9E18
		public override bool IsReceiveOnly
		{
			get
			{
				return this.addr.IsReceiveOnly;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000CBC25 File Offset: 0x000C9E25
		public override string Name
		{
			get
			{
				return this.addr.FriendlyName;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x000CBC32 File Offset: 0x000C9E32
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.addr.IfType;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000CBC3F File Offset: 0x000C9E3F
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.addr.OperStatus;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x000CBC4C File Offset: 0x000C9E4C
		public override long Speed
		{
			get
			{
				return (long)((ulong)((this.mib6.Index >= 0) ? this.mib6.Speed : this.mib4.Speed));
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000CBC75 File Offset: 0x000C9E75
		public override bool SupportsMulticast
		{
			get
			{
				return !this.addr.NoMulticast;
			}
		}

		// Token: 0x040022A7 RID: 8871
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x040022A8 RID: 8872
		private Win32_MIB_IFROW mib4;

		// Token: 0x040022A9 RID: 8873
		private Win32_MIB_IFROW mib6;

		// Token: 0x040022AA RID: 8874
		private Win32IPv4InterfaceStatistics ip4stats;

		// Token: 0x040022AB RID: 8875
		private IPInterfaceProperties ip_if_props;
	}
}
