using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents creating a new array and possibly initializing the elements of the new array.</summary>
	// Token: 0x0200028C RID: 652
	[DebuggerTypeProxy(typeof(Expression.NewArrayExpressionProxy))]
	public class NewArrayExpression : Expression
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x0003C455 File Offset: 0x0003A655
		internal NewArrayExpression(Type type, ReadOnlyCollection<Expression> expressions)
		{
			this.Expressions = expressions;
			this.Type = type;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0003C46B File Offset: 0x0003A66B
		internal static NewArrayExpression Make(ExpressionType nodeType, Type type, ReadOnlyCollection<Expression> expressions)
		{
			if (nodeType == ExpressionType.NewArrayInit)
			{
				return new NewArrayInitExpression(type, expressions);
			}
			return new NewArrayBoundsExpression(type, expressions);
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.NewArrayExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0003C481 File Offset: 0x0003A681
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>Gets the bounds of the array if the value of the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" />, or the values to initialize the elements of the new array if the value of the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" />.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent either the bounds of the array or the initialization values.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0003C489 File Offset: 0x0003A689
		public ReadOnlyCollection<Expression> Expressions
		{
			[CompilerGenerated]
			get
			{
				return this.<Expressions>k__BackingField;
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x060012F0 RID: 4848 RVA: 0x0003C491 File Offset: 0x0003A691
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNewArray(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="expressions">The <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060012F1 RID: 4849 RVA: 0x0003C49C File Offset: 0x0003A69C
		public NewArrayExpression Update(IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			if (ExpressionUtils.SameElements<Expression>(ref expressions, this.Expressions))
			{
				return this;
			}
			if (this.NodeType != ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayBounds(this.Type.GetElementType(), expressions);
			}
			return Expression.NewArrayInit(this.Type.GetElementType(), expressions);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0000235B File Offset: 0x0000055B
		internal NewArrayExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A43 RID: 2627
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x04000A44 RID: 2628
		[CompilerGenerated]
		private readonly ReadOnlyCollection<Expression> <Expressions>k__BackingField;
	}
}
