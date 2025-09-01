using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000724 RID: 1828
	internal class Win32IPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06003A5C RID: 14940 RVA: 0x000CA8DC File Offset: 0x000C8ADC
		private unsafe void FillTcpTable(out List<Win32IPGlobalProperties.Win32_MIB_TCPROW> tab4, out List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> tab6)
		{
			tab4 = new List<Win32IPGlobalProperties.Win32_MIB_TCPROW>();
			int num = 0;
			Win32IPGlobalProperties.GetTcpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetTcpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_TCPROW win32_MIB_TCPROW = new Win32IPGlobalProperties.Win32_MIB_TCPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_TCPROW);
					tab4.Add(win32_MIB_TCPROW);
				}
			}
			tab6 = new List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>();
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetTcp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetTcp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_TCP6ROW win32_MIB_TCP6ROW = new Win32IPGlobalProperties.Win32_MIB_TCP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_TCP6ROW);
						tab6.Add(win32_MIB_TCP6ROW);
					}
				}
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000CAA22 File Offset: 0x000C8C22
		private bool IsListenerState(TcpState state)
		{
			return state - TcpState.Listen <= 1 || state - TcpState.FinWait1 <= 2;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000CAA34 File Offset: 0x000C8C34
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			int count = list.Count;
			TcpConnectionInformation[] array = new TcpConnectionInformation[count + list2.Count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i].TcpInfo;
			}
			for (int j = 0; j < list2.Count; j++)
			{
				array[count + j] = list2[j].TcpInfo;
			}
			return array;
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000CAAB0 File Offset: 0x000C8CB0
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			List<IPEndPoint> list3 = new List<IPEndPoint>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (this.IsListenerState(list[i].State))
				{
					list3.Add(list[i].LocalEndPoint);
				}
				i++;
			}
			int j = 0;
			int count2 = list2.Count;
			while (j < count2)
			{
				if (this.IsListenerState(list2[j].State))
				{
					list3.Add(list2[j].LocalEndPoint);
				}
				j++;
			}
			return list3.ToArray();
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000CAB54 File Offset: 0x000C8D54
		public unsafe override IPEndPoint[] GetActiveUdpListeners()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			int num = 0;
			Win32IPGlobalProperties.GetUdpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetUdpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_UDPROW win32_MIB_UDPROW = new Win32IPGlobalProperties.Win32_MIB_UDPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_UDPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_UDPROW);
					list.Add(win32_MIB_UDPROW.LocalEndPoint);
				}
			}
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetUdp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetUdp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_UDP6ROW win32_MIB_UDP6ROW = new Win32IPGlobalProperties.Win32_MIB_UDP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_UDP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_UDP6ROW);
						list.Add(win32_MIB_UDP6ROW.LocalEndPoint);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000CACA8 File Offset: 0x000C8EA8
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIBICMPINFO info;
			Win32IPGlobalProperties.GetIcmpStatistics(out info, 2);
			return new Win32IcmpV4Statistics(info);
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000CACD4 File Offset: 0x000C8ED4
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_ICMP_EX info;
			Win32IPGlobalProperties.GetIcmpStatisticsEx(out info, 23);
			return new Win32IcmpV6Statistics(info);
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000CAD00 File Offset: 0x000C8F00
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS info;
			Win32IPGlobalProperties.GetIpStatisticsEx(out info, 2);
			return new Win32IPGlobalStatistics(info);
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000CAD2C File Offset: 0x000C8F2C
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS info;
			Win32IPGlobalProperties.GetIpStatisticsEx(out info, 23);
			return new Win32IPGlobalStatistics(info);
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000CAD58 File Offset: 0x000C8F58
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS info;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out info, 2);
			return new Win32TcpStatistics(info);
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000CAD84 File Offset: 0x000C8F84
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS info;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out info, 23);
			return new Win32TcpStatistics(info);
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000CADB0 File Offset: 0x000C8FB0
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS info;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out info, 2);
			return new Win32UdpStatistics(info);
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000CADDC File Offset: 0x000C8FDC
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS info;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out info, 23);
			return new Win32UdpStatistics(info);
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000CAE06 File Offset: 0x000C9006
		public override string DhcpScopeName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.ScopeId;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000CAE12 File Offset: 0x000C9012
		public override string DomainName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.DomainName;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000CAE1E File Offset: 0x000C901E
		public override string HostName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.HostName;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000CAE2A File Offset: 0x000C902A
		public override bool IsWinsProxy
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableProxy > 0U;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000CAE39 File Offset: 0x000C9039
		public override NetBiosNodeType NodeType
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.NodeType;
			}
		}

		// Token: 0x06003A6E RID: 14958
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpTable(byte[] pTcpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06003A6F RID: 14959
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcp6Table(byte[] TcpTable, ref int SizePointer, bool Order);

		// Token: 0x06003A70 RID: 14960
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdpTable(byte[] pUdpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06003A71 RID: 14961
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdp6Table(byte[] Udp6Table, ref int SizePointer, bool Order);

		// Token: 0x06003A72 RID: 14962
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpStatisticsEx(out Win32_MIB_TCPSTATS pStats, int dwFamily);

		// Token: 0x06003A73 RID: 14963
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdpStatisticsEx(out Win32_MIB_UDPSTATS pStats, int dwFamily);

		// Token: 0x06003A74 RID: 14964
		[DllImport("iphlpapi.dll")]
		private static extern int GetIcmpStatistics(out Win32_MIBICMPINFO pStats, int dwFamily);

		// Token: 0x06003A75 RID: 14965
		[DllImport("iphlpapi.dll")]
		private static extern int GetIcmpStatisticsEx(out Win32_MIB_ICMP_EX pStats, int dwFamily);

		// Token: 0x06003A76 RID: 14966
		[DllImport("iphlpapi.dll")]
		private static extern int GetIpStatisticsEx(out Win32_MIB_IPSTATS pStats, int dwFamily);

		// Token: 0x06003A77 RID: 14967
		[DllImport("Ws2_32.dll")]
		private static extern ushort ntohs(ushort netshort);

		// Token: 0x06003A78 RID: 14968 RVA: 0x000CAE45 File Offset: 0x000C9045
		public Win32IPGlobalProperties()
		{
		}

		// Token: 0x04002247 RID: 8775
		public const int AF_INET = 2;

		// Token: 0x04002248 RID: 8776
		public const int AF_INET6 = 23;

		// Token: 0x02000725 RID: 1829
		[StructLayout(LayoutKind.Explicit)]
		private struct Win32_IN6_ADDR
		{
			// Token: 0x04002249 RID: 8777
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Bytes;
		}

		// Token: 0x02000726 RID: 1830
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCPROW
		{
			// Token: 0x17000CBF RID: 3263
			// (get) Token: 0x06003A79 RID: 14969 RVA: 0x000CAE4D File Offset: 0x000C904D
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x17000CC0 RID: 3264
			// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000CAE67 File Offset: 0x000C9067
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.RemoteAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.RemotePort));
				}
			}

			// Token: 0x17000CC1 RID: 3265
			// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000CAE81 File Offset: 0x000C9081
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new SystemTcpConnectionInformation(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x06003A7C RID: 14972 RVA: 0x0000219B File Offset: 0x0000039B
			public Win32_MIB_TCPROW()
			{
			}

			// Token: 0x0400224A RID: 8778
			public TcpState State;

			// Token: 0x0400224B RID: 8779
			public uint LocalAddr;

			// Token: 0x0400224C RID: 8780
			public uint LocalPort;

			// Token: 0x0400224D RID: 8781
			public uint RemoteAddr;

			// Token: 0x0400224E RID: 8782
			public uint RemotePort;
		}

		// Token: 0x02000727 RID: 1831
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCP6ROW
		{
			// Token: 0x17000CC2 RID: 3266
			// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000CAE9A File Offset: 0x000C909A
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x17000CC3 RID: 3267
			// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000CAEC4 File Offset: 0x000C90C4
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.RemoteAddr.Bytes, (long)((ulong)this.RemoteScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.RemotePort));
				}
			}

			// Token: 0x17000CC4 RID: 3268
			// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000CAEEE File Offset: 0x000C90EE
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new SystemTcpConnectionInformation(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x06003A80 RID: 14976 RVA: 0x0000219B File Offset: 0x0000039B
			public Win32_MIB_TCP6ROW()
			{
			}

			// Token: 0x0400224F RID: 8783
			public TcpState State;

			// Token: 0x04002250 RID: 8784
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04002251 RID: 8785
			public uint LocalScopeId;

			// Token: 0x04002252 RID: 8786
			public uint LocalPort;

			// Token: 0x04002253 RID: 8787
			public Win32IPGlobalProperties.Win32_IN6_ADDR RemoteAddr;

			// Token: 0x04002254 RID: 8788
			public uint RemoteScopeId;

			// Token: 0x04002255 RID: 8789
			public uint RemotePort;
		}

		// Token: 0x02000728 RID: 1832
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDPROW
		{
			// Token: 0x17000CC5 RID: 3269
			// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000CAF07 File Offset: 0x000C9107
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x06003A82 RID: 14978 RVA: 0x0000219B File Offset: 0x0000039B
			public Win32_MIB_UDPROW()
			{
			}

			// Token: 0x04002256 RID: 8790
			public uint LocalAddr;

			// Token: 0x04002257 RID: 8791
			public uint LocalPort;
		}

		// Token: 0x02000729 RID: 1833
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDP6ROW
		{
			// Token: 0x17000CC6 RID: 3270
			// (get) Token: 0x06003A83 RID: 14979 RVA: 0x000CAF21 File Offset: 0x000C9121
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x06003A84 RID: 14980 RVA: 0x0000219B File Offset: 0x0000039B
			public Win32_MIB_UDP6ROW()
			{
			}

			// Token: 0x04002258 RID: 8792
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04002259 RID: 8793
			public uint LocalScopeId;

			// Token: 0x0400225A RID: 8794
			public uint LocalPort;
		}
	}
}
