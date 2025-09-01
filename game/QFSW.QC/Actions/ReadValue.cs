using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Actions
{
	// Token: 0x02000075 RID: 117
	public class ReadValue<T> : Composite
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000A848 File Offset: 0x00008A48
		public ReadValue(Action<T> getValue, ResponseConfig config) : base(ReadValue<T>.Generate(getValue, config))
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A857 File Offset: 0x00008A57
		public ReadValue(Action<T> getValue) : this(getValue, ResponseConfig.Default)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A865 File Offset: 0x00008A65
		private static IEnumerator<ICommandAction> Generate(Action<T> getValue, ResponseConfig config)
		{
			string line = null;
			yield return new ReadLine(delegate(string t)
			{
				line = t;
			}, config);
			T obj = ReadValue<T>.Parser.Parse<T>(line);
			getValue(obj);
			yield break;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A87B File Offset: 0x00008A7B
		// Note: this type is marked as 'beforefieldinit'.
		static ReadValue()
		{
		}

		// Token: 0x0400015D RID: 349
		private static readonly QuantumParser Parser = new QuantumParser();

		// Token: 0x020000BD RID: 189
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000398 RID: 920 RVA: 0x0000CEBC File Offset: 0x0000B0BC
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000399 RID: 921 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
			internal void <Generate>b__0(string t)
			{
				this.line = t;
			}

			// Token: 0x0400025E RID: 606
			public string line;
		}

		// Token: 0x020000BE RID: 190
		[CompilerGenerated]
		private sealed class <Generate>d__3 : IEnumerator<ICommandAction>, IEnumerator, IDisposable
		{
			// Token: 0x0600039A RID: 922 RVA: 0x0000CECD File Offset: 0x0000B0CD
			[DebuggerHidden]
			public <Generate>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600039B RID: 923 RVA: 0x0000CEDC File Offset: 0x0000B0DC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600039C RID: 924 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					CS$<>8__locals1 = new ReadValue<T>.<>c__DisplayClass3_0();
					CS$<>8__locals1.line = null;
					this.<>2__current = new ReadLine(new Action<string>(CS$<>8__locals1.<Generate>b__0), config);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				T obj = ReadValue<T>.Parser.Parse<T>(CS$<>8__locals1.line);
				getValue(obj);
				return false;
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x0600039D RID: 925 RVA: 0x0000CF70 File Offset: 0x0000B170
			ICommandAction IEnumerator<ICommandAction>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600039E RID: 926 RVA: 0x0000CF78 File Offset: 0x0000B178
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600039F RID: 927 RVA: 0x0000CF7F File Offset: 0x0000B17F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400025F RID: 607
			private int <>1__state;

			// Token: 0x04000260 RID: 608
			private ICommandAction <>2__current;

			// Token: 0x04000261 RID: 609
			public ResponseConfig config;

			// Token: 0x04000262 RID: 610
			private ReadValue<T>.<>c__DisplayClass3_0 <>8__1;

			// Token: 0x04000263 RID: 611
			public Action<T> getValue;
		}
	}
}
