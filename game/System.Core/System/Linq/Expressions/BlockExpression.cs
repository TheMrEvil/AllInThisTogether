using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Threading;

namespace System.Linq.Expressions
{
	/// <summary>Represents a block that contains a sequence of expressions where variables can be defined.</summary>
	// Token: 0x0200022E RID: 558
	[DebuggerTypeProxy(typeof(Expression.BlockExpressionProxy))]
	public class BlockExpression : Expression
	{
		/// <summary>Gets the expressions in this block.</summary>
		/// <returns>The read-only collection containing all the expressions in this block.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00034C5B File Offset: 0x00032E5B
		public ReadOnlyCollection<Expression> Expressions
		{
			get
			{
				return this.GetOrMakeExpressions();
			}
		}

		/// <summary>Gets the variables defined in this block.</summary>
		/// <returns>The read-only collection containing all the variables defined in this block.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00034C63 File Offset: 0x00032E63
		public ReadOnlyCollection<ParameterExpression> Variables
		{
			get
			{
				return this.GetOrMakeVariables();
			}
		}

		/// <summary>Gets the last expression in this block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the last expression in this block.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00034C6B File Offset: 0x00032E6B
		public Expression Result
		{
			get
			{
				return this.GetExpression(this.ExpressionCount - 1);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00034C7B File Offset: 0x00032E7B
		internal BlockExpression()
		{
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06000F4D RID: 3917 RVA: 0x00034C83 File Offset: 0x00032E83
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBlock(this);
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00034C8C File Offset: 0x00032E8C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Block;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.BlockExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00034C90 File Offset: 0x00032E90
		public override Type Type
		{
			get
			{
				return this.GetExpression(this.ExpressionCount - 1).Type;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="variables">The <see cref="P:System.Linq.Expressions.BlockExpression.Variables" /> property of the result. </param>
		/// <param name="expressions">The <see cref="P:System.Linq.Expressions.BlockExpression.Expressions" /> property of the result. </param>
		/// <returns>This expression if no children changed, or an expression with the updated children.</returns>
		// Token: 0x06000F50 RID: 3920 RVA: 0x00034CA8 File Offset: 0x00032EA8
		public BlockExpression Update(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			if (expressions != null)
			{
				ICollection<ParameterExpression> collection;
				if (variables == null)
				{
					collection = null;
				}
				else
				{
					collection = (variables as ICollection<ParameterExpression>);
					if (collection == null)
					{
						collection = (variables = variables.ToReadOnly<ParameterExpression>());
					}
				}
				if (this.SameVariables(collection))
				{
					ICollection<Expression> collection2 = expressions as ICollection<Expression>;
					if (collection2 == null)
					{
						collection2 = (expressions = expressions.ToReadOnly<Expression>());
					}
					if (this.SameExpressions(collection2))
					{
						return this;
					}
				}
			}
			return Expression.Block(this.Type, variables, expressions);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00034D08 File Offset: 0x00032F08
		internal virtual bool SameVariables(ICollection<ParameterExpression> variables)
		{
			return variables == null || variables.Count == 0;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual bool SameExpressions(ICollection<Expression> expressions)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual Expression GetExpression(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual int ExpressionCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00034D1F File Offset: 0x00032F1F
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00034D28 File Offset: 0x00032F28
		internal static ReadOnlyCollection<Expression> ReturnReadOnlyExpressions(BlockExpression provider, ref object collection)
		{
			Expression expression = collection as Expression;
			if (expression != null)
			{
				Interlocked.CompareExchange(ref collection, new ReadOnlyCollection<Expression>(new BlockExpressionList(provider, expression)), expression);
			}
			return (ReadOnlyCollection<Expression>)collection;
		}
	}
}
