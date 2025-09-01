using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a call to either static or an instance method.</summary>
	// Token: 0x0200027E RID: 638
	[DebuggerTypeProxy(typeof(Expression.MethodCallExpressionProxy))]
	public class MethodCallExpression : Expression, IArgumentProvider
	{
		// Token: 0x06001293 RID: 4755 RVA: 0x0003B8EA File Offset: 0x00039AEA
		internal MethodCallExpression(MethodInfo method)
		{
			this.Method = method;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0000392D File Offset: 0x00001B2D
		internal virtual Expression GetInstance()
		{
			return null;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0003B8F9 File Offset: 0x00039AF9
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Call;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.MethodCallExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0003B8FC File Offset: 0x00039AFC
		public sealed override Type Type
		{
			get
			{
				return this.Method.ReturnType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodInfo" /> for the method to be called.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the called method.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0003B909 File Offset: 0x00039B09
		public MethodInfo Method
		{
			[CompilerGenerated]
			get
			{
				return this.<Method>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> that represents the instance for instance method calls or null for static method calls.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the receiving object of the method.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0003B911 File Offset: 0x00039B11
		public Expression Object
		{
			get
			{
				return this.GetInstance();
			}
		}

		/// <summary>Gets a collection of expressions that represent arguments of the called method.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent the arguments to the called method.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0003B919 File Offset: 0x00039B19
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="object">The <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property of the result.</param>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x0600129A RID: 4762 RVA: 0x0003B924 File Offset: 0x00039B24
		public MethodCallExpression Update(Expression @object, IEnumerable<Expression> arguments)
		{
			if (@object == this.Object)
			{
				ICollection<Expression> collection;
				if (arguments == null)
				{
					collection = null;
				}
				else
				{
					collection = (arguments as ICollection<Expression>);
					if (collection == null)
					{
						collection = (arguments = arguments.ToReadOnly<Expression>());
					}
				}
				if (this.SameArguments(collection))
				{
					return this;
				}
			}
			return Expression.Call(@object, this.Method, arguments);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual bool SameArguments(ICollection<Expression> arguments)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x0600129D RID: 4765 RVA: 0x0003B96D File Offset: 0x00039B6D
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMethodCall(this);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual Expression GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual int ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0000235B File Offset: 0x0000055B
		internal MethodCallExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A2A RID: 2602
		[CompilerGenerated]
		private readonly MethodInfo <Method>k__BackingField;
	}
}
