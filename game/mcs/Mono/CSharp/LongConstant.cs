using System;

namespace Mono.CSharp
{
	// Token: 0x0200014A RID: 330
	public class LongConstant : IntegralConstant
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x000434C8 File Offset: 0x000416C8
		public LongConstant(BuiltinTypes types, long v, Location loc) : this(types.Long, v, loc)
		{
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000434D8 File Offset: 0x000416D8
		public LongConstant(TypeSpec type, long v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000434E9 File Offset: 0x000416E9
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000434F7 File Offset: 0x000416F7
		public override void Emit(EmitContext ec)
		{
			ec.EmitLong(this.Value);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00043505 File Offset: 0x00041705
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00043512 File Offset: 0x00041712
		public override long GetValueAsLong()
		{
			return this.Value;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0004351A File Offset: 0x0004171A
		public override Constant Increment()
		{
			return new LongConstant(this.type, checked(this.Value + 1L), this.loc);
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00043536 File Offset: 0x00041736
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0L;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00043542 File Offset: 0x00041742
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0L;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0004354E File Offset: 0x0004174E
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1L;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00043536 File Offset: 0x00041736
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0L;
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004355C File Offset: 0x0004175C
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && (this.Value < 0L || this.Value > 255L))
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && (this.Value < -128L || this.Value > 127L))
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && (this.Value < 0L || this.Value > 65535L))
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && (this.Value < -32768L || this.Value > 32767L))
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && (this.Value < 0L || this.Value > 65535L))
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Int:
				if (in_checked_context && (this.Value < -2147483648L || this.Value > 2147483647L))
				{
					throw new OverflowException();
				}
				return new IntConstant(target_type, (int)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				if (in_checked_context && (this.Value < 0L || this.Value > (long)((ulong)-1)))
				{
					throw new OverflowException();
				}
				return new UIntConstant(target_type, (uint)this.Value, base.Location);
			case BuiltinTypeSpec.Type.ULong:
				if (in_checked_context && this.Value < 0L)
				{
					throw new OverflowException();
				}
				return new ULongConstant(target_type, (ulong)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)((float)this.Value), base.Location);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, (double)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, this.Value, base.Location);
			}
			return null;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0004378A File Offset: 0x0004198A
		public override Constant ConvertImplicitly(TypeSpec type)
		{
			if (this.Value >= 0L && type.BuiltinType == BuiltinTypeSpec.Type.ULong)
			{
				return new ULongConstant(type, (ulong)this.Value, this.loc);
			}
			return base.ConvertImplicitly(type);
		}

		// Token: 0x0400073E RID: 1854
		public readonly long Value;
	}
}
