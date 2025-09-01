using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074F RID: 1871
	internal struct Win32_SOCKET_ADDRESS
	{
		// Token: 0x06003B28 RID: 15144 RVA: 0x000CBE4C File Offset: 0x000CA04C
		public IPAddress GetIPAddress()
		{
			Win32_SOCKADDR win32_SOCKADDR = (Win32_SOCKADDR)Marshal.PtrToStructure(this.Sockaddr, typeof(Win32_SOCKADDR));
			byte[] array;
			if (win32_SOCKADDR.AddressFamily == 23)
			{
				array = new byte[16];
				Array.Copy(win32_SOCKADDR.AddressData, 6, array, 0, 16);
			}
			else
			{
				array = new byte[4];
				Array.Copy(win32_SOCKADDR.AddressData, 2, array, 0, 4);
			}
			return new IPAddress(array);
		}

		// Token: 0x04002348 RID: 9032
		public IntPtr Sockaddr;

		// Token: 0x04002349 RID: 9033
		public int SockaddrLength;

		// Token: 0x0400234A RID: 9034
		private const int AF_INET6 = 23;
	}
}
