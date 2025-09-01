using System;

namespace IKVM.Reflection
{
	// Token: 0x02000055 RID: 85
	internal sealed class PropertyInfoWithReflectedType : PropertyInfo
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000CB10 File Offset: 0x0000AD10
		internal PropertyInfoWithReflectedType(Type reflectedType, PropertyInfo property)
		{
			this.reflectedType = reflectedType;
			this.property = property;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000CB26 File Offset: 0x0000AD26
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.property.Attributes;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000CB33 File Offset: 0x0000AD33
		public override bool CanRead
		{
			get
			{
				return this.property.CanRead;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000CB40 File Offset: 0x0000AD40
		public override bool CanWrite
		{
			get
			{
				return this.property.CanWrite;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000CB4D File Offset: 0x0000AD4D
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.property.GetGetMethod(nonPublic), this.reflectedType);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000CB66 File Offset: 0x0000AD66
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.property.GetSetMethod(nonPublic), this.reflectedType);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000CB7F File Offset: 0x0000AD7F
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.property.GetAccessors(nonPublic), this.reflectedType);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000CB98 File Offset: 0x0000AD98
		public override object GetRawConstantValue()
		{
			return this.property.GetRawConstantValue();
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
		internal override bool IsPublic
		{
			get
			{
				return this.property.IsPublic;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000CBB2 File Offset: 0x0000ADB2
		internal override bool IsNonPrivate
		{
			get
			{
				return this.property.IsNonPrivate;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000CBBF File Offset: 0x0000ADBF
		internal override bool IsStatic
		{
			get
			{
				return this.property.IsStatic;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		internal override PropertySignature PropertySignature
		{
			get
			{
				return this.property.PropertySignature;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		public override ParameterInfo[] GetIndexParameters()
		{
			ParameterInfo[] indexParameters = this.property.GetIndexParameters();
			for (int i = 0; i < indexParameters.Length; i++)
			{
				indexParameters[i] = new ParameterInfoWrapper(this, indexParameters[i]);
			}
			return indexParameters;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000CC10 File Offset: 0x0000AE10
		internal override PropertyInfo BindTypeParameters(Type type)
		{
			return this.property.BindTypeParameters(type);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000CC1E File Offset: 0x0000AE1E
		public override string ToString()
		{
			return this.property.ToString();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000CC2B File Offset: 0x0000AE2B
		public override bool __IsMissing
		{
			get
			{
				return this.property.__IsMissing;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000CC38 File Offset: 0x0000AE38
		public override Type DeclaringType
		{
			get
			{
				return this.property.DeclaringType;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000CC45 File Offset: 0x0000AE45
		public override Type ReflectedType
		{
			get
			{
				return this.reflectedType;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public override bool Equals(object obj)
		{
			PropertyInfoWithReflectedType propertyInfoWithReflectedType = obj as PropertyInfoWithReflectedType;
			return propertyInfoWithReflectedType != null && propertyInfoWithReflectedType.reflectedType == this.reflectedType && propertyInfoWithReflectedType.property == this.property;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000CC93 File Offset: 0x0000AE93
		public override int GetHashCode()
		{
			return this.reflectedType.GetHashCode() ^ this.property.GetHashCode();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public override int MetadataToken
		{
			get
			{
				return this.property.MetadataToken;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000CCB9 File Offset: 0x0000AEB9
		public override Module Module
		{
			get
			{
				return this.property.Module;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000CCC6 File Offset: 0x0000AEC6
		public override string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		internal override bool IsBaked
		{
			get
			{
				return this.property.IsBaked;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		internal override int GetCurrentToken()
		{
			return this.property.GetCurrentToken();
		}

		// Token: 0x040001C3 RID: 451
		private readonly Type reflectedType;

		// Token: 0x040001C4 RID: 452
		private readonly PropertyInfo property;
	}
}
