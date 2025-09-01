using System;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A4 RID: 164
	internal sealed class PropertyInfoImpl : PropertyInfo
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
		internal PropertyInfoImpl(ModuleReader module, Type declaringType, int index)
		{
			this.module = module;
			this.declaringType = declaringType;
			this.index = index;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		public override bool Equals(object obj)
		{
			PropertyInfoImpl propertyInfoImpl = obj as PropertyInfoImpl;
			return propertyInfoImpl != null && propertyInfoImpl.DeclaringType == this.declaringType && propertyInfoImpl.index == this.index;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001D404 File Offset: 0x0001B604
		public override int GetHashCode()
		{
			return this.declaringType.GetHashCode() * 77 + this.index;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001D41C File Offset: 0x0001B61C
		internal override PropertySignature PropertySignature
		{
			get
			{
				if (this.sig == null)
				{
					this.sig = PropertySignature.ReadSig(this.module, this.module.GetBlob(this.module.Property.records[this.index].Type), this.declaringType);
				}
				return this.sig;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001D479 File Offset: 0x0001B679
		public override PropertyAttributes Attributes
		{
			get
			{
				return (PropertyAttributes)this.module.Property.records[this.index].Flags;
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001D49B File Offset: 0x0001B69B
		public override object GetRawConstantValue()
		{
			return this.module.Constant.GetRawConstantValue(this.module, this.MetadataToken);
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001D4B9 File Offset: 0x0001B6B9
		public override bool CanRead
		{
			get
			{
				return this.GetGetMethod(true) != null;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001D4C8 File Offset: 0x0001B6C8
		public override bool CanWrite
		{
			get
			{
				return this.GetSetMethod(true) != null;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001D4D7 File Offset: 0x0001B6D7
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethod(this.module, this.MetadataToken, nonPublic, 2);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001D4F7 File Offset: 0x0001B6F7
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethod(this.module, this.MetadataToken, nonPublic, 1);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001D517 File Offset: 0x0001B717
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethods(this.module, this.MetadataToken, nonPublic, 7);
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0001D537 File Offset: 0x0001B737
		public override Type DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001D53F File Offset: 0x0001B73F
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001D547 File Offset: 0x0001B747
		public override int MetadataToken
		{
			get
			{
				return (23 << 24) + this.index + 1;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001D557 File Offset: 0x0001B757
		public override string Name
		{
			get
			{
				return this.module.GetString(this.module.Property.records[this.index].Name);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001D584 File Offset: 0x0001B784
		internal override bool IsPublic
		{
			get
			{
				if (!this.flagsCached)
				{
					this.ComputeFlags();
				}
				return this.isPublic;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001D59A File Offset: 0x0001B79A
		internal override bool IsNonPrivate
		{
			get
			{
				if (!this.flagsCached)
				{
					this.ComputeFlags();
				}
				return this.isNonPrivate;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		internal override bool IsStatic
		{
			get
			{
				if (!this.flagsCached)
				{
					this.ComputeFlags();
				}
				return this.isStatic;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001D5C6 File Offset: 0x0001B7C6
		private void ComputeFlags()
		{
			this.module.MethodSemantics.ComputeFlags(this.module, this.MetadataToken, out this.isPublic, out this.isNonPrivate, out this.isStatic);
			this.flagsCached = true;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00010856 File Offset: 0x0000EA56
		internal override int GetCurrentToken()
		{
			return this.MetadataToken;
		}

		// Token: 0x0400038D RID: 909
		private readonly ModuleReader module;

		// Token: 0x0400038E RID: 910
		private readonly Type declaringType;

		// Token: 0x0400038F RID: 911
		private readonly int index;

		// Token: 0x04000390 RID: 912
		private PropertySignature sig;

		// Token: 0x04000391 RID: 913
		private bool isPublic;

		// Token: 0x04000392 RID: 914
		private bool isNonPrivate;

		// Token: 0x04000393 RID: 915
		private bool isStatic;

		// Token: 0x04000394 RID: 916
		private bool flagsCached;
	}
}
