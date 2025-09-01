using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that applies a delegate or lambda expression to a list of argument expressions.</summary>
	// Token: 0x02000261 RID: 609
	[DebuggerTypeProxy(typeof(Expression.InvocationExpressionProxy))]
	public class InvocationExpression : Expression, IArgumentProvider
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x0003A9DE File Offset: 0x00038BDE
		internal InvocationExpression(Expression expression, Type returnType)
		{
			this.Expression = expression;
			this.Type = returnType;
		}

		/// <summary>Gets the static type of the expression that this <see cref="P:System.Linq.Expressions.InvocationExpression.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.InvocationExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0003A9F4 File Offset: 0x00038BF4
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0003A9FC File Offset: 0x00038BFC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Invoke;
			}
		}

		/// <summary>Gets the delegate or lambda expression to be applied.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate to be applied.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0003AA00 File Offset: 0x00038C00
		public Expression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
		}

		/// <summary>Gets the arguments that the delegate or lambda expression is applied to.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent the arguments that the delegate is applied to.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0003AA08 File Offset: 0x00038C08
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.InvocationExpression.Expression" /> property of the result.</param>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.InvocationExpression.Arguments" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060011D0 RID: 4560 RVA: 0x0003AA10 File Offset: 0x00038C10
		public InvocationExpression Update(Expression expression, IEnumerable<Expression> arguments)
		{
			if ((expression == this.Expression & arguments != null) && ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			return Expression.Invoke(expression, arguments);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual Expression GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual int ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003AA3A File Offset: 0x00038C3A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitInvocation(this);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0003AA43 File Offset: 0x00038C43
		internal LambdaExpression LambdaOperand
		{
			get
			{
				if (this.Expression.NodeType != ExpressionType.Quote)
				{
					return this.Expression as LambdaExpression;
				}
				return (LambdaExpression)((UnaryExpression)this.Expression).Operand;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0000235B File Offset: 0x0000055B
		internal InvocationExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040009F7 RID: 2551
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x040009F8 RID: 2552
		[CompilerGenerated]
		private readonly Expression <Expression>k__BackingField;
	}
}
