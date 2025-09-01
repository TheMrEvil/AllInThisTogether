using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001D4 RID: 468
	internal class PropertyPattern : ComplexPatternExpression
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x00076F8A File Offset: 0x0007518A
		public PropertyPattern(ATypeNameExpression typeExpresion, List<PropertyPatternMember> members, Location loc) : base(typeExpresion, loc)
		{
			this.Members = members;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00076F9B File Offset: 0x0007519B
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x00076FA3 File Offset: 0x000751A3
		public List<PropertyPatternMember> Members
		{
			[CompilerGenerated]
			get
			{
				return this.<Members>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Members>k__BackingField = value;
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00076FAC File Offset: 0x000751AC
		protected override Expression DoResolve(ResolveContext rc)
		{
			this.type = base.TypeExpression.ResolveAsType(rc, false);
			if (this.type == null)
			{
				return null;
			}
			this.comparisons = new Expression[this.Members.Count];
			this.instance = new LocalTemporary(this.type);
			int i = 0;
			while (i < this.Members.Count)
			{
				PropertyPatternMember propertyPatternMember = this.Members[i];
				Expression expression = Expression.MemberLookup(rc, false, this.type, propertyPatternMember.Name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc);
				if (expression != null)
				{
					goto IL_AD;
				}
				expression = Expression.MemberLookup(rc, true, this.type, propertyPatternMember.Name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc);
				if (expression == null)
				{
					goto IL_AD;
				}
				Expression.ErrorIsInaccesible(rc, expression.GetSignatureForError(), this.loc);
				IL_16E:
				i++;
				continue;
				IL_AD:
				if (expression == null)
				{
					Expression.Error_TypeDoesNotContainDefinition(rc, base.Location, base.Type, propertyPatternMember.Name);
					goto IL_16E;
				}
				PropertyExpr propertyExpr = expression as PropertyExpr;
				if (propertyExpr == null || expression is FieldExpr)
				{
					rc.Report.Error(-2001, propertyPatternMember.Location, "`{0}' is not a valid pattern member", propertyPatternMember.Name);
					goto IL_16E;
				}
				if (propertyExpr != null && !propertyExpr.PropertyInfo.HasGet)
				{
					rc.Report.Error(-2002, propertyPatternMember.Location, "Property `{0}.get' accessor is required", propertyExpr.GetSignatureForError());
					goto IL_16E;
				}
				Expression expression2 = propertyPatternMember.Expr.Resolve(rc);
				if (expression2 != null)
				{
					MemberExpr memberExpr = (MemberExpr)expression;
					memberExpr.InstanceExpression = this.instance;
					this.comparisons[i] = PropertyPattern.ResolveComparison(rc, expression2, memberExpr);
					goto IL_16E;
				}
				goto IL_16E;
			}
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00077144 File Offset: 0x00075344
		private static Expression ResolveComparison(ResolveContext rc, Expression expr, Expression instance)
		{
			if (expr is WildcardPattern)
			{
				return new EmptyExpression(expr.Type);
			}
			return new Is(instance, expr, expr.Location).Resolve(rc);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0007716D File Offset: 0x0007536D
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			this.instance.Store(ec);
			base.EmitBranchable(ec, target, on_true);
		}

		// Token: 0x040009AA RID: 2474
		private LocalTemporary instance;

		// Token: 0x040009AB RID: 2475
		[CompilerGenerated]
		private List<PropertyPatternMember> <Members>k__BackingField;
	}
}
