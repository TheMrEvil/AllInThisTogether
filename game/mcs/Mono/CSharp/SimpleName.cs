using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020001B6 RID: 438
	public class SimpleName : ATypeNameExpression
	{
		// Token: 0x060016F7 RID: 5879 RVA: 0x0006D673 File Offset: 0x0006B873
		public SimpleName(string name, Location l) : base(name, l)
		{
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0006D67D File Offset: 0x0006B87D
		public SimpleName(string name, TypeArguments args, Location l) : base(name, args, l)
		{
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0006D688 File Offset: 0x0006B888
		public SimpleName(string name, int arity, Location l) : base(name, arity, l)
		{
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0006D693 File Offset: 0x0006B893
		public SimpleName GetMethodGroup()
		{
			return new SimpleName(base.Name, this.targs, this.loc);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0006D6AC File Offset: 0x0006B8AC
		protected override Expression DoResolve(ResolveContext rc)
		{
			return this.SimpleNameResolve(rc, null);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0006D6B6 File Offset: 0x0006B8B6
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			return this.SimpleNameResolve(ec, right_side);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0006D6C0 File Offset: 0x0006B8C0
		public void Error_NameDoesNotExist(ResolveContext rc)
		{
			rc.Report.Error(103, this.loc, "The name `{0}' does not exist in the current context", base.Name);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0006D6E0 File Offset: 0x0006B8E0
		protected virtual void Error_TypeOrNamespaceNotFound(IMemberContext ctx)
		{
			if (ctx.CurrentType != null)
			{
				MemberExpr memberExpr = Expression.MemberLookup(ctx, false, ctx.CurrentType, base.Name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc) as MemberExpr;
				if (memberExpr != null)
				{
					Expression.Error_UnexpectedKind(ctx, memberExpr, "type", memberExpr.KindName, this.loc);
					return;
				}
			}
			Report report = ctx.Module.Compiler.Report;
			FullNamedExpression fullNamedExpression = ctx.LookupNamespaceOrType(base.Name, base.Arity, LookupMode.IgnoreAccessibility, this.loc);
			if (fullNamedExpression != null)
			{
				report.SymbolRelatedToPreviousError(fullNamedExpression.Type);
				Expression.ErrorIsInaccesible(ctx, fullNamedExpression.GetSignatureForError(), this.loc);
				return;
			}
			fullNamedExpression = ctx.LookupNamespaceOrType(base.Name, -Math.Max(1, base.Arity), LookupMode.Probing, this.loc);
			if (fullNamedExpression != null)
			{
				base.Error_TypeArgumentsCannotBeUsed(ctx, fullNamedExpression.Type, this.loc);
				return;
			}
			List<string> list = ctx.Module.GlobalRootNamespace.FindTypeNamespaces(ctx, base.Name, base.Arity);
			if (list == null)
			{
				report.Error(246, this.loc, "The type or namespace name `{0}' could not be found. Are you missing an assembly reference?", base.Name);
				return;
			}
			if (ctx is UsingAliasNamespace.AliasContext)
			{
				report.Error(246, this.loc, "The type or namespace name `{1}' could not be found. Consider using fully qualified name `{0}.{1}'", list[0], base.Name);
				return;
			}
			string arg = string.Join("' or `", list.ToArray());
			report.Error(246, this.loc, "The type or namespace name `{0}' could not be found. Are you missing `{1}' using directive?", base.Name, arg);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0006D854 File Offset: 0x0006BA54
		public override FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext mc, bool allowUnboundTypeArguments)
		{
			FullNamedExpression fullNamedExpression = mc.LookupNamespaceOrType(base.Name, base.Arity, LookupMode.Normal, this.loc);
			if (fullNamedExpression != null)
			{
				if (fullNamedExpression.Type != null && base.Arity > 0)
				{
					if (!base.HasTypeArguments)
					{
						this.targs.Resolve(mc, allowUnboundTypeArguments);
						return new GenericOpenTypeExpr(fullNamedExpression.Type, this.loc);
					}
					GenericTypeExpr genericTypeExpr = new GenericTypeExpr(fullNamedExpression.Type, this.targs, this.loc);
					if (genericTypeExpr.ResolveAsType(mc, false) == null)
					{
						return null;
					}
					return genericTypeExpr;
				}
				else if (!(fullNamedExpression is NamespaceExpression))
				{
					return fullNamedExpression;
				}
			}
			if (base.Arity == 0 && base.Name == "dynamic" && !(mc is NamespaceContainer) && mc.Module.Compiler.Settings.Version > LanguageVersion.V_3)
			{
				if (!mc.Module.PredefinedAttributes.Dynamic.IsDefined)
				{
					mc.Module.Compiler.Report.Error(1980, base.Location, "Dynamic keyword requires `{0}' to be defined. Are you missing System.Core.dll assembly reference?", mc.Module.PredefinedAttributes.Dynamic.GetSignatureForError());
				}
				fullNamedExpression = new DynamicTypeExpr(this.loc);
				fullNamedExpression.ResolveAsType(mc, false);
			}
			if (fullNamedExpression != null)
			{
				return fullNamedExpression;
			}
			this.Error_TypeOrNamespaceNotFound(mc);
			return null;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0006D99A File Offset: 0x0006BB9A
		public bool IsPossibleTypeOrNamespace(IMemberContext mc)
		{
			return mc.LookupNamespaceOrType(base.Name, base.Arity, LookupMode.Probing, this.loc) != null;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0006D9B8 File Offset: 0x0006BBB8
		public bool IsPossibleType(IMemberContext mc)
		{
			return mc.LookupNamespaceOrType(base.Name, base.Arity, LookupMode.Probing, this.loc) is TypeExpr;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0006D9DC File Offset: 0x0006BBDC
		public override Expression LookupNameExpression(ResolveContext rc, Expression.MemberLookupRestrictions restrictions)
		{
			int num = base.Arity;
			bool flag = false;
			Block currentBlock = rc.CurrentBlock;
			INamedBlockVariable namedBlockVariable = null;
			bool flag2 = false;
			Expression expression;
			MemberExpr memberExpr;
			PropertyExpr propertyExpr;
			Expression expression2;
			Tuple<FieldSpec, FieldInfo> tuple;
			for (;;)
			{
				if (currentBlock != null && num == 0 && currentBlock.ParametersBlock.TopBlock.GetLocalName(base.Name, currentBlock.Original, ref namedBlockVariable))
				{
					if (!namedBlockVariable.IsDeclared)
					{
						flag = true;
						flag2 = true;
					}
					else
					{
						expression = namedBlockVariable.CreateReferenceExpression(rc, this.loc);
						if (expression != null)
						{
							break;
						}
					}
				}
				for (TypeSpec typeSpec = rc.CurrentType; typeSpec != null; typeSpec = typeSpec.DeclaringType)
				{
					expression = Expression.MemberLookup(rc, flag, typeSpec, base.Name, num, restrictions, this.loc);
					if (expression != null)
					{
						memberExpr = (expression as MemberExpr);
						if (memberExpr == null)
						{
							if (expression is TypeExpr)
							{
								break;
							}
						}
						else if (flag)
						{
							if (namedBlockVariable == null)
							{
								goto IL_125;
							}
							if (memberExpr is FieldExpr || memberExpr is ConstantExpr || memberExpr is EventExpr || memberExpr is PropertyExpr)
							{
								goto IL_F8;
							}
							break;
						}
						else
						{
							propertyExpr = (memberExpr as PropertyExpr);
							if (propertyExpr == null)
							{
								goto IL_249;
							}
							if ((restrictions & Expression.MemberLookupRestrictions.ReadAccess) != Expression.MemberLookupRestrictions.None)
							{
								if (propertyExpr.PropertyInfo.HasGet && propertyExpr.PropertyInfo.Get.IsAccessible(rc))
								{
									goto Block_21;
								}
								break;
							}
							else
							{
								if (rc.HasSet(ResolveContext.Options.ConstructorScope) && propertyExpr.IsAutoPropertyAccess && propertyExpr.PropertyInfo.DeclaringType == rc.CurrentType && propertyExpr.IsStatic == rc.IsStatic)
								{
									goto Block_25;
								}
								if (!propertyExpr.PropertyInfo.HasSet || !propertyExpr.PropertyInfo.Set.IsAccessible(rc))
								{
									flag2 = true;
									break;
								}
								goto IL_237;
							}
						}
					}
				}
				if ((restrictions & Expression.MemberLookupRestrictions.InvocableOnly) == Expression.MemberLookupRestrictions.None && !flag2 && this.IsPossibleTypeOrNamespace(rc))
				{
					goto Block_30;
				}
				expression2 = NamespaceContainer.LookupStaticUsings(rc, base.Name, base.Arity, this.loc);
				if (expression2 != null)
				{
					goto Block_31;
				}
				if ((restrictions & Expression.MemberLookupRestrictions.NameOfExcluded) == Expression.MemberLookupRestrictions.None && base.Name == "nameof")
				{
					goto Block_35;
				}
				if (flag)
				{
					goto Block_36;
				}
				if (rc.Module.Evaluator != null)
				{
					tuple = rc.Module.Evaluator.LookupField(base.Name);
					if (tuple != null)
					{
						goto Block_52;
					}
				}
				num = 0;
				flag = true;
			}
			if (base.Arity > 0)
			{
				Expression.Error_TypeArgumentsCannotBeUsed(rc, "variable", base.Name, this.loc);
			}
			return expression;
			IL_F8:
			rc.Report.Error(844, this.loc, "A local variable `{0}' cannot be used before it is declared. Consider renaming the local variable when it hides the member `{1}'", base.Name, memberExpr.GetSignatureForError());
			goto IL_249;
			IL_125:
			if (!(memberExpr is MethodGroupExpr) && !(memberExpr is PropertyExpr) && !(memberExpr is IndexerExpr))
			{
				Expression.ErrorIsInaccesible(rc, memberExpr.GetSignatureForError(), this.loc);
				goto IL_249;
			}
			goto IL_249;
			Block_21:
			PropertyExpr propertyExpr2 = propertyExpr;
			propertyExpr2.Getter = propertyExpr2.PropertyInfo.Get;
			goto IL_249;
			Block_25:
			return new FieldExpr(((Property)propertyExpr.PropertyInfo.MemberDefinition).BackingField, this.loc);
			IL_237:
			PropertyExpr propertyExpr3 = propertyExpr;
			propertyExpr3.Setter = propertyExpr3.PropertyInfo.Set;
			IL_249:
			memberExpr = memberExpr.ResolveMemberAccess(rc, null, null);
			if (base.Arity > 0)
			{
				this.targs.Resolve(rc, false);
				memberExpr.SetTypeArguments(rc, this.targs);
			}
			return memberExpr;
			Block_30:
			return this.ResolveAsTypeOrNamespace(rc, false);
			Block_31:
			if (base.Arity > 0)
			{
				this.targs.Resolve(rc, false);
				MemberExpr memberExpr2 = expression2 as MemberExpr;
				if (memberExpr2 != null)
				{
					memberExpr2.SetTypeArguments(rc, this.targs);
				}
			}
			return expression2;
			Block_35:
			return new NameOf(this);
			Block_36:
			if (flag2)
			{
				rc.Report.Error(841, this.loc, "A local variable `{0}' cannot be used before it is declared", base.Name);
			}
			else
			{
				if (base.Arity > 0)
				{
					TypeParameters currentTypeParameters = rc.CurrentTypeParameters;
					if (currentTypeParameters != null && currentTypeParameters.Find(base.Name) != null)
					{
						Expression.Error_TypeArgumentsCannotBeUsed(rc, "type parameter", base.Name, this.loc);
						return null;
					}
					TypeSpec typeSpec2 = rc.CurrentType;
					for (;;)
					{
						if (typeSpec2.MemberDefinition.TypeParametersCount > 0)
						{
							TypeParameterSpec[] typeParameters = typeSpec2.MemberDefinition.TypeParameters;
							for (int i = 0; i < typeParameters.Length; i++)
							{
								if (typeParameters[i].Name == base.Name)
								{
									goto Block_42;
								}
							}
						}
						typeSpec2 = typeSpec2.DeclaringType;
						if (typeSpec2 == null)
						{
							goto IL_402;
						}
					}
					Block_42:
					Expression.Error_TypeArgumentsCannotBeUsed(rc, "type parameter", base.Name, this.loc);
					return null;
				}
				IL_402:
				if ((restrictions & Expression.MemberLookupRestrictions.InvocableOnly) == Expression.MemberLookupRestrictions.None)
				{
					expression = rc.LookupNamespaceOrType(base.Name, base.Arity, LookupMode.IgnoreAccessibility, this.loc);
					if (expression != null)
					{
						rc.Report.SymbolRelatedToPreviousError(expression.Type);
						Expression.ErrorIsInaccesible(rc, expression.GetSignatureForError(), this.loc);
						return expression;
					}
				}
				else
				{
					MemberExpr memberExpr3 = Expression.MemberLookup(rc, false, rc.CurrentType, base.Name, base.Arity, restrictions & ~Expression.MemberLookupRestrictions.InvocableOnly, this.loc) as MemberExpr;
					if (memberExpr3 != null)
					{
						Expression.Error_UnexpectedKind(rc, memberExpr3, "method group", memberExpr3.KindName, this.loc);
						return ErrorExpression.Instance;
					}
				}
				expression = rc.LookupNamespaceOrType(base.Name, -Math.Max(1, base.Arity), LookupMode.Probing, this.loc);
				if (expression != null)
				{
					if (expression.Type.Arity != base.Arity && (restrictions & Expression.MemberLookupRestrictions.IgnoreArity) == Expression.MemberLookupRestrictions.None)
					{
						base.Error_TypeArgumentsCannotBeUsed(rc, expression.Type, this.loc);
						return expression;
					}
					if (expression is TypeExpr)
					{
						if (expression is TypeExpression)
						{
							expression = new TypeExpression(expression.Type, this.loc);
						}
						return expression;
					}
				}
				this.Error_NameDoesNotExist(rc);
			}
			return ErrorExpression.Instance;
			Block_52:
			return new FieldExpr(tuple.Item1, this.loc);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0006DF48 File Offset: 0x0006C148
		private Expression SimpleNameResolve(ResolveContext ec, Expression right_side)
		{
			Expression expression = this.LookupNameExpression(ec, (right_side == null) ? Expression.MemberLookupRestrictions.ReadAccess : Expression.MemberLookupRestrictions.None);
			if (expression == null)
			{
				return null;
			}
			if (expression is FullNamedExpression && expression.eclass != ExprClass.Unresolved)
			{
				Expression.Error_UnexpectedKind(ec, expression, "variable", expression.ExprClassName, this.loc);
				return expression;
			}
			if (right_side != null)
			{
				expression = expression.ResolveLValue(ec, right_side);
			}
			else
			{
				expression = expression.Resolve(ec);
			}
			return expression;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0006DFAA File Offset: 0x0006C1AA
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
