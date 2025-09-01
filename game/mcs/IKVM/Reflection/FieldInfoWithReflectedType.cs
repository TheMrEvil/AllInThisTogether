using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x0200002A RID: 42
	internal sealed class FieldInfoWithReflectedType : FieldInfo
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00005A41 File Offset: 0x00003C41
		internal FieldInfoWithReflectedType(Type reflectedType, FieldInfo field)
		{
			this.reflectedType = reflectedType;
			this.field = field;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005A57 File Offset: 0x00003C57
		public override FieldAttributes Attributes
		{
			get
			{
				return this.field.Attributes;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005A64 File Offset: 0x00003C64
		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			this.field.__GetDataFromRVA(data, offset, length);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005A74 File Offset: 0x00003C74
		public override int __FieldRVA
		{
			get
			{
				return this.field.__FieldRVA;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005A81 File Offset: 0x00003C81
		public override bool __TryGetFieldOffset(out int offset)
		{
			return this.field.__TryGetFieldOffset(out offset);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005A8F File Offset: 0x00003C8F
		public override object GetRawConstantValue()
		{
			return this.field.GetRawConstantValue();
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005A9C File Offset: 0x00003C9C
		internal override FieldSignature FieldSignature
		{
			get
			{
				return this.field.FieldSignature;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005AA9 File Offset: 0x00003CA9
		public override FieldInfo __GetFieldOnTypeDefinition()
		{
			return this.field.__GetFieldOnTypeDefinition();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005AB6 File Offset: 0x00003CB6
		internal override int ImportTo(ModuleBuilder module)
		{
			return this.field.ImportTo(module);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005AC4 File Offset: 0x00003CC4
		internal override FieldInfo BindTypeParameters(Type type)
		{
			return this.field.BindTypeParameters(type);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005AD2 File Offset: 0x00003CD2
		public override bool __IsMissing
		{
			get
			{
				return this.field.__IsMissing;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005ADF File Offset: 0x00003CDF
		public override Type DeclaringType
		{
			get
			{
				return this.field.DeclaringType;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005AEC File Offset: 0x00003CEC
		public override Type ReflectedType
		{
			get
			{
				return this.reflectedType;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public override bool Equals(object obj)
		{
			FieldInfoWithReflectedType fieldInfoWithReflectedType = obj as FieldInfoWithReflectedType;
			return fieldInfoWithReflectedType != null && fieldInfoWithReflectedType.reflectedType == this.reflectedType && fieldInfoWithReflectedType.field == this.field;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005B37 File Offset: 0x00003D37
		public override int GetHashCode()
		{
			return this.reflectedType.GetHashCode() ^ this.field.GetHashCode();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005B50 File Offset: 0x00003D50
		public override int MetadataToken
		{
			get
			{
				return this.field.MetadataToken;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005B5D File Offset: 0x00003D5D
		public override Module Module
		{
			get
			{
				return this.field.Module;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005B6A File Offset: 0x00003D6A
		public override string Name
		{
			get
			{
				return this.field.Name;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005B77 File Offset: 0x00003D77
		public override string ToString()
		{
			return this.field.ToString();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005B84 File Offset: 0x00003D84
		internal override int GetCurrentToken()
		{
			return this.field.GetCurrentToken();
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005B91 File Offset: 0x00003D91
		internal override bool IsBaked
		{
			get
			{
				return this.field.IsBaked;
			}
		}

		// Token: 0x04000121 RID: 289
		private readonly Type reflectedType;

		// Token: 0x04000122 RID: 290
		private readonly FieldInfo field;
	}
}
