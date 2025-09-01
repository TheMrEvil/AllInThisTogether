using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000178 RID: 376
	public struct MatchMakingKeyValuePair_t
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x0000C227 File Offset: 0x0000A427
		private MatchMakingKeyValuePair_t(string strKey, string strValue)
		{
			this.m_szKey = strKey;
			this.m_szValue = strValue;
		}

		// Token: 0x04000A09 RID: 2569
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szKey;

		// Token: 0x04000A0A RID: 2570
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szValue;
	}
}
