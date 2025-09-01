using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Containers
{
	// Token: 0x0200006A RID: 106
	public struct StringContainer : IReadOnlyList<char>, IEnumerable<char>, IEnumerable, IReadOnlyCollection<char>
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000A290 File Offset: 0x00008490
		public StringContainer(string str)
		{
			this._str = str;
		}

		// Token: 0x1700004D RID: 77
		public char this[int index]
		{
			get
			{
				return this._str[index];
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A2A7 File Offset: 0x000084A7
		public int Count
		{
			get
			{
				return this._str.Length;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public IEnumerator<char> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this._str.Length; i = num + 1)
			{
				yield return this._str[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A2C8 File Offset: 0x000084C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this._str.Length; i = num + 1)
			{
				yield return this._str[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A2DC File Offset: 0x000084DC
		public static implicit operator StringContainer(string str)
		{
			return new StringContainer(str);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public static implicit operator string(StringContainer str)
		{
			return str._str;
		}

		// Token: 0x0400014A RID: 330
		private readonly string _str;

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__6 : IEnumerator<char>, IEnumerator, IDisposable
		{
			// Token: 0x0600037E RID: 894 RVA: 0x0000C95E File Offset: 0x0000AB5E
			[DebuggerHidden]
			public <GetEnumerator>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000C96D File Offset: 0x0000AB6D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000C970 File Offset: 0x0000AB70
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
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= this._str.Length)
				{
					return false;
				}
				this.<>2__current = this._str[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C9F2 File Offset: 0x0000ABF2
			char IEnumerator<char>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000C9FA File Offset: 0x0000ABFA
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CA01 File Offset: 0x0000AC01
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000245 RID: 581
			private int <>1__state;

			// Token: 0x04000246 RID: 582
			private char <>2__current;

			// Token: 0x04000247 RID: 583
			public StringContainer <>4__this;

			// Token: 0x04000248 RID: 584
			private int <i>5__2;
		}

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		private sealed class <System-Collections-IEnumerable-GetEnumerator>d__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000384 RID: 900 RVA: 0x0000CA0E File Offset: 0x0000AC0E
			[DebuggerHidden]
			public <System-Collections-IEnumerable-GetEnumerator>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000CA1D File Offset: 0x0000AC1D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000CA20 File Offset: 0x0000AC20
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
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= this._str.Length)
				{
					return false;
				}
				this.<>2__current = this._str[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000387 RID: 903 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000388 RID: 904 RVA: 0x0000CAAF File Offset: 0x0000ACAF
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000389 RID: 905 RVA: 0x0000CAB6 File Offset: 0x0000ACB6
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000249 RID: 585
			private int <>1__state;

			// Token: 0x0400024A RID: 586
			private object <>2__current;

			// Token: 0x0400024B RID: 587
			public StringContainer <>4__this;

			// Token: 0x0400024C RID: 588
			private int <i>5__2;
		}
	}
}
