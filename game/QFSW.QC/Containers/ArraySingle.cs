using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Containers
{
	// Token: 0x02000068 RID: 104
	public struct ArraySingle<T> : IReadOnlyList<T>, IEnumerable<!0>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000A24C File Offset: 0x0000844C
		public ArraySingle(T data)
		{
			this._data = data;
		}

		// Token: 0x1700004B RID: 75
		public T this[int index]
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A25D File Offset: 0x0000845D
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A260 File Offset: 0x00008460
		public IEnumerator<T> GetEnumerator()
		{
			yield return this._data;
			yield break;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A274 File Offset: 0x00008474
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return this._data;
			yield break;
		}

		// Token: 0x04000149 RID: 329
		private readonly T _data;

		// Token: 0x020000B5 RID: 181
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__6 : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x06000372 RID: 882 RVA: 0x0000C877 File Offset: 0x0000AA77
			[DebuggerHidden]
			public <GetEnumerator>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000373 RID: 883 RVA: 0x0000C886 File Offset: 0x0000AA86
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000374 RID: 884 RVA: 0x0000C888 File Offset: 0x0000AA88
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = this._data;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000375 RID: 885 RVA: 0x0000C8CE File Offset: 0x0000AACE
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000376 RID: 886 RVA: 0x0000C8D6 File Offset: 0x0000AAD6
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0000C8DD File Offset: 0x0000AADD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400023F RID: 575
			private int <>1__state;

			// Token: 0x04000240 RID: 576
			private T <>2__current;

			// Token: 0x04000241 RID: 577
			public ArraySingle<T> <>4__this;
		}

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class <System-Collections-IEnumerable-GetEnumerator>d__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000378 RID: 888 RVA: 0x0000C8EA File Offset: 0x0000AAEA
			[DebuggerHidden]
			public <System-Collections-IEnumerable-GetEnumerator>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000379 RID: 889 RVA: 0x0000C8F9 File Offset: 0x0000AAF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600037A RID: 890 RVA: 0x0000C8FC File Offset: 0x0000AAFC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = this._data;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x0600037B RID: 891 RVA: 0x0000C947 File Offset: 0x0000AB47
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000C94F File Offset: 0x0000AB4F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C956 File Offset: 0x0000AB56
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000242 RID: 578
			private int <>1__state;

			// Token: 0x04000243 RID: 579
			private object <>2__current;

			// Token: 0x04000244 RID: 580
			public ArraySingle<T> <>4__this;
		}
	}
}
