using System;
using System.Reflection;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents assignment operation for a field or property of an object.</summary>
	// Token: 0x02000275 RID: 629
	public sealed class MemberAssignment : MemberBinding
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x0003B54C File Offset: 0x0003974C
		internal MemberAssignment(MemberInfo member, Expression expression) : base(MemberBindingType.Assignment, member)
		{
			this._expression = expression;
		}

		/// <summary>Gets the expression to assign to the field or property.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that represents the value to assign to the field or property.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0003B55D File Offset: 0x0003975D
		public Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001263 RID: 4707 RVA: 0x0003B565 File Offset: 0x00039765
		public MemberAssignment Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.Bind(base.Member, expression);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00003A59 File Offset: 0x00001C59
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemberAssignment()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A1C RID: 2588
		private readonly Expression _expression;
	}
}
