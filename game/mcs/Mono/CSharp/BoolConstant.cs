using System;

namespace Mono.CSharp
{
	// Token: 0x02000142 RID: 322
	public class BoolConstant : Constant
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x000422ED File Offset: 0x000404ED
		public BoolConstant(BuiltinTypes types, bool val, Location loc) : this(types.Bool, val, loc)
		{
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000422FD File Offset: 0x000404FD
		public BoolConstant(TypeSpec type, bool val, Location loc) : base(loc)
		{
			this.eclass = ExprClass.Value;
			this.type = type;
			this.Value = val;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0004231B File Offset: 0x0004051B
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00042328 File Offset: 0x00040528
		public override string GetValueAsLiteral()
		{
			if (!this.Value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004233D File Offset: 0x0004053D
		public override long GetValueAsLong()
		{
			return this.Value ? 1L : 0L;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0004234C File Offset: 0x0004054C
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0004235A File Offset: 0x0004055A
		public override void Emit(EmitContext ec)
		{
			if (this.Value)
			{
				ec.EmitInt(1);
				return;
			}
			ec.EmitInt(0);
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00042373 File Offset: 0x00040573
		public override bool IsDefaultValue
		{
			get
			{
				return !this.Value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00042373 File Offset: 0x00040573
		public override bool IsZeroInteger
		{
			get
			{
				return !this.Value;
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			return null;
		}

		// Token: 0x04000736 RID: 1846
		public readonly bool Value;
	}
}
