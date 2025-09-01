using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection
{
	// Token: 0x02000057 RID: 87
	internal abstract class Signature
	{
		// Token: 0x06000441 RID: 1089
		internal abstract void WriteSig(ModuleBuilder module, ByteBuffer bb);

		// Token: 0x06000442 RID: 1090 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		private static Type ReadGenericInst(ModuleReader module, ByteReader br, IGenericContext context)
		{
			byte b = br.ReadByte();
			Type type;
			if (b != 17)
			{
				if (b != 18)
				{
					throw new BadImageFormatException();
				}
				type = Signature.ReadTypeDefOrRefEncoded(module, br, context).MarkNotValueType();
			}
			else
			{
				type = Signature.ReadTypeDefOrRefEncoded(module, br, context).MarkValueType();
			}
			if (!type.__IsMissing && !type.IsGenericTypeDefinition)
			{
				throw new BadImageFormatException();
			}
			int num = br.ReadCompressedUInt();
			Type[] array = new Type[num];
			CustomModifiers[] array2 = null;
			for (int i = 0; i < num; i++)
			{
				CustomModifiers customModifiers = CustomModifiers.Read(module, br, context);
				if (!customModifiers.IsEmpty)
				{
					if (array2 == null)
					{
						array2 = new CustomModifiers[num];
					}
					array2[i] = customModifiers;
				}
				array[i] = Signature.ReadType(module, br, context);
			}
			return GenericTypeInstance.Make(type, array, array2);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000D084 File Offset: 0x0000B284
		internal static Type ReadTypeSpec(ModuleReader module, ByteReader br, IGenericContext context)
		{
			CustomModifiers.Skip(br);
			return Signature.ReadType(module, br, context);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D094 File Offset: 0x0000B294
		private static Type ReadFunctionPointer(ModuleReader module, ByteReader br, IGenericContext context)
		{
			__StandAloneMethodSig sig = MethodSignature.ReadStandAloneMethodSig(module, br, context);
			if (module.universe.EnableFunctionPointers)
			{
				return FunctionPointerType.Make(module.universe, sig);
			}
			return module.universe.System_IntPtr;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		internal static Type[] ReadMethodSpec(ModuleReader module, ByteReader br, IGenericContext context)
		{
			if (br.ReadByte() != 10)
			{
				throw new BadImageFormatException();
			}
			Type[] array = new Type[br.ReadCompressedUInt()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Signature.ReadType(module, br, context);
			}
			return array;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000D114 File Offset: 0x0000B314
		private static int[] ReadArraySizes(ByteReader br)
		{
			int num = br.ReadCompressedUInt();
			if (num == 0)
			{
				return null;
			}
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = br.ReadCompressedUInt();
			}
			return array;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D14C File Offset: 0x0000B34C
		private static int[] ReadArrayBounds(ByteReader br)
		{
			int num = br.ReadCompressedUInt();
			if (num == 0)
			{
				return null;
			}
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = br.ReadCompressedInt();
			}
			return array;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D182 File Offset: 0x0000B382
		private static Type ReadTypeOrVoid(ModuleReader module, ByteReader br, IGenericContext context)
		{
			if (br.PeekByte() == 1)
			{
				br.ReadByte();
				return module.universe.System_Void;
			}
			return Signature.ReadType(module, br, context);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		protected static Type ReadType(ModuleReader module, ByteReader br, IGenericContext context)
		{
			switch (br.ReadByte())
			{
			case 2:
				return module.universe.System_Boolean;
			case 3:
				return module.universe.System_Char;
			case 4:
				return module.universe.System_SByte;
			case 5:
				return module.universe.System_Byte;
			case 6:
				return module.universe.System_Int16;
			case 7:
				return module.universe.System_UInt16;
			case 8:
				return module.universe.System_Int32;
			case 9:
				return module.universe.System_UInt32;
			case 10:
				return module.universe.System_Int64;
			case 11:
				return module.universe.System_UInt64;
			case 12:
				return module.universe.System_Single;
			case 13:
				return module.universe.System_Double;
			case 14:
				return module.universe.System_String;
			case 15:
			{
				CustomModifiers customModifiers = CustomModifiers.Read(module, br, context);
				return Signature.ReadTypeOrVoid(module, br, context).__MakePointerType(customModifiers);
			}
			case 17:
				return Signature.ReadTypeDefOrRefEncoded(module, br, context).MarkValueType();
			case 18:
				return Signature.ReadTypeDefOrRefEncoded(module, br, context).MarkNotValueType();
			case 19:
				return context.GetGenericTypeArgument(br.ReadCompressedUInt());
			case 20:
			{
				CustomModifiers customModifiers = CustomModifiers.Read(module, br, context);
				return Signature.ReadType(module, br, context).__MakeArrayType(br.ReadCompressedUInt(), Signature.ReadArraySizes(br), Signature.ReadArrayBounds(br), customModifiers);
			}
			case 21:
				return Signature.ReadGenericInst(module, br, context);
			case 24:
				return module.universe.System_IntPtr;
			case 25:
				return module.universe.System_UIntPtr;
			case 27:
				return Signature.ReadFunctionPointer(module, br, context);
			case 28:
				return module.universe.System_Object;
			case 29:
			{
				CustomModifiers customModifiers = CustomModifiers.Read(module, br, context);
				return Signature.ReadType(module, br, context).__MakeArrayType(customModifiers);
			}
			case 30:
				return context.GetGenericMethodArgument(br.ReadCompressedUInt());
			}
			throw new BadImageFormatException();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal static void ReadLocalVarSig(ModuleReader module, ByteReader br, IGenericContext context, List<LocalVariableInfo> list)
		{
			if (br.Length < 2 || br.ReadByte() != 7)
			{
				throw new BadImageFormatException("Invalid local variable signature");
			}
			int num = br.ReadCompressedUInt();
			for (int i = 0; i < num; i++)
			{
				if (br.PeekByte() == 22)
				{
					br.ReadByte();
					list.Add(new LocalVariableInfo(i, module.universe.System_TypedReference, false, default(CustomModifiers)));
				}
				else
				{
					CustomModifiers mods = CustomModifiers.Read(module, br, context);
					bool pinned = false;
					if (br.PeekByte() == 69)
					{
						br.ReadByte();
						pinned = true;
					}
					CustomModifiers mods2 = CustomModifiers.Read(module, br, context);
					Type type = Signature.ReadTypeOrByRef(module, br, context);
					list.Add(new LocalVariableInfo(i, type, pinned, CustomModifiers.Combine(mods, mods2)));
				}
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D468 File Offset: 0x0000B668
		private static Type ReadTypeOrByRef(ModuleReader module, ByteReader br, IGenericContext context)
		{
			if (br.PeekByte() == 16)
			{
				br.ReadByte();
				CustomModifiers customModifiers = CustomModifiers.Read(module, br, context);
				return Signature.ReadTypeOrVoid(module, br, context).__MakeByRefType(customModifiers);
			}
			return Signature.ReadType(module, br, context);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		protected static Type ReadRetType(ModuleReader module, ByteReader br, IGenericContext context)
		{
			byte b = br.PeekByte();
			if (b == 1)
			{
				br.ReadByte();
				return module.universe.System_Void;
			}
			if (b != 22)
			{
				return Signature.ReadTypeOrByRef(module, br, context);
			}
			br.ReadByte();
			return module.universe.System_TypedReference;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		protected static Type ReadParam(ModuleReader module, ByteReader br, IGenericContext context)
		{
			byte b = br.PeekByte();
			if (b == 22)
			{
				br.ReadByte();
				return module.universe.System_TypedReference;
			}
			return Signature.ReadTypeOrByRef(module, br, context);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000D52C File Offset: 0x0000B72C
		protected static void WriteType(ModuleBuilder module, ByteBuffer bb, Type type)
		{
			while (type.HasElementType)
			{
				byte sigElementType = type.SigElementType;
				bb.Write(sigElementType);
				if (sigElementType == 20)
				{
					Signature.WriteCustomModifiers(module, bb, type.__GetCustomModifiers());
					Signature.WriteType(module, bb, type.GetElementType());
					bb.WriteCompressedUInt(type.GetArrayRank());
					int[] array = type.__GetArraySizes();
					bb.WriteCompressedUInt(array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						bb.WriteCompressedUInt(array[i]);
					}
					int[] array2 = type.__GetArrayLowerBounds();
					bb.WriteCompressedUInt(array2.Length);
					for (int j = 0; j < array2.Length; j++)
					{
						bb.WriteCompressedInt(array2[j]);
					}
					return;
				}
				Signature.WriteCustomModifiers(module, bb, type.__GetCustomModifiers());
				type = type.GetElementType();
			}
			if (type.__IsBuiltIn)
			{
				bb.Write(type.SigElementType);
				return;
			}
			if (type.IsGenericParameter)
			{
				bb.Write(type.SigElementType);
				bb.WriteCompressedUInt(type.GenericParameterPosition);
				return;
			}
			if (!type.__IsMissing && type.IsGenericType)
			{
				Signature.WriteGenericSignature(module, bb, type);
				return;
			}
			if (type.__IsFunctionPointer)
			{
				bb.Write(27);
				Signature.WriteStandAloneMethodSig(module, bb, type.__MethodSignature);
				return;
			}
			if (type.IsValueType)
			{
				bb.Write(17);
			}
			else
			{
				bb.Write(18);
			}
			bb.WriteTypeDefOrRefEncoded(module.GetTypeToken(type).Token);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D688 File Offset: 0x0000B888
		private static void WriteGenericSignature(ModuleBuilder module, ByteBuffer bb, Type type)
		{
			Type[] genericArguments = type.GetGenericArguments();
			CustomModifiers[] array = type.__GetGenericArgumentsCustomModifiers();
			if (!type.IsGenericTypeDefinition)
			{
				type = type.GetGenericTypeDefinition();
			}
			bb.Write(21);
			if (type.IsValueType)
			{
				bb.Write(17);
			}
			else
			{
				bb.Write(18);
			}
			bb.WriteTypeDefOrRefEncoded(module.GetTypeToken(type).Token);
			bb.WriteCompressedUInt(genericArguments.Length);
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Signature.WriteCustomModifiers(module, bb, array[i]);
				Signature.WriteType(module, bb, genericArguments[i]);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000D71C File Offset: 0x0000B91C
		protected static void WriteCustomModifiers(ModuleBuilder module, ByteBuffer bb, CustomModifiers modifiers)
		{
			foreach (CustomModifiers.Entry entry in modifiers)
			{
				bb.Write(entry.IsRequired ? 31 : 32);
				bb.WriteTypeDefOrRefEncoded(module.GetTypeTokenForMemberRef(entry.Type));
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D78C File Offset: 0x0000B98C
		internal static Type ReadTypeDefOrRefEncoded(ModuleReader module, ByteReader br, IGenericContext context)
		{
			int num = br.ReadCompressedUInt();
			switch (num & 3)
			{
			case 0:
				return module.ResolveType((2 << 24) + (num >> 2), null, null);
			case 1:
				return module.ResolveType((1 << 24) + (num >> 2), null, null);
			case 2:
				return module.ResolveType((27 << 24) + (num >> 2), context);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		internal static void WriteStandAloneMethodSig(ModuleBuilder module, ByteBuffer bb, __StandAloneMethodSig sig)
		{
			if (sig.IsUnmanaged)
			{
				switch (sig.UnmanagedCallingConvention)
				{
				case CallingConvention.Winapi:
				case CallingConvention.StdCall:
					bb.Write(2);
					break;
				case CallingConvention.Cdecl:
					bb.Write(1);
					break;
				case CallingConvention.ThisCall:
					bb.Write(3);
					break;
				case CallingConvention.FastCall:
					bb.Write(4);
					break;
				default:
					throw new ArgumentOutOfRangeException("callingConvention");
				}
			}
			else
			{
				CallingConventions callingConvention = sig.CallingConvention;
				byte b = 0;
				if ((callingConvention & CallingConventions.HasThis) != (CallingConventions)0)
				{
					b |= 32;
				}
				if ((callingConvention & CallingConventions.ExplicitThis) != (CallingConventions)0)
				{
					b |= 64;
				}
				if ((callingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
				{
					b |= 5;
				}
				bb.Write(b);
			}
			Type[] parameterTypes = sig.ParameterTypes;
			Type[] optionalParameterTypes = sig.OptionalParameterTypes;
			bb.WriteCompressedUInt(parameterTypes.Length + optionalParameterTypes.Length);
			Signature.WriteCustomModifiers(module, bb, sig.GetReturnTypeCustomModifiers());
			Signature.WriteType(module, bb, sig.ReturnType);
			int num = 0;
			foreach (Type type in parameterTypes)
			{
				Signature.WriteCustomModifiers(module, bb, sig.GetParameterCustomModifiers(num++));
				Signature.WriteType(module, bb, type);
			}
			if (optionalParameterTypes.Length != 0)
			{
				bb.Write(65);
				foreach (Type type2 in optionalParameterTypes)
				{
					Signature.WriteCustomModifiers(module, bb, sig.GetParameterCustomModifiers(num++));
					Signature.WriteType(module, bb, type2);
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000D944 File Offset: 0x0000BB44
		internal static void WriteTypeSpec(ModuleBuilder module, ByteBuffer bb, Type type)
		{
			Signature.WriteType(module, bb, type);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D950 File Offset: 0x0000BB50
		internal static void WriteMethodSpec(ModuleBuilder module, ByteBuffer bb, Type[] genArgs)
		{
			bb.Write(10);
			bb.WriteCompressedUInt(genArgs.Length);
			foreach (Type type in genArgs)
			{
				Signature.WriteType(module, bb, type);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D98C File Offset: 0x0000BB8C
		internal static Type[] ReadOptionalParameterTypes(ModuleReader module, ByteReader br, IGenericContext context, out CustomModifiers[] customModifiers)
		{
			br.ReadByte();
			int num = br.ReadCompressedUInt();
			CustomModifiers.Skip(br);
			Signature.ReadRetType(module, br, context);
			for (int i = 0; i < num; i++)
			{
				if (br.PeekByte() == 65)
				{
					br.ReadByte();
					Type[] array = new Type[num - i];
					customModifiers = new CustomModifiers[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						customModifiers[j] = CustomModifiers.Read(module, br, context);
						array[j] = Signature.ReadType(module, br, context);
					}
					return array;
				}
				CustomModifiers.Skip(br);
				Signature.ReadType(module, br, context);
			}
			customModifiers = Empty<CustomModifiers>.Array;
			return Type.EmptyTypes;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		protected static Type[] BindTypeParameters(IGenericBinder binder, Type[] types)
		{
			if (types == null || types.Length == 0)
			{
				return Type.EmptyTypes;
			}
			Type[] array = new Type[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = types[i].BindTypeParameters(binder);
			}
			return array;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		internal static void WriteSignatureHelper(ModuleBuilder module, ByteBuffer bb, byte flags, ushort paramCount, List<Type> args)
		{
			bb.Write(flags);
			if (flags != 6)
			{
				bb.WriteCompressedUInt((int)paramCount);
			}
			foreach (Type type in args)
			{
				if (type == null)
				{
					bb.Write(1);
				}
				else if (type is MarkerType)
				{
					bb.Write(type.SigElementType);
				}
				else
				{
					Signature.WriteType(module, bb, type);
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Signature()
		{
		}

		// Token: 0x040001C9 RID: 457
		internal const byte DEFAULT = 0;

		// Token: 0x040001CA RID: 458
		internal const byte VARARG = 5;

		// Token: 0x040001CB RID: 459
		internal const byte GENERIC = 16;

		// Token: 0x040001CC RID: 460
		internal const byte HASTHIS = 32;

		// Token: 0x040001CD RID: 461
		internal const byte EXPLICITTHIS = 64;

		// Token: 0x040001CE RID: 462
		internal const byte FIELD = 6;

		// Token: 0x040001CF RID: 463
		internal const byte LOCAL_SIG = 7;

		// Token: 0x040001D0 RID: 464
		internal const byte PROPERTY = 8;

		// Token: 0x040001D1 RID: 465
		internal const byte GENERICINST = 10;

		// Token: 0x040001D2 RID: 466
		internal const byte SENTINEL = 65;

		// Token: 0x040001D3 RID: 467
		internal const byte ELEMENT_TYPE_VOID = 1;

		// Token: 0x040001D4 RID: 468
		internal const byte ELEMENT_TYPE_BOOLEAN = 2;

		// Token: 0x040001D5 RID: 469
		internal const byte ELEMENT_TYPE_CHAR = 3;

		// Token: 0x040001D6 RID: 470
		internal const byte ELEMENT_TYPE_I1 = 4;

		// Token: 0x040001D7 RID: 471
		internal const byte ELEMENT_TYPE_U1 = 5;

		// Token: 0x040001D8 RID: 472
		internal const byte ELEMENT_TYPE_I2 = 6;

		// Token: 0x040001D9 RID: 473
		internal const byte ELEMENT_TYPE_U2 = 7;

		// Token: 0x040001DA RID: 474
		internal const byte ELEMENT_TYPE_I4 = 8;

		// Token: 0x040001DB RID: 475
		internal const byte ELEMENT_TYPE_U4 = 9;

		// Token: 0x040001DC RID: 476
		internal const byte ELEMENT_TYPE_I8 = 10;

		// Token: 0x040001DD RID: 477
		internal const byte ELEMENT_TYPE_U8 = 11;

		// Token: 0x040001DE RID: 478
		internal const byte ELEMENT_TYPE_R4 = 12;

		// Token: 0x040001DF RID: 479
		internal const byte ELEMENT_TYPE_R8 = 13;

		// Token: 0x040001E0 RID: 480
		internal const byte ELEMENT_TYPE_STRING = 14;

		// Token: 0x040001E1 RID: 481
		internal const byte ELEMENT_TYPE_PTR = 15;

		// Token: 0x040001E2 RID: 482
		internal const byte ELEMENT_TYPE_BYREF = 16;

		// Token: 0x040001E3 RID: 483
		internal const byte ELEMENT_TYPE_VALUETYPE = 17;

		// Token: 0x040001E4 RID: 484
		internal const byte ELEMENT_TYPE_CLASS = 18;

		// Token: 0x040001E5 RID: 485
		internal const byte ELEMENT_TYPE_VAR = 19;

		// Token: 0x040001E6 RID: 486
		internal const byte ELEMENT_TYPE_ARRAY = 20;

		// Token: 0x040001E7 RID: 487
		internal const byte ELEMENT_TYPE_GENERICINST = 21;

		// Token: 0x040001E8 RID: 488
		internal const byte ELEMENT_TYPE_TYPEDBYREF = 22;

		// Token: 0x040001E9 RID: 489
		internal const byte ELEMENT_TYPE_I = 24;

		// Token: 0x040001EA RID: 490
		internal const byte ELEMENT_TYPE_U = 25;

		// Token: 0x040001EB RID: 491
		internal const byte ELEMENT_TYPE_FNPTR = 27;

		// Token: 0x040001EC RID: 492
		internal const byte ELEMENT_TYPE_OBJECT = 28;

		// Token: 0x040001ED RID: 493
		internal const byte ELEMENT_TYPE_SZARRAY = 29;

		// Token: 0x040001EE RID: 494
		internal const byte ELEMENT_TYPE_MVAR = 30;

		// Token: 0x040001EF RID: 495
		internal const byte ELEMENT_TYPE_CMOD_REQD = 31;

		// Token: 0x040001F0 RID: 496
		internal const byte ELEMENT_TYPE_CMOD_OPT = 32;

		// Token: 0x040001F1 RID: 497
		internal const byte ELEMENT_TYPE_PINNED = 69;
	}
}
