using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a unary operator.</summary>
	// Token: 0x020002A2 RID: 674
	[DebuggerTypeProxy(typeof(Expression.UnaryExpressionProxy))]
	public sealed class UnaryExpression : Expression
	{
		// Token: 0x06001408 RID: 5128 RVA: 0x0003D659 File Offset: 0x0003B859
		internal UnaryExpression(ExpressionType nodeType, Expression expression, Type type, MethodInfo method)
		{
			this.Operand = expression;
			this.Method = method;
			this.NodeType = nodeType;
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.UnaryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003D67E File Offset: 0x0003B87E
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0003D686 File Offset: 0x0003B886
		public sealed override ExpressionType NodeType
		{
			[CompilerGenerated]
			get
			{
				return this.<NodeType>k__BackingField;
			}
		}

		/// <summary>Gets the operand of the unary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand of the unary operation.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0003D68E File Offset: 0x0003B88E
		public Expression Operand
		{
			[CompilerGenerated]
			get
			{
				return this.<Operand>k__BackingField;
			}
		}

		/// <summary>Gets the implementing method for the unary operation.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0003D696 File Offset: 0x0003B896
		public MethodInfo Method
		{
			[CompilerGenerated]
			get
			{
				return this.<Method>k__BackingField;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator.</summary>
		/// <returns>
		///     <see langword="true" /> if the node represents a lifted call; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		public bool IsLifted
		{
			get
			{
				if (this.NodeType == ExpressionType.TypeAs || this.NodeType == ExpressionType.Quote || this.NodeType == ExpressionType.Throw)
				{
					return false;
				}
				bool flag = this.Operand.Type.IsNullableType();
				bool flag2 = this.Type.IsNullableType();
				if (this.Method != null)
				{
					return (flag && !TypeUtils.AreEquivalent(this.Method.GetParametersCached()[0].ParameterType, this.Operand.Type)) || (flag2 && !TypeUtils.AreEquivalent(this.Method.ReturnType, this.Type));
				}
				return flag || flag2;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.</summary>
		/// <returns>
		///     <see langword="true" /> if the operator's return type is lifted to a nullable type; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0003D743 File Offset: 0x0003B943
		public bool IsLiftedToNull
		{
			get
			{
				return this.IsLifted && this.Type.IsNullableType();
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003D75A File Offset: 0x0003B95A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitUnary(this);
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if a node can be reduced, otherwise false.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0003D764 File Offset: 0x0003B964
		public override bool CanReduce
		{
			get
			{
				ExpressionType nodeType = this.NodeType;
				return nodeType - ExpressionType.PreIncrementAssign <= 3;
			}
		}

		/// <summary>Reduces the expression node to a simpler expression. </summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06001411 RID: 5137 RVA: 0x0003D784 File Offset: 0x0003B984
		public override Expression Reduce()
		{
			if (!this.CanReduce)
			{
				return this;
			}
			ExpressionType nodeType = this.Operand.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType == ExpressionType.Index)
			{
				return this.ReduceIndex();
			}
			return this.ReduceVariable();
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0003D7C5 File Offset: 0x0003B9C5
		private bool IsPrefix
		{
			get
			{
				return this.NodeType == ExpressionType.PreIncrementAssign || this.NodeType == ExpressionType.PreDecrementAssign;
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
		private UnaryExpression FunctionalOp(Expression operand)
		{
			ExpressionType nodeType;
			if (this.NodeType == ExpressionType.PreIncrementAssign || this.NodeType == ExpressionType.PostIncrementAssign)
			{
				nodeType = ExpressionType.Increment;
			}
			else
			{
				nodeType = ExpressionType.Decrement;
			}
			return new UnaryExpression(nodeType, operand, operand.Type, this.Method);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0003D81C File Offset: 0x0003BA1C
		private Expression ReduceVariable()
		{
			if (this.IsPrefix)
			{
				return Expression.Assign(this.Operand, this.FunctionalOp(this.Operand));
			}
			ParameterExpression parameterExpression = Expression.Parameter(this.Operand.Type, null);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
			{
				parameterExpression
			}), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				Expression.Assign(parameterExpression, this.Operand),
				Expression.Assign(this.Operand, this.FunctionalOp(parameterExpression)),
				parameterExpression
			}));
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this.Operand;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Parameter(memberExpression.Expression.Type, null);
			BinaryExpression binaryExpression = Expression.Assign(parameterExpression, memberExpression.Expression);
			memberExpression = Expression.MakeMemberAccess(parameterExpression, memberExpression.Member);
			if (this.IsPrefix)
			{
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					binaryExpression,
					Expression.Assign(memberExpression, this.FunctionalOp(memberExpression))
				}));
			}
			ParameterExpression parameterExpression2 = Expression.Parameter(memberExpression.Type, null);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				binaryExpression,
				Expression.Assign(parameterExpression2, memberExpression),
				Expression.Assign(memberExpression, this.FunctionalOp(parameterExpression2)),
				parameterExpression2
			}));
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0003D988 File Offset: 0x0003BB88
		private Expression ReduceIndex()
		{
			bool isPrefix = this.IsPrefix;
			IndexExpression indexExpression = (IndexExpression)this.Operand;
			int argumentCount = indexExpression.ArgumentCount;
			Expression[] array = new Expression[argumentCount + (isPrefix ? 2 : 4)];
			ParameterExpression[] array2 = new ParameterExpression[argumentCount + (isPrefix ? 1 : 2)];
			ParameterExpression[] array3 = new ParameterExpression[argumentCount];
			int i = 0;
			array2[i] = Expression.Parameter(indexExpression.Object.Type, null);
			array[i] = Expression.Assign(array2[i], indexExpression.Object);
			for (i++; i <= argumentCount; i++)
			{
				Expression argument = indexExpression.GetArgument(i - 1);
				array3[i - 1] = (array2[i] = Expression.Parameter(argument.Type, null));
				array[i] = Expression.Assign(array2[i], argument);
			}
			Expression instance = array2[0];
			PropertyInfo indexer = indexExpression.Indexer;
			Expression[] list = array3;
			indexExpression = Expression.MakeIndex(instance, indexer, new TrueReadOnlyCollection<Expression>(list));
			if (!isPrefix)
			{
				ParameterExpression parameterExpression = array2[i] = Expression.Parameter(indexExpression.Type, null);
				array[i] = Expression.Assign(array2[i], indexExpression);
				i++;
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(parameterExpression));
				array[i++] = parameterExpression;
			}
			else
			{
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(indexExpression));
			}
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(array2), new TrueReadOnlyCollection<Expression>(array));
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="operand">The <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001417 RID: 5143 RVA: 0x0003DAE7 File Offset: 0x0003BCE7
		public UnaryExpression Update(Expression operand)
		{
			if (operand == this.Operand)
			{
				return this;
			}
			return Expression.MakeUnary(this.NodeType, operand, this.Type, this.Method);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0000235B File Offset: 0x0000055B
		internal UnaryExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A6A RID: 2666
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x04000A6B RID: 2667
		[CompilerGenerated]
		private readonly ExpressionType <NodeType>k__BackingField;

		// Token: 0x04000A6C RID: 2668
		[CompilerGenerated]
		private readonly Expression <Operand>k__BackingField;

		// Token: 0x04000A6D RID: 2669
		[CompilerGenerated]
		private readonly MethodInfo <Method>k__BackingField;
	}
}
