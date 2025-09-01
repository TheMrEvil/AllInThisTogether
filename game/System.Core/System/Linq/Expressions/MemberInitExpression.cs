using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents calling a constructor and initializing one or more members of the new object.</summary>
	// Token: 0x0200027B RID: 635
	[DebuggerTypeProxy(typeof(Expression.MemberInitExpressionProxy))]
	public sealed class MemberInitExpression : Expression
	{
		// Token: 0x0600127C RID: 4732 RVA: 0x0003B689 File Offset: 0x00039889
		internal MemberInitExpression(NewExpression newExpression, ReadOnlyCollection<MemberBinding> bindings)
		{
			this.NewExpression = newExpression;
			this.Bindings = bindings;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.MemberInitExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0003B69F File Offset: 0x0003989F
		public sealed override Type Type
		{
			get
			{
				return this.NewExpression.Type;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00007E1D File Offset: 0x0000601D
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0003B6AC File Offset: 0x000398AC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.MemberInit;
			}
		}

		/// <summary>Gets the expression that represents the constructor call.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the constructor call.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0003B6B0 File Offset: 0x000398B0
		public NewExpression NewExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<NewExpression>k__BackingField;
			}
		}

		/// <summary>Gets the bindings that describe how to initialize the members of the newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects which describe how to initialize the members.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0003B6B8 File Offset: 0x000398B8
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			[CompilerGenerated]
			get
			{
				return this.<Bindings>k__BackingField;
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0003B6C0 File Offset: 0x000398C0
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMemberInit(this);
		}

		/// <summary>Reduces the <see cref="T:System.Linq.Expressions.MemberInitExpression" /> to a simpler expression. </summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06001283 RID: 4739 RVA: 0x0003B6C9 File Offset: 0x000398C9
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceMemberInit(this.NewExpression, this.Bindings, true);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0003B6E0 File Offset: 0x000398E0
		private static Expression ReduceMemberInit(Expression objExpression, ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(objExpression.Type);
			int count = bindings.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, objExpression);
			for (int i = 0; i < count; i++)
			{
				array[i + 1] = MemberInitExpression.ReduceMemberBinding(parameterExpression, bindings[i]);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Utils.Empty);
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, array);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0003B754 File Offset: 0x00039954
		internal static Expression ReduceListInit(Expression listExpression, ReadOnlyCollection<ElementInit> initializers, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(listExpression.Type);
			int count = initializers.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, listExpression);
			for (int i = 0; i < count; i++)
			{
				ElementInit elementInit = initializers[i];
				array[i + 1] = Expression.Call(parameterExpression, elementInit.AddMethod, elementInit.Arguments);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Utils.Empty);
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, array);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003B7D8 File Offset: 0x000399D8
		internal static Expression ReduceMemberBinding(ParameterExpression objVar, MemberBinding binding)
		{
			MemberExpression memberExpression = Expression.MakeMemberAccess(objVar, binding.Member);
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return Expression.Assign(memberExpression, ((MemberAssignment)binding).Expression);
			case MemberBindingType.MemberBinding:
				return MemberInitExpression.ReduceMemberInit(memberExpression, ((MemberMemberBinding)binding).Bindings, false);
			case MemberBindingType.ListBinding:
				return MemberInitExpression.ReduceListInit(memberExpression, ((MemberListBinding)binding).Initializers, false);
			default:
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="newExpression">The <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property of the result.</param>
		/// <param name="bindings">The <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001287 RID: 4743 RVA: 0x0003B84A File Offset: 0x00039A4A
		public MemberInitExpression Update(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			if ((newExpression == this.NewExpression & bindings != null) && ExpressionUtils.SameElements<MemberBinding>(ref bindings, this.Bindings))
			{
				return this;
			}
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemberInitExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A26 RID: 2598
		[CompilerGenerated]
		private readonly NewExpression <NewExpression>k__BackingField;

		// Token: 0x04000A27 RID: 2599
		[CompilerGenerated]
		private readonly ReadOnlyCollection<MemberBinding> <Bindings>k__BackingField;
	}
}
