using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call that has a collection initializer.</summary>
	// Token: 0x02000273 RID: 627
	[DebuggerTypeProxy(typeof(Expression.ListInitExpressionProxy))]
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x0003B445 File Offset: 0x00039645
		internal ListInitExpression(NewExpression newExpression, ReadOnlyCollection<ElementInit> initializers)
		{
			this.NewExpression = newExpression;
			this.Initializers = initializers;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0003B45B File Offset: 0x0003965B
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.ListInit;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ListInitExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0003B45F File Offset: 0x0003965F
		public sealed override Type Type
		{
			get
			{
				return this.NewExpression.Type;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00007E1D File Offset: 0x0000601D
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the expression that contains a call to the constructor of a collection type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the call to the constructor of a collection type.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0003B46C File Offset: 0x0003966C
		public NewExpression NewExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<NewExpression>k__BackingField;
			}
		}

		/// <summary>Gets the element initializers that are used to initialize a collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects which represent the elements that are used to initialize the collection.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0003B474 File Offset: 0x00039674
		public ReadOnlyCollection<ElementInit> Initializers
		{
			[CompilerGenerated]
			get
			{
				return this.<Initializers>k__BackingField;
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003B47C File Offset: 0x0003967C
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitListInit(this);
		}

		/// <summary>Reduces the binary expression node to a simpler expression.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06001255 RID: 4693 RVA: 0x0003B485 File Offset: 0x00039685
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceListInit(this.NewExpression, this.Initializers, true);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="newExpression">The <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property of the result.</param>
		/// <param name="initializers">The <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001256 RID: 4694 RVA: 0x0003B499 File Offset: 0x00039699
		public ListInitExpression Update(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			if ((newExpression == this.NewExpression & initializers != null) && ExpressionUtils.SameElements<ElementInit>(ref initializers, this.Initializers))
			{
				return this;
			}
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0000235B File Offset: 0x0000055B
		internal ListInitExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A17 RID: 2583
		[CompilerGenerated]
		private readonly NewExpression <NewExpression>k__BackingField;

		// Token: 0x04000A18 RID: 2584
		[CompilerGenerated]
		private readonly ReadOnlyCollection<ElementInit> <Initializers>k__BackingField;
	}
}
