using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an infinite loop. It can be exited with "break".</summary>
	// Token: 0x02000274 RID: 628
	[DebuggerTypeProxy(typeof(Expression.LoopExpressionProxy))]
	public sealed class LoopExpression : Expression
	{
		// Token: 0x06001258 RID: 4696 RVA: 0x0003B4C3 File Offset: 0x000396C3
		internal LoopExpression(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			this.Body = body;
			this.BreakLabel = @break;
			this.ContinueLabel = @continue;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LoopExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0003B4E0 File Offset: 0x000396E0
		public sealed override Type Type
		{
			get
			{
				if (this.BreakLabel != null)
				{
					return this.BreakLabel.Type;
				}
				return typeof(void);
			}
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0003B500 File Offset: 0x00039700
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Loop;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> that is the body of the loop.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is the body of the loop.</returns>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0003B504 File Offset: 0x00039704
		public Expression Body
		{
			[CompilerGenerated]
			get
			{
				return this.<Body>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a break statement target.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a break statement target.</returns>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0003B50C File Offset: 0x0003970C
		public LabelTarget BreakLabel
		{
			[CompilerGenerated]
			get
			{
				return this.<BreakLabel>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a continue statement target.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a continue statement target.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0003B514 File Offset: 0x00039714
		public LabelTarget ContinueLabel
		{
			[CompilerGenerated]
			get
			{
				return this.<ContinueLabel>k__BackingField;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003B51C File Offset: 0x0003971C
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLoop(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="breakLabel">The <see cref="P:System.Linq.Expressions.LoopExpression.BreakLabel" /> property of the result.</param>
		/// <param name="continueLabel">The <see cref="P:System.Linq.Expressions.LoopExpression.ContinueLabel" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.LoopExpression.Body" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x0600125F RID: 4703 RVA: 0x0003B525 File Offset: 0x00039725
		public LoopExpression Update(LabelTarget breakLabel, LabelTarget continueLabel, Expression body)
		{
			if (breakLabel == this.BreakLabel && continueLabel == this.ContinueLabel && body == this.Body)
			{
				return this;
			}
			return Expression.Loop(body, breakLabel, continueLabel);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0000235B File Offset: 0x0000055B
		internal LoopExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A19 RID: 2585
		[CompilerGenerated]
		private readonly Expression <Body>k__BackingField;

		// Token: 0x04000A1A RID: 2586
		[CompilerGenerated]
		private readonly LabelTarget <BreakLabel>k__BackingField;

		// Token: 0x04000A1B RID: 2587
		[CompilerGenerated]
		private readonly LabelTarget <ContinueLabel>k__BackingField;
	}
}
