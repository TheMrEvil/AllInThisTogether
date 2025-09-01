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
	/// <summary>Represents a constructor call.</summary>
	// Token: 0x0200028F RID: 655
	[DebuggerTypeProxy(typeof(Expression.NewExpressionProxy))]
	public class NewExpression : Expression, IArgumentProvider
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0003C504 File Offset: 0x0003A704
		internal NewExpression(ConstructorInfo constructor, IReadOnlyList<Expression> arguments, ReadOnlyCollection<MemberInfo> members)
		{
			this.Constructor = constructor;
			this._arguments = arguments;
			this.Members = members;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.NewExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0003C521 File Offset: 0x0003A721
		public override Type Type
		{
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0003C52E File Offset: 0x0003A72E
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.New;
			}
		}

		/// <summary>Gets the called constructor.</summary>
		/// <returns>The <see cref="T:System.Reflection.ConstructorInfo" /> that represents the called constructor.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0003C532 File Offset: 0x0003A732
		public ConstructorInfo Constructor
		{
			[CompilerGenerated]
			get
			{
				return this.<Constructor>k__BackingField;
			}
		}

		/// <summary>Gets the arguments to the constructor.</summary>
		/// <returns>A collection of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments to the constructor.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0003C53A File Offset: 0x0003A73A
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0003C547 File Offset: 0x0003A747
		public Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0003C555 File Offset: 0x0003A755
		public int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		/// <summary>Gets the members that can retrieve the values of the fields that were initialized with constructor arguments.</summary>
		/// <returns>A collection of <see cref="T:System.Reflection.MemberInfo" /> objects that represent the members that can retrieve the values of the fields that were initialized with constructor arguments.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0003C562 File Offset: 0x0003A762
		public ReadOnlyCollection<MemberInfo> Members
		{
			[CompilerGenerated]
			get
			{
				return this.<Members>k__BackingField;
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x060012FF RID: 4863 RVA: 0x0003C56A File Offset: 0x0003A76A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNew(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001300 RID: 4864 RVA: 0x0003C573 File Offset: 0x0003A773
		public NewExpression Update(IEnumerable<Expression> arguments)
		{
			if (ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			if (this.Members == null)
			{
				return Expression.New(this.Constructor, arguments);
			}
			return Expression.New(this.Constructor, arguments, this.Members);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0000235B File Offset: 0x0000055B
		internal NewExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A45 RID: 2629
		private IReadOnlyList<Expression> _arguments;

		// Token: 0x04000A46 RID: 2630
		[CompilerGenerated]
		private readonly ConstructorInfo <Constructor>k__BackingField;

		// Token: 0x04000A47 RID: 2631
		[CompilerGenerated]
		private readonly ReadOnlyCollection<MemberInfo> <Members>k__BackingField;
	}
}
