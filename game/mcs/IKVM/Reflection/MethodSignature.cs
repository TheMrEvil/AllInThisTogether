using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection
{
	// Token: 0x0200003E RID: 62
	internal sealed class MethodSignature : Signature
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000936E File Offset: 0x0000756E
		internal MethodSignature(Type returnType, Type[] parameterTypes, PackedCustomModifiers modifiers, CallingConventions callingConvention, int genericParamCount)
		{
			this.returnType = returnType;
			this.parameterTypes = parameterTypes;
			this.modifiers = modifiers;
			this.callingConvention = callingConvention;
			this.genericParamCount = genericParamCount;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000939C File Offset: 0x0000759C
		public override bool Equals(object obj)
		{
			MethodSignature methodSignature = obj as MethodSignature;
			return methodSignature != null && methodSignature.callingConvention == this.callingConvention && methodSignature.genericParamCount == this.genericParamCount && methodSignature.returnType.Equals(this.returnType) && Util.ArrayEquals(methodSignature.parameterTypes, this.parameterTypes) && methodSignature.modifiers.Equals(this.modifiers);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000940C File Offset: 0x0000760C
		public override int GetHashCode()
		{
			return this.genericParamCount ^ (int)((CallingConventions)77 * this.callingConvention) ^ 3 * this.returnType.GetHashCode() ^ Util.GetHashCode(this.parameterTypes) * 5 ^ this.modifiers.GetHashCode() * 55;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009460 File Offset: 0x00007660
		internal static MethodSignature ReadSig(ModuleReader module, ByteReader br, IGenericContext context)
		{
			byte b = br.ReadByte();
			int num = (int)(b & 7);
			CallingConventions callingConventions;
			if (num != 0)
			{
				if (num != 5)
				{
					throw new BadImageFormatException();
				}
				callingConventions = CallingConventions.VarArgs;
			}
			else
			{
				callingConventions = CallingConventions.Standard;
			}
			if ((b & 32) != 0)
			{
				callingConventions |= CallingConventions.HasThis;
			}
			if ((b & 64) != 0)
			{
				callingConventions |= CallingConventions.ExplicitThis;
			}
			int num2 = 0;
			if ((b & 16) != 0)
			{
				num2 = br.ReadCompressedUInt();
				context = new MethodSignature.UnboundGenericMethodContext(context);
			}
			int num3 = br.ReadCompressedUInt();
			CustomModifiers[] array = null;
			PackedCustomModifiers.Pack(ref array, 0, CustomModifiers.Read(module, br, context), num3 + 1);
			Type type = Signature.ReadRetType(module, br, context);
			Type[] array2 = new Type[num3];
			int i = 0;
			while (i < array2.Length)
			{
				if ((callingConventions & CallingConventions.VarArgs) != (CallingConventions)0 && br.PeekByte() == 65)
				{
					Array.Resize<Type>(ref array2, i);
					if (array != null)
					{
						Array.Resize<CustomModifiers>(ref array, i + 1);
						break;
					}
					break;
				}
				else
				{
					PackedCustomModifiers.Pack(ref array, i + 1, CustomModifiers.Read(module, br, context), num3 + 1);
					array2[i] = Signature.ReadParam(module, br, context);
					i++;
				}
			}
			return new MethodSignature(type, array2, PackedCustomModifiers.Wrap(array), callingConventions, num2);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009564 File Offset: 0x00007764
		internal static __StandAloneMethodSig ReadStandAloneMethodSig(ModuleReader module, ByteReader br, IGenericContext context)
		{
			CallingConventions callingConventions = (CallingConventions)0;
			CallingConvention unmanagedCallingConvention = (CallingConvention)0;
			byte b = br.ReadByte();
			bool unmanaged;
			switch (b & 7)
			{
			case 0:
				callingConventions = CallingConventions.Standard;
				unmanaged = false;
				break;
			case 1:
				unmanagedCallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
				unmanaged = true;
				break;
			case 2:
				unmanagedCallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall;
				unmanaged = true;
				break;
			case 3:
				unmanagedCallingConvention = System.Runtime.InteropServices.CallingConvention.ThisCall;
				unmanaged = true;
				break;
			case 4:
				unmanagedCallingConvention = System.Runtime.InteropServices.CallingConvention.FastCall;
				unmanaged = true;
				break;
			case 5:
				callingConventions = CallingConventions.VarArgs;
				unmanaged = false;
				break;
			default:
				throw new BadImageFormatException();
			}
			if ((b & 32) != 0)
			{
				callingConventions |= CallingConventions.HasThis;
			}
			if ((b & 64) != 0)
			{
				callingConventions |= CallingConventions.ExplicitThis;
			}
			if ((b & 16) != 0)
			{
				throw new BadImageFormatException();
			}
			int num = br.ReadCompressedUInt();
			CustomModifiers[] array = null;
			PackedCustomModifiers.Pack(ref array, 0, CustomModifiers.Read(module, br, context), num + 1);
			Type type = Signature.ReadRetType(module, br, context);
			List<Type> list = new List<Type>();
			List<Type> list2 = new List<Type>();
			List<Type> list3 = list;
			for (int i = 0; i < num; i++)
			{
				if (br.PeekByte() == 65)
				{
					br.ReadByte();
					list3 = list2;
				}
				PackedCustomModifiers.Pack(ref array, i + 1, CustomModifiers.Read(module, br, context), num + 1);
				list3.Add(Signature.ReadParam(module, br, context));
			}
			return new __StandAloneMethodSig(unmanaged, unmanagedCallingConvention, callingConventions, type, list.ToArray(), list2.ToArray(), PackedCustomModifiers.Wrap(array));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009694 File Offset: 0x00007894
		internal int GetParameterCount()
		{
			return this.parameterTypes.Length;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000969E File Offset: 0x0000789E
		internal Type GetParameterType(int index)
		{
			return this.parameterTypes[index];
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000096A8 File Offset: 0x000078A8
		internal Type GetReturnType(IGenericBinder binder)
		{
			return this.returnType.BindTypeParameters(binder);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000096B8 File Offset: 0x000078B8
		internal CustomModifiers GetReturnTypeCustomModifiers(IGenericBinder binder)
		{
			return this.modifiers.GetReturnTypeCustomModifiers().Bind(binder);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000096DC File Offset: 0x000078DC
		internal Type GetParameterType(IGenericBinder binder, int index)
		{
			return this.parameterTypes[index].BindTypeParameters(binder);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000096EC File Offset: 0x000078EC
		internal CustomModifiers GetParameterCustomModifiers(IGenericBinder binder, int index)
		{
			return this.modifiers.GetParameterCustomModifiers(index).Bind(binder);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009711 File Offset: 0x00007911
		internal CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00009719 File Offset: 0x00007919
		internal int GenericParameterCount
		{
			get
			{
				return this.genericParamCount;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009724 File Offset: 0x00007924
		internal MethodSignature Bind(Type type, Type[] methodArgs)
		{
			MethodSignature.Binder binder = new MethodSignature.Binder(type, methodArgs);
			return new MethodSignature(this.returnType.BindTypeParameters(binder), Signature.BindTypeParameters(binder, this.parameterTypes), this.modifiers.Bind(binder), this.callingConvention, this.genericParamCount);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009771 File Offset: 0x00007971
		internal static MethodSignature MakeFromBuilder(Type returnType, Type[] parameterTypes, PackedCustomModifiers modifiers, CallingConventions callingConvention, int genericParamCount)
		{
			if (genericParamCount > 0)
			{
				returnType = returnType.BindTypeParameters(MethodSignature.Unbinder.Instance);
				parameterTypes = Signature.BindTypeParameters(MethodSignature.Unbinder.Instance, parameterTypes);
				modifiers = modifiers.Bind(MethodSignature.Unbinder.Instance);
			}
			return new MethodSignature(returnType, parameterTypes, modifiers, callingConvention, genericParamCount);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000097AB File Offset: 0x000079AB
		internal bool MatchParameterTypes(MethodSignature other)
		{
			return Util.ArrayEquals(other.parameterTypes, this.parameterTypes);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000097BE File Offset: 0x000079BE
		internal bool MatchParameterTypes(Type[] types)
		{
			return Util.ArrayEquals(types, this.parameterTypes);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000097CC File Offset: 0x000079CC
		internal override void WriteSig(ModuleBuilder module, ByteBuffer bb)
		{
			this.WriteSigImpl(module, bb, this.parameterTypes.Length);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000097E0 File Offset: 0x000079E0
		internal void WriteMethodRefSig(ModuleBuilder module, ByteBuffer bb, Type[] optionalParameterTypes, CustomModifiers[] customModifiers)
		{
			this.WriteSigImpl(module, bb, this.parameterTypes.Length + optionalParameterTypes.Length);
			if (optionalParameterTypes.Length != 0)
			{
				bb.Write(65);
				for (int i = 0; i < optionalParameterTypes.Length; i++)
				{
					Signature.WriteCustomModifiers(module, bb, Util.NullSafeElementAt<CustomModifiers>(customModifiers, i));
					Signature.WriteType(module, bb, optionalParameterTypes[i]);
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009834 File Offset: 0x00007A34
		private void WriteSigImpl(ModuleBuilder module, ByteBuffer bb, int parameterCount)
		{
			byte b;
			if ((this.callingConvention & CallingConventions.Any) == CallingConventions.VarArgs)
			{
				b = 5;
			}
			else if (this.genericParamCount > 0)
			{
				b = 16;
			}
			else
			{
				b = 0;
			}
			if ((this.callingConvention & CallingConventions.HasThis) != (CallingConventions)0)
			{
				b |= 32;
			}
			if ((this.callingConvention & CallingConventions.ExplicitThis) != (CallingConventions)0)
			{
				b |= 64;
			}
			bb.Write(b);
			if (this.genericParamCount > 0)
			{
				bb.WriteCompressedUInt(this.genericParamCount);
			}
			bb.WriteCompressedUInt(parameterCount);
			Signature.WriteCustomModifiers(module, bb, this.modifiers.GetReturnTypeCustomModifiers());
			Signature.WriteType(module, bb, this.returnType);
			for (int i = 0; i < this.parameterTypes.Length; i++)
			{
				Signature.WriteCustomModifiers(module, bb, this.modifiers.GetParameterCustomModifiers(i));
				Signature.WriteType(module, bb, this.parameterTypes[i]);
			}
		}

		// Token: 0x04000166 RID: 358
		private readonly Type returnType;

		// Token: 0x04000167 RID: 359
		private readonly Type[] parameterTypes;

		// Token: 0x04000168 RID: 360
		private readonly PackedCustomModifiers modifiers;

		// Token: 0x04000169 RID: 361
		private readonly CallingConventions callingConvention;

		// Token: 0x0400016A RID: 362
		private readonly int genericParamCount;

		// Token: 0x02000324 RID: 804
		private sealed class UnboundGenericMethodContext : IGenericContext
		{
			// Token: 0x06002584 RID: 9604 RVA: 0x000B3D3B File Offset: 0x000B1F3B
			internal UnboundGenericMethodContext(IGenericContext original)
			{
				this.original = original;
			}

			// Token: 0x06002585 RID: 9605 RVA: 0x000B3D4A File Offset: 0x000B1F4A
			public Type GetGenericTypeArgument(int index)
			{
				return this.original.GetGenericTypeArgument(index);
			}

			// Token: 0x06002586 RID: 9606 RVA: 0x000B3D58 File Offset: 0x000B1F58
			public Type GetGenericMethodArgument(int index)
			{
				return UnboundGenericMethodParameter.Make(index);
			}

			// Token: 0x04000E40 RID: 3648
			private readonly IGenericContext original;
		}

		// Token: 0x02000325 RID: 805
		private sealed class Binder : IGenericBinder
		{
			// Token: 0x06002587 RID: 9607 RVA: 0x000B3D60 File Offset: 0x000B1F60
			internal Binder(Type declaringType, Type[] methodArgs)
			{
				this.declaringType = declaringType;
				this.methodArgs = methodArgs;
			}

			// Token: 0x06002588 RID: 9608 RVA: 0x000B3D76 File Offset: 0x000B1F76
			public Type BindTypeParameter(Type type)
			{
				return this.declaringType.GetGenericTypeArgument(type.GenericParameterPosition);
			}

			// Token: 0x06002589 RID: 9609 RVA: 0x000B3D89 File Offset: 0x000B1F89
			public Type BindMethodParameter(Type type)
			{
				if (this.methodArgs == null)
				{
					return type;
				}
				return this.methodArgs[type.GenericParameterPosition];
			}

			// Token: 0x04000E41 RID: 3649
			private readonly Type declaringType;

			// Token: 0x04000E42 RID: 3650
			private readonly Type[] methodArgs;
		}

		// Token: 0x02000326 RID: 806
		private sealed class Unbinder : IGenericBinder
		{
			// Token: 0x0600258A RID: 9610 RVA: 0x00002CCC File Offset: 0x00000ECC
			private Unbinder()
			{
			}

			// Token: 0x0600258B RID: 9611 RVA: 0x0004D50E File Offset: 0x0004B70E
			public Type BindTypeParameter(Type type)
			{
				return type;
			}

			// Token: 0x0600258C RID: 9612 RVA: 0x000B3DA2 File Offset: 0x000B1FA2
			public Type BindMethodParameter(Type type)
			{
				return UnboundGenericMethodParameter.Make(type.GenericParameterPosition);
			}

			// Token: 0x0600258D RID: 9613 RVA: 0x000B3DAF File Offset: 0x000B1FAF
			// Note: this type is marked as 'beforefieldinit'.
			static Unbinder()
			{
			}

			// Token: 0x04000E43 RID: 3651
			internal static readonly MethodSignature.Unbinder Instance = new MethodSignature.Unbinder();
		}
	}
}
