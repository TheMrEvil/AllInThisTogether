using System;

namespace Mono.CSharp
{
	// Token: 0x02000149 RID: 329
	public class UIntConstant : IntegralConstant
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x0004326F File Offset: 0x0004146F
		public UIntConstant(BuiltinTypes types, uint v, Location loc) : this(types.UInt, v, loc)
		{
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004327F File Offset: 0x0004147F
		public UIntConstant(TypeSpec type, uint v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00043290 File Offset: 0x00041490
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0004329E File Offset: 0x0004149E
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt((int)this.Value);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000432AC File Offset: 0x000414AC
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000432B9 File Offset: 0x000414B9
		public override long GetValueAsLong()
		{
			return (long)((ulong)this.Value);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000432C2 File Offset: 0x000414C2
		public override Constant Increment()
		{
			return new UIntConstant(this.type, checked(this.Value + 1U), this.loc);
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x000432DD File Offset: 0x000414DD
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0U;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000432E8 File Offset: 0x000414E8
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1U;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x000432DD File Offset: 0x000414DD
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0U;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000432F4 File Offset: 0x000414F4
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && (this.Value < 0U || this.Value > 255U))
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && (ulong)this.Value > 127UL)
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && (this.Value < 0U || this.Value > 65535U))
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && (ulong)this.Value > 32767UL)
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && (this.Value < 0U || this.Value > 65535U))
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Int:
				if (in_checked_context && this.Value > 2147483647U)
				{
					throw new OverflowException();
				}
				return new IntConstant(target_type, (int)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Long:
				return new LongConstant(target_type, (long)((ulong)this.Value), base.Location);
			case BuiltinTypeSpec.Type.ULong:
				return new ULongConstant(target_type, (ulong)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, this.Value, base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, this.Value, base.Location);
			}
			return null;
		}

		// Token: 0x0400073D RID: 1853
		public readonly uint Value;
	}
}
