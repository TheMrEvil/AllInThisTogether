using System;

namespace IKVM.Reflection
{
	// Token: 0x02000032 RID: 50
	internal sealed class GenericPropertyInfo : PropertyInfo
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00007E32 File Offset: 0x00006032
		internal GenericPropertyInfo(Type typeInstance, PropertyInfo property)
		{
			this.typeInstance = typeInstance;
			this.property = property;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007E48 File Offset: 0x00006048
		public override bool Equals(object obj)
		{
			GenericPropertyInfo genericPropertyInfo = obj as GenericPropertyInfo;
			return genericPropertyInfo != null && genericPropertyInfo.typeInstance == this.typeInstance && genericPropertyInfo.property == this.property;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007E8B File Offset: 0x0000608B
		public override int GetHashCode()
		{
			return this.typeInstance.GetHashCode() * 537 + this.property.GetHashCode();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007EAA File Offset: 0x000060AA
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.property.Attributes;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007EB7 File Offset: 0x000060B7
		public override bool CanRead
		{
			get
			{
				return this.property.CanRead;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007EC4 File Offset: 0x000060C4
		public override bool CanWrite
		{
			get
			{
				return this.property.CanWrite;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007ED1 File Offset: 0x000060D1
		private MethodInfo Wrap(MethodInfo method)
		{
			if (method == null)
			{
				return null;
			}
			return new GenericMethodInstance(this.typeInstance, method, null);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007EEB File Offset: 0x000060EB
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.Wrap(this.property.GetGetMethod(nonPublic));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007EFF File Offset: 0x000060FF
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.Wrap(this.property.GetSetMethod(nonPublic));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007F14 File Offset: 0x00006114
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			MethodInfo[] accessors = this.property.GetAccessors(nonPublic);
			for (int i = 0; i < accessors.Length; i++)
			{
				accessors[i] = this.Wrap(accessors[i]);
			}
			return accessors;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007F49 File Offset: 0x00006149
		public override object GetRawConstantValue()
		{
			return this.property.GetRawConstantValue();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007F56 File Offset: 0x00006156
		internal override bool IsPublic
		{
			get
			{
				return this.property.IsPublic;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007F63 File Offset: 0x00006163
		internal override bool IsNonPrivate
		{
			get
			{
				return this.property.IsNonPrivate;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007F70 File Offset: 0x00006170
		internal override bool IsStatic
		{
			get
			{
				return this.property.IsStatic;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007F7D File Offset: 0x0000617D
		internal override PropertySignature PropertySignature
		{
			get
			{
				return this.property.PropertySignature.ExpandTypeParameters(this.typeInstance);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007F95 File Offset: 0x00006195
		public override string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007FA2 File Offset: 0x000061A2
		public override Type DeclaringType
		{
			get
			{
				return this.typeInstance;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007FAA File Offset: 0x000061AA
		public override Module Module
		{
			get
			{
				return this.typeInstance.Module;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007FB7 File Offset: 0x000061B7
		public override int MetadataToken
		{
			get
			{
				return this.property.MetadataToken;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007FC4 File Offset: 0x000061C4
		internal override PropertyInfo BindTypeParameters(Type type)
		{
			return new GenericPropertyInfo(this.typeInstance.BindTypeParameters(type), this.property);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007FDD File Offset: 0x000061DD
		internal override bool IsBaked
		{
			get
			{
				return this.property.IsBaked;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007FEA File Offset: 0x000061EA
		internal override int GetCurrentToken()
		{
			return this.property.GetCurrentToken();
		}

		// Token: 0x04000142 RID: 322
		private readonly Type typeInstance;

		// Token: 0x04000143 RID: 323
		private readonly PropertyInfo property;
	}
}
