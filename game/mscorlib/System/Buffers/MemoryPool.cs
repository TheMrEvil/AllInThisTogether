using System;

namespace System.Buffers
{
	// Token: 0x02000AE3 RID: 2787
	public abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06006319 RID: 25369 RVA: 0x0014B659 File Offset: 0x00149859
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x0600631A RID: 25370
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600631B RID: 25371
		public abstract int MaxBufferSize { get; }

		// Token: 0x0600631C RID: 25372 RVA: 0x0000259F File Offset: 0x0000079F
		protected MemoryPool()
		{
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x0014B660 File Offset: 0x00149860
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600631E RID: 25374
		protected abstract void Dispose(bool disposing);

		// Token: 0x0600631F RID: 25375 RVA: 0x0014B66F File Offset: 0x0014986F
		// Note: this type is marked as 'beforefieldinit'.
		static MemoryPool()
		{
		}

		// Token: 0x04003A5B RID: 14939
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
