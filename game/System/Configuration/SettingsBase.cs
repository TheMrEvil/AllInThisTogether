using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides the base class used to support user property settings.</summary>
	// Token: 0x020001C3 RID: 451
	public abstract class SettingsBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsBase" /> class.</summary>
		// Token: 0x06000BDA RID: 3034 RVA: 0x00031A4A File Offset: 0x0002FC4A
		protected SettingsBase()
		{
		}

		/// <summary>Initializes internal properties used by <see cref="T:System.Configuration.SettingsBase" /> object.</summary>
		/// <param name="context">The settings context related to the settings properties.</param>
		/// <param name="properties">The settings properties that will be accessible from the <see cref="T:System.Configuration.SettingsBase" /> instance.</param>
		/// <param name="providers">The initialized providers that should be used when loading and saving property values.</param>
		// Token: 0x06000BDB RID: 3035 RVA: 0x00031A5D File Offset: 0x0002FC5D
		public void Initialize(SettingsContext context, SettingsPropertyCollection properties, SettingsProviderCollection providers)
		{
			this.context = context;
			this.properties = properties;
			this.providers = providers;
		}

		/// <summary>Stores the current values of the settings properties.</summary>
		// Token: 0x06000BDC RID: 3036 RVA: 0x00031A74 File Offset: 0x0002FC74
		public virtual void Save()
		{
			if (this.sync)
			{
				lock (this)
				{
					this.SaveCore();
					return;
				}
			}
			this.SaveCore();
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00031AC0 File Offset: 0x0002FCC0
		private void SaveCore()
		{
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj2 in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (settingsPropertyValue.Property.Provider == settingsProvider)
					{
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				if (settingsPropertyValueCollection.Count > 0)
				{
					settingsProvider.SetPropertyValues(this.Context, settingsPropertyValueCollection);
				}
			}
		}

		/// <summary>Provides a <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized (thread safe).</summary>
		/// <param name="settingsBase">The class used to support user property settings.</param>
		/// <returns>A <see cref="T:System.Configuration.SettingsBase" /> class that is synchronized.</returns>
		// Token: 0x06000BDE RID: 3038 RVA: 0x00031B90 File Offset: 0x0002FD90
		public static SettingsBase Synchronized(SettingsBase settingsBase)
		{
			settingsBase.sync = true;
			return settingsBase;
		}

		/// <summary>Gets the associated settings context.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings instance.</returns>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00031B9A File Offset: 0x0002FD9A
		public virtual SettingsContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets a value indicating whether access to the object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.SettingsBase" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00031BA2 File Offset: 0x0002FDA2
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.sync;
			}
		}

		/// <summary>Gets or sets the value of the specified settings property.</summary>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <returns>If found, the value of the named settings property.</returns>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current object, or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		// Token: 0x170001FB RID: 507
		public virtual object this[string propertyName]
		{
			get
			{
				if (this.sync)
				{
					lock (this)
					{
						return this.GetPropertyValue(propertyName);
					}
				}
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				if (this.sync)
				{
					lock (this)
					{
						this.SetPropertyValue(propertyName, value);
						return;
					}
				}
				this.SetPropertyValue(propertyName, value);
			}
		}

		/// <summary>Gets the collection of settings properties.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> collection containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects.</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00031C4C File Offset: 0x0002FE4C
		public virtual SettingsPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets a collection of settings property values.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects representing the actual data values for the properties managed by the <see cref="T:System.Configuration.SettingsBase" /> instance.</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00031C54 File Offset: 0x0002FE54
		public virtual SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (this.sync)
				{
					lock (this)
					{
						return this.values;
					}
				}
				return this.values;
			}
		}

		/// <summary>Gets a collection of settings providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing <see cref="T:System.Configuration.SettingsProvider" /> objects.</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		public virtual SettingsProviderCollection Providers
		{
			get
			{
				return this.providers;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00031CA8 File Offset: 0x0002FEA8
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty;
			if (this.Properties == null || (settingsProperty = this.Properties[propertyName]) == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("The settings property '{0}' was not found", propertyName));
			}
			if (this.values[propertyName] == null)
			{
				foreach (object obj in settingsProperty.Provider.GetPropertyValues(this.Context, this.Properties))
				{
					SettingsPropertyValue property = (SettingsPropertyValue)obj;
					this.values.Add(property);
				}
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00031D64 File Offset: 0x0002FF64
		private void SetPropertyValue(string propertyName, object value)
		{
			SettingsProperty settingsProperty;
			if (this.Properties == null || (settingsProperty = this.Properties[propertyName]) == null)
			{
				throw new SettingsPropertyNotFoundException(string.Format("The settings property '{0}' was not found", propertyName));
			}
			if (settingsProperty.IsReadOnly)
			{
				throw new SettingsPropertyIsReadOnlyException(string.Format("The settings property '{0}' is read only", propertyName));
			}
			if (settingsProperty.PropertyType != value.GetType())
			{
				throw new SettingsPropertyWrongTypeException(string.Format("The value supplied is of a type incompatible with the settings property '{0}'", propertyName));
			}
			this.PropertyValues[propertyName].PropertyValue = value;
		}

		// Token: 0x04000790 RID: 1936
		private bool sync;

		// Token: 0x04000791 RID: 1937
		private SettingsContext context;

		// Token: 0x04000792 RID: 1938
		private SettingsPropertyCollection properties;

		// Token: 0x04000793 RID: 1939
		private SettingsProviderCollection providers;

		// Token: 0x04000794 RID: 1940
		private SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();
	}
}
