using System;

namespace Mono.CSharp
{
	// Token: 0x0200014B RID: 331
	public class ULongConstant : IntegralConstant
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x000437BA File Offset: 0x000419BA
		public ULongConstant(BuiltinTypes types, ulong v, Location loc) : this(types.ULong, v, loc)
		{
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000437CA File Offset: 0x000419CA
		public ULongConstant(TypeSpec type, ulong v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000437DB File Offset: 0x000419DB
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000437E9 File Offset: 0x000419E9
		public override void Emit(EmitContext ec)
		{
			ec.EmitLong((long)this.Value);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000437F7 File Offset: 0x000419F7
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00043804 File Offset: 0x00041A04
		public override long GetValueAsLong()
		{
			return (long)this.Value;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004380C File Offset: 0x00041A0C
		public override Constant Increment()
		{
			return new ULongConstant(this.type, checked(this.Value + 1UL), this.loc);
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00043828 File Offset: 0x00041A28
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0UL;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00043834 File Offset: 0x00041A34
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1UL;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00043828 File Offset: 0x00041A28
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0UL;
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00043840 File Offset: 0x00041A40
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && this.Value > 255UL)
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && this.Value > 127UL)
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && this.Value > 65535UL)
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && this.Value > 32767UL)
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && this.Value > 65535UL)
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Int:
				if (in_checked_context && this.Value > (ulong)-1)
				{
					throw new OverflowException();
				}
				return new IntConstant(target_type, (int)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				if (in_checked_context && this.Value > (ulong)-1)
				{
					throw new OverflowException();
				}
				return new UIntConstant(target_type, (uint)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Long:
				if (in_checked_context && this.Value > 9223372036854775807UL)
				{
					throw new OverflowException();
				}
				return new LongConstant(target_type, (long)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, this.Value, base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, this.Value, base.Location);
			}
			return null;
		}

		// Token: 0x0400073F RID: 1855
		public readonly ulong Value;
	}
}
