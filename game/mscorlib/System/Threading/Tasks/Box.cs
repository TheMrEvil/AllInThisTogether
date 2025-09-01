using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200031B RID: 795
	internal class Box<T>
	{
		// Token: 0x060021DC RID: 8668 RVA: 0x00079347 File Offset: 0x00077547
		internal Box(T value)
		{
			this.Value = value;
		}

		// Token: 0x04001BE8 RID: 7144
		internal T Value;
	}
}
