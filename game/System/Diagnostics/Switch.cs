using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;

namespace System.Diagnostics
{
	/// <summary>Provides an abstract base class to create new debugging and tracing switches.</summary>
	// Token: 0x02000224 RID: 548
	public abstract class Switch
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000464C0 File Offset: 0x000446C0
		private object IntializedLock
		{
			get
			{
				if (this.m_intializedLock == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref this.m_intializedLock, value, null);
				}
				return this.m_intializedLock;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class.</summary>
		/// <param name="displayName">The name of the switch.</param>
		/// <param name="description">The description for the switch.</param>
		// Token: 0x06000FE7 RID: 4071 RVA: 0x000464EF File Offset: 0x000446EF
		protected Switch(string displayName, string description) : this(displayName, description, "0")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class, specifying the display name, description, and default value for the switch.</summary>
		/// <param name="displayName">The name of the switch.</param>
		/// <param name="description">The description of the switch.</param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00046500 File Offset: 0x00044700
		protected Switch(string displayName, string description, string defaultSwitchValue)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.displayName = displayName;
			this.description = description;
			List<WeakReference> obj = Switch.switches;
			lock (obj)
			{
				Switch._pruneCachedSwitches();
				Switch.switches.Add(new WeakReference(this));
			}
			this.defaultValue = defaultSwitchValue;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00046580 File Offset: 0x00044780
		private static void _pruneCachedSwitches()
		{
			List<WeakReference> obj = Switch.switches;
			lock (obj)
			{
				if (Switch.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list = new List<WeakReference>(Switch.switches.Count);
					for (int i = 0; i < Switch.switches.Count; i++)
					{
						if ((Switch)Switch.switches[i].Target != null)
						{
							list.Add(Switch.switches[i]);
						}
					}
					if (list.Count < Switch.switches.Count)
					{
						Switch.switches.Clear();
						Switch.switches.AddRange(list);
						Switch.switches.TrimExcess();
					}
					Switch.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the case-insensitive custom attributes for the trace switch.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00046654 File Offset: 0x00044854
		[XmlIgnore]
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a name used to identify the switch.</summary>
		/// <returns>The name used to identify the switch. The default value is an empty string ("").</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00046675 File Offset: 0x00044875
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets a description of the switch.</summary>
		/// <returns>The description of the switch. The default value is an empty string ("").</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0004667D File Offset: 0x0004487D
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the current setting for this switch.</summary>
		/// <returns>The current setting for this switch. The default is zero.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00046693 File Offset: 0x00044893
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x000466B4 File Offset: 0x000448B4
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized && this.InitializeWithStatus())
				{
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				bool flag = false;
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					this.initialized = true;
					if (this.switchSetting != value)
					{
						this.switchSetting = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnSwitchSettingChanged();
				}
			}
		}

		/// <summary>Gets or sets the value of the switch.</summary>
		/// <returns>A string representing the value of the switch.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is <see langword="null" />.  
		///  -or-  
		///  The value does not consist solely of an optional negative sign followed by a sequence of digits ranging from 0 to 9.  
		///  -or-  
		///  The value represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00046714 File Offset: 0x00044914
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x00046724 File Offset: 0x00044924
		protected string Value
		{
			get
			{
				this.Initialize();
				return this.switchValueString;
			}
			set
			{
				this.Initialize();
				this.switchValueString = value;
				try
				{
					this.OnValueChanged();
				}
				catch (ArgumentException inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[]
					{
						this.DisplayName
					}), inner);
				}
				catch (FormatException inner2)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[]
					{
						this.DisplayName
					}), inner2);
				}
				catch (OverflowException inner3)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[]
					{
						this.DisplayName
					}), inner3);
				}
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000467D4 File Offset: 0x000449D4
		private void Initialize()
		{
			this.InitializeWithStatus();
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000467E0 File Offset: 0x000449E0
		private bool InitializeWithStatus()
		{
			if (!this.initialized)
			{
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					if (this.initialized || this.initializing)
					{
						return false;
					}
					this.initializing = true;
					if (this.switchSettings == null && !this.InitializeConfigSettings())
					{
						this.initialized = true;
						this.initializing = false;
						return false;
					}
					if (this.switchSettings != null)
					{
						SwitchElement switchElement = this.switchSettings[this.displayName];
						if (switchElement != null)
						{
							string value = switchElement.Value;
							if (value != null)
							{
								this.Value = value;
							}
							else
							{
								this.Value = this.defaultValue;
							}
							try
							{
								TraceUtils.VerifyAttributes(switchElement.Attributes, this.GetSupportedAttributes(), this);
							}
							catch (ConfigurationException)
							{
								this.initialized = false;
								this.initializing = false;
								throw;
							}
							this.attributes = new StringDictionary();
							this.attributes.ReplaceHashtable(switchElement.Attributes);
						}
						else
						{
							this.switchValueString = this.defaultValue;
							this.OnValueChanged();
						}
					}
					else
					{
						this.switchValueString = this.defaultValue;
						this.OnValueChanged();
					}
					this.initialized = true;
					this.initializing = false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00046958 File Offset: 0x00044B58
		private bool InitializeConfigSettings()
		{
			if (this.switchSettings != null)
			{
				return true;
			}
			if (!DiagnosticsConfiguration.CanInitialize())
			{
				return false;
			}
			this.switchSettings = DiagnosticsConfiguration.SwitchSettings;
			return true;
		}

		/// <summary>Gets the custom attributes supported by the switch.</summary>
		/// <returns>A string array that contains the names of the custom attributes supported by the switch, or <see langword="null" /> if there no custom attributes are supported.</returns>
		// Token: 0x06000FF4 RID: 4084 RVA: 0x00002F6A File Offset: 0x0000116A
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property is changed.</summary>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnSwitchSettingChanged()
		{
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.Value" /> property is changed.</summary>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x00046979 File Offset: 0x00044B79
		protected virtual void OnValueChanged()
		{
			this.SwitchSetting = int.Parse(this.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00046994 File Offset: 0x00044B94
		internal static void RefreshAll()
		{
			List<WeakReference> obj = Switch.switches;
			lock (obj)
			{
				Switch._pruneCachedSwitches();
				for (int i = 0; i < Switch.switches.Count; i++)
				{
					Switch @switch = (Switch)Switch.switches[i].Target;
					if (@switch != null)
					{
						@switch.Refresh();
					}
				}
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00046A08 File Offset: 0x00044C08
		internal void Refresh()
		{
			object intializedLock = this.IntializedLock;
			lock (intializedLock)
			{
				this.initialized = false;
				this.switchSettings = null;
				this.Initialize();
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00046A58 File Offset: 0x00044C58
		// Note: this type is marked as 'beforefieldinit'.
		static Switch()
		{
		}

		// Token: 0x040009B5 RID: 2485
		private SwitchElementsCollection switchSettings;

		// Token: 0x040009B6 RID: 2486
		private readonly string description;

		// Token: 0x040009B7 RID: 2487
		private readonly string displayName;

		// Token: 0x040009B8 RID: 2488
		private int switchSetting;

		// Token: 0x040009B9 RID: 2489
		private volatile bool initialized;

		// Token: 0x040009BA RID: 2490
		private bool initializing;

		// Token: 0x040009BB RID: 2491
		private volatile string switchValueString = string.Empty;

		// Token: 0x040009BC RID: 2492
		private StringDictionary attributes;

		// Token: 0x040009BD RID: 2493
		private string defaultValue;

		// Token: 0x040009BE RID: 2494
		private object m_intializedLock;

		// Token: 0x040009BF RID: 2495
		private static List<WeakReference> switches = new List<WeakReference>();

		// Token: 0x040009C0 RID: 2496
		private static int s_LastCollectionCount;
	}
}
