using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001FB RID: 507
	internal class Shared<T>
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x0002B3AF File Offset: 0x000295AF
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x040008C2 RID: 2242
		internal T Value;
	}
}
