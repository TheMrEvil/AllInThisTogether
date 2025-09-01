using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002AA RID: 682
	public class Throw : Statement
	{
		// Token: 0x060020CC RID: 8396 RVA: 0x000A1221 File Offset: 0x0009F421
		public Throw(Expression expr, Location l)
		{
			this.expr = expr;
			this.loc = l;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000A1237 File Offset: 0x0009F437
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000A1240 File Offset: 0x0009F440
		public override bool Resolve(BlockContext ec)
		{
			if (this.expr == null)
			{
				if (!ec.HasSet(ResolveContext.Options.CatchScope))
				{
					ec.Report.Error(156, this.loc, "A throw statement with no arguments is not allowed outside of a catch clause");
				}
				else if (ec.HasSet(ResolveContext.Options.FinallyScope))
				{
					Block block = ec.CurrentBlock;
					while (block != null && !block.IsCatchBlock)
					{
						if (block.IsFinallyBlock)
						{
							ec.Report.Error(724, this.loc, "A throw statement with no arguments is not allowed inside of a finally clause nested inside of the innermost catch clause");
							break;
						}
						block = block.Parent;
					}
				}
				return true;
			}
			this.expr = this.expr.Resolve(ec, ResolveFlags.VariableOrValue | ResolveFlags.Type);
			if (this.expr == null)
			{
				return false;
			}
			BuiltinTypeSpec exception = ec.BuiltinTypes.Exception;
			if (Convert.ImplicitConversionExists(ec, this.expr, exception))
			{
				this.expr = Convert.ImplicitConversion(ec, this.expr, exception, this.loc);
			}
			else
			{
				ec.Report.Error(155, this.expr.Location, "The type caught or thrown must be derived from System.Exception");
			}
			return true;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000A133C File Offset: 0x0009F53C
		protected override void DoEmit(EmitContext ec)
		{
			if (this.expr != null)
			{
				this.expr.Emit(ec);
				ec.Emit(OpCodes.Throw);
				return;
			}
			LocalVariable asyncThrowVariable = ec.AsyncThrowVariable;
			if (asyncThrowVariable != null)
			{
				if (asyncThrowVariable.HoistedVariant != null)
				{
					asyncThrowVariable.HoistedVariant.Emit(ec);
				}
				else
				{
					asyncThrowVariable.Emit(ec);
				}
				ec.Emit(OpCodes.Throw);
				return;
			}
			ec.Emit(OpCodes.Rethrow);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000A13A7 File Offset: 0x0009F5A7
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.expr != null)
			{
				this.expr.FlowAnalysis(fc);
			}
			return true;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0008953C File Offset: 0x0008773C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return Reachability.CreateUnreachable();
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000A13C0 File Offset: 0x0009F5C0
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Throw @throw = (Throw)t;
			if (this.expr != null)
			{
				@throw.expr = this.expr.Clone(clonectx);
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000A13EE File Offset: 0x0009F5EE
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C39 RID: 3129
		private Expression expr;
	}
}
