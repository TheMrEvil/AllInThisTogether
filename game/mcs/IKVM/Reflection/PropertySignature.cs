using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection
{
	// Token: 0x02000056 RID: 86
	internal sealed class PropertySignature : Signature
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0000CCED File Offset: 0x0000AEED
		internal static PropertySignature Create(CallingConventions callingConvention, Type propertyType, Type[] parameterTypes, PackedCustomModifiers customModifiers)
		{
			return new PropertySignature(callingConvention, propertyType, Util.Copy(parameterTypes), customModifiers);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000CCFD File Offset: 0x0000AEFD
		private PropertySignature(CallingConventions callingConvention, Type propertyType, Type[] parameterTypes, PackedCustomModifiers customModifiers)
		{
			this.callingConvention = callingConvention;
			this.propertyType = propertyType;
			this.parameterTypes = parameterTypes;
			this.customModifiers = customModifiers;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public override bool Equals(object obj)
		{
			PropertySignature propertySignature = obj as PropertySignature;
			return propertySignature != null && propertySignature.propertyType.Equals(this.propertyType) && propertySignature.customModifiers.Equals(this.customModifiers);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public override int GetHashCode()
		{
			return this.propertyType.GetHashCode() ^ this.customModifiers.GetHashCode();
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000CD91 File Offset: 0x0000AF91
		internal int ParameterCount
		{
			get
			{
				return this.parameterTypes.Length;
			}
		}

		// Token: 0x1700018A RID: 394
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		internal bool HasThis
		{
			set
			{
				if (value)
				{
					this.callingConvention |= CallingConventions.HasThis;
					return;
				}
				this.callingConvention &= ~CallingConventions.HasThis;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		internal Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		internal CustomModifiers GetCustomModifiers()
		{
			return this.customModifiers.GetReturnTypeCustomModifiers();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		internal PropertySignature ExpandTypeParameters(Type declaringType)
		{
			return new PropertySignature(this.callingConvention, this.propertyType.BindTypeParameters(declaringType), Signature.BindTypeParameters(declaringType, this.parameterTypes), this.customModifiers.Bind(declaringType));
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000CE24 File Offset: 0x0000B024
		internal override void WriteSig(ModuleBuilder module, ByteBuffer bb)
		{
			byte b = 8;
			if ((this.callingConvention & CallingConventions.HasThis) != (CallingConventions)0)
			{
				b |= 32;
			}
			if ((this.callingConvention & CallingConventions.ExplicitThis) != (CallingConventions)0)
			{
				b |= 64;
			}
			if ((this.callingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				b |= 5;
			}
			bb.Write(b);
			bb.WriteCompressedUInt((this.parameterTypes == null) ? 0 : this.parameterTypes.Length);
			Signature.WriteCustomModifiers(module, bb, this.customModifiers.GetReturnTypeCustomModifiers());
			Signature.WriteType(module, bb, this.propertyType);
			if (this.parameterTypes != null)
			{
				for (int i = 0; i < this.parameterTypes.Length; i++)
				{
					Signature.WriteCustomModifiers(module, bb, this.customModifiers.GetParameterCustomModifiers(i));
					Signature.WriteType(module, bb, this.parameterTypes[i]);
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000CEE6 File Offset: 0x0000B0E6
		internal Type GetParameter(int parameter)
		{
			return this.parameterTypes[parameter];
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		internal CustomModifiers GetParameterCustomModifiers(int parameter)
		{
			return this.customModifiers.GetParameterCustomModifiers(parameter);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000CF0C File Offset: 0x0000B10C
		internal CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000CF14 File Offset: 0x0000B114
		internal bool MatchParameterTypes(Type[] types)
		{
			return Util.ArrayEquals(types, this.parameterTypes);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000CF24 File Offset: 0x0000B124
		internal static PropertySignature ReadSig(ModuleReader module, ByteReader br, IGenericContext context)
		{
			byte b = br.ReadByte();
			if ((b & 8) == 0)
			{
				throw new BadImageFormatException();
			}
			CallingConventions callingConventions = CallingConventions.Standard;
			if ((b & 32) != 0)
			{
				callingConventions |= CallingConventions.HasThis;
			}
			if ((b & 64) != 0)
			{
				callingConventions |= CallingConventions.ExplicitThis;
			}
			int num = br.ReadCompressedUInt();
			CustomModifiers[] modifiers = null;
			PackedCustomModifiers.Pack(ref modifiers, 0, CustomModifiers.Read(module, br, context), num + 1);
			Type type = Signature.ReadRetType(module, br, context);
			Type[] array = new Type[num];
			for (int i = 0; i < array.Length; i++)
			{
				PackedCustomModifiers.Pack(ref modifiers, i + 1, CustomModifiers.Read(module, br, context), num + 1);
				array[i] = Signature.ReadParam(module, br, context);
			}
			return new PropertySignature(callingConventions, type, array, PackedCustomModifiers.Wrap(modifiers));
		}

		// Token: 0x040001C5 RID: 453
		private CallingConventions callingConvention;

		// Token: 0x040001C6 RID: 454
		private readonly Type propertyType;

		// Token: 0x040001C7 RID: 455
		private readonly Type[] parameterTypes;

		// Token: 0x040001C8 RID: 456
		private readonly PackedCustomModifiers customModifiers;
	}
}
