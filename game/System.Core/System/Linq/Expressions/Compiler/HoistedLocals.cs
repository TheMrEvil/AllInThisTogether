using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002B5 RID: 693
	internal sealed class HoistedLocals
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x0003F9B4 File Offset: 0x0003DBB4
		internal HoistedLocals(HoistedLocals parent, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (parent != null)
			{
				vars = vars.AddFirst(parent.SelfVariable);
			}
			Dictionary<Expression, int> dictionary = new Dictionary<Expression, int>(vars.Count);
			for (int i = 0; i < vars.Count; i++)
			{
				dictionary.Add(vars[i], i);
			}
			this.SelfVariable = Expression.Variable(typeof(object[]), null);
			this.Parent = parent;
			this.Variables = vars;
			this.Indexes = new ReadOnlyDictionary<Expression, int>(dictionary);
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0003FA33 File Offset: 0x0003DC33
		internal ParameterExpression ParentVariable
		{
			get
			{
				HoistedLocals parent = this.Parent;
				if (parent == null)
				{
					return null;
				}
				return parent.SelfVariable;
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0003FA46 File Offset: 0x0003DC46
		internal static object[] GetParent(object[] locals)
		{
			return ((StrongBox<object[]>)locals[0]).Value;
		}

		// Token: 0x04000AC0 RID: 2752
		internal readonly HoistedLocals Parent;

		// Token: 0x04000AC1 RID: 2753
		internal readonly ReadOnlyDictionary<Expression, int> Indexes;

		// Token: 0x04000AC2 RID: 2754
		internal readonly ReadOnlyCollection<ParameterExpression> Variables;

		// Token: 0x04000AC3 RID: 2755
		internal readonly ParameterExpression SelfVariable;
	}
}
