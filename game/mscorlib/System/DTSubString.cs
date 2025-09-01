using System;

namespace System
{
	// Token: 0x02000127 RID: 295
	internal ref struct DTSubString
	{
		// Token: 0x170000E0 RID: 224
		internal unsafe char this[int relativeIndex]
		{
			get
			{
				return (char)(*this.s[this.index + relativeIndex]);
			}
		}

		// Token: 0x0400114D RID: 4429
		internal ReadOnlySpan<char> s;

		// Token: 0x0400114E RID: 4430
		internal int index;

		// Token: 0x0400114F RID: 4431
		internal int length;

		// Token: 0x04001150 RID: 4432
		internal DTSubStringType type;

		// Token: 0x04001151 RID: 4433
		internal int value;
	}
}
