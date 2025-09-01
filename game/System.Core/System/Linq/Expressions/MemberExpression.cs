using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Represents accessing a field or property.</summary>
	// Token: 0x02000278 RID: 632
	[DebuggerTypeProxy(typeof(Expression.MemberExpressionProxy))]
	public class MemberExpression : Expression
	{
		/// <summary>Gets the field or property to be accessed.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be accessed.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0003B5B4 File Offset: 0x000397B4
		public MemberInfo Member
		{
			get
			{
				return this.GetMember();
			}
		}

		/// <summary>Gets the containing object of the field or property.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the containing object of the field or property.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0003B5BC File Offset: 0x000397BC
		public Expression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003B5C4 File Offset: 0x000397C4
		internal MemberExpression(Expression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0003B5D3 File Offset: 0x000397D3
		internal static PropertyExpression Make(Expression expression, PropertyInfo property)
		{
			return new PropertyExpression(expression, property);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003B5DC File Offset: 0x000397DC
		internal static FieldExpression Make(Expression expression, FieldInfo field)
		{
			return new FieldExpression(expression, field);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0003B5E8 File Offset: 0x000397E8
		internal static MemberExpression Make(Expression expression, MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (!(fieldInfo == null))
			{
				return MemberExpression.Make(expression, fieldInfo);
			}
			return MemberExpression.Make(expression, (PropertyInfo)member);
		}

		/// <summary>Returns the node type of this <see cref="P:System.Linq.Expressions.MemberExpression.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0003B619 File Offset: 0x00039819
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.MemberAccess;
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual MemberInfo GetMember()
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06001273 RID: 4723 RVA: 0x0003B61D File Offset: 0x0003981D
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMember(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property of the result.</param>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		// Token: 0x06001274 RID: 4724 RVA: 0x0003B626 File Offset: 0x00039826
		public MemberExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.MakeMemberAccess(expression, this.Member);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemberExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A23 RID: 2595
		[CompilerGenerated]
		private readonly Expression <Expression>k__BackingField;
	}
}
