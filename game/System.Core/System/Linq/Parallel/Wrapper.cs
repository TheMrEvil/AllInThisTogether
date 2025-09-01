using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000205 RID: 517
	internal struct Wrapper<T>
	{
		// Token: 0x06000C80 RID: 3200 RVA: 0x0002BD4A File Offset: 0x00029F4A
		internal Wrapper(T value)
		{
			this.Value = value;
		}

		// Token: 0x040008D2 RID: 2258
		internal T Value;
	}
}
