using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200014C RID: 332
	public class FloatConstant : Constant
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x00043A24 File Offset: 0x00041C24
		public FloatConstant(BuiltinTypes types, double v, Location loc) : this(types.Float, v, loc)
		{
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00043A34 File Offset: 0x00041C34
		public FloatConstant(TypeSpec type, double v, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Value;
			this.DoubleValue = v;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00043A52 File Offset: 0x00041C52
		public override Constant ConvertImplicitly(TypeSpec type)
		{
			if (type.BuiltinType == BuiltinTypeSpec.Type.Double)
			{
				return new DoubleConstant(type, this.DoubleValue, this.loc);
			}
			return base.ConvertImplicitly(type);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00043A78 File Offset: 0x00041C78
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00043A86 File Offset: 0x00041C86
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Ldc_R4, this.Value);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00043A99 File Offset: 0x00041C99
		public float Value
		{
			get
			{
				return (float)this.DoubleValue;
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00043AA2 File Offset: 0x00041CA2
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00043AB0 File Offset: 0x00041CB0
		public override string GetValueAsLiteral()
		{
			return this.Value.ToString();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0000225C File Offset: 0x0000045C
		public override long GetValueAsLong()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00043ACB File Offset: 0x00041CCB
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0f;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00043ADA File Offset: 0x00041CDA
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0f;
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00043AEC File Offset: 0x00041CEC
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && (this.Value < 0f || this.Value > 255f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && (this.Value < -128f || this.Value > 127f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && (this.Value < 0f || this.Value > 65535f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && (this.Value < -32768f || this.Value > 32767f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && (this.Value < 0f || this.Value > 65535f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.Int:
				if (in_checked_context && (this.Value < -2.1474836E+09f || this.Value > 2.1474836E+09f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new IntConstant(target_type, (int)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				if (in_checked_context && (this.Value < 0f || this.Value > 4.2949673E+09f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new UIntConstant(target_type, (uint)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.Long:
				if (in_checked_context && (this.Value < -9.223372E+18f || this.Value > 9.223372E+18f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new LongConstant(target_type, (long)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.ULong:
				if (in_checked_context && (this.Value < 0f || this.Value > 1.8446744E+19f || float.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new ULongConstant(target_type, (ulong)this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, this.DoubleValue, base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, (decimal)this.DoubleValue, base.Location);
			}
			return null;
		}

		// Token: 0x04000740 RID: 1856
		public readonly double DoubleValue;
	}
}
