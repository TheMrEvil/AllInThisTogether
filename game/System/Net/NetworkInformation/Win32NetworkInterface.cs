using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073C RID: 1852
	internal class Win32NetworkInterface
	{
		// Token: 0x06003B1D RID: 15133
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetNetworkParams(IntPtr ptr, ref int size);

		// Token: 0x06003B1E RID: 15134
		[DllImport("kernel32.dll", SetLastError = true)]
		private unsafe static extern int MultiByteToWideChar(uint CodePage, uint dwFlags, byte* lpMultiByteStr, int cbMultiByte, char* lpWideCharStr, int cchWideChar);

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000CBCB8 File Offset: 0x000C9EB8
		public unsafe static Win32_FIXED_INFO FixedInfo
		{
			get
			{
				if (!Win32NetworkInterface.initialized)
				{
					int cb = 0;
					Win32NetworkInterface.GetNetworkParams(IntPtr.Zero, ref cb);
					IntPtr ptr = Marshal.AllocHGlobal(cb);
					Win32NetworkInterface.GetNetworkParams(ptr, ref cb);
					Win32_FIXED_INFO_Marshal win32_FIXED_INFO_Marshal = Marshal.PtrToStructure<Win32_FIXED_INFO_Marshal>(ptr);
					Win32NetworkInterface.fixedInfo = new Win32_FIXED_INFO
					{
						HostName = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.HostName.FixedElementField),
						DomainName = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.DomainName.FixedElementField),
						CurrentDnsServer = win32_FIXED_INFO_Marshal.CurrentDnsServer,
						DnsServerList = win32_FIXED_INFO_Marshal.DnsServerList,
						NodeType = win32_FIXED_INFO_Marshal.NodeType,
						ScopeId = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.ScopeId.FixedElementField),
						EnableRouting = win32_FIXED_INFO_Marshal.EnableRouting,
						EnableProxy = win32_FIXED_INFO_Marshal.EnableProxy,
						EnableDns = win32_FIXED_INFO_Marshal.EnableDns
					};
					Win32NetworkInterface.initialized = true;
				}
				return Win32NetworkInterface.fixedInfo;
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x0000219B File Offset: 0x0000039B
		public Win32NetworkInterface()
		{
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000CBDA8 File Offset: 0x000C9FA8
		[CompilerGenerated]
		internal unsafe static string <get_FixedInfo>g__GetStringFromMultiByte|5_0(byte* bytes)
		{
			int num = Win32NetworkInterface.MultiByteToWideChar(0U, 0U, bytes, -1, null, 0);
			if (num == 0)
			{
				return string.Empty;
			}
			char[] array2;
			char[] array = array2 = new char[num];
			char* lpWideCharStr;
			if (array == null || array2.Length == 0)
			{
				lpWideCharStr = null;
			}
			else
			{
				lpWideCharStr = &array2[0];
			}
			Win32NetworkInterface.MultiByteToWideChar(0U, 0U, bytes, -1, lpWideCharStr, num);
			array2 = null;
			return new string(array);
		}

		// Token: 0x040022AC RID: 8876
		private static Win32_FIXED_INFO fixedInfo;

		// Token: 0x040022AD RID: 8877
		private static bool initialized;
	}
}
