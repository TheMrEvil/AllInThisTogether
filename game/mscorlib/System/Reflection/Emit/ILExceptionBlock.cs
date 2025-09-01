using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000929 RID: 2345
	internal struct ILExceptionBlock
	{
		// Token: 0x06005091 RID: 20625 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void Debug()
		{
		}

		// Token: 0x0400317F RID: 12671
		public const int CATCH = 0;

		// Token: 0x04003180 RID: 12672
		public const int FILTER = 1;

		// Token: 0x04003181 RID: 12673
		public const int FINALLY = 2;

		// Token: 0x04003182 RID: 12674
		public const int FAULT = 4;

		// Token: 0x04003183 RID: 12675
		public const int FILTER_START = -1;

		// Token: 0x04003184 RID: 12676
		internal Type extype;

		// Token: 0x04003185 RID: 12677
		internal int type;

		// Token: 0x04003186 RID: 12678
		internal int start;

		// Token: 0x04003187 RID: 12679
		internal int len;

		// Token: 0x04003188 RID: 12680
		internal int filter_offset;
	}
}
