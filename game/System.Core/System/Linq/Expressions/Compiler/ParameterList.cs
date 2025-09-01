using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002B1 RID: 689
	internal sealed class ParameterList : IReadOnlyList<ParameterExpression>, IReadOnlyCollection<ParameterExpression>, IEnumerable<ParameterExpression>, IEnumerable
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x0003F180 File Offset: 0x0003D380
		public ParameterList(IParameterProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x170003B4 RID: 948
		public ParameterExpression this[int index]
		{
			get
			{
				return this._provider.GetParameter(index);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0003F19D File Offset: 0x0003D39D
		public int Count
		{
			get
			{
				return this._provider.ParameterCount;
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0003F1AA File Offset: 0x0003D3AA
		public IEnumerator<ParameterExpression> GetEnumerator()
		{
			int i = 0;
			int j = this._provider.ParameterCount;
			while (i < j)
			{
				yield return this._provider.GetParameter(i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0003F1B9 File Offset: 0x0003D3B9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000AB2 RID: 2738
		private readonly IParameterProvider _provider;

		// Token: 0x020002B2 RID: 690
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__6 : IEnumerator<ParameterExpression>, IDisposable, IEnumerator
		{
			// Token: 0x06001475 RID: 5237 RVA: 0x0003F1C1 File Offset: 0x0003D3C1
			[DebuggerHidden]
			public <GetEnumerator>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001476 RID: 5238 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x0003F1D0 File Offset: 0x0003D3D0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ParameterList parameterList = this;
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
					j = parameterList._provider.ParameterCount;
				}
				if (i >= j)
				{
					return false;
				}
				this.<>2__current = parameterList._provider.GetParameter(i);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06001478 RID: 5240 RVA: 0x0003F25B File Offset: 0x0003D45B
			ParameterExpression IEnumerator<ParameterExpression>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001479 RID: 5241 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x0600147A RID: 5242 RVA: 0x0003F25B File Offset: 0x0003D45B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000AB3 RID: 2739
			private int <>1__state;

			// Token: 0x04000AB4 RID: 2740
			private ParameterExpression <>2__current;

			// Token: 0x04000AB5 RID: 2741
			public ParameterList <>4__this;

			// Token: 0x04000AB6 RID: 2742
			private int <i>5__2;

			// Token: 0x04000AB7 RID: 2743
			private int <n>5__3;
		}
	}
}
