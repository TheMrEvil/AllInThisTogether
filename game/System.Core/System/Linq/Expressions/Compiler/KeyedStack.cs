using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002B7 RID: 695
	internal sealed class KeyedStack<TKey, TValue> where TValue : class
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x00040E08 File Offset: 0x0003F008
		internal void Push(TKey key, TValue value)
		{
			Stack<TValue> stack;
			if (!this._data.TryGetValue(key, out stack))
			{
				this._data.Add(key, stack = new Stack<TValue>());
			}
			stack.Push(value);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00040E40 File Offset: 0x0003F040
		internal TValue TryPop(TKey key)
		{
			Stack<TValue> stack;
			TValue result;
			if (!this._data.TryGetValue(key, out stack) || !stack.TryPop(out result))
			{
				return default(TValue);
			}
			return result;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00040E72 File Offset: 0x0003F072
		public KeyedStack()
		{
		}

		// Token: 0x04000AC4 RID: 2756
		private readonly Dictionary<TKey, Stack<TValue>> _data = new Dictionary<TKey, Stack<TValue>>();
	}
}
