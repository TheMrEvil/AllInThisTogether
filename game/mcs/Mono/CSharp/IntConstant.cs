using System;

namespace Mono.CSharp
{
	// Token: 0x02000148 RID: 328
	public class IntConstant : IntegralConstant
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00042E4D File Offset: 0x0004104D
		public IntConstant(BuiltinTypes types, int v, Location loc) : this(types.Int, v, loc)
		{
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00042E5D File Offset: 0x0004105D
		public IntConstant(TypeSpec type, int v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00042E6E File Offset: 0x0004106E
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00042E7C File Offset: 0x0004107C
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt(this.Value);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00042E8A File Offset: 0x0004108A
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00042E97 File Offset: 0x00041097
		public override long GetValueAsLong()
		{
			return (long)this.Value;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00042EA0 File Offset: 0x000410A0
		public override Constant Increment()
		{
			return new IntConstant(this.type, checked(this.Value + 1), this.loc);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00042EBB File Offset: 0x000410BB
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00042EC6 File Offset: 0x000410C6
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00042ED1 File Offset: 0x000410D1
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00042EBB File Offset: 0x000410BB
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00042EDC File Offset: 0x000410DC
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
				if (in_checked_context && (this.Value < 0 || this.Value > 65535))
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && (this.Value < -32768 || this.Value > 32767))
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && (this.Value < 0 || this.Value > 65535))
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				if (in_checked_context && (long)this.Value < 0L)
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

		// Token: 0x0600106B RID: 4203 RVA: 0x000430D4 File Offset: 0x000412D4
		public override Constant ConvertImplicitly(TypeSpec type)
		{
			if (this.type == type)
			{
				return this;
			}
			Constant constant = this.TryImplicitIntConversion(type);
			if (constant != null)
			{
				return constant;
			}
			return base.ConvertImplicitly(type);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00043100 File Offset: 0x00041300
		private Constant TryImplicitIntConversion(TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (this.Value >= 0 && this.Value <= 255)
				{
					return new ByteConstant(target_type, (byte)this.Value, this.loc);
				}
				break;
			case BuiltinTypeSpec.Type.SByte:
				if (this.Value >= -128 && this.Value <= 127)
				{
					return new SByteConstant(target_type, (sbyte)this.Value, this.loc);
				}
				break;
			case BuiltinTypeSpec.Type.Short:
				if (this.Value >= -32768 && this.Value <= 32767)
				{
					return new ShortConstant(target_type, (short)this.Value, this.loc);
				}
				break;
			case BuiltinTypeSpec.Type.UShort:
				if (this.Value >= 0 && this.Value <= 65535)
				{
					return new UShortConstant(target_type, (ushort)this.Value, this.loc);
				}
				break;
			case BuiltinTypeSpec.Type.UInt:
				if (this.Value >= 0)
				{
					return new UIntConstant(target_type, (uint)this.Value, this.loc);
				}
				break;
			case BuiltinTypeSpec.Type.ULong:
				if (this.Value >= 0)
				{
					return new ULongConstant(target_type, (ulong)((long)this.Value), this.loc);
				}
				break;
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)((float)this.Value), this.loc);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, (double)this.Value, this.loc);
			}
			return null;
		}

		// Token: 0x0400073C RID: 1852
		public readonly int Value;
	}
}
