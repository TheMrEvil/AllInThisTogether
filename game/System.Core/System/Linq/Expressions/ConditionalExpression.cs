using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a conditional operator.</summary>
	// Token: 0x0200023E RID: 574
	[DebuggerTypeProxy(typeof(Expression.ConditionalExpressionProxy))]
	public class ConditionalExpression : Expression
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x00035782 File Offset: 0x00033982
		internal ConditionalExpression(Expression test, Expression ifTrue)
		{
			this.Test = test;
			this.IfTrue = ifTrue;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00035798 File Offset: 0x00033998
		internal static ConditionalExpression Make(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			if (ifTrue.Type != type || ifFalse.Type != type)
			{
				return new FullConditionalExpressionWithType(test, ifTrue, ifFalse, type);
			}
			if (ifFalse is DefaultExpression && ifFalse.Type == typeof(void))
			{
				return new ConditionalExpression(test, ifTrue);
			}
			return new FullConditionalExpression(test, ifTrue, ifFalse);
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000357FA File Offset: 0x000339FA
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Conditional;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ConditionalExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000357FD File Offset: 0x000339FD
		public override Type Type
		{
			get
			{
				return this.IfTrue.Type;
			}
		}

		/// <summary>Gets the test of the conditional operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the test of the conditional operation.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0003580A File Offset: 0x00033A0A
		public Expression Test
		{
			[CompilerGenerated]
			get
			{
				return this.<Test>k__BackingField;
			}
		}

		/// <summary>Gets the expression to execute if the test evaluates to <see langword="true" />.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is <see langword="true" />.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00035812 File Offset: 0x00033A12
		public Expression IfTrue
		{
			[CompilerGenerated]
			get
			{
				return this.<IfTrue>k__BackingField;
			}
		}

		/// <summary>Gets the expression to execute if the test evaluates to <see langword="false" />.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is <see langword="false" />.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0003581A File Offset: 0x00033A1A
		public Expression IfFalse
		{
			get
			{
				return this.GetFalse();
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00035822 File Offset: 0x00033A22
		internal virtual Expression GetFalse()
		{
			return Utils.Empty;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00035829 File Offset: 0x00033A29
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConditional(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression</summary>
		/// <param name="test">The <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property of the result.</param>
		/// <param name="ifTrue">The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property of the result.</param>
		/// <param name="ifFalse">The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property of the result.</param>
		/// <returns>This expression if no children changed, or an expression with the updated children.</returns>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x00035832 File Offset: 0x00033A32
		public ConditionalExpression Update(Expression test, Expression ifTrue, Expression ifFalse)
		{
			if (test == this.Test && ifTrue == this.IfTrue && ifFalse == this.IfFalse)
			{
				return this;
			}
			return Expression.Condition(test, ifTrue, ifFalse, this.Type);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0000235B File Offset: 0x0000055B
		internal ConditionalExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000963 RID: 2403
		[CompilerGenerated]
		private readonly Expression <Test>k__BackingField;

		// Token: 0x04000964 RID: 2404
		[CompilerGenerated]
		private readonly Expression <IfTrue>k__BackingField;
	}
}
