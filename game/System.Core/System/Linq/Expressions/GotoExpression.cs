using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an unconditional jump. This includes return statements, break and continue statements, and other jumps.</summary>
	// Token: 0x0200025C RID: 604
	[DebuggerTypeProxy(typeof(Expression.GotoExpressionProxy))]
	public sealed class GotoExpression : Expression
	{
		// Token: 0x060011AF RID: 4527 RVA: 0x0003A875 File Offset: 0x00038A75
		internal GotoExpression(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			this.Kind = kind;
			this.Value = value;
			this.Target = target;
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.GotoExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0003A89A File Offset: 0x00038A9A
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
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0003A8A2 File Offset: 0x00038AA2
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Goto;
			}
		}

		/// <summary>The value passed to the target, or null if the target is of type System.Void.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the value passed to the target or null.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0003A8A6 File Offset: 0x00038AA6
		public Expression Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		/// <summary>The target label where this node jumps to.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> object representing the target label for this node.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0003A8AE File Offset: 0x00038AAE
		public LabelTarget Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
		}

		/// <summary>The kind of the "go to" expression. Serves information purposes only.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.GotoExpressionKind" /> object representing the kind of the "go to" expression.</returns>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0003A8B6 File Offset: 0x00038AB6
		public GotoExpressionKind Kind
		{
			[CompilerGenerated]
			get
			{
				return this.<Kind>k__BackingField;
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003A8BE File Offset: 0x00038ABE
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitGoto(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="target">The <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property of the result. </param>
		/// <param name="value">The <see cref="P:System.Linq.Expressions.GotoExpression.Value" /> property of the result. </param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060011B6 RID: 4534 RVA: 0x0003A8C7 File Offset: 0x00038AC7
		public GotoExpression Update(LabelTarget target, Expression value)
		{
			if (target == this.Target && value == this.Value)
			{
				return this;
			}
			return Expression.MakeGoto(this.Kind, target, value, this.Type);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0000235B File Offset: 0x0000055B
		internal GotoExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040009F0 RID: 2544
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x040009F1 RID: 2545
		[CompilerGenerated]
		private readonly Expression <Value>k__BackingField;

		// Token: 0x040009F2 RID: 2546
		[CompilerGenerated]
		private readonly LabelTarget <Target>k__BackingField;

		// Token: 0x040009F3 RID: 2547
		[CompilerGenerated]
		private readonly GotoExpressionKind <Kind>k__BackingField;
	}
}
