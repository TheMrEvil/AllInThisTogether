using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a catch statement in a try block.</summary>
	// Token: 0x0200023A RID: 570
	[DebuggerTypeProxy(typeof(Expression.CatchBlockProxy))]
	public sealed class CatchBlock
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x00035626 File Offset: 0x00033826
		internal CatchBlock(Type test, ParameterExpression variable, Expression body, Expression filter)
		{
			this.Test = test;
			this.Variable = variable;
			this.Body = body;
			this.Filter = filter;
		}

		/// <summary>Gets a reference to the <see cref="T:System.Exception" /> object caught by this handler.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ParameterExpression" /> object representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003564B File Offset: 0x0003384B
		public ParameterExpression Variable
		{
			[CompilerGenerated]
			get
			{
				return this.<Variable>k__BackingField;
			}
		}

		/// <summary>Gets the type of <see cref="T:System.Exception" /> this handler catches.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of <see cref="T:System.Exception" /> this handler catches.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00035653 File Offset: 0x00033853
		public Type Test
		{
			[CompilerGenerated]
			get
			{
				return this.<Test>k__BackingField;
			}
		}

		/// <summary>Gets the body of the catch block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the catch body.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003565B File Offset: 0x0003385B
		public Expression Body
		{
			[CompilerGenerated]
			get
			{
				return this.<Body>k__BackingField;
			}
		}

		/// <summary>Gets the body of the <see cref="T:System.Linq.Expressions.CatchBlock" /> filter.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the body of the <see cref="T:System.Linq.Expressions.CatchBlock" /> filter.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00035663 File Offset: 0x00033863
		public Expression Filter
		{
			[CompilerGenerated]
			get
			{
				return this.<Filter>k__BackingField;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003566B File Offset: 0x0003386B
		public override string ToString()
		{
			return ExpressionStringBuilder.CatchBlockToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="variable">The <see cref="P:System.Linq.Expressions.CatchBlock.Variable" /> property of the result.</param>
		/// <param name="filter">The <see cref="P:System.Linq.Expressions.CatchBlock.Filter" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.CatchBlock.Body" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06000FA8 RID: 4008 RVA: 0x00035673 File Offset: 0x00033873
		public CatchBlock Update(ParameterExpression variable, Expression filter, Expression body)
		{
			if (variable == this.Variable && filter == this.Filter && body == this.Body)
			{
				return this;
			}
			return Expression.MakeCatchBlock(this.Test, variable, body, filter);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0000235B File Offset: 0x0000055B
		internal CatchBlock()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400095A RID: 2394
		[CompilerGenerated]
		private readonly ParameterExpression <Variable>k__BackingField;

		// Token: 0x0400095B RID: 2395
		[CompilerGenerated]
		private readonly Type <Test>k__BackingField;

		// Token: 0x0400095C RID: 2396
		[CompilerGenerated]
		private readonly Expression <Body>k__BackingField;

		// Token: 0x0400095D RID: 2397
		[CompilerGenerated]
		private readonly Expression <Filter>k__BackingField;
	}
}
