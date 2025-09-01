using System;

namespace System.Buffers
{
	// Token: 0x02000AD0 RID: 2768
	// (Invoke) Token: 0x060062CA RID: 25290
	public delegate void ReadOnlySpanAction<T, in TArg>(ReadOnlySpan<T> span, TArg arg);
}
