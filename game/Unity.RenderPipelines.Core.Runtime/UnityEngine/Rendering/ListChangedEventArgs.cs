using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005F RID: 95
	public sealed class ListChangedEventArgs<T> : EventArgs
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000EC62 File Offset: 0x0000CE62
		public ListChangedEventArgs(int index, T item)
		{
			this.index = index;
			this.item = item;
		}

		// Token: 0x04000204 RID: 516
		public readonly int index;

		// Token: 0x04000205 RID: 517
		public readonly T item;
	}
}
