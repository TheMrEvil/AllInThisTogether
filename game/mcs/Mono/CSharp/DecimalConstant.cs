using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200014E RID: 334
	public class DecimalConstant : Constant
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0004419B File Offset: 0x0004239B
		public DecimalConstant(BuiltinTypes types, decimal d, Location loc) : this(types.Decimal, d, loc)
		{
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000441AB File Offset: 0x000423AB
		public DecimalConstant(TypeSpec type, decimal d, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Value;
			this.Value = d;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000441CC File Offset: 0x000423CC
		public override void Emit(EmitContext ec)
		{
			int[] bits = decimal.GetBits(this.Value);
			int num = bits[3] >> 16 & 255;
			MethodSpec methodSpec;
			if (num == 0)
			{
				if (this.Value <= 2147483647m && this.Value >= -2147483648m)
				{
					methodSpec = ec.Module.PredefinedMembers.DecimalCtorInt.Resolve(this.loc);
					if (methodSpec == null)
					{
						return;
					}
					ec.EmitInt((int)this.Value);
					ec.Emit(OpCodes.Newobj, methodSpec);
					return;
				}
				else if (this.Value <= 9223372036854775807m && this.Value >= -9223372036854775808m)
				{
					methodSpec = ec.Module.PredefinedMembers.DecimalCtorLong.Resolve(this.loc);
					if (methodSpec == null)
					{
						return;
					}
					ec.EmitLong((long)this.Value);
					ec.Emit(OpCodes.Newobj, methodSpec);
					return;
				}
			}
			ec.EmitInt(bits[0]);
			ec.EmitInt(bits[1]);
			ec.EmitInt(bits[2]);
			ec.EmitInt(bits[3] >> 31);
			ec.EmitInt(num);
			methodSpec = ec.Module.PredefinedMembers.DecimalCtor.Resolve(this.loc);
			if (methodSpec != null)
			{
				ec.Emit(OpCodes.Newobj, methodSpec);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00044331 File Offset: 0x00042531
		public override bool IsDefaultValue
		{
			get
			{
				return this.Value == 0m;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00044343 File Offset: 0x00042543
		public override bool IsNegative
		{
			get
			{
				return this.Value < 0m;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00044358 File Offset: 0x00042558
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				return new ByteConstant(target_type, (byte)this.Value, this.loc);
			case BuiltinTypeSpec.Type.SByte:
				return new SByteConstant(target_type, (sbyte)this.Value, this.loc);
			case BuiltinTypeSpec.Type.Char:
				return new CharConstant(target_type, (char)this.Value, this.loc);
			case BuiltinTypeSpec.Type.Short:
				return new ShortConstant(target_type, (short)this.Value, this.loc);
			case BuiltinTypeSpec.Type.UShort:
				return new UShortConstant(target_type, (ushort)this.Value, this.loc);
			case BuiltinTypeSpec.Type.Int:
				return new IntConstant(target_type, (int)this.Value, this.loc);
			case BuiltinTypeSpec.Type.UInt:
				return new UIntConstant(target_type, (uint)this.Value, this.loc);
			case BuiltinTypeSpec.Type.Long:
				return new LongConstant(target_type, (long)this.Value, this.loc);
			case BuiltinTypeSpec.Type.ULong:
				return new ULongConstant(target_type, (ulong)this.Value, this.loc);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(target_type, (double)((float)this.Value), this.loc);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(target_type, (double)this.Value, this.loc);
			default:
				return null;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000444B1 File Offset: 0x000426B1
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000444C0 File Offset: 0x000426C0
		public override string GetValueAsLiteral()
		{
			return this.Value.ToString() + "M";
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0000225C File Offset: 0x0000045C
		public override long GetValueAsLong()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000742 RID: 1858
		public readonly decimal Value;
	}
}
