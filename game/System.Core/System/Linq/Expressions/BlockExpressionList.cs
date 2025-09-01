using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000238 RID: 568
	internal class BlockExpressionList : IList<Expression>, ICollection<Expression>, IEnumerable<Expression>, IEnumerable
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x00035454 File Offset: 0x00033654
		internal BlockExpressionList(BlockExpression provider, Expression arg0)
		{
			this._block = provider;
			this._arg0 = arg0;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003546C File Offset: 0x0003366C
		public int IndexOf(Expression item)
		{
			if (this._arg0 == item)
			{
				return 0;
			}
			for (int i = 1; i < this._block.ExpressionCount; i++)
			{
				if (this._block.GetExpression(i) == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void Insert(int index, Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x17000287 RID: 647
		public Expression this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this._arg0;
				}
				return this._block.GetExpression(index);
			}
			[ExcludeFromCodeCoverage]
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void Add(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000354C4 File Offset: 0x000336C4
		public bool Contains(Expression item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000354D4 File Offset: 0x000336D4
		public void CopyTo(Expression[] array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			int expressionCount = this._block.ExpressionCount;
			if (index + expressionCount > array.Length)
			{
				throw new ArgumentException();
			}
			array[index++] = this._arg0;
			for (int i = 1; i < expressionCount; i++)
			{
				array[index++] = this._block.GetExpression(i);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00035543 File Offset: 0x00033743
		public int Count
		{
			get
			{
				return this._block.ExpressionCount;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public bool IsReadOnly
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public bool Remove(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00035550 File Offset: 0x00033750
		public IEnumerator<Expression> GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._block.ExpressionCount; i = num + 1)
			{
				yield return this._block.GetExpression(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003555F File Offset: 0x0003375F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000954 RID: 2388
		private readonly BlockExpression _block;

		// Token: 0x04000955 RID: 2389
		private readonly Expression _arg0;

		// Token: 0x02000239 RID: 569
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__18 : IEnumerator<Expression>, IDisposable, IEnumerator
		{
			// Token: 0x06000F9C RID: 3996 RVA: 0x00035567 File Offset: 0x00033767
			[DebuggerHidden]
			public <GetEnumerator>d__18(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000F9D RID: 3997 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x00035578 File Offset: 0x00033778
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				BlockExpressionList blockExpressionList = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = blockExpressionList._arg0;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					i = 1;
					break;
				case 2:
				{
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
					break;
				}
				default:
					return false;
				}
				if (i >= blockExpressionList._block.ExpressionCount)
				{
					return false;
				}
				this.<>2__current = blockExpressionList._block.GetExpression(i);
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0003561E File Offset: 0x0003381E
			Expression IEnumerator<Expression>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000FA0 RID: 4000 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003561E File Offset: 0x0003381E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000956 RID: 2390
			private int <>1__state;

			// Token: 0x04000957 RID: 2391
			private Expression <>2__current;

			// Token: 0x04000958 RID: 2392
			public BlockExpressionList <>4__this;

			// Token: 0x04000959 RID: 2393
			private int <i>5__2;
		}
	}
}
