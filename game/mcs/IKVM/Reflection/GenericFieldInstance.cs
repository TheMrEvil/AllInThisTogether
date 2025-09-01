using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000030 RID: 48
	internal sealed class GenericFieldInstance : FieldInfo
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00007C18 File Offset: 0x00005E18
		internal GenericFieldInstance(Type declaringType, FieldInfo field)
		{
			this.declaringType = declaringType;
			this.field = field;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007C30 File Offset: 0x00005E30
		public override bool Equals(object obj)
		{
			GenericFieldInstance genericFieldInstance = obj as GenericFieldInstance;
			return genericFieldInstance != null && genericFieldInstance.declaringType.Equals(this.declaringType) && genericFieldInstance.field.Equals(this.field);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007C73 File Offset: 0x00005E73
		public override int GetHashCode()
		{
			return this.declaringType.GetHashCode() * 3 ^ this.field.GetHashCode();
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007C8E File Offset: 0x00005E8E
		public override FieldAttributes Attributes
		{
			get
			{
				return this.field.Attributes;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007C9B File Offset: 0x00005E9B
		public override string Name
		{
			get
			{
				return this.field.Name;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007CA8 File Offset: 0x00005EA8
		public override Type DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007CB0 File Offset: 0x00005EB0
		public override Module Module
		{
			get
			{
				return this.declaringType.Module;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007CBD File Offset: 0x00005EBD
		public override int MetadataToken
		{
			get
			{
				return this.field.MetadataToken;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007CCA File Offset: 0x00005ECA
		public override object GetRawConstantValue()
		{
			return this.field.GetRawConstantValue();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007CD7 File Offset: 0x00005ED7
		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			this.field.__GetDataFromRVA(data, offset, length);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007CE7 File Offset: 0x00005EE7
		public override int __FieldRVA
		{
			get
			{
				return this.field.__FieldRVA;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public override bool __TryGetFieldOffset(out int offset)
		{
			return this.field.__TryGetFieldOffset(out offset);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007D02 File Offset: 0x00005F02
		public override FieldInfo __GetFieldOnTypeDefinition()
		{
			return this.field;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007D0A File Offset: 0x00005F0A
		internal override FieldSignature FieldSignature
		{
			get
			{
				return this.field.FieldSignature.ExpandTypeParameters(this.declaringType);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007D22 File Offset: 0x00005F22
		internal override int ImportTo(ModuleBuilder module)
		{
			return module.ImportMethodOrField(this.declaringType, this.field.Name, this.field.FieldSignature);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007D46 File Offset: 0x00005F46
		internal override FieldInfo BindTypeParameters(Type type)
		{
			return new GenericFieldInstance(this.declaringType.BindTypeParameters(type), this.field);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007D5F File Offset: 0x00005F5F
		internal override int GetCurrentToken()
		{
			return this.field.GetCurrentToken();
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007D6C File Offset: 0x00005F6C
		internal override bool IsBaked
		{
			get
			{
				return this.field.IsBaked;
			}
		}

		// Token: 0x0400013E RID: 318
		private readonly Type declaringType;

		// Token: 0x0400013F RID: 319
		private readonly FieldInfo field;
	}
}
