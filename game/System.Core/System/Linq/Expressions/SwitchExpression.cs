using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents a control expression that handles multiple selections by passing control to <see cref="T:System.Linq.Expressions.SwitchCase" />.</summary>
	// Token: 0x0200029D RID: 669
	[DebuggerTypeProxy(typeof(Expression.SwitchExpressionProxy))]
	public sealed class SwitchExpression : Expression
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x0003D139 File Offset: 0x0003B339
		internal SwitchExpression(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, ReadOnlyCollection<SwitchCase> cases)
		{
			this.Type = type;
			this.SwitchValue = switchValue;
			this.DefaultBody = defaultBody;
			this.Comparison = comparison;
			this.Cases = cases;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.SwitchExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0003D166 File Offset: 0x0003B366
		public sealed override Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0003D16E File Offset: 0x0003B36E
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Switch;
			}
		}

		/// <summary>Gets the test for the switch.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the test for the switch.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0003D172 File Offset: 0x0003B372
		public Expression SwitchValue
		{
			[CompilerGenerated]
			get
			{
				return this.<SwitchValue>k__BackingField;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Linq.Expressions.SwitchCase" /> objects for the switch.</summary>
		/// <returns>The collection of <see cref="T:System.Linq.Expressions.SwitchCase" /> objects.</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0003D17A File Offset: 0x0003B37A
		public ReadOnlyCollection<SwitchCase> Cases
		{
			[CompilerGenerated]
			get
			{
				return this.<Cases>k__BackingField;
			}
		}

		/// <summary>Gets the test for the switch.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the test for the switch.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0003D182 File Offset: 0x0003B382
		public Expression DefaultBody
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultBody>k__BackingField;
			}
		}

		/// <summary>Gets the equality comparison method, if any.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object representing the equality comparison method.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0003D18A File Offset: 0x0003B38A
		public MethodInfo Comparison
		{
			[CompilerGenerated]
			get
			{
				return this.<Comparison>k__BackingField;
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0003D192 File Offset: 0x0003B392
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitSwitch(this);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0003D19C File Offset: 0x0003B39C
		internal bool IsLifted
		{
			get
			{
				return this.SwitchValue.Type.IsNullableType() && (this.Comparison == null || !TypeUtils.AreEquivalent(this.SwitchValue.Type, this.Comparison.GetParametersCached()[0].ParameterType.GetNonRefType()));
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="switchValue">The <see cref="P:System.Linq.Expressions.SwitchExpression.SwitchValue" /> property of the result.</param>
		/// <param name="cases">The <see cref="P:System.Linq.Expressions.SwitchExpression.Cases" /> property of the result.</param>
		/// <param name="defaultBody">The <see cref="P:System.Linq.Expressions.SwitchExpression.DefaultBody" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x060013E4 RID: 5092 RVA: 0x0003D1F8 File Offset: 0x0003B3F8
		public SwitchExpression Update(Expression switchValue, IEnumerable<SwitchCase> cases, Expression defaultBody)
		{
			if ((switchValue == this.SwitchValue & defaultBody == this.DefaultBody & cases != null) && ExpressionUtils.SameElements<SwitchCase>(ref cases, this.Cases))
			{
				return this;
			}
			return Expression.Switch(this.Type, switchValue, defaultBody, this.Comparison, cases);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0000235B File Offset: 0x0000055B
		internal SwitchExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A58 RID: 2648
		[CompilerGenerated]
		private readonly Type <Type>k__BackingField;

		// Token: 0x04000A59 RID: 2649
		[CompilerGenerated]
		private readonly Expression <SwitchValue>k__BackingField;

		// Token: 0x04000A5A RID: 2650
		[CompilerGenerated]
		private readonly ReadOnlyCollection<SwitchCase> <Cases>k__BackingField;

		// Token: 0x04000A5B RID: 2651
		[CompilerGenerated]
		private readonly Expression <DefaultBody>k__BackingField;

		// Token: 0x04000A5C RID: 2652
		[CompilerGenerated]
		private readonly MethodInfo <Comparison>k__BackingField;
	}
}
