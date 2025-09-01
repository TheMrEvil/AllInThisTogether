using System;

namespace Mono.CSharp
{
	// Token: 0x02000144 RID: 324
	public class CharConstant : Constant
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x00042541 File Offset: 0x00040741
		public CharConstant(BuiltinTypes types, char v, Location loc) : this(types.Char, v, loc)
		{
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00042551 File Offset: 0x00040751
		public CharConstant(TypeSpec type, char v, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Value;
			this.Value = v;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0004256F File Offset: 0x0004076F
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			enc.Encode((ushort)this.Value);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004257D File Offset: 0x0004077D
		public override void Emit(EmitContext ec)
		{
			ec.EmitInt((int)this.Value);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004258C File Offset: 0x0004078C
		private static string descape(char c)
		{
			if (c <= '"')
			{
				switch (c)
				{
				case '\0':
					return "\\0";
				case '\u0001':
				case '\u0002':
				case '\u0003':
				case '\u0004':
				case '\u0005':
				case '\u0006':
					break;
				case '\a':
					return "\\a";
				case '\b':
					return "\\b";
				case '\t':
					return "\\t";
				case '\n':
					return "\\n";
				case '\v':
					return "\\v";
				case '\f':
					return "\\f";
				case '\r':
					return "\\r";
				default:
					if (c == '"')
					{
						return "\\\"";
					}
					break;
				}
			}
			else
			{
				if (c == '\'')
				{
					return "\\'";
				}
				if (c == '\\')
				{
					return "\\\\";
				}
			}
			return c.ToString();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00042638 File Offset: 0x00040838
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00042645 File Offset: 0x00040845
		public override long GetValueAsLong()
		{
			return (long)((ulong)this.Value);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0004264E File Offset: 0x0004084E
		public override string GetValueAsLiteral()
		{
			return "\"" + CharConstant.descape(this.Value) + "\"";
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0004266A File Offset: 0x0004086A
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == '\0';
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0004266A File Offset: 0x0004086A
		public override bool IsZeroInteger
		{
			get
			{
				return this.Value == '\0';
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00042678 File Offset: 0x00040878
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				if (in_checked_context && (this.Value < '\0' || this.Value > 'ÿ'))
				{
					throw new OverflowException();
				}
				return new ByteConstant(target_type, (byte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.SByte:
				if (in_checked_context && this.Value > '\u007f')
				{
					throw new OverflowException();
				}
				return new SByteConstant(target_type, (sbyte)this.Value, base.Location);
			case BuiltinTypeSpec.Type.Short:
				if (in_checked_context && this.Value > '翿')
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

		// Token: 0x04000738 RID: 1848
		public readonly char Value;
	}
}
