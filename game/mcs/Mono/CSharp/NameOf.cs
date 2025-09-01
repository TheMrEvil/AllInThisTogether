using System;

namespace Mono.CSharp
{
	// Token: 0x02000150 RID: 336
	internal class NameOf : StringConstant
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x00044662 File Offset: 0x00042862
		public NameOf(SimpleName name) : base(name.Location)
		{
			this.name = name;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00044677 File Offset: 0x00042877
		private static void Error_MethodGroupWithTypeArguments(ResolveContext rc, Location loc)
		{
			rc.Report.Error(8084, loc, "An argument to nameof operator cannot be method group with type arguments");
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0000225C File Offset: 0x0000045C
		protected override Expression DoResolve(ResolveContext rc)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00044690 File Offset: 0x00042890
		private bool ResolveArgumentExpression(ResolveContext rc, Expression expr)
		{
			SimpleName simpleName = expr as SimpleName;
			if (simpleName != null)
			{
				base.Value = simpleName.Name;
				if (rc.Module.Compiler.Settings.Version < LanguageVersion.V_6)
				{
					rc.Report.FeatureIsNotAvailable(rc.Module.Compiler, base.Location, "nameof operator");
				}
				Expression expression = simpleName.LookupNameExpression(rc, Expression.MemberLookupRestrictions.IgnoreAmbiguity | Expression.MemberLookupRestrictions.NameOfExcluded);
				if (simpleName.HasTypeArguments && expression is MethodGroupExpr)
				{
					NameOf.Error_MethodGroupWithTypeArguments(rc, expr.Location);
				}
				return true;
			}
			MemberAccess memberAccess = expr as MemberAccess;
			if (memberAccess == null)
			{
				rc.Report.Error(8081, this.loc, "Expression does not have a name");
				return false;
			}
			Expression leftExpression = memberAccess.LeftExpression;
			Expression expression2 = memberAccess.LookupNameExpression(rc, Expression.MemberLookupRestrictions.IgnoreAmbiguity);
			if (expression2 == null)
			{
				return false;
			}
			if (rc.Module.Compiler.Settings.Version < LanguageVersion.V_6)
			{
				rc.Report.FeatureIsNotAvailable(rc.Module.Compiler, base.Location, "nameof operator");
			}
			if (memberAccess is QualifiedAliasMember)
			{
				rc.Report.Error(8083, this.loc, "An alias-qualified name is not an expression");
				return false;
			}
			if (!NameOf.IsLeftExpressionValid(leftExpression))
			{
				rc.Report.Error(8082, leftExpression.Location, "An argument to nameof operator cannot include sub-expression");
				return false;
			}
			MethodGroupExpr methodGroupExpr = expression2 as MethodGroupExpr;
			if (methodGroupExpr != null)
			{
				ExtensionMethodGroupExpr extensionMethodGroupExpr = expression2 as ExtensionMethodGroupExpr;
				if (extensionMethodGroupExpr != null && !extensionMethodGroupExpr.ResolveNameOf(rc, memberAccess))
				{
					return true;
				}
				if (!methodGroupExpr.HasAccessibleCandidate(rc))
				{
					Expression.ErrorIsInaccesible(rc, memberAccess.GetSignatureForError(), this.loc);
				}
				if (memberAccess.HasTypeArguments)
				{
					NameOf.Error_MethodGroupWithTypeArguments(rc, memberAccess.Location);
				}
			}
			base.Value = memberAccess.Name;
			return true;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00044840 File Offset: 0x00042A40
		private static bool IsLeftExpressionValid(Expression expr)
		{
			if (expr is SimpleName)
			{
				return true;
			}
			if (expr is This)
			{
				return true;
			}
			if (expr is NamespaceExpression)
			{
				return true;
			}
			if (expr is TypeExpr)
			{
				return true;
			}
			MemberAccess memberAccess = expr as MemberAccess;
			return memberAccess != null && NameOf.IsLeftExpressionValid(memberAccess.LeftExpression);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0004488C File Offset: 0x00042A8C
		public Expression ResolveOverload(ResolveContext rc, Arguments args)
		{
			if (args == null || args.Count != 1)
			{
				this.name.Error_NameDoesNotExist(rc);
				return null;
			}
			Argument argument = args[0];
			if (!this.ResolveArgumentExpression(rc, argument.Expr))
			{
				return null;
			}
			this.type = rc.BuiltinTypes.String;
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x04000744 RID: 1860
		private readonly SimpleName name;
	}
}
