using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving concrete wrapper classes to implement the application settings feature in Window Forms applications.</summary>
	// Token: 0x02000199 RID: 409
	public abstract class ApplicationSettingsBase : SettingsBase, INotifyPropertyChanged
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class to its default state.</summary>
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002DD40 File Offset: 0x0002BF40
		protected ApplicationSettingsBase()
		{
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is <see langword="null" />.</exception>
		// Token: 0x06000AAC RID: 2732 RVA: 0x0002DD60 File Offset: 0x0002BF60
		protected ApplicationSettingsBase(IComponent owner) : this(owner, string.Empty)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied settings key.</summary>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		// Token: 0x06000AAD RID: 2733 RVA: 0x0002DD6E File Offset: 0x0002BF6E
		protected ApplicationSettingsBase(string settingsKey)
		{
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component and settings key.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is <see langword="null" />.</exception>
		// Token: 0x06000AAE RID: 2734 RVA: 0x0002DD98 File Offset: 0x0002BF98
		protected ApplicationSettingsBase(IComponent owner, string settingsKey)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.providerService = (ISettingsProviderService)owner.Site.GetService(typeof(ISettingsProviderService));
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Occurs after the value of an application settings property is changed.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000AAF RID: 2735 RVA: 0x0002DDF4 File Offset: 0x0002BFF4
		// (remove) Token: 0x06000AB0 RID: 2736 RVA: 0x0002DE2C File Offset: 0x0002C02C
		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		/// <summary>Occurs before the value of an application settings property is changed.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x0002DE64 File Offset: 0x0002C064
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x0002DE9C File Offset: 0x0002C09C
		public event SettingChangingEventHandler SettingChanging
		{
			[CompilerGenerated]
			add
			{
				SettingChangingEventHandler settingChangingEventHandler = this.SettingChanging;
				SettingChangingEventHandler settingChangingEventHandler2;
				do
				{
					settingChangingEventHandler2 = settingChangingEventHandler;
					SettingChangingEventHandler value2 = (SettingChangingEventHandler)Delegate.Combine(settingChangingEventHandler2, value);
					settingChangingEventHandler = Interlocked.CompareExchange<SettingChangingEventHandler>(ref this.SettingChanging, value2, settingChangingEventHandler2);
				}
				while (settingChangingEventHandler != settingChangingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SettingChangingEventHandler settingChangingEventHandler = this.SettingChanging;
				SettingChangingEventHandler settingChangingEventHandler2;
				do
				{
					settingChangingEventHandler2 = settingChangingEventHandler;
					SettingChangingEventHandler value2 = (SettingChangingEventHandler)Delegate.Remove(settingChangingEventHandler2, value);
					settingChangingEventHandler = Interlocked.CompareExchange<SettingChangingEventHandler>(ref this.SettingChanging, value2, settingChangingEventHandler2);
				}
				while (settingChangingEventHandler != settingChangingEventHandler2);
			}
		}

		/// <summary>Occurs after the application settings are retrieved from storage.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000AB3 RID: 2739 RVA: 0x0002DED4 File Offset: 0x0002C0D4
		// (remove) Token: 0x06000AB4 RID: 2740 RVA: 0x0002DF0C File Offset: 0x0002C10C
		public event SettingsLoadedEventHandler SettingsLoaded
		{
			[CompilerGenerated]
			add
			{
				SettingsLoadedEventHandler settingsLoadedEventHandler = this.SettingsLoaded;
				SettingsLoadedEventHandler settingsLoadedEventHandler2;
				do
				{
					settingsLoadedEventHandler2 = settingsLoadedEventHandler;
					SettingsLoadedEventHandler value2 = (SettingsLoadedEventHandler)Delegate.Combine(settingsLoadedEventHandler2, value);
					settingsLoadedEventHandler = Interlocked.CompareExchange<SettingsLoadedEventHandler>(ref this.SettingsLoaded, value2, settingsLoadedEventHandler2);
				}
				while (settingsLoadedEventHandler != settingsLoadedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SettingsLoadedEventHandler settingsLoadedEventHandler = this.SettingsLoaded;
				SettingsLoadedEventHandler settingsLoadedEventHandler2;
				do
				{
					settingsLoadedEventHandler2 = settingsLoadedEventHandler;
					SettingsLoadedEventHandler value2 = (SettingsLoadedEventHandler)Delegate.Remove(settingsLoadedEventHandler2, value);
					settingsLoadedEventHandler = Interlocked.CompareExchange<SettingsLoadedEventHandler>(ref this.SettingsLoaded, value2, settingsLoadedEventHandler2);
				}
				while (settingsLoadedEventHandler != settingsLoadedEventHandler2);
			}
		}

		/// <summary>Occurs before values are saved to the data store.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000AB5 RID: 2741 RVA: 0x0002DF44 File Offset: 0x0002C144
		// (remove) Token: 0x06000AB6 RID: 2742 RVA: 0x0002DF7C File Offset: 0x0002C17C
		public event SettingsSavingEventHandler SettingsSaving
		{
			[CompilerGenerated]
			add
			{
				SettingsSavingEventHandler settingsSavingEventHandler = this.SettingsSaving;
				SettingsSavingEventHandler settingsSavingEventHandler2;
				do
				{
					settingsSavingEventHandler2 = settingsSavingEventHandler;
					SettingsSavingEventHandler value2 = (SettingsSavingEventHandler)Delegate.Combine(settingsSavingEventHandler2, value);
					settingsSavingEventHandler = Interlocked.CompareExchange<SettingsSavingEventHandler>(ref this.SettingsSaving, value2, settingsSavingEventHandler2);
				}
				while (settingsSavingEventHandler != settingsSavingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SettingsSavingEventHandler settingsSavingEventHandler = this.SettingsSaving;
				SettingsSavingEventHandler settingsSavingEventHandler2;
				do
				{
					settingsSavingEventHandler2 = settingsSavingEventHandler;
					SettingsSavingEventHandler value2 = (SettingsSavingEventHandler)Delegate.Remove(settingsSavingEventHandler2, value);
					settingsSavingEventHandler = Interlocked.CompareExchange<SettingsSavingEventHandler>(ref this.SettingsSaving, value2, settingsSavingEventHandler2);
				}
				while (settingsSavingEventHandler != settingsSavingEventHandler2);
			}
		}

		/// <summary>Returns the value of the named settings property for the previous version of the same application.</summary>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the settings property whose value is to be returned.</param>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the specified <see cref="T:System.Configuration.SettingsProperty" /> if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">The property does not exist. The property count is zero or the property cannot be found in the data store.</exception>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000829A File Offset: 0x0000649A
		public object GetPreviousVersion(string propertyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the application settings property values from persistent storage.</summary>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		public void Reload()
		{
			if (this.PropertyValues != null)
			{
				this.PropertyValues.Clear();
			}
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				this.OnPropertyChanged(this, new PropertyChangedEventArgs(settingsProperty.Name));
			}
		}

		/// <summary>Restores the persisted application settings values to their corresponding default properties.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002E02C File Offset: 0x0002C22C
		public void Reset()
		{
			if (this.Properties != null)
			{
				foreach (object obj in this.Providers)
				{
					IApplicationSettingsProvider applicationSettingsProvider = ((SettingsProvider)obj) as IApplicationSettingsProvider;
					if (applicationSettingsProvider != null)
					{
						applicationSettingsProvider.Reset(this.Context);
					}
				}
				this.InternalSave();
			}
			this.Reload();
		}

		/// <summary>Stores the current values of the application settings properties.</summary>
		// Token: 0x06000ABA RID: 2746 RVA: 0x0002E0A8 File Offset: 0x0002C2A8
		public override void Save()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnSettingsSaving(this, cancelEventArgs);
			if (cancelEventArgs.Cancel)
			{
				return;
			}
			this.InternalSave();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
		private void InternalSave()
		{
			this.Context.CurrentSettings = this;
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
			this.Context.CurrentSettings = null;
		}

		/// <summary>Updates application settings to reflect a more recent installation of the application.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		// Token: 0x06000ABC RID: 2748 RVA: 0x0002E1BC File Offset: 0x0002C3BC
		public virtual void Upgrade()
		{
			if (this.Properties != null)
			{
				foreach (object obj in this.Providers)
				{
					SettingsProvider settingsProvider = (SettingsProvider)obj;
					IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
					if (applicationSettingsProvider != null)
					{
						applicationSettingsProvider.Upgrade(this.Context, this.GetPropertiesForProvider(settingsProvider));
					}
				}
			}
			this.Reload();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002E23C File Offset: 0x0002C43C
		private SettingsPropertyCollection GetPropertiesForProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			return settingsPropertyCollection;
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.PropertyChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000ABE RID: 2750 RVA: 0x0002E2A8 File Offset: 0x0002C4A8
		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x06000ABF RID: 2751 RVA: 0x0002E2BF File Offset: 0x0002C4BF
		protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			if (this.SettingChanging != null)
			{
				this.SettingChanging(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002E2D6 File Offset: 0x0002C4D6
		protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			if (this.SettingsLoaded != null)
			{
				this.SettingsLoaded(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002E2ED File Offset: 0x0002C4ED
		protected virtual void OnSettingsSaving(object sender, CancelEventArgs e)
		{
			if (this.SettingsSaving != null)
			{
				this.SettingsSaving(sender, e);
			}
		}

		/// <summary>Gets the application settings context associated with the settings group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings group.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002E304 File Offset: 0x0002C504
		[Browsable(false)]
		public override SettingsContext Context
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsContext result;
				try
				{
					if (this.context == null)
					{
						this.context = new SettingsContext();
						this.context["SettingsKey"] = "";
						Type type = base.GetType();
						this.context["GroupName"] = type.FullName;
						this.context["SettingsClassType"] = type;
					}
					result = this.context;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		private void CacheValuesByProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection.Count > 0)
			{
				foreach (object obj2 in provider.GetPropertyValues(this.Context, settingsPropertyCollection))
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (this.PropertyValues[settingsPropertyValue.Name] != null)
					{
						this.PropertyValues[settingsPropertyValue.Name].PropertyValue = settingsPropertyValue.PropertyValue;
					}
					else
					{
						this.PropertyValues.Add(settingsPropertyValue);
					}
				}
			}
			this.OnSettingsLoaded(this, new SettingsLoadedEventArgs(provider));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00003917 File Offset: 0x00001B17
		private void InitializeSettings(SettingsPropertyCollection settings)
		{
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(propertyName);
			}
			if (this.propertyValues == null)
			{
				this.InitializeSettings(this.Properties);
			}
			if (this.PropertyValues[propertyName] == null)
			{
				this.CacheValuesByProvider(settingsProperty.Provider);
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		/// <summary>Gets or sets the value of the specified application settings property.</summary>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <returns>If found, the value of the named settings property; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current wrapper or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be parsed.</exception>
		// Token: 0x170001AF RID: 431
		[MonoTODO]
		public override object this[string propertyName]
		{
			get
			{
				if (base.IsSynchronized)
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
				SettingsProperty settingsProperty = this.Properties[propertyName];
				if (settingsProperty == null)
				{
					throw new SettingsPropertyNotFoundException(propertyName);
				}
				if (settingsProperty.IsReadOnly)
				{
					throw new SettingsPropertyIsReadOnlyException(propertyName);
				}
				if (value != null && !settingsProperty.PropertyType.IsAssignableFrom(value.GetType()))
				{
					throw new SettingsPropertyWrongTypeException(propertyName);
				}
				if (this.PropertyValues[propertyName] == null)
				{
					this.CacheValuesByProvider(settingsProperty.Provider);
				}
				SettingChangingEventArgs settingChangingEventArgs = new SettingChangingEventArgs(propertyName, base.GetType().FullName, this.settingsKey, value, false);
				this.OnSettingChanging(this, settingChangingEventArgs);
				if (!settingChangingEventArgs.Cancel)
				{
					this.PropertyValues[propertyName].PropertyValue = value;
					this.OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
			}
		}

		/// <summary>Gets the collection of settings properties in the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects used in the current wrapper.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The associated settings provider could not be found or its instantiation failed.</exception>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002E610 File Offset: 0x0002C810
		[Browsable(false)]
		public override SettingsPropertyCollection Properties
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyCollection result;
				try
				{
					if (this.properties == null)
					{
						SettingsProvider settingsProvider = null;
						this.properties = new SettingsPropertyCollection();
						Type type = base.GetType();
						SettingsProviderAttribute[] array = (SettingsProviderAttribute[])type.GetCustomAttributes(typeof(SettingsProviderAttribute), false);
						if (array != null && array.Length != 0)
						{
							SettingsProvider settingsProvider2 = (SettingsProvider)Activator.CreateInstance(Type.GetType(array[0].ProviderTypeName));
							settingsProvider2.Initialize(null, null);
							if (settingsProvider2 != null && this.Providers[settingsProvider2.Name] == null)
							{
								this.Providers.Add(settingsProvider2);
								settingsProvider = settingsProvider2;
							}
						}
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							SettingAttribute[] array3 = (SettingAttribute[])propertyInfo.GetCustomAttributes(typeof(SettingAttribute), false);
							if (array3 != null && array3.Length != 0)
							{
								this.CreateSettingsProperty(propertyInfo, this.properties, ref settingsProvider);
							}
						}
					}
					result = this.properties;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002E728 File Offset: 0x0002C928
		private void CreateSettingsProperty(PropertyInfo prop, SettingsPropertyCollection properties, ref SettingsProvider local_provider)
		{
			SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
			SettingsProvider settingsProvider = null;
			object defaultValue = null;
			SettingsSerializeAs serializeAs = SettingsSerializeAs.String;
			bool flag = false;
			foreach (Attribute attribute in prop.GetCustomAttributes(false))
			{
				if (attribute is SettingsProviderAttribute)
				{
					string providerTypeName = ((SettingsProviderAttribute)attribute).ProviderTypeName;
					Type type = Type.GetType(providerTypeName);
					if (type == null)
					{
						string[] array = providerTypeName.Split('.', StringSplitOptions.None);
						if (array.Length > 1)
						{
							Assembly assembly = Assembly.Load(array[0]);
							if (assembly != null)
							{
								type = assembly.GetType(providerTypeName);
							}
						}
					}
					settingsProvider = (SettingsProvider)Activator.CreateInstance(type);
					settingsProvider.Initialize(null, null);
				}
				else if (attribute is DefaultSettingValueAttribute)
				{
					defaultValue = ((DefaultSettingValueAttribute)attribute).Value;
				}
				else if (attribute is SettingsSerializeAsAttribute)
				{
					serializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
					flag = true;
				}
				else if (attribute is ApplicationScopedSettingAttribute || attribute is UserScopedSettingAttribute)
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
				else
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
			}
			if (!flag)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(prop.PropertyType);
				if (converter != null && (!converter.CanConvertFrom(typeof(string)) || !converter.CanConvertTo(typeof(string))))
				{
					serializeAs = SettingsSerializeAs.Xml;
				}
			}
			SettingsProperty settingsProperty = new SettingsProperty(prop.Name, prop.PropertyType, settingsProvider, false, defaultValue, serializeAs, settingsAttributeDictionary, false, false);
			if (this.providerService != null)
			{
				settingsProperty.Provider = this.providerService.GetSettingsProvider(settingsProperty);
			}
			if (settingsProvider == null)
			{
				if (local_provider == null)
				{
					local_provider = new LocalFileSettingsProvider();
					local_provider.Initialize(null, null);
				}
				settingsProperty.Provider = local_provider;
				settingsProvider = local_provider;
			}
			if (settingsProvider != null)
			{
				SettingsProvider settingsProvider2 = this.Providers[settingsProvider.Name];
				if (settingsProvider2 != null)
				{
					settingsProperty.Provider = settingsProvider2;
				}
			}
			properties.Add(settingsProperty);
			if (settingsProperty.Provider != null && this.Providers[settingsProperty.Provider.Name] == null)
			{
				this.Providers.Add(settingsProperty.Provider);
			}
		}

		/// <summary>Gets a collection of property values.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> of property values.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002E940 File Offset: 0x0002CB40
		[Browsable(false)]
		public override SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyValueCollection result;
				try
				{
					if (this.propertyValues == null)
					{
						this.propertyValues = new SettingsPropertyValueCollection();
					}
					result = this.propertyValues;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		/// <summary>Gets the collection of application settings providers used by the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing all the <see cref="T:System.Configuration.SettingsProvider" /> objects used by the settings properties of the current settings wrapper.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002E998 File Offset: 0x0002CB98
		[Browsable(false)]
		public override SettingsProviderCollection Providers
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsProviderCollection result;
				try
				{
					if (this.providers == null)
					{
						this.providers = new SettingsProviderCollection();
					}
					result = this.providers;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return result;
			}
		}

		/// <summary>Gets or sets the settings key for the application settings group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current settings group.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002E9F0 File Offset: 0x0002CBF0
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0002E9F8 File Offset: 0x0002CBF8
		[Browsable(false)]
		public string SettingsKey
		{
			get
			{
				return this.settingsKey;
			}
			set
			{
				this.settingsKey = value;
			}
		}

		// Token: 0x04000723 RID: 1827
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanged;

		// Token: 0x04000724 RID: 1828
		[CompilerGenerated]
		private SettingChangingEventHandler SettingChanging;

		// Token: 0x04000725 RID: 1829
		[CompilerGenerated]
		private SettingsLoadedEventHandler SettingsLoaded;

		// Token: 0x04000726 RID: 1830
		[CompilerGenerated]
		private SettingsSavingEventHandler SettingsSaving;

		// Token: 0x04000727 RID: 1831
		private string settingsKey;

		// Token: 0x04000728 RID: 1832
		private SettingsContext context;

		// Token: 0x04000729 RID: 1833
		private SettingsPropertyCollection properties;

		// Token: 0x0400072A RID: 1834
		private ISettingsProviderService providerService;

		// Token: 0x0400072B RID: 1835
		private SettingsPropertyValueCollection propertyValues;

		// Token: 0x0400072C RID: 1836
		private SettingsProviderCollection providers;
	}
}
