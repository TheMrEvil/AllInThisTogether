using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x0200017F RID: 383
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x0003D931 File Offset: 0x0003BB31
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003D946 File Offset: 0x0003BB46
		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0003D95B File Offset: 0x0003BB5B
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x040012E7 RID: 4839
		private readonly T[] _array;
	}
}
