using System;
using System.Collections.Generic;
using Mono.CSharp.Linq;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001F9 RID: 505
	public class MemberAccess : ATypeNameExpression
	{
		// Token: 0x06001A3A RID: 6714 RVA: 0x00080512 File Offset: 0x0007E712
		public MemberAccess(Expression expr, string id) : base(id, expr.Location)
		{
			this.expr = expr;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00080528 File Offset: 0x0007E728
		public MemberAccess(Expression expr, string identifier, Location loc) : base(identifier, loc)
		{
			this.expr = expr;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00080539 File Offset: 0x0007E739
		public MemberAccess(Expression expr, string identifier, TypeArguments args, Location loc) : base(identifier, args, loc)
		{
			this.expr = expr;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0008054C File Offset: 0x0007E74C
		public MemberAccess(Expression expr, string identifier, int arity, Location loc) : base(identifier, arity, loc)
		{
			this.expr = expr;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0008055F File Offset: 0x0007E75F
		public Expression LeftExpression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00080567 File Offset: 0x0007E767
		public override Location StartLocation
		{
			get
			{
				if (this.expr != null)
				{
					return this.expr.StartLocation;
				}
				return this.loc;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00080584 File Offset: 0x0007E784
		protected override Expression DoResolve(ResolveContext rc)
		{
			Expression expression = this.LookupNameExpression(rc, Expression.MemberLookupRestrictions.ReadAccess);
			if (expression != null)
			{
				expression = expression.Resolve(rc, ResolveFlags.VariableOrValue | ResolveFlags.Type | ResolveFlags.MethodGroup);
			}
			return expression;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000805A8 File Offset: 0x0007E7A8
		public override Expression DoResolveLValue(ResolveContext rc, Expression rhs)
		{
			Expression expression = this.LookupNameExpression(rc, Expression.MemberLookupRestrictions.None);
			if (expression is TypeExpr)
			{
				expression.Error_UnexpectedKind(rc, ResolveFlags.VariableOrValue, this.loc);
				return null;
			}
			if (expression != null)
			{
				expression = expression.ResolveLValue(rc, rhs);
			}
			return expression;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000805E4 File Offset: 0x0007E7E4
		protected virtual void Error_OperatorCannotBeApplied(ResolveContext rc, TypeSpec type)
		{
			if (type == InternalType.NullLiteral && rc.IsRuntimeBinder)
			{
				rc.Report.Error(10000, this.loc, "Cannot perform member binding on `null' value");
				return;
			}
			this.expr.Error_OperatorCannotBeApplied(rc, this.loc, ".", type);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00080635 File Offset: 0x0007E835
		public override bool HasConditionalAccess()
		{
			return this.LeftExpression.HasConditionalAccess();
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00080642 File Offset: 0x0007E842
		public static bool IsValidDotExpression(TypeSpec type)
		{
			return (type.Kind & (MemberKind.Class | MemberKind.Struct | MemberKind.Delegate | MemberKind.Enum | MemberKind.Interface | MemberKind.TypeParameter | MemberKind.ArrayType)) != (MemberKind)0 || type.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00080660 File Offset: 0x0007E860
		public override Expression LookupNameExpression(ResolveContext rc, Expression.MemberLookupRestrictions restrictions)
		{
			SimpleName simpleName = this.expr as SimpleName;
			if (simpleName != null)
			{
				this.expr = simpleName.LookupNameExpression(rc, Expression.MemberLookupRestrictions.ExactArity | Expression.MemberLookupRestrictions.ReadAccess);
				if (this.expr is VariableReference || this.expr is ConstantExpr || this.expr is TransparentMemberAccess || this.expr is EventExpr)
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
				bool flag = false;
				if (!rc.HasSet(ResolveContext.Options.ConditionalAccessReceiver) && this.expr.HasConditionalAccess())
				{
					flag = true;
					using (rc.Set(ResolveContext.Options.ConditionalAccessReceiver))
					{
						this.expr = this.expr.Resolve(rc, ResolveFlags.VariableOrValue | ResolveFlags.Type);
					}
				}
				if (!flag)
				{
					this.expr = this.expr.Resolve(rc, ResolveFlags.VariableOrValue | ResolveFlags.Type);
				}
			}
			if (this.expr == null)
			{
				return null;
			}
			NamespaceExpression namespaceExpression = this.expr as NamespaceExpression;
			if (namespaceExpression != null)
			{
				FullNamedExpression fullNamedExpression = namespaceExpression.LookupTypeOrNamespace(rc, base.Name, base.Arity, LookupMode.Normal, this.loc);
				if (fullNamedExpression == null)
				{
					namespaceExpression.Error_NamespaceDoesNotExist(rc, base.Name, base.Arity, this.loc);
					return null;
				}
				if (base.Arity > 0)
				{
					if (base.HasTypeArguments)
					{
						return new GenericTypeExpr(fullNamedExpression.Type, this.targs, this.loc);
					}
					this.targs.Resolve(rc, false);
				}
				return fullNamedExpression;
			}
			else
			{
				TypeSpec type = this.expr.Type;
				if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					MemberExpr memberExpr = this.expr as MemberExpr;
					if (memberExpr != null)
					{
						memberExpr.ResolveInstanceExpression(rc, null);
					}
					Arguments arguments = new Arguments(1);
					arguments.Add(new Argument(this.expr));
					return new DynamicMemberBinder(base.Name, arguments, this.loc);
				}
				ConditionalMemberAccess conditionalMemberAccess = this as ConditionalMemberAccess;
				if (conditionalMemberAccess != null)
				{
					if (!Expression.IsNullPropagatingValid(this.expr.Type))
					{
						this.expr.Error_OperatorCannotBeApplied(rc, this.loc, "?", this.expr.Type);
						return null;
					}
					if (type.IsNullableType)
					{
						this.expr = Unwrap.Create(this.expr, true).Resolve(rc);
						type = this.expr.Type;
					}
				}
				if (!MemberAccess.IsValidDotExpression(type))
				{
					this.Error_OperatorCannotBeApplied(rc, type);
					return null;
				}
				int arity = base.Arity;
				bool flag2 = false;
				Expression expression;
				ExtensionMethodCandidates extensionMethodCandidates;
				for (;;)
				{
					expression = Expression.MemberLookup(rc, flag2, type, base.Name, arity, restrictions, this.loc);
					if (expression == null && MethodGroupExpr.IsExtensionMethodArgument(this.expr))
					{
						extensionMethodCandidates = rc.LookupExtensionMethod(base.Name, arity);
						if (extensionMethodCandidates != null)
						{
							break;
						}
					}
					if (flag2)
					{
						goto Block_27;
					}
					if (expression != null)
					{
						goto IL_39B;
					}
					arity = 0;
					restrictions &= ~Expression.MemberLookupRestrictions.InvocableOnly;
					flag2 = true;
				}
				ExtensionMethodGroupExpr extensionMethodGroupExpr = new ExtensionMethodGroupExpr(extensionMethodCandidates, this.expr, this.loc);
				if (base.HasTypeArguments)
				{
					if (!this.targs.Resolve(rc, false))
					{
						return null;
					}
					extensionMethodGroupExpr.SetTypeArguments(rc, this.targs);
				}
				if (conditionalMemberAccess != null)
				{
					extensionMethodGroupExpr.ConditionalAccess = true;
				}
				return extensionMethodGroupExpr.Resolve(rc);
				Block_27:
				if (expression == null)
				{
					List<MissingTypeSpecReference> missingDependencies = type.GetMissingDependencies();
					if (missingDependencies != null)
					{
						ImportedTypeDefinition.Error_MissingDependency(rc, missingDependencies, this.loc);
					}
					else if (this.expr is TypeExpr)
					{
						base.Error_TypeDoesNotContainDefinition(rc, type, base.Name);
					}
					else
					{
						this.Error_TypeDoesNotContainDefinition(rc, type, base.Name);
					}
					return null;
				}
				if (!(expression is MethodGroupExpr) && !(expression is PropertyExpr) && !(expression is TypeExpr))
				{
					Expression.ErrorIsInaccesible(rc, expression.GetSignatureForError(), this.loc);
				}
				IL_39B:
				TypeExpr typeExpr = expression as TypeExpr;
				if (typeExpr == null)
				{
					MemberExpr memberExpr = expression as MemberExpr;
					if (simpleName != null && memberExpr.IsStatic && (this.expr = memberExpr.ProbeIdenticalTypeName(rc, this.expr, simpleName)) != this.expr)
					{
						simpleName = null;
					}
					if (conditionalMemberAccess != null)
					{
						memberExpr.ConditionalAccess = true;
					}
					memberExpr = memberExpr.ResolveMemberAccess(rc, this.expr, simpleName);
					if (base.Arity > 0)
					{
						if (!this.targs.Resolve(rc, false))
						{
							return null;
						}
						memberExpr.SetTypeArguments(rc, this.targs);
					}
					return memberExpr;
				}
				if (!(this.expr is TypeExpr) && (simpleName == null || this.expr.ProbeIdenticalTypeName(rc, this.expr, simpleName) == this.expr))
				{
					rc.Report.Error(572, this.loc, "`{0}': cannot reference a type through an expression. Consider using `{1}' instead", base.Name, typeExpr.GetSignatureForError());
				}
				if (!typeExpr.Type.IsAccessible(rc))
				{
					rc.Report.SymbolRelatedToPreviousError(expression.Type);
					Expression.ErrorIsInaccesible(rc, expression.Type.GetSignatureForError(), this.loc);
					return null;
				}
				if (base.HasTypeArguments)
				{
					return new GenericTypeExpr(expression.Type, this.targs, this.loc);
				}
				return expression;
			}
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00080B54 File Offset: 0x0007ED54
		public override FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext rc, bool allowUnboundTypeArguments)
		{
			FullNamedExpression fullNamedExpression = this.expr as FullNamedExpression;
			if (fullNamedExpression == null)
			{
				this.expr.ResolveAsType(rc, false);
				return null;
			}
			FullNamedExpression fullNamedExpression2 = fullNamedExpression.ResolveAsTypeOrNamespace(rc, allowUnboundTypeArguments);
			if (fullNamedExpression2 == null)
			{
				return null;
			}
			NamespaceExpression namespaceExpression = fullNamedExpression2 as NamespaceExpression;
			if (namespaceExpression != null)
			{
				FullNamedExpression fullNamedExpression3 = namespaceExpression.LookupTypeOrNamespace(rc, base.Name, base.Arity, LookupMode.Normal, this.loc);
				if (fullNamedExpression3 == null)
				{
					namespaceExpression.Error_NamespaceDoesNotExist(rc, base.Name, base.Arity, this.loc);
				}
				else if (base.Arity > 0)
				{
					if (base.HasTypeArguments)
					{
						fullNamedExpression3 = new GenericTypeExpr(fullNamedExpression3.Type, this.targs, this.loc);
						if (fullNamedExpression3.ResolveAsType(rc, false) == null)
						{
							return null;
						}
					}
					else
					{
						this.targs.Resolve(rc, allowUnboundTypeArguments);
						fullNamedExpression3 = new GenericOpenTypeExpr(fullNamedExpression3.Type, this.loc);
					}
				}
				return fullNamedExpression3;
			}
			TypeSpec typeSpec = fullNamedExpression2.ResolveAsType(rc, false);
			if (typeSpec == null)
			{
				return null;
			}
			TypeSpec typeSpec2 = typeSpec;
			if (TypeManager.IsGenericParameter(typeSpec2))
			{
				rc.Module.Compiler.Report.Error(704, this.loc, "A nested type cannot be specified through a type parameter `{0}'", typeSpec.GetSignatureForError());
				return null;
			}
			QualifiedAliasMember qualifiedAliasMember = this as QualifiedAliasMember;
			if (qualifiedAliasMember != null)
			{
				rc.Module.Compiler.Report.Error(431, this.loc, "Alias `{0}' cannot be used with `::' since it denotes a type. Consider replacing `::' with `.'", qualifiedAliasMember.Alias);
			}
			TypeSpec typeSpec3 = null;
			while (typeSpec2 != null)
			{
				typeSpec3 = MemberCache.FindNestedType(typeSpec2, base.Name, base.Arity);
				if (typeSpec3 == null)
				{
					if (typeSpec2 == typeSpec)
					{
						this.Error_IdentifierNotFound(rc, typeSpec2);
						return null;
					}
					typeSpec2 = typeSpec;
					typeSpec3 = MemberCache.FindNestedType(typeSpec2, base.Name, base.Arity);
					Expression.ErrorIsInaccesible(rc, typeSpec3.GetSignatureForError(), this.loc);
					break;
				}
				else
				{
					if (typeSpec3.IsAccessible(rc) || typeSpec2.MemberDefinition == rc.CurrentMemberDefinition)
					{
						break;
					}
					typeSpec2 = typeSpec2.BaseType;
				}
			}
			TypeExpr typeExpr;
			if (base.Arity > 0)
			{
				if (base.HasTypeArguments)
				{
					typeExpr = new GenericTypeExpr(typeSpec3, this.targs, this.loc);
				}
				else
				{
					this.targs.Resolve(rc, allowUnboundTypeArguments && !(fullNamedExpression2 is GenericTypeExpr));
					typeExpr = new GenericOpenTypeExpr(typeSpec3, this.loc);
				}
			}
			else if (fullNamedExpression2 is GenericOpenTypeExpr)
			{
				typeExpr = new GenericOpenTypeExpr(typeSpec3, this.loc);
			}
			else
			{
				typeExpr = new TypeExpression(typeSpec3, this.loc);
			}
			if (typeExpr.ResolveAsType(rc, false) == null)
			{
				return null;
			}
			return typeExpr;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00080DC4 File Offset: 0x0007EFC4
		public void Error_IdentifierNotFound(IMemberContext rc, TypeSpec expr_type)
		{
			TypeSpec typeSpec = MemberCache.FindNestedType(expr_type, base.Name, -Math.Max(1, base.Arity));
			if (typeSpec != null)
			{
				base.Error_TypeArgumentsCannotBeUsed(rc, typeSpec, this.expr.Location);
				return;
			}
			Expression expression = Expression.MemberLookup(rc, false, expr_type, base.Name, 0, Expression.MemberLookupRestrictions.None, this.loc);
			if (expression != null)
			{
				Expression.Error_UnexpectedKind(rc, expression, "type", expression.ExprClassName, this.loc);
				return;
			}
			rc.Module.Compiler.Report.Error(426, this.loc, "The nested type `{0}' does not exist in the type `{1}'", base.Name, expr_type.GetSignatureForError());
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00080E66 File Offset: 0x0007F066
		protected override void Error_InvalidExpressionStatement(Report report, Location loc)
		{
			base.Error_InvalidExpressionStatement(report, this.LeftExpression.Location);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00080E7C File Offset: 0x0007F07C
		public override void Error_TypeDoesNotContainDefinition(ResolveContext ec, TypeSpec type, string name)
		{
			if (ec.Module.Compiler.Settings.Version > LanguageVersion.ISO_2 && !ec.IsRuntimeBinder && MethodGroupExpr.IsExtensionMethodArgument(this.expr))
			{
				ec.Report.SymbolRelatedToPreviousError(type);
				List<string> list = ec.Module.GlobalRootNamespace.FindExtensionMethodNamespaces(ec, name, base.Arity);
				string text;
				if (list != null)
				{
					text = "`" + string.Join("' or `", list.ToArray()) + "' using directive";
				}
				else
				{
					text = "an assembly reference";
				}
				ec.Report.Error(1061, this.loc, "Type `{0}' does not contain a definition for `{1}' and no extension method `{1}' of type `{0}' could be found. Are you missing {2}?", new string[]
				{
					type.GetSignatureForError(),
					name,
					text
				});
				return;
			}
			base.Error_TypeDoesNotContainDefinition(ec, type, name);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00080F4B File Offset: 0x0007F14B
		public override string GetSignatureForError()
		{
			return this.expr.GetSignatureForError() + "." + base.GetSignatureForError();
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00080F68 File Offset: 0x0007F168
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((MemberAccess)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00080F81 File Offset: 0x0007F181
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009E8 RID: 2536
		protected Expression expr;
	}
}
