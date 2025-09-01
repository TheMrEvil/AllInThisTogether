using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002A8 RID: 680
	public class GotoCase : SwitchGoto
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x000A1053 File Offset: 0x0009F253
		public GotoCase(Expression e, Location l) : base(l)
		{
			this.expr = e;
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x000A1063 File Offset: 0x0009F263
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000A106B File Offset: 0x0009F26B
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x000A1073 File Offset: 0x0009F273
		public SwitchLabel Label
		{
			[CompilerGenerated]
			get
			{
				return this.<Label>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Label>k__BackingField = value;
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000A107C File Offset: 0x0009F27C
		public override bool Resolve(BlockContext ec)
		{
			if (ec.Switch == null)
			{
				base.Error_GotoCaseRequiresSwitchBlock(ec);
				return false;
			}
			Constant constant = this.expr.ResolveLabelConstant(ec);
			if (constant == null)
			{
				return false;
			}
			Constant constant2;
			if (ec.Switch.IsNullable && constant is NullLiteral)
			{
				constant2 = constant;
			}
			else
			{
				TypeSpec switchType = ec.Switch.SwitchType;
				constant2 = constant.Reduce(ec, switchType);
				if (constant2 == null)
				{
					constant.Error_ValueCannotBeConverted(ec, switchType, true);
					return false;
				}
				if (!Convert.ImplicitStandardConversionExists(constant, switchType))
				{
					ec.Report.Warning(469, 2, this.loc, "The `goto case' value is not implicitly convertible to type `{0}'", switchType.GetSignatureForError());
				}
			}
			ec.Switch.RegisterGotoCase(this, constant2);
			base.Resolve(ec);
			this.expr = constant2;
			return true;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000A1130 File Offset: 0x0009F330
		protected override void DoEmit(EmitContext ec)
		{
			ec.Emit(this.unwind_protect ? OpCodes.Leave : OpCodes.Br, this.Label.GetILLabel(ec));
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000A1158 File Offset: 0x0009F358
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			((GotoCase)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000A1174 File Offset: 0x0009F374
		public override Reachability MarkReachable(Reachability rc)
		{
			if (!rc.IsUnreachable)
			{
				SwitchLabel switchLabel = this.switch_statement.FindLabel((Constant)this.expr);
				if (switchLabel.IsUnreachable)
				{
					switchLabel.MarkReachable(rc);
					this.switch_statement.Block.ScanGotoJump(switchLabel);
				}
			}
			return base.MarkReachable(rc);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000A11C9 File Offset: 0x0009F3C9
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C35 RID: 3125
		private Expression expr;

		// Token: 0x04000C36 RID: 3126
		[CompilerGenerated]
		private SwitchLabel <Label>k__BackingField;
	}
}
