using System;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000151 RID: 337
	public class NullConstant : Constant
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x000448E5 File Offset: 0x00042AE5
		public NullConstant(TypeSpec type, Location loc) : base(loc)
		{
			this.eclass = ExprClass.Value;
			this.type = type;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000448FC File Offset: 0x00042AFC
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.type == InternalType.NullLiteral || this.type.BuiltinType == BuiltinTypeSpec.Type.Object)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this));
				return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
			}
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00044950 File Offset: 0x00042B50
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			switch (targetType.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Object:
				enc.Encode(rc.Module.Compiler.BuiltinTypes.String);
				break;
			case BuiltinTypeSpec.Type.Dynamic:
				goto IL_4A;
			case BuiltinTypeSpec.Type.String:
			case BuiltinTypeSpec.Type.Type:
				break;
			default:
				goto IL_4A;
			}
			enc.Encode(byte.MaxValue);
			return;
			IL_4A:
			ArrayContainer arrayContainer = targetType as ArrayContainer;
			if (arrayContainer != null && arrayContainer.Rank == 1 && !arrayContainer.Element.IsArray)
			{
				enc.Encode(uint.MaxValue);
				return;
			}
			base.EncodeAttributeValue(rc, enc, targetType, parameterType);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000449DA File Offset: 0x00042BDA
		public override void Emit(EmitContext ec)
		{
			ec.EmitNull();
			if (this.type.IsGenericParameter)
			{
				ec.Emit(OpCodes.Unbox_Any, this.type);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00044A00 File Offset: 0x00042C00
		public override string ExprClassName
		{
			get
			{
				return this.GetSignatureForError();
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00044A08 File Offset: 0x00042C08
		public override Constant ConvertExplicitly(bool inCheckedContext, TypeSpec targetType)
		{
			if (targetType.IsPointer)
			{
				if (this.IsLiteral || this is NullPointer)
				{
					return new NullPointer(targetType, this.loc);
				}
				return null;
			}
			else
			{
				if (targetType.Kind == MemberKind.InternalCompilerType && targetType.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
				{
					return null;
				}
				if (!this.IsLiteral && !Convert.ImplicitStandardConversionExists(this, targetType))
				{
					return null;
				}
				if (TypeSpec.IsReferenceType(targetType))
				{
					return new NullConstant(targetType, this.loc);
				}
				if (targetType.IsNullableType)
				{
					return LiftedNull.Create(targetType, this.loc);
				}
				return null;
			}
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00044A93 File Offset: 0x00042C93
		public override Constant ConvertImplicitly(TypeSpec targetType)
		{
			return this.ConvertExplicitly(false, targetType);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00044A9D File Offset: 0x00042C9D
		public override string GetSignatureForError()
		{
			return "null";
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000055E7 File Offset: 0x000037E7
		public override object GetValue()
		{
			return null;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00044A00 File Offset: 0x00042C00
		public override string GetValueAsLiteral()
		{
			return this.GetSignatureForError();
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0000225C File Offset: 0x0000045C
		public override long GetValueAsLong()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsDefaultValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsZeroInteger
		{
			get
			{
				return true;
			}
		}
	}
}
