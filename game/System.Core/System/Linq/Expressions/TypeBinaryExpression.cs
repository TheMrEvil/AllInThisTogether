using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an operation between an expression and a type.</summary>
	// Token: 0x020002A1 RID: 673
	[DebuggerTypeProxy(typeof(Expression.TypeBinaryExpressionProxy))]
	public sealed class TypeBinaryExpression : Expression
	{
		// Token: 0x060013FD RID: 5117 RVA: 0x0003D3F2 File Offset: 0x0003B5F2
		internal TypeBinaryExpression(Expression expression, Type typeOperand, ExpressionType nodeType)
		{
			this.Expression = expression;
			this.TypeOperand = typeOperand;
			this.NodeType = nodeType;
		}

		/// <summary>Gets the static type of the expression that this <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0002D0E1 File Offset: 0x0002B2E1
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0003D40F File Offset: 0x0003B60F
		public sealed override ExpressionType NodeType
		{
			[CompilerGenerated]
			get
			{
				return this.<NodeType>k__BackingField;
			}
		}

		/// <summary>Gets the expression operand of a type test operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression operand of a type test operation.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0003D417 File Offset: 0x0003B617
		public Expression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
		}

		/// <summary>Gets the type operand of a type test operation.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type operand of a type test operation.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0003D41F File Offset: 0x0003B61F
		public Type TypeOperand
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeOperand>k__BackingField;
			}
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0003D428 File Offset: 0x0003B628
		internal Expression ReduceTypeEqual()
		{
			Type type = this.Expression.Type;
			if (type.IsValueType || this.TypeOperand.IsPointer)
			{
				if (!type.IsNullableType())
				{
					return Expression.Block(this.Expression, Utils.Constant(type == this.TypeOperand.GetNonNullableType()));
				}
				if (type.GetNonNullableType() != this.TypeOperand.GetNonNullableType())
				{
					return Expression.Block(this.Expression, Utils.Constant(false));
				}
				return Expression.NotEqual(this.Expression, Expression.Constant(null, this.Expression.Type));
			}
			else
			{
				if (this.Expression.NodeType == ExpressionType.Constant)
				{
					return this.ReduceConstantTypeEqual();
				}
				ParameterExpression parameterExpression = this.Expression as ParameterExpression;
				if (parameterExpression != null && !parameterExpression.IsByRef)
				{
					return this.ByValParameterTypeEqual(parameterExpression);
				}
				parameterExpression = Expression.Parameter(typeof(object));
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, this.Expression),
					this.ByValParameterTypeEqual(parameterExpression)
				}));
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0003D544 File Offset: 0x0003B744
		private Expression ByValParameterTypeEqual(ParameterExpression value)
		{
			Expression expression = Expression.Call(value, CachedReflectionInfo.Object_GetType);
			if (this.TypeOperand.IsInterface)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(Type));
				expression = Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, expression),
					parameterExpression
				}));
			}
			return Expression.AndAlso(Expression.ReferenceNotEqual(value, Utils.Null), Expression.ReferenceEqual(expression, Expression.Constant(this.TypeOperand.GetNonNullableType(), typeof(Type))));
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		private Expression ReduceConstantTypeEqual()
		{
			ConstantExpression constantExpression = this.Expression as ConstantExpression;
			if (constantExpression.Value == null)
			{
				return Utils.Constant(false);
			}
			return Utils.Constant(this.TypeOperand.GetNonNullableType() == constantExpression.Value.GetType());
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0003D620 File Offset: 0x0003B820
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTypeBinary(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001406 RID: 5126 RVA: 0x0003D629 File Offset: 0x0003B829
		public TypeBinaryExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			if (this.NodeType == ExpressionType.TypeIs)
			{
				return Expression.TypeIs(expression, this.TypeOperand);
			}
			return Expression.TypeEqual(expression, this.TypeOperand);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0000235B File Offset: 0x0000055B
		internal TypeBinaryExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A67 RID: 2663
		[CompilerGenerated]
		private readonly ExpressionType <NodeType>k__BackingField;

		// Token: 0x04000A68 RID: 2664
		[CompilerGenerated]
		private readonly Expression <Expression>k__BackingField;

		// Token: 0x04000A69 RID: 2665
		[CompilerGenerated]
		private readonly Type <TypeOperand>k__BackingField;
	}
}
