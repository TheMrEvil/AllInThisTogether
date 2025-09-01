using System;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x0200031F RID: 799
	internal abstract class SqlStreamChars : INullable, IDisposable
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060025F0 RID: 9712
		public abstract bool IsNull { get; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060025F1 RID: 9713
		public abstract long Length { get; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060025F2 RID: 9714
		// (set) Token: 0x060025F3 RID: 9715
		public abstract long Position { get; set; }

		// Token: 0x060025F4 RID: 9716
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x060025F5 RID: 9717
		public abstract void Write(char[] buffer, int offset, int count);

		// Token: 0x060025F6 RID: 9718
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x060025F7 RID: 9719
		public abstract void SetLength(long value);

		// Token: 0x060025F8 RID: 9720 RVA: 0x000A969E File Offset: 0x000A789E
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00003D93 File Offset: 0x00001F93
		protected SqlStreamChars()
		{
		}
	}
}
