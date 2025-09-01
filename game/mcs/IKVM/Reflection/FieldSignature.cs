using System;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection
{
	// Token: 0x0200002B RID: 43
	internal sealed class FieldSignature : Signature
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00005B9E File Offset: 0x00003D9E
		internal static FieldSignature Create(Type fieldType, CustomModifiers customModifiers)
		{
			return new FieldSignature(fieldType, customModifiers);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005BA7 File Offset: 0x00003DA7
		private FieldSignature(Type fieldType, CustomModifiers mods)
		{
			this.fieldType = fieldType;
			this.mods = mods;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public override bool Equals(object obj)
		{
			FieldSignature fieldSignature = obj as FieldSignature;
			return fieldSignature != null && fieldSignature.fieldType.Equals(this.fieldType) && fieldSignature.mods.Equals(this.mods);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005C00 File Offset: 0x00003E00
		public override int GetHashCode()
		{
			return this.fieldType.GetHashCode() ^ this.mods.GetHashCode();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005C2D File Offset: 0x00003E2D
		internal Type FieldType
		{
			get
			{
				return this.fieldType;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005C35 File Offset: 0x00003E35
		internal CustomModifiers GetCustomModifiers()
		{
			return this.mods;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005C40 File Offset: 0x00003E40
		internal FieldSignature ExpandTypeParameters(Type declaringType)
		{
			return new FieldSignature(this.fieldType.BindTypeParameters(declaringType), this.mods.Bind(declaringType));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005C70 File Offset: 0x00003E70
		internal static FieldSignature ReadSig(ModuleReader module, ByteReader br, IGenericContext context)
		{
			if (br.ReadByte() != 6)
			{
				throw new BadImageFormatException();
			}
			CustomModifiers customModifiers = CustomModifiers.Read(module, br, context);
			return new FieldSignature(Signature.ReadType(module, br, context), customModifiers);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005CA3 File Offset: 0x00003EA3
		internal override void WriteSig(ModuleBuilder module, ByteBuffer bb)
		{
			bb.Write(6);
			Signature.WriteCustomModifiers(module, bb, this.mods);
			Signature.WriteType(module, bb, this.fieldType);
		}

		// Token: 0x04000123 RID: 291
		private readonly Type fieldType;

		// Token: 0x04000124 RID: 292
		private readonly CustomModifiers mods;
	}
}
