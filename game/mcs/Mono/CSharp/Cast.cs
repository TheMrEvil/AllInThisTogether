using System;

namespace Mono.CSharp
{
	// Token: 0x020001D9 RID: 473
	public class Cast : ShimExpression
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x00077486 File Offset: 0x00075686
		public Cast(Expression cast_type, Expression expr, Location loc) : base(expr)
		{
			this.target_type = cast_type;
			this.loc = loc;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0007749D File Offset: 0x0007569D
		public Expression TargetType
		{
			get
			{
				return this.target_type;
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000774A8 File Offset: 0x000756A8
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null)
			{
				return null;
			}
			this.type = this.target_type.ResolveAsType(ec, false);
			if (this.type == null)
			{
				return null;
			}
			if (this.type.IsStatic)
			{
				ec.Report.Error(716, this.loc, "Cannot convert to static type `{0}'", this.type.GetSignatureForError());
				return null;
			}
			if (this.type.IsPointer && !ec.IsUnsafe)
			{
				Expression.UnsafeError(ec, this.loc);
			}
			this.eclass = ExprClass.Value;
			Constant constant = this.expr as Constant;
			if (constant != null)
			{
				constant = constant.Reduce(ec, this.type);
				if (constant != null)
				{
					return constant;
				}
			}
			Expression expression = Convert.ExplicitConversion(ec, this.expr, this.type, this.loc);
			if (expression == this.expr)
			{
				return EmptyCast.Create(expression, this.type);
			}
			return expression;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0007759D File Offset: 0x0007579D
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Cast cast = (Cast)t;
			cast.target_type = this.target_type.Clone(clonectx);
			cast.expr = this.expr.Clone(clonectx);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000775C8 File Offset: 0x000757C8
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009B1 RID: 2481
		private Expression target_type;
	}
}
