using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a constant value.</summary>
	// Token: 0x02000241 RID: 577
	[DebuggerTypeProxy(typeof(Expression.ConstantExpressionProxy))]
	public class ConstantExpression : Expression
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x00035893 File Offset: 0x00033A93
		internal ConstantExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ConstantExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000358A2 File Offset: 0x00033AA2
		public override Type Type
		{
			get
			{
				if (this.Value == null)
				{
					return typeof(object);
				}
				return this.Value.GetType();
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000358C2 File Offset: 0x00033AC2
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Constant;
			}
		}

		/// <summary>Gets the value of the constant expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> equal to the value of the represented expression.</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000358C6 File Offset: 0x00033AC6
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06000FC1 RID: 4033 RVA: 0x000358CE File Offset: 0x00033ACE
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConstant(this);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0000235B File Offset: 0x0000055B
		internal ConstantExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000967 RID: 2407
		[CompilerGenerated]
		private readonly object <Value>k__BackingField;
	}
}
