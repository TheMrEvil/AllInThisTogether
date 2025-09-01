using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents a set of binding restrictions on the <see cref="T:System.Dynamic.DynamicMetaObject" /> under which the dynamic binding is valid.</summary>
	// Token: 0x020002F0 RID: 752
	[DebuggerTypeProxy(typeof(BindingRestrictions.BindingRestrictionsProxy))]
	[DebuggerDisplay("{DebugView}")]
	public abstract class BindingRestrictions
	{
		// Token: 0x060016C4 RID: 5828 RVA: 0x00002162 File Offset: 0x00000362
		private BindingRestrictions()
		{
		}

		// Token: 0x060016C5 RID: 5829
		internal abstract Expression GetExpression();

		/// <summary>Merges the set of binding restrictions with the current binding restrictions.</summary>
		/// <param name="restrictions">The set of restrictions with which to merge the current binding restrictions.</param>
		/// <returns>The new set of binding restrictions.</returns>
		// Token: 0x060016C6 RID: 5830 RVA: 0x0004C980 File Offset: 0x0004AB80
		public BindingRestrictions Merge(BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			if (this == BindingRestrictions.Empty)
			{
				return restrictions;
			}
			if (restrictions == BindingRestrictions.Empty)
			{
				return this;
			}
			return new BindingRestrictions.MergedRestriction(this, restrictions);
		}

		/// <summary>Creates the binding restriction that check the expression for runtime type identity.</summary>
		/// <param name="expression">The expression to test.</param>
		/// <param name="type">The exact type to test.</param>
		/// <returns>The new binding restrictions.</returns>
		// Token: 0x060016C7 RID: 5831 RVA: 0x0004C9A8 File Offset: 0x0004ABA8
		public static BindingRestrictions GetTypeRestriction(Expression expression, Type type)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			return new BindingRestrictions.TypeRestriction(expression, type);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004C9C7 File Offset: 0x0004ABC7
		internal static BindingRestrictions GetTypeRestriction(DynamicMetaObject obj)
		{
			if (obj.Value == null && obj.HasValue)
			{
				return BindingRestrictions.GetInstanceRestriction(obj.Expression, null);
			}
			return BindingRestrictions.GetTypeRestriction(obj.Expression, obj.LimitType);
		}

		/// <summary>Creates the binding restriction that checks the expression for object instance identity.</summary>
		/// <param name="expression">The expression to test.</param>
		/// <param name="instance">The exact object instance to test.</param>
		/// <returns>The new binding restrictions.</returns>
		// Token: 0x060016C9 RID: 5833 RVA: 0x0004C9F7 File Offset: 0x0004ABF7
		public static BindingRestrictions GetInstanceRestriction(Expression expression, object instance)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			return new BindingRestrictions.InstanceRestriction(expression, instance);
		}

		/// <summary>Creates the binding restriction that checks the expression for arbitrary immutable properties.</summary>
		/// <param name="expression">The expression representing the restrictions.</param>
		/// <returns>The new binding restrictions.</returns>
		// Token: 0x060016CA RID: 5834 RVA: 0x0004CA0B File Offset: 0x0004AC0B
		public static BindingRestrictions GetExpressionRestriction(Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.Requires(expression.Type == typeof(bool), "expression");
			return new BindingRestrictions.CustomRestriction(expression);
		}

		/// <summary>Combines binding restrictions from the list of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances into one set of restrictions.</summary>
		/// <param name="contributingObjects">The list of <see cref="T:System.Dynamic.DynamicMetaObject" /> instances from which to combine restrictions.</param>
		/// <returns>The new set of binding restrictions.</returns>
		// Token: 0x060016CB RID: 5835 RVA: 0x0004CA40 File Offset: 0x0004AC40
		public static BindingRestrictions Combine(IList<DynamicMetaObject> contributingObjects)
		{
			BindingRestrictions bindingRestrictions = BindingRestrictions.Empty;
			if (contributingObjects != null)
			{
				foreach (DynamicMetaObject dynamicMetaObject in contributingObjects)
				{
					if (dynamicMetaObject != null)
					{
						bindingRestrictions = bindingRestrictions.Merge(dynamicMetaObject.Restrictions);
					}
				}
			}
			return bindingRestrictions;
		}

		/// <summary>Creates the <see cref="T:System.Linq.Expressions.Expression" /> representing the binding restrictions.</summary>
		/// <returns>The expression tree representing the restrictions.</returns>
		// Token: 0x060016CC RID: 5836 RVA: 0x0004CA9C File Offset: 0x0004AC9C
		public Expression ToExpression()
		{
			return this.GetExpression();
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0004CAA4 File Offset: 0x0004ACA4
		private string DebugView
		{
			get
			{
				return this.ToExpression().ToString();
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004CAB1 File Offset: 0x0004ACB1
		// Note: this type is marked as 'beforefieldinit'.
		static BindingRestrictions()
		{
		}

		/// <summary>Represents an empty set of binding restrictions. This field is read only.</summary>
		// Token: 0x04000B69 RID: 2921
		public static readonly BindingRestrictions Empty = new BindingRestrictions.CustomRestriction(Utils.Constant(true));

		// Token: 0x04000B6A RID: 2922
		private const int TypeRestrictionHash = 1227133513;

		// Token: 0x04000B6B RID: 2923
		private const int InstanceRestrictionHash = -1840700270;

		// Token: 0x04000B6C RID: 2924
		private const int CustomRestrictionHash = 613566756;

		// Token: 0x020002F1 RID: 753
		private sealed class TestBuilder
		{
			// Token: 0x060016CF RID: 5839 RVA: 0x0004CAC3 File Offset: 0x0004ACC3
			internal void Append(BindingRestrictions restrictions)
			{
				if (this._unique.Add(restrictions))
				{
					this.Push(restrictions.GetExpression(), 0);
				}
			}

			// Token: 0x060016D0 RID: 5840 RVA: 0x0004CAE0 File Offset: 0x0004ACE0
			internal Expression ToExpression()
			{
				Expression expression = this._tests.Pop().Node;
				while (this._tests.Count > 0)
				{
					expression = Expression.AndAlso(this._tests.Pop().Node, expression);
				}
				return expression;
			}

			// Token: 0x060016D1 RID: 5841 RVA: 0x0004CB28 File Offset: 0x0004AD28
			private void Push(Expression node, int depth)
			{
				while (this._tests.Count > 0 && this._tests.Peek().Depth == depth)
				{
					node = Expression.AndAlso(this._tests.Pop().Node, node);
					depth++;
				}
				this._tests.Push(new BindingRestrictions.TestBuilder.AndNode
				{
					Node = node,
					Depth = depth
				});
			}

			// Token: 0x060016D2 RID: 5842 RVA: 0x0004CB99 File Offset: 0x0004AD99
			public TestBuilder()
			{
			}

			// Token: 0x04000B6D RID: 2925
			private readonly HashSet<BindingRestrictions> _unique = new HashSet<BindingRestrictions>();

			// Token: 0x04000B6E RID: 2926
			private readonly Stack<BindingRestrictions.TestBuilder.AndNode> _tests = new Stack<BindingRestrictions.TestBuilder.AndNode>();

			// Token: 0x020002F2 RID: 754
			private struct AndNode
			{
				// Token: 0x04000B6F RID: 2927
				internal int Depth;

				// Token: 0x04000B70 RID: 2928
				internal Expression Node;
			}
		}

		// Token: 0x020002F3 RID: 755
		private sealed class MergedRestriction : BindingRestrictions
		{
			// Token: 0x060016D3 RID: 5843 RVA: 0x0004CBB7 File Offset: 0x0004ADB7
			internal MergedRestriction(BindingRestrictions left, BindingRestrictions right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x060016D4 RID: 5844 RVA: 0x0004CBD0 File Offset: 0x0004ADD0
			internal override Expression GetExpression()
			{
				BindingRestrictions.TestBuilder testBuilder = new BindingRestrictions.TestBuilder();
				Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
				BindingRestrictions bindingRestrictions = this;
				for (;;)
				{
					BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
					if (mergedRestriction != null)
					{
						stack.Push(mergedRestriction.Right);
						bindingRestrictions = mergedRestriction.Left;
					}
					else
					{
						testBuilder.Append(bindingRestrictions);
						if (stack.Count == 0)
						{
							break;
						}
						bindingRestrictions = stack.Pop();
					}
				}
				return testBuilder.ToExpression();
			}

			// Token: 0x04000B71 RID: 2929
			internal readonly BindingRestrictions Left;

			// Token: 0x04000B72 RID: 2930
			internal readonly BindingRestrictions Right;
		}

		// Token: 0x020002F4 RID: 756
		private sealed class CustomRestriction : BindingRestrictions
		{
			// Token: 0x060016D5 RID: 5845 RVA: 0x0004CC28 File Offset: 0x0004AE28
			internal CustomRestriction(Expression expression)
			{
				this._expression = expression;
			}

			// Token: 0x060016D6 RID: 5846 RVA: 0x0004CC37 File Offset: 0x0004AE37
			public override bool Equals(object obj)
			{
				BindingRestrictions.CustomRestriction customRestriction = obj as BindingRestrictions.CustomRestriction;
				return ((customRestriction != null) ? customRestriction._expression : null) == this._expression;
			}

			// Token: 0x060016D7 RID: 5847 RVA: 0x0004CC53 File Offset: 0x0004AE53
			public override int GetHashCode()
			{
				return 613566756 ^ this._expression.GetHashCode();
			}

			// Token: 0x060016D8 RID: 5848 RVA: 0x0004CC66 File Offset: 0x0004AE66
			internal override Expression GetExpression()
			{
				return this._expression;
			}

			// Token: 0x04000B73 RID: 2931
			private readonly Expression _expression;
		}

		// Token: 0x020002F5 RID: 757
		private sealed class TypeRestriction : BindingRestrictions
		{
			// Token: 0x060016D9 RID: 5849 RVA: 0x0004CC6E File Offset: 0x0004AE6E
			internal TypeRestriction(Expression parameter, Type type)
			{
				this._expression = parameter;
				this._type = type;
			}

			// Token: 0x060016DA RID: 5850 RVA: 0x0004CC84 File Offset: 0x0004AE84
			public override bool Equals(object obj)
			{
				BindingRestrictions.TypeRestriction typeRestriction = obj as BindingRestrictions.TypeRestriction;
				return ((typeRestriction != null) ? typeRestriction._expression : null) == this._expression && TypeUtils.AreEquivalent(typeRestriction._type, this._type);
			}

			// Token: 0x060016DB RID: 5851 RVA: 0x0004CCBF File Offset: 0x0004AEBF
			public override int GetHashCode()
			{
				return 1227133513 ^ this._expression.GetHashCode() ^ this._type.GetHashCode();
			}

			// Token: 0x060016DC RID: 5852 RVA: 0x0004CCDE File Offset: 0x0004AEDE
			internal override Expression GetExpression()
			{
				return Expression.TypeEqual(this._expression, this._type);
			}

			// Token: 0x04000B74 RID: 2932
			private readonly Expression _expression;

			// Token: 0x04000B75 RID: 2933
			private readonly Type _type;
		}

		// Token: 0x020002F6 RID: 758
		private sealed class InstanceRestriction : BindingRestrictions
		{
			// Token: 0x060016DD RID: 5853 RVA: 0x0004CCF1 File Offset: 0x0004AEF1
			internal InstanceRestriction(Expression parameter, object instance)
			{
				this._expression = parameter;
				this._instance = instance;
			}

			// Token: 0x060016DE RID: 5854 RVA: 0x0004CD08 File Offset: 0x0004AF08
			public override bool Equals(object obj)
			{
				BindingRestrictions.InstanceRestriction instanceRestriction = obj as BindingRestrictions.InstanceRestriction;
				return ((instanceRestriction != null) ? instanceRestriction._expression : null) == this._expression && instanceRestriction._instance == this._instance;
			}

			// Token: 0x060016DF RID: 5855 RVA: 0x0004CD40 File Offset: 0x0004AF40
			public override int GetHashCode()
			{
				return -1840700270 ^ RuntimeHelpers.GetHashCode(this._instance) ^ this._expression.GetHashCode();
			}

			// Token: 0x060016E0 RID: 5856 RVA: 0x0004CD60 File Offset: 0x0004AF60
			internal override Expression GetExpression()
			{
				if (this._instance == null)
				{
					return Expression.Equal(Expression.Convert(this._expression, typeof(object)), Utils.Null);
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.Constant(this._instance, typeof(object))),
					Expression.AndAlso(Expression.NotEqual(parameterExpression, Utils.Null), Expression.Equal(Expression.Convert(this._expression, typeof(object)), parameterExpression))
				}));
			}

			// Token: 0x04000B76 RID: 2934
			private readonly Expression _expression;

			// Token: 0x04000B77 RID: 2935
			private readonly object _instance;
		}

		// Token: 0x020002F7 RID: 759
		private sealed class BindingRestrictionsProxy
		{
			// Token: 0x060016E1 RID: 5857 RVA: 0x0004CE11 File Offset: 0x0004B011
			public BindingRestrictionsProxy(BindingRestrictions node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0004CE2B File Offset: 0x0004B02B
			public bool IsEmpty
			{
				get
				{
					return this._node == BindingRestrictions.Empty;
				}
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x060016E3 RID: 5859 RVA: 0x0004CE3A File Offset: 0x0004B03A
			public Expression Test
			{
				get
				{
					return this._node.ToExpression();
				}
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0004CE48 File Offset: 0x0004B048
			public BindingRestrictions[] Restrictions
			{
				get
				{
					List<BindingRestrictions> list = new List<BindingRestrictions>();
					Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
					BindingRestrictions bindingRestrictions = this._node;
					for (;;)
					{
						BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
						if (mergedRestriction != null)
						{
							stack.Push(mergedRestriction.Right);
							bindingRestrictions = mergedRestriction.Left;
						}
						else
						{
							list.Add(bindingRestrictions);
							if (stack.Count == 0)
							{
								break;
							}
							bindingRestrictions = stack.Pop();
						}
					}
					return list.ToArray();
				}
			}

			// Token: 0x060016E5 RID: 5861 RVA: 0x0004CEA5 File Offset: 0x0004B0A5
			public override string ToString()
			{
				return this._node.DebugView;
			}

			// Token: 0x04000B78 RID: 2936
			private readonly BindingRestrictions _node;
		}
	}
}
