using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000AD9 RID: 2777
	public abstract class MemoryManager<T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060062EA RID: 25322 RVA: 0x0014ABD0 File Offset: 0x00148DD0
		public virtual Memory<T> Memory
		{
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x060062EB RID: 25323
		public abstract Span<T> GetSpan();

		// Token: 0x060062EC RID: 25324
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x060062ED RID: 25325
		public abstract void Unpin();

		// Token: 0x060062EE RID: 25326 RVA: 0x0014ABF1 File Offset: 0x00148DF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x0014ABFA File Offset: 0x00148DFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x0014AC04 File Offset: 0x00148E04
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x0014AC0E File Offset: 0x00148E0E
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060062F2 RID: 25330
		protected abstract void Dispose(bool disposing);

		// Token: 0x060062F3 RID: 25331 RVA: 0x0000259F File Offset: 0x0000079F
		protected MemoryManager()
		{
		}
	}
}
