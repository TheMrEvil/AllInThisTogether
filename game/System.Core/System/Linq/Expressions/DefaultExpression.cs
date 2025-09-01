using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents the default value of a type or an empty expression.</summary>
	// Token: 0x02000248 RID: 584
	[DebuggerTypeProxy(typeof(Expression.DefaultExpressionProxy))]
	public sealed class DefaultExpression : Expression
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x00037395 File Offset: 0x00035595
		internal DefaultExpression(Type type)
		{
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.DefaultExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000373A4 File Offset: 0x000355A4
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
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000373AC File Offset: 0x000355AC
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Default;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000373B0 File Offset: 0x000355B0
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDefault(this);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0000235B File Offset: 0x0000055B
		internal DefaultExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400097E RID: 2430
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;
	}
}
