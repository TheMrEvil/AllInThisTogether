using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002B8 RID: 696
	public class SwitchLabel : Statement
	{
		// Token: 0x06002194 RID: 8596 RVA: 0x000A4398 File Offset: 0x000A2598
		public SwitchLabel(Expression expr, Location l)
		{
			this.label = expr;
			this.loc = l;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000A43AE File Offset: 0x000A25AE
		public bool IsDefault
		{
			get
			{
				return this.label == null;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x000A43B9 File Offset: 0x000A25B9
		public Expression Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000A43C1 File Offset: 0x000A25C1
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000A43C9 File Offset: 0x000A25C9
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x000A43D1 File Offset: 0x000A25D1
		public Constant Converted
		{
			get
			{
				return this.converted;
			}
			set
			{
				this.converted = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000A43DA File Offset: 0x000A25DA
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x000A43E2 File Offset: 0x000A25E2
		public bool PatternMatching
		{
			[CompilerGenerated]
			get
			{
				return this.<PatternMatching>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PatternMatching>k__BackingField = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x000A43EB File Offset: 0x000A25EB
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x000A43F3 File Offset: 0x000A25F3
		public bool SectionStart
		{
			[CompilerGenerated]
			get
			{
				return this.<SectionStart>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SectionStart>k__BackingField = value;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000A43FC File Offset: 0x000A25FC
		public Label GetILLabel(EmitContext ec)
		{
			if (this.il_label == null)
			{
				this.il_label = new Label?(ec.DefineLabel());
			}
			return this.il_label.Value;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000A4427 File Offset: 0x000A2627
		protected override void DoEmit(EmitContext ec)
		{
			ec.MarkLabel(this.GetILLabel(ec));
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000A4436 File Offset: 0x000A2636
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (!this.SectionStart)
			{
				return false;
			}
			fc.BranchDefiniteAssignment(fc.SwitchInitialDefinitiveAssignment);
			return false;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000A4450 File Offset: 0x000A2650
		public override bool Resolve(BlockContext bc)
		{
			if (this.ResolveAndReduce(bc))
			{
				bc.Switch.RegisterLabel(bc, this);
			}
			return true;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000A446C File Offset: 0x000A266C
		private bool ResolveAndReduce(BlockContext bc)
		{
			if (this.IsDefault)
			{
				return true;
			}
			Switch @switch = bc.Switch;
			if (this.PatternMatching)
			{
				this.label = new Is(@switch.ExpressionValue, this.label, this.loc).Resolve(bc);
				return this.label != null;
			}
			Constant constant = this.label.ResolveLabelConstant(bc);
			if (constant == null)
			{
				return false;
			}
			if (@switch.IsNullable && constant is NullLiteral)
			{
				this.converted = constant;
				return true;
			}
			if (@switch.IsPatternMatching)
			{
				this.label = new Is(@switch.ExpressionValue, this.label, this.loc).Resolve(bc);
				return true;
			}
			this.converted = constant.ImplicitConversionRequired(bc, @switch.SwitchType);
			return this.converted != null;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000A4533 File Offset: 0x000A2733
		public void Error_AlreadyOccurs(ResolveContext ec, SwitchLabel collision_with)
		{
			ec.Report.SymbolRelatedToPreviousError(collision_with.loc, null);
			ec.Report.Error(152, this.loc, "The label `{0}' already occurs in this switch statement", this.GetSignatureForError());
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000A4568 File Offset: 0x000A2768
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
			SwitchLabel switchLabel = (SwitchLabel)target;
			if (this.label != null)
			{
				switchLabel.label = this.label.Clone(clonectx);
			}
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000A4596 File Offset: 0x000A2796
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000A45A0 File Offset: 0x000A27A0
		public string GetSignatureForError()
		{
			string arg;
			if (this.converted == null)
			{
				arg = "default";
			}
			else
			{
				arg = this.converted.GetValueAsLiteral();
			}
			return string.Format("case {0}:", arg);
		}

		// Token: 0x04000C61 RID: 3169
		private Constant converted;

		// Token: 0x04000C62 RID: 3170
		private Expression label;

		// Token: 0x04000C63 RID: 3171
		private Label? il_label;

		// Token: 0x04000C64 RID: 3172
		[CompilerGenerated]
		private bool <PatternMatching>k__BackingField;

		// Token: 0x04000C65 RID: 3173
		[CompilerGenerated]
		private bool <SectionStart>k__BackingField;
	}
}
