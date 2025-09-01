using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000043 RID: 67
	[Flags]
	internal enum HttpHeaderKind
	{
		// Token: 0x040000FE RID: 254
		None = 0,
		// Token: 0x040000FF RID: 255
		Request = 1,
		// Token: 0x04000100 RID: 256
		Response = 2,
		// Token: 0x04000101 RID: 257
		Content = 4
	}
}
