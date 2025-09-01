using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;

namespace System.Runtime.CompilerServices
{
	/// <summary>Contains helper methods called from dynamically generated methods.</summary>
	// Token: 0x020002D5 RID: 725
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	public static class RuntimeOps
	{
		/// <summary>Gets the value of an item in an expando object.</summary>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <param name="value">The out parameter containing the value of the member.</param>
		/// <returns>True if the member exists in the expando object, otherwise false.</returns>
		// Token: 0x0600161A RID: 5658 RVA: 0x0004A93A File Offset: 0x00048B3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static bool ExpandoTryGetValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			return expando.TryGetValue(indexClass, index, name, ignoreCase, out value);
		}

		/// <summary>Sets the value of an item in an expando object.</summary>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="value">The value of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <returns>Returns the index for the set member.</returns>
		// Token: 0x0600161B RID: 5659 RVA: 0x0004A949 File Offset: 0x00048B49
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static object ExpandoTrySetValue(ExpandoObject expando, object indexClass, int index, object value, string name, bool ignoreCase)
		{
			expando.TrySetValue(indexClass, index, value, name, ignoreCase, false);
			return value;
		}

		/// <summary>Deletes the value of an item in an expando object.</summary>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <returns>true if the item was successfully removed; otherwise, false.</returns>
		// Token: 0x0600161C RID: 5660 RVA: 0x0004A95A File Offset: 0x00048B5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static bool ExpandoTryDeleteValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase)
		{
			return expando.TryDeleteValue(indexClass, index, name, ignoreCase, ExpandoObject.Uninitialized);
		}

		/// <summary>Checks the version of the Expando object.</summary>
		/// <param name="expando">The Expando object.</param>
		/// <param name="version">The version to check.</param>
		/// <returns>Returns true if the version is equal; otherwise, false.</returns>
		// Token: 0x0600161D RID: 5661 RVA: 0x0004A96C File Offset: 0x00048B6C
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool ExpandoCheckVersion(ExpandoObject expando, object version)
		{
			return expando.Class == version;
		}

		/// <summary>Promotes an Expando object from one class to a new class.</summary>
		/// <param name="expando">The Expando object.</param>
		/// <param name="oldClass">The old class of the Expando object.</param>
		/// <param name="newClass">The new class of the Expando object.</param>
		// Token: 0x0600161E RID: 5662 RVA: 0x0004A977 File Offset: 0x00048B77
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void ExpandoPromoteClass(ExpandoObject expando, object oldClass, object newClass)
		{
			expando.PromoteClass(oldClass, newClass);
		}

		/// <summary>Quotes the provided expression tree.</summary>
		/// <param name="expression">The expression to quote.</param>
		/// <param name="hoistedLocals">The hoisted local state provided by the compiler.</param>
		/// <param name="locals">The actual hoisted local values.</param>
		/// <returns>The quoted expression.</returns>
		// Token: 0x0600161F RID: 5663 RVA: 0x0004A981 File Offset: 0x00048B81
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static Expression Quote(Expression expression, object hoistedLocals, object[] locals)
		{
			return new RuntimeOps.ExpressionQuoter((HoistedLocals)hoistedLocals, locals).Visit(expression);
		}

		/// <summary>Combines two runtime variable lists and returns a new list.</summary>
		/// <param name="first">The first list.</param>
		/// <param name="second">The second list.</param>
		/// <param name="indexes">The index array indicating which list to get variables from.</param>
		/// <returns>The merged runtime variables.</returns>
		// Token: 0x06001620 RID: 5664 RVA: 0x0004A995 File Offset: 0x00048B95
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static IRuntimeVariables MergeRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
		{
			return new RuntimeOps.MergedRuntimeVariables(first, second, indexes);
		}

		/// <summary>Creates an interface that can be used to modify closed over variables at runtime.</summary>
		/// <param name="data">The closure array.</param>
		/// <param name="indexes">An array of indicies into the closure array where variables are found.</param>
		/// <returns>An interface to access variables.</returns>
		// Token: 0x06001621 RID: 5665 RVA: 0x0004A99F File Offset: 0x00048B9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static IRuntimeVariables CreateRuntimeVariables(object[] data, long[] indexes)
		{
			return new RuntimeOps.RuntimeVariableList(data, indexes);
		}

		/// <summary>Creates an interface that can be used to modify closed over variables at runtime.</summary>
		/// <returns>An interface to access variables.</returns>
		// Token: 0x06001622 RID: 5666 RVA: 0x0004A9A8 File Offset: 0x00048BA8
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IRuntimeVariables CreateRuntimeVariables()
		{
			return new RuntimeOps.EmptyRuntimeVariables();
		}

