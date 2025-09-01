using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001EE RID: 494
	public class ArglistAccess : Expression
	{
		// Token: 0x060019E1 RID: 6625 RVA: 0x000419DF File Offset: 0x0003FBDF
		public ArglistAccess(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0007F94C File Offset: 0x0007DB4C
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Variable;
			this.type = ec.Module.PredefinedTypes.RuntimeArgumentHandle.Resolve();
			if (ec.HasSet(ResolveContext.Options.FieldInitializerScope) || !ec.CurrentBlock.ParametersBlock.Parameters.HasArglist)
			{
				ec.Report.Error(190, this.loc, "The __arglist construct is valid only within a variable argument method");
			}
			return this;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0007F9B8 File Offset: 0x0007DBB8
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Arglist);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0007F9C5 File Offset: 0x0007DBC5
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
