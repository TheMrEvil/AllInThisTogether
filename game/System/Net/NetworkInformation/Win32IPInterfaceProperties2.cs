using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072D RID: 1837
	internal class Win32IPInterfaceProperties2 : IPInterfaceProperties
	{
		// Token: 0x06003A9D RID: 15005 RVA: 0x000CB093 File Offset: 0x000C9293
		public Win32IPInterfaceProperties2(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib4, Win32_MIB_IFROW mib6)
		{
			this.addr = addr;
			this.mib4 = mib4;
			this.mib6 = mib6;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000CB0B0 File Offset: 0x000C92B0
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			return new Win32IPv4InterfaceProperties(this.addr, this.mib4);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000CB0C3 File Offset: 0x000C92C3
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			return new Win32IPv6InterfaceProperties(this.mib6);
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x000CB0D0 File Offset: 0x000C92D0
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return Win32IPInterfaceProperties2.Win32FromAnycast(this.addr.FirstAnycastAddress);
			}
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000CB0E4 File Offset: 0x000C92E4
		private static IPAddressInformationCollection Win32FromAnycast(IntPtr ptr)
		{
			IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsTransient));
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			return ipaddressInformationCollection;
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000CB158 File Offset: 0x000C9358
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				IPAddressCollection result;
				try
				{
					result = Win32IPAddressCollection.FromSocketAddress(this.addr.Dhcpv4Server);
				}
				catch (IndexOutOfRangeException)
				{
					result = Win32IPAddressCollection.Empty;
				}
				return result;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x000CB194 File Offset: 0x000C9394
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return Win32IPAddressCollection.FromDnsServer(this.addr.FirstDnsServerAddress);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000CB1A6 File Offset: 0x000C93A6
		public override string DnsSuffix
		{
			get
			{
				return this.addr.DnsSuffix;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x000CB1B4 File Offset: 0x000C93B4
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
				try
				{
					IntPtr intPtr = this.addr.FirstGatewayAddress;
					while (intPtr != IntPtr.Zero)
					{
						Win32_IP_ADAPTER_GATEWAY_ADDRESS win32_IP_ADAPTER_GATEWAY_ADDRESS = (Win32_IP_ADAPTER_GATEWAY_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_GATEWAY_ADDRESS));
						gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(win32_IP_ADAPTER_GATEWAY_ADDRESS.Address.GetIPAddress()));
						intPtr = win32_IP_ADAPTER_GATEWAY_ADDRESS.Next;
					}
				}
				catch (IndexOutOfRangeException)
				{
				}
				return gatewayIPAddressInformationCollection;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000CB22C File Offset: 0x000C942C
		public override bool IsDnsEnabled
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableDns > 0U;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x000CB23C File Offset: 0x000C943C
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.addr.DdnsEnabled;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000CB257 File Offset: 0x000C9457
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return Win32IPInterfaceProperties2.Win32FromMulticast(this.addr.FirstMulticastAddress);
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000CB26C File Offset: 0x000C946C
		private static MulticastIPAddressInformationCollection Win32FromMulticast(IntPtr ptr)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_MULTICAST_ADDRESS win32_IP_ADAPTER_MULTICAST_ADDRESS = (Win32_IP_ADAPTER_MULTICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_MULTICAST_ADDRESS));
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(new SystemIPAddressInformation(win32_IP_ADAPTER_MULTICAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsTransient)));
				intPtr = win32_IP_ADAPTER_MULTICAST_ADDRESS.Next;
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000CB2E4 File Offset: 0x000C94E4
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				UnicastIPAddressInformationCollection result;
				try
				{
					result = Win32IPInterfaceProperties2.Win32FromUnicast(this.addr.FirstUnicastAddress);
				}
				catch (IndexOutOfRangeException)
				{
					result = new UnicastIPAddressInformationCollection();
				}
				return result;
			}
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000CB320 File Offset: 0x000C9520
		private static UnicastIPAddressInformationCollection Win32FromUnicast(IntPtr ptr)
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_UNICAST_ADDRESS win32_IP_ADAPTER_UNICAST_ADDRESS = (Win32_IP_ADAPTER_UNICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_UNICAST_ADDRESS));
				unicastIPAddressInformationCollection.InternalAdd(new Win32UnicastIPAddressInformation(win32_IP_ADAPTER_UNICAST_ADDRESS));
				intPtr = win32_IP_ADAPTER_UNICAST_ADDRESS.Next;
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000CB370 File Offset: 0x000C9570
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				IPAddressCollection result;
				try
				{
					result = Win32IPAddressCollection.FromWinsServer(this.addr.FirstWinsServerAddress);
				}
				catch (IndexOutOfRangeException)
				{
					result = Win32IPAddressCollection.Empty;
				}
				return result;
			}
		}

		// Token: 0x04002273 RID: 8819
		private readonly Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04002274 RID: 8820
		private readonly Win32_MIB_IFROW mib4;

		// Token: 0x04002275 RID: 8821
		private readonly Win32_MIB_IFROW mib6;
	}
}