		// Token: 0x020002D6 RID: 726
		private sealed class ExpressionQuoter : ExpressionVisitor
		{
			// Token: 0x06001623 RID: 5667 RVA: 0x0004A9AF File Offset: 0x00048BAF
			internal ExpressionQuoter(HoistedLocals scope, object[] locals)
			{
				this._scope = scope;
				this._locals = locals;
			}

			// Token: 0x06001624 RID: 5668 RVA: 0x0004A9D0 File Offset: 0x00048BD0
			protected internal override Expression VisitLambda<T>(Expression<T> node)
			{
				if (node.ParameterCount > 0)
				{
					HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
					int i = 0;
					int parameterCount = node.ParameterCount;
					while (i < parameterCount)
					{
						hashSet.Add(node.GetParameter(i));
						i++;
					}
					this._shadowedVars.Push(hashSet);
				}
				Expression expression = this.Visit(node.Body);
				if (node.ParameterCount > 0)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body)
				{
					return node;
				}
				return node.Rewrite(expression, null);
			}

			// Token: 0x06001625 RID: 5669 RVA: 0x0004AA50 File Offset: 0x00048C50
			protected internal override Expression VisitBlock(BlockExpression node)
			{
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Push(new HashSet<ParameterExpression>(node.Variables));
				}
				Expression[] array = ExpressionVisitorUtils.VisitBlockExpressions(this, node);
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Pop();
				}
				if (array == null)
				{
					return node;
				}
				return node.Rewrite(node.Variables, array);
			}

			// Token: 0x06001626 RID: 5670 RVA: 0x0004AAB8 File Offset: 0x00048CB8
			protected override CatchBlock VisitCatchBlock(CatchBlock node)
			{
				if (node.Variable != null)
				{
					this._shadowedVars.Push(new HashSet<ParameterExpression>
					{
						node.Variable
					});
				}
				Expression expression = this.Visit(node.Body);
				Expression expression2 = this.Visit(node.Filter);
				if (node.Variable != null)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body && expression2 == node.Filter)
				{
					return node;
				}
				return Expression.MakeCatchBlock(node.Test, node.Variable, expression, expression2);
			}

			// Token: 0x06001627 RID: 5671 RVA: 0x0004AB40 File Offset: 0x00048D40
			protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
			{
				int count = node.Variables.Count;
				List<IStrongBox> list = new List<IStrongBox>();
				List<ParameterExpression> list2 = new List<ParameterExpression>();
				int[] array = new int[count];
				for (int i = 0; i < array.Length; i++)
				{
					IStrongBox box = this.GetBox(node.Variables[i]);
					if (box == null)
					{
						array[i] = list2.Count;
						list2.Add(node.Variables[i]);
					}
					else
					{
						array[i] = -1 - list.Count;
						list.Add(box);
					}
				}
				if (list.Count == 0)
				{
					return node;
				}
				ConstantExpression constantExpression = Expression.Constant(new RuntimeOps.RuntimeVariables(list.ToArray()), typeof(IRuntimeVariables));
				if (list2.Count == 0)
				{
					return constantExpression;
				}
				return Expression.Call(CachedReflectionInfo.RuntimeOps_MergeRuntimeVariables, Expression.RuntimeVariables(new TrueReadOnlyCollection<ParameterExpression>(list2.ToArray())), constantExpression, Expression.Constant(array));
			}

			// Token: 0x06001628 RID: 5672 RVA: 0x0004AC18 File Offset: 0x00048E18
			protected internal override Expression VisitParameter(ParameterExpression node)
			{
				IStrongBox box = this.GetBox(node);
				if (box == null)
				{
					return node;
				}
				return Expression.Field(Expression.Constant(box), "Value");
			}

			// Token: 0x06001629 RID: 5673 RVA: 0x0004AC44 File Offset: 0x00048E44
			private IStrongBox GetBox(ParameterExpression variable)
			{
				using (Stack<HashSet<ParameterExpression>>.Enumerator enumerator = this._shadowedVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Contains(variable))
						{
							return null;
						}
					}
				}
				HoistedLocals hoistedLocals = this._scope;
				object[] array = this._locals;
				int num;
				while (!hoistedLocals.Indexes.TryGetValue(variable, out num))
				{
					hoistedLocals = hoistedLocals.Parent;
					if (hoistedLocals == null)
					{
						throw ContractUtils.Unreachable;
					}
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[num];
			}

			// Token: 0x04000B37 RID: 2871
			private readonly HoistedLocals _scope;

			// Token: 0x04000B38 RID: 2872
			private readonly object[] _locals;

			// Token: 0x04000B39 RID: 2873
			private readonly Stack<HashSet<ParameterExpression>> _shadowedVars = new Stack<HashSet<ParameterExpression>>();
		}

		// Token: 0x020002D7 RID: 727
		internal sealed class MergedRuntimeVariables : IRuntimeVariables
		{
			// Token: 0x0600162A RID: 5674 RVA: 0x0004ACE0 File Offset: 0x00048EE0
			internal MergedRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
			{
				this._first = first;
				this._second = second;
				this._indexes = indexes;
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x0600162B RID: 5675 RVA: 0x0004ACFD File Offset: 0x00048EFD
			public int Count
			{
				get
				{
					return this._indexes.Length;
				}
			}

			// Token: 0x170003CC RID: 972
			public object this[int index]
			{
				get
				{
					index = this._indexes[index];
					if (index < 0)
					{
						return this._second[-1 - index];
					}
					return this._first[index];
				}
				set
				{
					index = this._indexes[index];
					if (index >= 0)
					{
						this._first[index] = value;
						return;
					}
					this._second[-1 - index] = value;
				}
			}

			// Token: 0x04000B3A RID: 2874
			private readonly IRuntimeVariables _first;

			// Token: 0x04000B3B RID: 2875
			private readonly IRuntimeVariables _second;

			// Token: 0x04000B3C RID: 2876
			private readonly int[] _indexes;
		}

		// Token: 0x020002D8 RID: 728
		private sealed class EmptyRuntimeVariables : IRuntimeVariables
		{
			// Token: 0x170003CD RID: 973
			// (get) Token: 0x0600162E RID: 5678 RVA: 0x000023D1 File Offset: 0x000005D1
			int IRuntimeVariables.Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170003CE RID: 974
			object IRuntimeVariables.this[int index]
			{
				get
				{
					throw new IndexOutOfRangeException();
				}
				set
				{
					throw new IndexOutOfRangeException();
				}
			}

			// Token: 0x06001631 RID: 5681 RVA: 0x00002162 File Offset: 0x00000362
			public EmptyRuntimeVariables()
			{
			}
		}

		// Token: 0x020002D9 RID: 729
		private sealed class RuntimeVariableList : IRuntimeVariables
		{
			// Token: 0x06001632 RID: 5682 RVA: 0x0004AD66 File Offset: 0x00048F66
			internal RuntimeVariableList(object[] data, long[] indexes)
			{
				this._data = data;
				this._indexes = indexes;
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06001633 RID: 5683 RVA: 0x0004AD7C File Offset: 0x00048F7C
			public int Count
			{
				get
				{
					return this._indexes.Length;
				}
			}

			// Token: 0x170003D0 RID: 976
			public object this[int index]
			{
				get
				{
					return this.GetStrongBox(index).Value;
				}
				set
				{
					this.GetStrongBox(index).Value = value;
				}
			}

			// Token: 0x06001636 RID: 5686 RVA: 0x0004ADA4 File Offset: 0x00048FA4
			private IStrongBox GetStrongBox(int index)
			{
				long num = this._indexes[index];
				object[] array = this._data;
				for (int i = (int)(num >> 32); i > 0; i--)
				{
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[(int)num];
			}

			// Token: 0x04000B3D RID: 2877
			private readonly object[] _data;

			// Token: 0x04000B3E RID: 2878
			private readonly long[] _indexes;
		}

		// Token: 0x020002DA RID: 730
		internal sealed class RuntimeVariables : IRuntimeVariables
		{
			// Token: 0x06001637 RID: 5687 RVA: 0x0004ADE1 File Offset: 0x00048FE1
			internal RuntimeVariables(IStrongBox[] boxes)
			{
				this._boxes = boxes;
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004ADF0 File Offset: 0x00048FF0
			int IRuntimeVariables.Count
			{
				get
				{
					return this._boxes.Length;
				}
			}

			// Token: 0x170003D2 RID: 978
			object IRuntimeVariables.this[int index]
			{
				get
				{
					return this._boxes[index].Value;
				}
				set
				{
					this._boxes[index].Value = value;
				}
			}

			// Token: 0x04000B3F RID: 2879
			private readonly IStrongBox[] _boxes;
		}
	}
}
