using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.CSharp.Linq;

namespace Mono.CSharp
{
	// Token: 0x0200013A RID: 314
	public class CompletionMemberAccess : CompletingExpression
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x000410F7 File Offset: 0x0003F2F7
		public CompletionMemberAccess(Expression e, string partial_name, Location l)
		{
			this.expr = e;
			this.loc = l;
			this.partial_name = partial_name;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00041114 File Offset: 0x0003F314
		public CompletionMemberAccess(Expression e, string partial_name, TypeArguments targs, Location l)
		{
			this.expr = e;
			this.loc = l;
			this.partial_name = partial_name;
			this.targs = targs;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0004113C File Offset: 0x0003F33C
		protected override Expression DoResolve(ResolveContext rc)
		{
			SimpleName simpleName = this.expr as SimpleName;
			if (simpleName != null)
			{
				this.expr = simpleName.LookupNameExpression(rc, Expression.MemberLookupRestrictions.ExactArity | Expression.MemberLookupRestrictions.ReadAccess);
				if (this.expr is VariableReference || this.expr is ConstantExpr || this.expr is TransparentMemberAccess)
				{
					this.expr = this.expr.Resolve(rc);
				}
				else if (this.expr is TypeParameterExpr)
				{
					this.expr.Error_UnexpectedKind(rc, ResolveFlags.VariableOrValue | ResolveFlags.Type, simpleName.Location);
					this.expr = null;
				}
			}
			else
			{
				this.expr = this.expr.Resolve(rc, ResolveFlags.VariableOrValue | ResolveFlags.Type);
			}
			if (this.expr == null)
			{
				return null;
			}
			TypeSpec type = this.expr.Type;
			if (type.IsPointer || type.Kind == MemberKind.Void || type == InternalType.NullLiteral || type == InternalType.AnonymousMethod)
			{
				this.expr.Error_OperatorCannotBeApplied(rc, this.loc, ".", type);
				return null;
			}
			if (this.targs != null && !this.targs.Resolve(rc, true))
			{
				return null;
			}
			List<string> list = new List<string>();
			NamespaceExpression namespaceExpression = this.expr as NamespaceExpression;
			if (namespaceExpression != null)
			{
				string prefix;
				if (this.partial_name == null)
				{
					prefix = namespaceExpression.Namespace.Name;
				}
				else
				{
					prefix = namespaceExpression.Namespace.Name + "." + this.partial_name;
				}
				rc.CurrentMemberDefinition.GetCompletionStartingWith(prefix, list);
				if (this.partial_name != null)
				{
					list = (from l in list
					select l.Substring(this.partial_name.Length)).ToList<string>();
				}
			}
			else
			{
				IEnumerable<string> names = from l in MemberCache.GetCompletitionMembers(rc, type, this.partial_name)
				select l.Name;
				CompletingExpression.AppendResults(list, this.partial_name, names);
			}
			throw new CompletionResult((this.partial_name == null) ? "" : this.partial_name, list.Distinct<string>().ToArray<string>());
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0004132C File Offset: 0x0003F52C
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			CompletionMemberAccess completionMemberAccess = (CompletionMemberAccess)t;
			if (this.targs != null)
			{
				completionMemberAccess.targs = this.targs.Clone();
			}
			completionMemberAccess.expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0004136B File Offset: 0x0003F56B
		[CompilerGenerated]
		private string <DoResolve>b__5_0(string l)
		{
			return l.Substring(this.partial_name.Length);
		}

		// Token: 0x0400072C RID: 1836
		private Expression expr;

		// Token: 0x0400072D RID: 1837
		private string partial_name;

		// Token: 0x0400072E RID: 1838
		private TypeArguments targs;

		// Token: 0x02000388 RID: 904
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060026B8 RID: 9912 RVA: 0x000B6E7B File Offset: 0x000B507B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060026B9 RID: 9913 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x060026BA RID: 9914 RVA: 0x000B6E73 File Offset: 0x000B5073
			internal string <DoResolve>b__5_1(MemberSpec l)
			{
				return l.Name;
			}

			// Token: 0x04000F5A RID: 3930
			public static readonly CompletionMemberAccess.<>c <>9 = new CompletionMemberAccess.<>c();

			// Token: 0x04000F5B RID: 3931
			public static Func<MemberSpec, string> <>9__5_1;
		}
	}
}
