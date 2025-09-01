using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	// Token: 0x0200001F RID: 31
	internal struct FixedBitArray3 : IEnumerable<bool>, IEnumerable
	{
		// Token: 0x17000029 RID: 41
		public bool this[int index]
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

		// Token: 0x060000EC RID: 236 RVA: 0x00005384 File Offset: 0x00003584
		public bool Contains(bool value)
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

		// Token: 0x060000ED RID: 237 RVA: 0x000053AC File Offset: 0x000035AC
		public int IndexOf(bool value)
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

		// Token: 0x060000EE RID: 238 RVA: 0x000053D4 File Offset: 0x000035D4
		public void Clear()
		{
			this._0 = (this._1 = (this._2 = false));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000053FC File Offset: 0x000035FC
		public void Clear(bool value)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this[i] == value)
				{
					this[i] = false;
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005427 File Offset: 0x00003627
		private IEnumerable<bool> Enumerate()
		{
			int num;
			for (int i = 0; i < 3; i = num)
			{
				yield return this[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000543C File Offset: 0x0000363C
		public IEnumerator<bool> GetEnumerator()
		{
			return this.Enumerate().GetEnumerator();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005449 File Offset: 0x00003649
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000053 RID: 83
		public bool _0;

		// Token: 0x04000054 RID: 84
		public bool _1;

		// Token: 0x04000055 RID: 85
		public bool _2;

		// Token: 0x02000022 RID: 34
		[CompilerGenerated]
		private sealed class <Enumerate>d__10 : IEnumerable<bool>, IEnumerable, IEnumerator<bool>, IEnumerator, IDisposable
		{
			// Token: 0x060000FD RID: 253 RVA: 0x0000556F File Offset: 0x0000376F
			[DebuggerHidden]
			public <Enumerate>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00005589 File Offset: 0x00003789
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000FF RID: 255 RVA: 0x0000558C File Offset: 0x0000378C
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

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000100 RID: 256 RVA: 0x000055FA File Offset: 0x000037FA
			bool IEnumerator<bool>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00005602 File Offset: 0x00003802
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00005609 File Offset: 0x00003809
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00005618 File Offset: 0x00003818
			[DebuggerHidden]
			IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
			{
				FixedBitArray3.<Enumerate>d__10 <Enumerate>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Enumerate>d__ = this;
				}
				else
				{
					<Enumerate>d__ = new FixedBitArray3.<Enumerate>d__10(0);
				}
				<Enumerate>d__.<>4__this = ref this;
				return <Enumerate>d__;
			}

			// Token: 0x06000104 RID: 260 RVA: 0x0000565B File Offset: 0x0000385B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Boolean>.GetEnumerator();
			}

			// Token: 0x0400005D RID: 93
			private int <>1__state;

			// Token: 0x0400005E RID: 94
			private bool <>2__current;

			// Token: 0x0400005F RID: 95
			private int <>l__initialThreadId;

			// Token: 0x04000060 RID: 96
			public FixedBitArray3 <>4__this;

			// Token: 0x04000061 RID: 97
			public FixedBitArray3 <>3__<>4__this;

			// Token: 0x04000062 RID: 98
			private int <i>5__2;
		}
	}
}
