using System;

namespace Mono.CSharp
{
	// Token: 0x020001D0 RID: 464
	public abstract class Probe : Expression
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x00075D85 File Offset: 0x00073F85
		protected Probe(Expression expr, Expression probe_type, Location l)
		{
			this.ProbeType = probe_type;
			this.loc = l;
			this.expr = expr;
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00075DA2 File Offset: 0x00073FA2
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00075DAA File Offset: 0x00073FAA
		public override bool ContainsEmitWithAwait()
		{
			return this.expr.ContainsEmitWithAwait();
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00075DB8 File Offset: 0x00073FB8
		protected Expression ResolveCommon(ResolveContext rc)
		{
			this.expr = this.expr.Resolve(rc);
			if (this.expr == null)
			{
				return null;
			}
			this.ResolveProbeType(rc);
			if (this.probe_type_expr == null)
			{
				return this;
			}
			if (this.probe_type_expr.IsStatic)
			{
				rc.Report.Error(7023, this.loc, "The second operand of `is' or `as' operator cannot be static type `{0}'", this.probe_type_expr.GetSignatureForError());
				return null;
			}
			if (this.expr.Type.IsPointer || this.probe_type_expr.IsPointer)
			{
				rc.Report.Error(244, this.loc, "The `{0}' operator cannot be applied to an operand of pointer type", this.OperatorName);
				return null;
			}
			if (this.expr.Type == InternalType.AnonymousMethod || this.expr.Type == InternalType.MethodGroup)
			{
				rc.Report.Error(837, this.loc, "The `{0}' operator cannot be applied to a lambda expression, anonymous method, or method group", this.OperatorName);
				return null;
			}
			return this;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00075EB1 File Offset: 0x000740B1
		protected virtual void ResolveProbeType(ResolveContext rc)
		{
			this.probe_type_expr = this.ProbeType.ResolveAsType(rc, false);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00075EC6 File Offset: 0x000740C6
		public override void EmitSideEffect(EmitContext ec)
		{
			this.expr.EmitSideEffect(ec);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00075ED4 File Offset: 0x000740D4
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00075EE2 File Offset: 0x000740E2
		public override bool HasConditionalAccess()
		{
			return this.expr.HasConditionalAccess();
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001872 RID: 6258
		protected abstract string OperatorName { get; }

		// Token: 0x06001873 RID: 6259 RVA: 0x00075EEF File Offset: 0x000740EF
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Probe probe = (Probe)t;
			probe.expr = this.expr.Clone(clonectx);
			probe.ProbeType = this.ProbeType.Clone(clonectx);
		}

		// Token: 0x040009A0 RID: 2464
		public Expression ProbeType;

		// Token: 0x040009A1 RID: 2465
		protected Expression expr;

		// Token: 0x040009A2 RID: 2466
		protected TypeSpec probe_type_expr;
	}
}
