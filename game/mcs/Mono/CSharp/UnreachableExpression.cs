using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001B4 RID: 436
	public class UnreachableExpression : Expression
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x0006D4C5 File Offset: 0x0006B6C5
		public UnreachableExpression(Expression expr)
		{
			this.loc = expr.Location;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0000225C File Offset: 0x0000045C
		protected override Expression DoResolve(ResolveContext rc)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0006D4D9 File Offset: 0x0006B6D9
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			fc.Report.Warning(429, 4, this.loc, "Unreachable expression code detected");
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
		}
	}
}
