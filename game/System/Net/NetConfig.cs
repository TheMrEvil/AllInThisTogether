using System;

namespace System.Net
{
	// Token: 0x020006A9 RID: 1705
	internal class NetConfig : ICloneable
	{
		// Token: 0x06003682 RID: 13954 RVA: 0x000BF7AE File Offset: 0x000BD9AE
		internal NetConfig()
		{
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000BF7BE File Offset: 0x000BD9BE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04001FD4 RID: 8148
		internal bool ipv6Enabled;

		// Token: 0x04001FD5 RID: 8149
		internal int MaxResponseHeadersLength = 64;
	}
}
