using System;

namespace Mono.CSharp
{
	// Token: 0x02000153 RID: 339
	public class SideEffectConstant : Constant
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x00044AD4 File Offset: 0x00042CD4
		public SideEffectConstant(Constant value, Expression side_effect, Location loc) : base(loc)
		{
			this.value = value;
			this.type = value.Type;
			this.eclass = ExprClass.Value;
			while (side_effect is SideEffectConstant)
			{
				side_effect = ((SideEffectConstant)side_effect).side_effect;
			}
			this.side_effect = side_effect;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsSideEffectFree
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00044B20 File Offset: 0x00042D20
		public override bool ContainsEmitWithAwait()
		{
			return this.side_effect.ContainsEmitWithAwait();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00044B2D File Offset: 0x00042D2D
		public override object GetValue()
		{
			return this.value.GetValue();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00044B3A File Offset: 0x00042D3A
		public override string GetValueAsLiteral()
		{
			return this.value.GetValueAsLiteral();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00044B47 File Offset: 0x00042D47
		public override long GetValueAsLong()
		{
			return this.value.GetValueAsLong();
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00044B54 File Offset: 0x00042D54
		public override void Emit(EmitContext ec)
		{
			this.side_effect.EmitSideEffect(ec);
			this.value.Emit(ec);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00044B6E File Offset: 0x00042D6E
		public override void EmitSideEffect(EmitContext ec)
		{
			this.side_effect.EmitSideEffect(ec);
			this.value.EmitSideEffect(ec);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00044B88 File Offset: 0x00042D88
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.side_effect.FlowAnalysis(fc);
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00044B96 File Offset: 0x00042D96
		public override bool IsDefaultValue
		{
			get
			{
				return this.value.IsDefaultValue;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00044BA3 File Offset: 0x00042DA3
		public override bool IsNegative
		{
			get
			{
				return this.value.IsNegative;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00044BB0 File Offset: 0x00042DB0
		public override bool IsZeroInteger
		{
			get
			{
				return this.value.IsZeroInteger;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00044BC0 File Offset: 0x00042DC0
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			Constant constant = this.value.ConvertExplicitly(in_checked_context, target_type);
			if (constant == null)
			{
				return null;
			}
			return new SideEffectConstant(constant, this.side_effect, constant.Location)
			{
				type = target_type,
				eclass = this.eclass
			};
		}

		// Token: 0x04000745 RID: 1861
		public readonly Constant value;

		// Token: 0x04000746 RID: 1862
		private Expression side_effect;
	}
}
