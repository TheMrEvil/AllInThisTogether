using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a label, which can be put in any <see cref="T:System.Linq.Expressions.Expression" /> context. If it is jumped to, it will get the value provided by the corresponding <see cref="T:System.Linq.Expressions.GotoExpression" />. Otherwise, it receives the value in <see cref="P:System.Linq.Expressions.LabelExpression.DefaultValue" />. If the <see cref="T:System.Type" /> equals System.Void, no value should be provided.</summary>
	// Token: 0x02000269 RID: 617
	[DebuggerTypeProxy(typeof(Expression.LabelExpressionProxy))]
	public sealed class LabelExpression : Expression
	{
		// Token: 0x060011FB RID: 4603 RVA: 0x0003AE13 File Offset: 0x00039013
		internal LabelExpression(LabelTarget label, Expression defaultValue)
		{
			this.Target = label;
			this.DefaultValue = defaultValue;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LabelExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0003AE29 File Offset: 0x00039029
		public sealed override Type Type
		{
			get
			{
				return this.Target.Type;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0003AE36 File Offset: 0x00039036
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Label;
			}
		}

		/// <summary>The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this label is associated with.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this label is associated with.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0003AE3A File Offset: 0x0003903A
		public LabelTarget Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
		}

		/// <summary>The value of the <see cref="T:System.Linq.Expressions.LabelExpression" /> when the label is reached through regular control flow (for example, is not jumped to).</summary>
		/// <returns>The Expression object representing the value of the <see cref="T:System.Linq.Expressions.LabelExpression" />.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0003AE42 File Offset: 0x00039042
		public Expression DefaultValue
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultValue>k__BackingField;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0003AE4A File Offset: 0x0003904A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLabel(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="target">The <see cref="P:System.Linq.Expressions.LabelExpression.Target" /> property of the result.</param>
		/// <param name="defaultValue">The <see cref="P:System.Linq.Expressions.LabelExpression.DefaultValue" /> property of the result</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001201 RID: 4609 RVA: 0x0003AE53 File Offset: 0x00039053
		public LabelExpression Update(LabelTarget target, Expression defaultValue)
		{
			if (target == this.Target && defaultValue == this.DefaultValue)
			{
				return this;
			}
			return Expression.Label(target, defaultValue);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0000235B File Offset: 0x0000055B
		internal LabelExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A09 RID: 2569
		[CompilerGenerated]
		private readonly LabelTarget <Target>k__BackingField;

		// Token: 0x04000A0A RID: 2570
		[CompilerGenerated]
		private readonly Expression <DefaultValue>k__BackingField;
	}
}
