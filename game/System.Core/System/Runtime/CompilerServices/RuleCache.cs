using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents a cache of runtime binding rules.</summary>
	/// <typeparam name="T">The delegate type.</typeparam>
	// Token: 0x020002E7 RID: 743
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RuleCache<T> where T : class
	{
		// Token: 0x060016A2 RID: 5794 RVA: 0x0004C594 File Offset: 0x0004A794
		internal RuleCache()
		{
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0004C5B2 File Offset: 0x0004A7B2
		internal T[] GetRules()
		{
			return this._rules;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0004C5BC File Offset: 0x0004A7BC
		internal void MoveRule(T rule, int i)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				int num = this._rules.Length - i;
				if (num > 8)
				{
					num = 8;
				}
				int num2 = -1;
				int num3 = Math.Min(this._rules.Length, i + num);
				for (int j = i; j < num3; j++)
				{
					if (this._rules[j] == rule)
					{
						num2 = j;
						break;
					}
				}
				if (num2 >= 2)
				{
					T t = this._rules[num2];
					this._rules[num2] = this._rules[num2 - 1];
					this._rules[num2 - 1] = this._rules[num2 - 2];
					this._rules[num2 - 2] = t;
				}
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0004C6A8 File Offset: 0x0004A8A8
		internal void AddRule(T newRule)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0004C6F4 File Offset: 0x0004A8F4
		internal void ReplaceRule(T oldRule, T newRule)
		{
			object cacheLock = this._cacheLock;
			lock (cacheLock)
			{
				int num = Array.IndexOf<T>(this._rules, oldRule);
				if (num >= 0)
				{
					this._rules[num] = newRule;
				}
				else
				{
					this._rules = RuleCache<T>.AddOrInsert(this._rules, newRule);
				}
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0004C760 File Offset: 0x0004A960
		private static T[] AddOrInsert(T[] rules, T item)
		{
			if (rules.Length < 64)
			{
				return rules.AddLast(item);
			}
			int num = rules.Length + 1;
			T[] array;
			if (num > 128)
			{
				num = 128;
				array = rules;
			}
			else
			{
				array = new T[num];
				Array.Copy(rules, 0, array, 0, 64);
			}
			array[64] = item;
			Array.Copy(rules, 64, array, 65, num - 64 - 1);
			return array;
		}

		// Token: 0x04000B5E RID: 2910
		private T[] _rules = Array.Empty<T>();

		// Token: 0x04000B5F RID: 2911
		private readonly object _cacheLock = new object();

		// Token: 0x04000B60 RID: 2912
		private const int MaxRules = 128;

		// Token: 0x04000B61 RID: 2913
		private const int InsertPosition = 64;
	}
}
