using System;

namespace Mono.CSharp
{
	// Token: 0x02000145 RID: 325
	public class SByteConstant : IntegralConstant
	{
		// Token: 0x0600103B RID: 4155 RVA: 0x000427DD File Offset: 0x000409DD
		public SByteConstant(BuiltinTypes types, sbyte v, Location loc) : this(types.SByte, v, loc)
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000427ED File Offset: 0x000409ED
		public SByteConstant(TypeSpec type, sbyte v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000427FE File Offset: 0x000409FE
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004280C File Offset: 0x00040A0C
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt((int)this.Value);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004281A File Offset: 0x00040A1A
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00042827 File Offset: 0x00040A27
		public override long GetValueAsLong()
		{
			return (long)this.Value;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00042830 File Offset: 0x00040A30
		public override Constant Increment()
		{
			return new SByteConstant(this.type, checked(this.Value + 1), this.loc);
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0004284C File Offset: 0x00040A4C
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00042857 File Offset: 0x00040A57
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00042862 File Offset: 0x00040A62
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0004284C File Offset: 0x00040A4C
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00042870 File Offset: 0x00040A70
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && this.Value < 0)
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && this.Value < 0)
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && this.Value < 0)
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Int:
				return new IntConstant(target_type, (int)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				if (in_checked_context && this.Value < 0)
				{
					throw new OverflowException();
				}
				return new UIntConstant(target_type, (uint)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Long:
				return new LongConstant(target_type, (long)this.Value, base.Location);
			case BuiltinTypeSpec.Type.ULong:
				if (in_checked_context && this.Value < 0)
				{
					throw new OverflowException();
				}
				return new ULongConstant(target_type, (ulong)((long)this.Value), base.Location);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)((float)this.Value), base.Location);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, (double)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, this.Value, base.Location);
			}
			return null;
		}

		// Token: 0x04000739 RID: 1849
		public readonly sbyte Value;
	}
}
