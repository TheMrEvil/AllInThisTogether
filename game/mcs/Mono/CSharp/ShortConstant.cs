using System;

namespace Mono.CSharp
{
	// Token: 0x02000146 RID: 326
	public class ShortConstant : IntegralConstant
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x000429FA File Offset: 0x00040BFA
		public ShortConstant(BuiltinTypes types, short v, Location loc) : this(types.Short, v, loc)
		{
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00042A0A File Offset: 0x00040C0A
		public ShortConstant(TypeSpec type, short v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00042A1B File Offset: 0x00040C1B
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00042A29 File Offset: 0x00040C29
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt((int)this.Value);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00042A37 File Offset: 0x00040C37
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00042A44 File Offset: 0x00040C44
		public override long GetValueAsLong()
		{
			return (long)this.Value;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00042A4D File Offset: 0x00040C4D
		public override Constant Increment()
		{
			return new ShortConstant(this.type, checked(this.Value + 1), this.loc);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00042A69 File Offset: 0x00040C69
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00042A69 File Offset: 0x00040C69
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00042A74 File Offset: 0x00040C74
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00042A7F File Offset: 0x00040C7F
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1;
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00042A8C File Offset: 0x00040C8C
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && (this.Value < 0 || this.Value > 255))
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && (this.Value < -128 || this.Value > 127))
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && this.Value < 0)
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
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

		// Token: 0x0400073A RID: 1850
		public readonly short Value;
	}
}
