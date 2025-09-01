using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001DC RID: 476
	public class DefaultValueExpression : Expression
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x00077841 File Offset: 0x00075A41
		public DefaultValueExpression(Expression expr, Location loc)
		{
			this.expr = expr;
			this.loc = loc;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00077857 File Offset: 0x00075A57
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00077860 File Offset: 0x00075A60
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this));
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000778AC File Offset: 0x00075AAC
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.type = this.expr.ResolveAsType(ec, false);
			if (this.type == null)
			{
				return null;
			}
			if (this.type.IsStatic)
			{
				ec.Report.Error(-244, this.loc, "The `default value' operator cannot be applied to an operand of a static type");
			}
			if (this.type.IsPointer)
			{
				return new NullLiteral(base.Location).ConvertImplicitly(this.type);
			}
			if (TypeSpec.IsReferenceType(this.type))
			{
				return new NullConstant(this.type, this.loc);
			}
			Constant constant = New.Constantify(this.type, this.expr.Location);
			if (constant != null)
			{
				return constant;
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00077965 File Offset: 0x00075B65
		public override void Emit(EmitContext ec)
		{
			LocalTemporary localTemporary = new LocalTemporary(this.type);
			localTemporary.AddressOf(ec, AddressOp.LoadStore);
			ec.Emit(OpCodes.Initobj, this.type);
			localTemporary.Emit(ec);
			localTemporary.Release(ec);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00077998 File Offset: 0x00075B98
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((DefaultValueExpression)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000779B1 File Offset: 0x00075BB1
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009B7 RID: 2487
		private Expression expr;
	}
}
