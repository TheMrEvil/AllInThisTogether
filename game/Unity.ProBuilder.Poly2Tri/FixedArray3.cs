using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200001E RID: 30
	internal struct FixedArray3<T> : IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x17000028 RID: 40
		public T this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this._0;
				case 1:
					return this._1;
				case 2:
					return this._2;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this._0 = value;
					return;
				case 1:
					this._1 = value;
					return;
				case 2:
					this._2 = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005228 File Offset: 0x00003428
		public bool Contains(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005258 File Offset: 0x00003458
		public int IndexOf(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005288 File Offset: 0x00003488
		public void Clear()
		{
			this._0 = (this._1 = (this._2 = default(T)));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000052B8 File Offset: 0x000034B8
		public void Clear(T value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					this[i] = default(T);
				}
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000052F5 File Offset: 0x000034F5
		private IEnumerable<T> Enumerate()
		{
			int num;
			for (int i = 0; i < 3; i = num)
			{
				yield return this[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000530A File Offset: 0x0000350A
		public IEnumerator<T> GetEnumerator()
		{
			return this.Enumerate().GetEnumerator();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005317 File Offset: 0x00003517
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000050 RID: 80
		public T _0;

		// Token: 0x04000051 RID: 81
		public T _1;

		// Token: 0x04000052 RID: 82
		public T _2;

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		private sealed class <Enumerate>d__10 : IEnumerable<T>, IEnumerable, IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060000F5 RID: 245 RVA: 0x00005479 File Offset: 0x00003679
			[DebuggerHidden]
			public <Enumerate>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x00005493 File Offset: 0x00003693
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x00005498 File Offset: 0x00003698
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i + 1;
					i = num2;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= 3)
				{
					return false;
				}
				this.<>2__current = base[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005506 File Offset: 0x00003706
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x0000550E File Offset: 0x0000370E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000FA RID: 250 RVA: 0x00005515 File Offset: 0x00003715
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00005524 File Offset: 0x00003724
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				FixedArray3<T>.<Enumerate>d__10 <Enumerate>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Enumerate>d__ = this;
				}
				else
				{
					<Enumerate>d__ = new FixedArray3<T>.<Enumerate>d__10(0);
				}
				<Enumerate>d__.<>4__this = ref this;
				return <Enumerate>d__;
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00005567 File Offset: 0x00003767
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000057 RID: 87
			private int <>1__state;

			// Token: 0x04000058 RID: 88
			private T <>2__current;

			// Token: 0x04000059 RID: 89
			private int <>l__initialThreadId;

			// Token: 0x0400005A RID: 90
			public FixedArray3<T> <>4__this;

			// Token: 0x0400005B RID: 91
			public FixedArray3<T> <>3__<>4__this;

			// Token: 0x0400005C RID: 92
			private int <i>5__2;
		}
	}
}
