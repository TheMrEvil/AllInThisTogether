using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000237 RID: 567
	internal class TypedElement : ConfigurationElement
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x0004A7FE File Offset: 0x000489FE
		public TypedElement(Type baseType)
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(TypedElement._propTypeName);
			this._properties.Add(TypedElement._propInitData);
			this._baseType = baseType;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0004A838 File Offset: 0x00048A38
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x0004A84A File Offset: 0x00048A4A
		[ConfigurationProperty("initializeData", DefaultValue = "")]
		public string InitData
		{
			get
			{
				return (string)base[TypedElement._propInitData];
			}
			set
			{
				base[TypedElement._propInitData] = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0004A858 File Offset: 0x00048A58
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004A860 File Offset: 0x00048A60
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x0004A872 File Offset: 0x00048A72
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public virtual string TypeName
		{
			get
			{
				return (string)base[TypedElement._propTypeName];
			}
			set
			{
				base[TypedElement._propTypeName] = value;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0004A880 File Offset: 0x00048A80
		protected object BaseGetRuntimeObject()
		{
			if (this._runtimeObject == null)
			{
				this._runtimeObject = TraceUtils.GetRuntimeObject(this.TypeName, this._baseType, this.InitData);
			}
			return this._runtimeObject;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0004A8B0 File Offset: 0x00048AB0
		// Note: this type is marked as 'beforefieldinit'.
		static TypedElement()
		{
		}

		// Token: 0x04000A11 RID: 2577
		protected static readonly ConfigurationProperty _propTypeName = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04000A12 RID: 2578
		protected static readonly ConfigurationProperty _propInitData = new ConfigurationProperty("initializeData", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04000A13 RID: 2579
		protected ConfigurationPropertyCollection _properties;

		// Token: 0x04000A14 RID: 2580
		protected object _runtimeObject;

		// Token: 0x04000A15 RID: 2581
		private Type _baseType;
	}
}
