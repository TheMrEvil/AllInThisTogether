using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents one case of a <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
	// Token: 0x0200029C RID: 668
	[DebuggerTypeProxy(typeof(Expression.SwitchCaseProxy))]
	public sealed class SwitchCase
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x0003D0E1 File Offset: 0x0003B2E1
		internal SwitchCase(Expression body, ReadOnlyCollection<Expression> testValues)
		{
			this.Body = body;
			this.TestValues = testValues;
		}

		/// <summary>Gets the values of this case. This case is selected for execution when the <see cref="P:System.Linq.Expressions.SwitchExpression.SwitchValue" /> matches any of these values.</summary>
		/// <returns>The read-only collection of the values for this case block.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0003D0F7 File Offset: 0x0003B2F7
		public ReadOnlyCollection<Expression> TestValues
		{
			[CompilerGenerated]
			get
			{
				return this.<TestValues>k__BackingField;
			}
		}

		/// <summary>Gets the body of this case.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object that represents the body of the case block.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0003D0FF File Offset: 0x0003B2FF
		public Expression Body
		{
			[CompilerGenerated]
			get
			{
				return this.<Body>k__BackingField;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060013D8 RID: 5080 RVA: 0x0003D107 File Offset: 0x0003B307
		public override string ToString()
		{
			return ExpressionStringBuilder.SwitchCaseToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="testValues">The <see cref="P:System.Linq.Expressions.SwitchCase.TestValues" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.SwitchCase.Body" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060013D9 RID: 5081 RVA: 0x0003D10F File Offset: 0x0003B30F
		public SwitchCase Update(IEnumerable<Expression> testValues, Expression body)
		{
			if ((body == this.Body & testValues != null) && ExpressionUtils.SameElements<Expression>(ref testValues, this.TestValues))
			{
				return this;
			}
			return Expression.SwitchCase(body, testValues);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0000235B File Offset: 0x0000055B
		internal SwitchCase()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A56 RID: 2646
		[CompilerGenerated]
		private readonly ReadOnlyCollection<Expression> <TestValues>k__BackingField;

		// Token: 0x04000A57 RID: 2647
		[CompilerGenerated]
		private readonly Expression <Body>k__BackingField;
	}
}
