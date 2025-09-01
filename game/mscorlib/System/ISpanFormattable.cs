using System;

namespace System
{
	// Token: 0x02000144 RID: 324
	internal interface ISpanFormattable
	{
		// Token: 0x06000C06 RID: 3078
		bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider);
	}
}
