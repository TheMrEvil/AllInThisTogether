using System;

namespace Mono.CSharp
{
	// Token: 0x020001C6 RID: 454
	public class TemporaryVariableReference : VariableReference
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x00073B33 File Offset: 0x00071D33
		public TemporaryVariableReference(LocalVariable li, Location loc)
		{
			this.li = li;
			this.type = li.Type;
			this.loc = loc;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0000AF70 File Offset: 0x00009170
		public override bool IsLockedByStatement
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00073B55 File Offset: 0x00071D55
		public LocalVariable LocalInfo
		{
			get
			{
				return this.li;
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00073B5D File Offset: 0x00071D5D
		public static TemporaryVariableReference Create(TypeSpec type, Block block, Location loc)
		{
			return new TemporaryVariableReference(LocalVariable.CreateCompilerGenerated(type, block, loc), loc);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00073B70 File Offset: 0x00071D70
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Variable;
			if (ec.CurrentAnonymousMethod is StateMachineInitializer && (ec.CurrentBlock.Explicit.HasYield || ec.CurrentBlock.Explicit.HasAwait) && ec.IsVariableCapturingRequired)
			{
				this.li.Block.Explicit.CreateAnonymousMethodStorey(ec).CaptureLocalVariable(ec, this.li);
			}
			return this;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00073BE0 File Offset: 0x00071DE0
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			return base.Resolve(ec);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00073BE9 File Offset: 0x00071DE9
		public override void Emit(EmitContext ec)
		{
			this.li.CreateBuilder(ec);
			base.Emit(ec, false);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00073BFF File Offset: 0x00071DFF
		public void EmitAssign(EmitContext ec, Expression source)
		{
			this.li.CreateBuilder(ec);
			base.EmitAssign(ec, source, false, false);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00073C17 File Offset: 0x00071E17
		public override HoistedVariable GetHoistedVariable(AnonymousExpression ae)
		{
			return this.li.HoistedVariant;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void SetHasAddressTaken()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00073B55 File Offset: 0x00071D55
		protected override ILocalVariable Variable
		{
			get
			{
				return this.li;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x000055E7 File Offset: 0x000037E7
		public override VariableInfo VariableInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400098D RID: 2445
		private LocalVariable li;

		// Token: 0x020003AD RID: 941
		public class Declarator : Statement
		{
			// Token: 0x06002703 RID: 9987 RVA: 0x000BAD6C File Offset: 0x000B8F6C
			public Declarator(TemporaryVariableReference variable)
			{
				this.variable = variable;
				this.loc = variable.loc;
			}

			// Token: 0x06002704 RID: 9988 RVA: 0x000BAD87 File Offset: 0x000B8F87
			protected override void DoEmit(EmitContext ec)
			{
				this.variable.li.CreateBuilder(ec);
			}

			// Token: 0x06002705 RID: 9989 RVA: 0x000A7A48 File Offset: 0x000A5C48
			public override void Emit(EmitContext ec)
			{
				this.DoEmit(ec);
			}

			// Token: 0x06002706 RID: 9990 RVA: 0x000022F4 File Offset: 0x000004F4
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				return false;
			}

			// Token: 0x06002707 RID: 9991 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
			}

			// Token: 0x04001062 RID: 4194
			private TemporaryVariableReference variable;
		}
	}
}
