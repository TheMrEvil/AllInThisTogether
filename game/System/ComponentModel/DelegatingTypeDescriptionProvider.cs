using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x0200039D RID: 925
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x06001E42 RID: 7746 RVA: 0x0006BA93 File Offset: 0x00069C93
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0006BAA2 File Offset: 0x00069CA2
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0006BAAF File Offset: 0x00069CAF
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0006BAC1 File Offset: 0x00069CC1
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0006BACF File Offset: 0x00069CCF
		public override string GetFullComponentName(object component)
		{
			return this.Provider.GetFullComponentName(component);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0006BADD File Offset: 0x00069CDD
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0006BAEB File Offset: 0x00069CEB
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0006BAF9 File Offset: 0x00069CF9
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0006BB08 File Offset: 0x00069D08
		public override Type GetRuntimeType(Type objectType)
		{
			return this.Provider.GetRuntimeType(objectType);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0006BB16 File Offset: 0x00069D16
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0006BB25 File Offset: 0x00069D25
		public override bool IsSupportedType(Type type)
		{
			return this.Provider.IsSupportedType(type);
		}

		// Token: 0x04000F1C RID: 3868
		private readonly Type _type;
	}
}
