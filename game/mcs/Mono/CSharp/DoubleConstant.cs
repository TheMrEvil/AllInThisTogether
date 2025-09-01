using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200014D RID: 333
	public class DoubleConstant : Constant
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x00043DCD File Offset: 0x00041FCD
		public DoubleConstant(BuiltinTypes types, double v, Location loc) : this(types.Double, v, loc)
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00043DDD File Offset: 0x00041FDD
		public DoubleConstant(TypeSpec type, double v, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Value;
			this.Value = v;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00043DFB File Offset: 0x00041FFB
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00043E09 File Offset: 0x00042009
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Ldc_R8, this.Value);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00043E1C File Offset: 0x0004201C
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00043E2C File Offset: 0x0004202C
		public override string GetValueAsLiteral()
		{
			return this.Value.ToString();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0000225C File Offset: 0x0000045C
		public override long GetValueAsLong()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00043E47 File Offset: 0x00042047
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0.0;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00043E5A File Offset: 0x0004205A
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0.0;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00043E70 File Offset: 0x00042070
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && (this.Value < 0.0 || this.Value > 255.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && (this.Value < -128.0 || this.Value > 127.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && (this.Value < 0.0 || this.Value > 65535.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && (this.Value < -32768.0 || this.Value > 32767.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				if (in_checked_context && (this.Value < 0.0 || this.Value > 65535.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Int:
				if (in_checked_context && (this.Value < -2147483648.0 || this.Value > 2147483647.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new IntConstant(target_type, (int)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				if (in_checked_context && (this.Value < 0.0 || this.Value > 4294967295.0 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new UIntConstant(target_type, (uint)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Long:
				if (in_checked_context && (this.Value < -9.223372036854776E+18 || this.Value > 9.223372036854776E+18 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new LongConstant(target_type, (long)this.Value, base.Location);
			case BuiltinTypeSpec.Type.ULong:
				if (in_checked_context && (this.Value < 0.0 || this.Value > 1.8446744073709552E+19 || double.IsNaN(this.Value)))
				{
					throw new OverflowException();
				}
				return new ULongConstant(target_type, (ulong)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)((float)this.Value), base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, (decimal)this.Value, base.Location);
			}
			return null;
		}

		// Token: 0x04000741 RID: 1857
		public readonly double Value;
	}
}
