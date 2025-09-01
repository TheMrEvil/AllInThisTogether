using System;
using System.Globalization;
using System.Linq.Expressions;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000140 RID: 320
	public abstract class Constant : Expression
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x000419DF File Offset: 0x0003FBDF
		protected Constant(Location loc)
		{
			this.loc = loc;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000419EE File Offset: 0x0003FBEE
		public override string ToString()
		{
			return base.GetType().Name + " (" + this.GetValueAsLiteral() + ")";
		}

		// Token: 0x06000FF6 RID: 4086
		public abstract object GetValue();

		// Token: 0x06000FF7 RID: 4087
		public abstract long GetValueAsLong();

		// Token: 0x06000FF8 RID: 4088
		public abstract string GetValueAsLiteral();

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00041A10 File Offset: 0x0003FC10
		public virtual object GetTypedValue()
		{
			return this.GetValue();
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00041A18 File Offset: 0x0003FC18
		public override void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec target, bool expl)
		{
			if (!expl && this.IsLiteral && this.type.BuiltinType != BuiltinTypeSpec.Type.Double && BuiltinTypeSpec.IsPrimitiveTypeOrDecimal(target) && BuiltinTypeSpec.IsPrimitiveTypeOrDecimal(this.type))
			{
				ec.Report.Error(31, this.loc, "Constant value `{0}' cannot be converted to a `{1}'", this.GetValueAsLiteral(), target.GetSignatureForError());
				return;
			}
			base.Error_ValueCannotBeConverted(ec, target, expl);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00041A82 File Offset: 0x0003FC82
		public Constant ImplicitConversionRequired(ResolveContext ec, TypeSpec type)
		{
			Constant constant = this.ConvertImplicitly(type);
			if (constant == null)
			{
				this.Error_ValueCannotBeConverted(ec, type, false);
			}
			return constant;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00041A98 File Offset: 0x0003FC98
		public virtual Constant ConvertImplicitly(TypeSpec type)
		{
			if (this.type == type)
			{
				return this;
			}
			if (!Convert.ImplicitNumericConversionExists(this.type, type))
			{
				return null;
			}
			bool flag;
			object v = Constant.ChangeType(this.GetValue(), type, out flag);
			if (flag)
			{
				throw new InternalErrorException("Missing constant conversion between `{0}' and `{1}'", new object[]
				{
					base.Type.GetSignatureForError(),
					type.GetSignatureForError()
				});
			}
			return Constant.CreateConstantFromValue(type, v, this.loc);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00041B08 File Offset: 0x0003FD08
		public static Constant CreateConstantFromValue(TypeSpec t, object v, Location loc)
		{
			switch (t.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
				return new BoolConstant(t, (bool)v, loc);
			case BuiltinTypeSpec.Type.Byte:
				return new ByteConstant(t, (byte)v, loc);
			case BuiltinTypeSpec.Type.SByte:
				return new SByteConstant(t, (sbyte)v, loc);
			case BuiltinTypeSpec.Type.Char:
				return new CharConstant(t, (char)v, loc);
			case BuiltinTypeSpec.Type.Short:
				return new ShortConstant(t, (short)v, loc);
			case BuiltinTypeSpec.Type.UShort:
				return new UShortConstant(t, (ushort)v, loc);
			case BuiltinTypeSpec.Type.Int:
				return new IntConstant(t, (int)v, loc);
			case BuiltinTypeSpec.Type.UInt:
				return new UIntConstant(t, (uint)v, loc);
			case BuiltinTypeSpec.Type.Long:
				return new LongConstant(t, (long)v, loc);
			case BuiltinTypeSpec.Type.ULong:
				return new ULongConstant(t, (ulong)v, loc);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(t, (double)((float)v), loc);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(t, (double)v, loc);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(t, (decimal)v, loc);
			case BuiltinTypeSpec.Type.String:
				return new StringConstant(t, (string)v, loc);
			}
			if (t.IsEnum)
			{
				return new EnumConstant(Constant.CreateConstantFromValue(EnumSpec.GetUnderlyingType(t), v, loc), t);
			}
			if (v == null)
			{
				if (t.IsNullableType)
				{
					return LiftedNull.Create(t, loc);
				}
				if (TypeSpec.IsReferenceType(t))
				{
					return new NullConstant(t, loc);
				}
			}
			return null;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00041C78 File Offset: 0x0003FE78
		public static Constant ExtractConstantFromValue(TypeSpec t, object v, Location loc)
		{
			switch (t.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
				if (v is bool)
				{
					return new BoolConstant(t, (bool)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Byte:
				if (v is byte)
				{
					return new ByteConstant(t, (byte)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.SByte:
				if (v is sbyte)
				{
					return new SByteConstant(t, (sbyte)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Char:
				if (v is char)
				{
					return new CharConstant(t, (char)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Short:
				if (v is short)
				{
					return new ShortConstant(t, (short)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.UShort:
				if (v is ushort)
				{
					return new UShortConstant(t, (ushort)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Int:
				if (v is int)
				{
					return new IntConstant(t, (int)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.UInt:
				if (v is uint)
				{
					return new UIntConstant(t, (uint)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Long:
				if (v is long)
				{
					return new LongConstant(t, (long)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.ULong:
				if (v is ulong)
				{
					return new ULongConstant(t, (ulong)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Float:
				if (v is float)
				{
					return new FloatConstant(t, (double)((float)v), loc);
				}
				break;
			case BuiltinTypeSpec.Type.Double:
				if (v is double)
				{
					return new DoubleConstant(t, (double)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.Decimal:
				if (v is decimal)
				{
					return new DecimalConstant(t, (decimal)v, loc);
				}
				break;
			case BuiltinTypeSpec.Type.String:
				if (v is string)
				{
					return new StringConstant(t, (string)v, loc);
				}
				break;
			}
			if (t.IsEnum)
			{
				return new EnumConstant(Constant.CreateConstantFromValue(EnumSpec.GetUnderlyingType(t), v, loc), t);
			}
			if (v == null)
			{
				if (t.IsNullableType)
				{
					return LiftedNull.Create(t, loc);
				}
				if (TypeSpec.IsReferenceType(t))
				{
					return new NullConstant(t, loc);
				}
			}
			return null;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00041E70 File Offset: 0x00040070
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this));
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
		}

		// Token: 0x06001001 RID: 4097
		public abstract Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type);

		// Token: 0x06001002 RID: 4098 RVA: 0x00041EBC File Offset: 0x000400BC
		private static object ChangeType(object value, TypeSpec targetType, out bool error)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				error = true;
				return null;
			}
			error = false;
			try
			{
				switch (targetType.BuiltinType)
				{
				case BuiltinTypeSpec.Type.FirstPrimitive:
					return convertible.ToBoolean(Constant.nfi);
				case BuiltinTypeSpec.Type.Byte:
					return convertible.ToByte(Constant.nfi);
				case BuiltinTypeSpec.Type.SByte:
					return convertible.ToSByte(Constant.nfi);
				case BuiltinTypeSpec.Type.Char:
					return convertible.ToChar(Constant.nfi);
				case BuiltinTypeSpec.Type.Short:
					return convertible.ToInt16(Constant.nfi);
				case BuiltinTypeSpec.Type.UShort:
					return convertible.ToUInt16(Constant.nfi);
				case BuiltinTypeSpec.Type.Int:
					return convertible.ToInt32(Constant.nfi);
				case BuiltinTypeSpec.Type.UInt:
					return convertible.ToUInt32(Constant.nfi);
				case BuiltinTypeSpec.Type.Long:
					return convertible.ToInt64(Constant.nfi);
				case BuiltinTypeSpec.Type.ULong:
					return convertible.ToUInt64(Constant.nfi);
				case BuiltinTypeSpec.Type.Float:
					if (convertible.GetType() == typeof(char))
					{
						return (float)convertible.ToInt32(Constant.nfi);
					}
					return convertible.ToSingle(Constant.nfi);
				case BuiltinTypeSpec.Type.Double:
					if (convertible.GetType() == typeof(char))
					{
						return (double)convertible.ToInt32(Constant.nfi);
					}
					return convertible.ToDouble(Constant.nfi);
				case BuiltinTypeSpec.Type.Decimal:
					if (convertible.GetType() == typeof(char))
					{
						return convertible.ToInt32(Constant.nfi);
					}
					return convertible.ToDecimal(Constant.nfi);
				case BuiltinTypeSpec.Type.Object:
					return value;
				case BuiltinTypeSpec.Type.String:
					return convertible.ToString(Constant.nfi);
				}
			}
			catch
			{
			}
			error = true;
			return null;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext rc)
		{
			return this;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00042100 File Offset: 0x00040300
		public Constant Reduce(ResolveContext ec, TypeSpec target_type)
		{
			Constant result;
			try
			{
				result = this.TryReduceConstant(ec, target_type);
			}
			catch (OverflowException)
			{
				if (ec.ConstantCheckState && base.Type.BuiltinType != BuiltinTypeSpec.Type.Decimal)
				{
					ec.Report.Error(221, this.loc, "Constant value `{0}' cannot be converted to a `{1}' (use `unchecked' syntax to override)", this.GetValueAsLiteral(), target_type.GetSignatureForError());
				}
				else
				{
					this.Error_ValueCannotBeConverted(ec, target_type, false);
				}
				result = New.Constantify(target_type, this.loc);
			}
			return result;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00042184 File Offset: 0x00040384
		public Constant TryReduce(ResolveContext rc, TypeSpec targetType)
		{
			Constant result;
			try
			{
				result = this.TryReduceConstant(rc, targetType);
			}
			catch (OverflowException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000421B4 File Offset: 0x000403B4
		private Constant TryReduceConstant(ResolveContext ec, TypeSpec target_type)
		{
			if (base.Type == target_type)
			{
				if (this.IsLiteral)
				{
					return Constant.CreateConstantFromValue(target_type, this.GetValue(), this.loc);
				}
				return this;
			}
			else
			{
				if (!target_type.IsEnum)
				{
					return this.ConvertExplicitly(ec.ConstantCheckState, target_type);
				}
				Constant constant = this.TryReduceConstant(ec, EnumSpec.GetUnderlyingType(target_type));
				if (constant == null)
				{
					return null;
				}
				return new EnumConstant(constant, target_type);
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00042217 File Offset: 0x00040417
		public bool IsDefaultInitializer(TypeSpec type)
		{
			if (type == base.Type)
			{
				return this.IsDefaultValue;
			}
			return this is NullLiteral;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001008 RID: 4104
		public abstract bool IsDefaultValue { get; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001009 RID: 4105
		public abstract bool IsNegative { get; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsLiteral
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsOneInteger
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsZeroInteger
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitSideEffect(EmitContext ec)
		{
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00005936 File Offset: 0x00003B36
		public sealed override Expression Clone(CloneContext clonectx)
		{
			return this;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00042232 File Offset: 0x00040432
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			throw new NotSupportedException("should not be reached");
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004223E File Offset: 0x0004043E
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Constant(this.GetTypedValue(), this.type.GetMetaInfo());
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0000212D File Offset: 0x0000032D
		public new bool Resolve(ResolveContext rc)
		{
			return true;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00042256 File Offset: 0x00040456
		// Note: this type is marked as 'beforefieldinit'.
		static Constant()
		{
		}

		// Token: 0x04000735 RID: 1845
		private static readonly NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;
	}
}
