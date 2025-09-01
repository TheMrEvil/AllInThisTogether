using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Parallel
{
	// Token: 0x020001F2 RID: 498
	internal class ListChunk<TInputOutput> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x0002ADE2 File Offset: 0x00028FE2
		internal ListChunk(int size)
		{
			this._chunk = new TInputOutput[size];
			this._chunkCount = 0;
			this._tailChunk = this;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002AE04 File Offset: 0x00029004
		internal void Add(TInputOutput e)
		{
			ListChunk<TInputOutput> listChunk = this._tailChunk;
			if (listChunk._chunkCount == listChunk._chunk.Length)
			{
				this._tailChunk = new ListChunk<TInputOutput>(listChunk._chunkCount * 2);
				listChunk = (listChunk._nextChunk = this._tailChunk);
			}
			TInputOutput[] chunk = listChunk._chunk;
			ListChunk<TInputOutput> listChunk2 = listChunk;
			int chunkCount = listChunk2._chunkCount;
			listChunk2._chunkCount = chunkCount + 1;
			chunk[chunkCount] = e;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002AE68 File Offset: 0x00029068
		internal ListChunk<TInputOutput> Next
		{
			get
			{
				return this._nextChunk;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0002AE70 File Offset: 0x00029070
		internal int Count
		{
			get
			{
				return this._chunkCount;
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002AE78 File Offset: 0x00029078
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			for (ListChunk<TInputOutput> curr = this; curr != null; curr = curr._nextChunk)
			{
				int num;
				for (int i = 0; i < curr._chunkCount; i = num + 1)
				{
					yield return curr._chunk[i];
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0000817A File Offset: 0x0000637A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x040008A3 RID: 2211
		internal TInputOutput[] _chunk;

		// Token: 0x040008A4 RID: 2212
		private int _chunkCount;

		// Token: 0x040008A5 RID: 2213
		private ListChunk<TInputOutput> _nextChunk;

		// Token: 0x040008A6 RID: 2214
		private ListChunk<TInputOutput> _tailChunk;

		// Token: 0x020001F3 RID: 499
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__10 : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000C3D RID: 3133 RVA: 0x0002AE87 File Offset: 0x00029087
			[DebuggerHidden]
			public <GetEnumerator>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000C3F RID: 3135 RVA: 0x0002AE98 File Offset: 0x00029098
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ListChunk<TInputOutput> listChunk = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					curr = listChunk;
					goto IL_90;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				int num2 = i;
				i = num2 + 1;
				IL_6C:
				if (i < curr._chunkCount)
				{
					this.<>2__current = curr._chunk[i];
					this.<>1__state = 1;
					return true;
				}
				curr = curr._nextChunk;
				IL_90:
				if (curr == null)
				{
					return false;
				}
				i = 0;
				goto IL_6C;
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0002AF3E File Offset: 0x0002913E
			TInputOutput IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000C41 RID: 3137 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002AF46 File Offset: 0x00029146
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040008A7 RID: 2215
			private int <>1__state;

			// Token: 0x040008A8 RID: 2216
			private TInputOutput <>2__current;

			// Token: 0x040008A9 RID: 2217
			public ListChunk<TInputOutput> <>4__this;

			// Token: 0x040008AA RID: 2218
			private ListChunk<TInputOutput> <curr>5__2;

			// Token: 0x040008AB RID: 2219
			private int <i>5__3;
		}
	}
}
