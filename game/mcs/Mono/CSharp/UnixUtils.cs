using System;
using System.Runtime.InteropServices;

namespace Mono.CSharp
{
	// Token: 0x020002CD RID: 717
	public class UnixUtils
	{
		// Token: 0x06002260 RID: 8800
		[DllImport("libc", EntryPoint = "isatty")]
		private static extern int _isatty(int fd);

		// Token: 0x06002261 RID: 8801 RVA: 0x000A8334 File Offset: 0x000A6534
		public static bool isatty(int fd)
		{
			bool result;
			try
			{
				result = (UnixUtils._isatty(fd) == 1);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00002CCC File Offset: 0x00000ECC
		public UnixUtils()
		{
		}
	}
}
