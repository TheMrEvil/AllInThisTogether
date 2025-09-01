using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a try/catch/finally/fault block.</summary>
	// Token: 0x020002A0 RID: 672
	[DebuggerTypeProxy(typeof(Expression.TryExpressionProxy))]
	public sealed class TryExpression : Expression
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x0003D342 File Offset: 0x0003B542
		internal TryExpression(Type type, Expression body, Expression @finally, Expression fault, ReadOnlyCollection<CatchBlock> handlers)
		{
			this.Type = type;
			this.Body = body;
			this.Handlers = handlers;
			this.Finally = @finally;
			this.Fault = fault;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.TryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0003D36F File Offset: 0x0003B56F
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
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0003D377 File Offset: 0x0003B577
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Try;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the body of the try block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the body of the try block.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0003D37B File Offset: 0x0003B57B
		public Expression Body
		{
			[CompilerGenerated]
			get
			{
				return this.<Body>k__BackingField;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions associated with the try block.</summary>
		/// <returns>The collection of <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions associated with the try block.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0003D383 File Offset: 0x0003B583
		public ReadOnlyCollection<CatchBlock> Handlers
		{
			[CompilerGenerated]
			get
			{
				return this.<Handlers>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the finally block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the finally block.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0003D38B File Offset: 0x0003B58B
		public Expression Finally
		{
			[CompilerGenerated]
			get
			{
				return this.<Finally>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the fault block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the fault block.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0003D393 File Offset: 0x0003B593
		public Expression Fault
		{
			[CompilerGenerated]
			get
			{
				return this.<Fault>k__BackingField;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0003D39B File Offset: 0x0003B59B
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTry(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.TryExpression.Body" /> property of the result.</param>
		/// <param name="handlers">The <see cref="P:System.Linq.Expressions.TryExpression.Handlers" /> property of the result.</param>
		/// <param name="finally">The <see cref="P:System.Linq.Expressions.TryExpression.Finally" /> property of the result.</param>
		/// <param name="fault">The <see cref="P:System.Linq.Expressions.TryExpression.Fault" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060013FB RID: 5115 RVA: 0x0003D3A4 File Offset: 0x0003B5A4
		public TryExpression Update(Expression body, IEnumerable<CatchBlock> handlers, Expression @finally, Expression fault)
		{
			if ((body == this.Body & @finally == this.Finally & fault == this.Fault) && ExpressionUtils.SameElements<CatchBlock>(ref handlers, this.Handlers))
			{
				return this;
			}
			return Expression.MakeTry(this.Type, body, @finally, fault, handlers);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0000235B File Offset: 0x0000055B
		internal TryExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A62 RID: 2658
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x04000A63 RID: 2659
		[CompilerGenerated]
		private readonly Expression <Body>k__BackingField;

		// Token: 0x04000A64 RID: 2660
		[CompilerGenerated]
		private readonly ReadOnlyCollection<CatchBlock> <Handlers>k__BackingField;

		// Token: 0x04000A65 RID: 2661
		[CompilerGenerated]
		private readonly Expression <Finally>k__BackingField;

		// Token: 0x04000A66 RID: 2662
		[CompilerGenerated]
		private readonly Expression <Fault>k__BackingField;
	}
}
