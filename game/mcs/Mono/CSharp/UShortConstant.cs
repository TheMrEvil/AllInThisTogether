using System;

namespace Mono.CSharp
{
	// Token: 0x02000147 RID: 327
	public class UShortConstant : IntegralConstant
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x00042C41 File Offset: 0x00040E41
		public UShortConstant(BuiltinTypes types, ushort v, Location loc) : this(types.UShort, v, loc)
		{
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00042C51 File Offset: 0x00040E51
		public UShortConstant(TypeSpec type, ushort v, Location loc) : base(type, loc)
		{
			this.Value = v;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00042C62 File Offset: 0x00040E62
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode(this.Value);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00042C70 File Offset: 0x00040E70
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt((int)this.Value);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00042C7E File Offset: 0x00040E7E
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00042C8B File Offset: 0x00040E8B
		public override long GetValueAsLong()
		{
			return (long)((ulong)this.Value);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00042C94 File Offset: 0x00040E94
		public override Constant Increment()
		{
			return new UShortConstant(this.type, checked(this.Value + 1), this.loc);
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x00042CB0 File Offset: 0x00040EB0
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00042CBB File Offset: 0x00040EBB
		public override bool IsOneInteger
		{
			get
			{
				return this.Value == 1;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00042CB0 File Offset: 0x00040EB0
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == 0;
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00042CC8 File Offset: 0x00040EC8
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && this.Value > 255)
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && this.Value > 127)
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Char:
				if (in_checked_context && this.Value > 65535)
				{
					throw new OverflowException();
				}
				return new CharConstant(target_type, (char)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && this.Value > 32767)
				{
					throw new OverflowException();
				}
				return new ShortConstant(target_type, (short)this.Value, base.Location);
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
			}
			return null;
		}

		// Token: 0x0400073B RID: 1851
		public readonly ushort Value;
	}
}
