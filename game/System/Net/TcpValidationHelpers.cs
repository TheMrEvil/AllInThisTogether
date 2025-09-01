using System;

namespace System.Net
{
	// Token: 0x0200056E RID: 1390
	internal static class TcpValidationHelpers
	{
		// Token: 0x06002CF4 RID: 11508 RVA: 0x0009A1C3 File Offset: 0x000983C3
		public static bool ValidatePortNumber(int port)
		{
			return port >= 0 && port <= 65535;
		}
	}
}
