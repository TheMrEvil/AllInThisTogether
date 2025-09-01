using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>An expression that provides runtime read/write permission for variables.</summary>
	// Token: 0x02000295 RID: 661
	[DebuggerTypeProxy(typeof(Expression.RuntimeVariablesExpressionProxy))]
	public sealed class RuntimeVariablesExpression : Expression
	{
		// Token: 0x06001313 RID: 4883 RVA: 0x0003C75A File Offset: 0x0003A95A
		internal RuntimeVariablesExpression(ReadOnlyCollection<ParameterExpression> variables)
		{
			this.Variables = variables;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0003C769 File Offset: 0x0003A969
		public sealed override Type Type
		{
			get
			{
				return typeof(IRuntimeVariables);
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0003C775 File Offset: 0x0003A975
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.RuntimeVariables;
			}
		}

		/// <summary>The variables or parameters to which to provide runtime access.</summary>
		/// <returns>The read-only collection containing parameters that will be provided the runtime access.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0003C779 File Offset: 0x0003A979
		public ReadOnlyCollection<ParameterExpression> Variables
		{
			[CompilerGenerated]
			get
			{
				return this.<Variables>k__BackingField;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0003C781 File Offset: 0x0003A981
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitRuntimeVariables(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="variables">The <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001318 RID: 4888 RVA: 0x0003C78A File Offset: 0x0003A98A
		public RuntimeVariablesExpression Update(IEnumerable<ParameterExpression> variables)
		{
			if (variables != null && ExpressionUtils.SameElements<ParameterExpression>(ref variables, this.Variables))
			{
				return this;
			}
			return Expression.RuntimeVariables(variables);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0000235B File Offset: 0x0000055B
		internal RuntimeVariablesExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A4B RID: 2635
		[CompilerGenerated]
		private readonly ReadOnlyCollection<ParameterExpression> <Variables>k__BackingField;
	}
}
