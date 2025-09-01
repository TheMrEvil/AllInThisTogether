using System;

namespace Mono.CSharp
{
	// Token: 0x02000143 RID: 323
	public class ByteConstant : IntegralConstant
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0004237E File Offset: 0x0004057E
		public ByteConstant(BuiltinTypes types, byte v, Location loc) : this(types.Byte, v, loc)
		{
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004238E File Offset: 0x0004058E
		public ByteConstant(TypeSpec type, byte v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004239F File Offset: 0x0004059F
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000423AD File Offset: 0x000405AD
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt((int)this.Value);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000423BB File Offset: 0x000405BB
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000423C8 File Offset: 0x000405C8
		public override long GetValueAsLong()
		{
			return (long)((ulong)this.Value);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000423D1 File Offset: 0x000405D1
		public override Constant Increment()
		{
			return new ByteConstant(this.type, checked(this.Value + 1), this.loc);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000423ED File Offset: 0x000405ED
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x000423F8 File Offset: 0x000405F8
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x000423ED File Offset: 0x000405ED
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00042404 File Offset: 0x00040604
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && this.Value > 127)
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				return new ShortConstant(target_type, (short)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UShort:
				return new UShortConstant(target_type, (ushort)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Int:
				return new IntConstant(target_type, (int)this.Value, base.Location);
			case BuiltinTypeSpec.Type.UInt:
				return new UIntConstant(target_type, (uint)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Long:
				return new LongConstant(target_type, (long)((ulong)this.Value), base.Location);
			case BuiltinTypeSpec.Type.ULong:
				return new ULongConstant(target_type, (ulong)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)((float)this.Value), base.Location);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, (double)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(target_type, this.Value, base.Location);
			default:
				return null;
			}
		}

		// Token: 0x04000737 RID: 1847
		public readonly byte Value;
	}
}
