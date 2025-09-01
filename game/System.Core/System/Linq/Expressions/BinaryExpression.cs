using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a binary operator.</summary>
	// Token: 0x02000208 RID: 520
	[DebuggerTypeProxy(typeof(Expression.BinaryExpressionProxy))]
	public class BinaryExpression : Expression
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002C9A0 File Offset: 0x0002ABA0
		internal BinaryExpression(Expression left, Expression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the expression tree node can be reduced, otherwise false.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002C9B6 File Offset: 0x0002ABB6
		public override bool CanReduce
		{
			get
			{
				return BinaryExpression.IsOpAssignment(this.NodeType);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002C9C3 File Offset: 0x0002ABC3
		private static bool IsOpAssignment(ExpressionType op)
		{
			return op - ExpressionType.AddAssign <= 13;
		}

		/// <summary>Gets the right operand of the binary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand of the binary operation.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0002C9D0 File Offset: 0x0002ABD0
		public Expression Right
		{
			[CompilerGenerated]
			get
			{
				return this.<Right>k__BackingField;
			}
		}

		/// <summary>Gets the left operand of the binary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand of the binary operation.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002C9D8 File Offset: 0x0002ABD8
		public Expression Left
		{
			[CompilerGenerated]
			get
			{
				return this.<Left>k__BackingField;
			}
		}

		/// <summary>Gets the implementing method for the binary operation.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		public MethodInfo Method
		{
			get
			{
				return this.GetMethod();
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0000392D File Offset: 0x00001B2D
		internal virtual MethodInfo GetMethod()
		{
			return null;
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="left">The <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property of the result. </param>
		/// <param name="conversion">The <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property of the result.</param>
		/// <param name="right">The <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property of the result. </param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06000CCA RID: 3274 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		public BinaryExpression Update(Expression left, LambdaExpression conversion, Expression right)
		{
			if (left == this.Left && right == this.Right && conversion == this.Conversion)
			{
				return this;
			}
			if (!this.IsReferenceComparison)
			{
				return Expression.MakeBinary(this.NodeType, left, right, this.IsLiftedToNull, this.Method, conversion);
			}
			if (this.NodeType == ExpressionType.Equal)
			{
				return Expression.ReferenceEqual(left, right);
			}
			return Expression.ReferenceNotEqual(left, right);
		}

		/// <summary>Reduces the binary expression node to a simpler expression.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000CCB RID: 3275 RVA: 0x0002CA50 File Offset: 0x0002AC50
		public override Expression Reduce()
		{
			if (!BinaryExpression.IsOpAssignment(this.NodeType))
			{
				return this;
			}
			ExpressionType nodeType = this.Left.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType != ExpressionType.Index)
			{
				return this.ReduceVariable();
			}
			return this.ReduceIndex();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002CA98 File Offset: 0x0002AC98
		private static ExpressionType GetBinaryOpFromAssignmentOp(ExpressionType op)
		{
			switch (op)
			{
			case ExpressionType.AddAssign:
				return ExpressionType.Add;
			case ExpressionType.AndAssign:
				return ExpressionType.And;
			case ExpressionType.DivideAssign:
				return ExpressionType.Divide;
			case ExpressionType.ExclusiveOrAssign:
				return ExpressionType.ExclusiveOr;
			case ExpressionType.LeftShiftAssign:
				return ExpressionType.LeftShift;
			case ExpressionType.ModuloAssign:
				return ExpressionType.Modulo;
			case ExpressionType.MultiplyAssign:
				return ExpressionType.Multiply;
			case ExpressionType.OrAssign:
				return ExpressionType.Or;
			case ExpressionType.PowerAssign:
				return ExpressionType.Power;
			case ExpressionType.RightShiftAssign:
				return ExpressionType.RightShift;
			case ExpressionType.SubtractAssign:
				return ExpressionType.Subtract;
			case ExpressionType.AddAssignChecked:
				return ExpressionType.AddChecked;
			case ExpressionType.MultiplyAssignChecked:
				return ExpressionType.MultiplyChecked;
			case ExpressionType.SubtractAssignChecked:
				return ExpressionType.SubtractChecked;
			default:
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002CB14 File Offset: 0x0002AD14
		private Expression ReduceVariable()
		{
			Expression expression = Expression.MakeBinary(BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType), this.Left, this.Right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression = Expression.Invoke(conversion, expression);
			}
			return Expression.Assign(this.Left, expression);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002CB64 File Offset: 0x0002AD64
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this.Left;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Variable(memberExpression.Expression.Type, "temp1");
			Expression expression = Expression.Assign(parameterExpression, memberExpression.Expression);
			Expression expression2 = Expression.MakeBinary(BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType), Expression.MakeMemberAccess(parameterExpression, memberExpression.Member), this.Right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression2 = Expression.Invoke(conversion, expression2);
			}
			ParameterExpression parameterExpression2 = Expression.Variable(expression2.Type, "temp2");
			expression2 = Expression.Assign(parameterExpression2, expression2);
			Expression expression3 = Expression.Assign(Expression.MakeMemberAccess(parameterExpression, memberExpression.Member), parameterExpression2);
			Expression expression4 = parameterExpression2;
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				expression,
				expression2,
				expression3,
				expression4
			}));
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002CC58 File Offset: 0x0002AE58
		private Expression ReduceIndex()
		{
			IndexExpression indexExpression = (IndexExpression)this.Left;
			ArrayBuilder<ParameterExpression> builder = new ArrayBuilder<ParameterExpression>(indexExpression.ArgumentCount + 2);
			ArrayBuilder<Expression> builder2 = new ArrayBuilder<Expression>(indexExpression.ArgumentCount + 3);
			ParameterExpression parameterExpression = Expression.Variable(indexExpression.Object.Type, "tempObj");
			builder.UncheckedAdd(parameterExpression);
			builder2.UncheckedAdd(Expression.Assign(parameterExpression, indexExpression.Object));
			int argumentCount = indexExpression.ArgumentCount;
			ArrayBuilder<Expression> builder3 = new ArrayBuilder<Expression>(argumentCount);
			for (int i = 0; i < argumentCount; i++)
			{
				Expression argument = indexExpression.GetArgument(i);
				ParameterExpression parameterExpression2 = Expression.Variable(argument.Type, "tempArg" + i.ToString());
				builder.UncheckedAdd(parameterExpression2);
				builder3.UncheckedAdd(parameterExpression2);
				builder2.UncheckedAdd(Expression.Assign(parameterExpression2, argument));
			}
			IndexExpression left = Expression.MakeIndex(parameterExpression, indexExpression.Indexer, builder3.ToReadOnly<Expression>());
			Expression expression = Expression.MakeBinary(BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType), left, this.Right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression = Expression.Invoke(conversion, expression);
			}
			ParameterExpression parameterExpression3 = Expression.Variable(expression.Type, "tempValue");
			builder.UncheckedAdd(parameterExpression3);
			builder2.UncheckedAdd(Expression.Assign(parameterExpression3, expression));
			builder2.UncheckedAdd(Expression.Assign(left, parameterExpression3));
			return Expression.Block(builder.ToReadOnly<ParameterExpression>(), builder2.ToReadOnly<Expression>());
		}

		/// <summary>Gets the type conversion function that is used by a coalescing or compound assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that represents a type conversion function.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002CDC7 File Offset: 0x0002AFC7
		public LambdaExpression Conversion
		{
			get
			{
				return this.GetConversion();
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0000392D File Offset: 0x00001B2D
		internal virtual LambdaExpression GetConversion()
		{
			return null;
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator.</summary>
		/// <returns>
		///     <see langword="true" /> if the node represents a lifted call; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002CDD0 File Offset: 0x0002AFD0
		public bool IsLifted
		{
			get
			{
				if (this.NodeType == ExpressionType.Coalesce || this.NodeType == ExpressionType.Assign)
				{
					return false;
				}
				if (this.Left.Type.IsNullableType())
				{
					MethodInfo method = this.GetMethod();
					return method == null || !TypeUtils.AreEquivalent(method.GetParametersCached()[0].ParameterType.GetNonRefType(), this.Left.Type);
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.</summary>
		/// <returns>
		///     <see langword="true" /> if the operator's return type is lifted to a nullable type; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002CE3D File Offset: 0x0002B03D
		public bool IsLiftedToNull
		{
			get
			{
				return this.IsLifted && this.Type.IsNullableType();
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002CE54 File Offset: 0x0002B054
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBinary(this);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002CE60 File Offset: 0x0002B060
		internal static BinaryExpression Create(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion)
		{
			if (conversion != null)
			{
				return new CoalesceConversionBinaryExpression(left, right, conversion);
			}
			if (method != null)
			{
				return new MethodBinaryExpression(nodeType, left, right, type, method);
			}
			if (type == typeof(bool))
			{
				return new LogicalBinaryExpression(nodeType, left, right);
			}
			return new SimpleBinaryExpression(nodeType, left, right, type);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002CEB8 File Offset: 0x0002B0B8
		internal bool IsLiftedLogical
		{
			get
			{
				Type type = this.Left.Type;
				Type type2 = this.Right.Type;
				MethodInfo method = this.GetMethod();
				ExpressionType nodeType = this.NodeType;
				return (nodeType == ExpressionType.AndAlso || nodeType == ExpressionType.OrElse) && TypeUtils.AreEquivalent(type2, type) && type.IsNullableType() && method != null && TypeUtils.AreEquivalent(method.ReturnType, type.GetNonNullableType());
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002CF24 File Offset: 0x0002B124
		internal bool IsReferenceComparison
		{
			get
			{
				Type type = this.Left.Type;
				Type type2 = this.Right.Type;
				MethodInfo method = this.GetMethod();
				ExpressionType nodeType = this.NodeType;
				return (nodeType == ExpressionType.Equal || nodeType == ExpressionType.NotEqual) && method == null && !type.IsValueType && !type2.IsValueType;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002CF80 File Offset: 0x0002B180
		internal Expression ReduceUserdefinedLifted()
		{
			ParameterExpression parameterExpression = Expression.Parameter(this.Left.Type, "left");
			ParameterExpression parameterExpression2 = Expression.Parameter(this.Right.Type, "right");
			string name = (this.NodeType == ExpressionType.AndAlso) ? "op_False" : "op_True";
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(this.Method.DeclaringType, name);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
			{
				parameterExpression
			}), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				Expression.Assign(parameterExpression, this.Left),
				Expression.Condition(Expression.Property(parameterExpression, "HasValue"), Expression.Condition(Expression.Call(booleanOperator, Expression.Call(parameterExpression, "GetValueOrDefault", null, Array.Empty<Expression>())), parameterExpression, Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression2
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression2, this.Right),
					Expression.Condition(Expression.Property(parameterExpression2, "HasValue"), Expression.Convert(Expression.Call(this.Method, Expression.Call(parameterExpression, "GetValueOrDefault", null, Array.Empty<Expression>()), Expression.Call(parameterExpression2, "GetValueOrDefault", null, Array.Empty<Expression>())), this.Type), Expression.Constant(null, this.Type))
				}))), Expression.Constant(null, this.Type))
			}));
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0000235B File Offset: 0x0000055B
		internal BinaryExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000913 RID: 2323
		[CompilerGenerated]
		private readonly Expression <Right>k__BackingField;

		// Token: 0x04000914 RID: 2324
		[CompilerGenerated]
		private readonly Expression <Left>k__BackingField;
	}
}
