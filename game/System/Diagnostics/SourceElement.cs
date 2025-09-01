using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x02000220 RID: 544
	internal class SourceElement : ConfigurationElement
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x00046120 File Offset: 0x00044320
		static SourceElement()
		{
			SourceElement._properties = new ConfigurationPropertyCollection();
			SourceElement._properties.Add(SourceElement._propName);
			SourceElement._properties.Add(SourceElement._propSwitchName);
			SourceElement._properties.Add(SourceElement._propSwitchValue);
			SourceElement._properties.Add(SourceElement._propSwitchType);
			SourceElement._properties.Add(SourceElement._propListeners);
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00046211 File Offset: 0x00044411
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00046231 File Offset: 0x00044431
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[SourceElement._propListeners];
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00046243 File Offset: 0x00044443
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SourceElement._propName];
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00046255 File Offset: 0x00044455
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SourceElement._properties;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0004625C File Offset: 0x0004445C
		[ConfigurationProperty("switchName")]
		public string SwitchName
		{
			get
			{
				return (string)base[SourceElement._propSwitchName];
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0004626E File Offset: 0x0004446E
		[ConfigurationProperty("switchValue")]
		public string SwitchValue
		{
			get
			{
				return (string)base[SourceElement._propSwitchValue];
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00046280 File Offset: 0x00044480
		[ConfigurationProperty("switchType")]
		public string SwitchType
		{
			get
			{
				return (string)base[SourceElement._propSwitchType];
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00046294 File Offset: 0x00044494
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (!string.IsNullOrEmpty(this.SwitchName) && !string.IsNullOrEmpty(this.SwitchValue))
			{
				throw new ConfigurationErrorsException(SR.GetString("'switchValue' and 'switchName' cannot both be specified on source '{0}'.", new object[]
				{
					this.Name
				}));
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000462E2 File Offset: 0x000444E2
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000462F4 File Offset: 0x000444F4
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string localName = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(localName, text);
					}
				}
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00046345 File Offset: 0x00044545
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004636C File Offset: 0x0004456C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SourceElement sourceElement2 = sourceElement as SourceElement;
			if (sourceElement2 != null && sourceElement2._attributes != null)
			{
				this._attributes = sourceElement2._attributes;
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000463A0 File Offset: 0x000445A0
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SourceElement._properties.Clear();
				SourceElement._properties.Add(SourceElement._propName);
				SourceElement._properties.Add(SourceElement._propSwitchName);
				SourceElement._properties.Add(SourceElement._propSwitchValue);
				SourceElement._properties.Add(SourceElement._propSwitchType);
				SourceElement._properties.Add(SourceElement._propListeners);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00031238 File Offset: 0x0002F438
		public SourceElement()
		{
		}

		// Token: 0x040009A4 RID: 2468
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040009A5 RID: 2469
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040009A6 RID: 2470
		private static readonly ConfigurationProperty _propSwitchName = new ConfigurationProperty("switchName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009A7 RID: 2471
		private static readonly ConfigurationProperty _propSwitchValue = new ConfigurationProperty("switchValue", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009A8 RID: 2472
		private static readonly ConfigurationProperty _propSwitchType = new ConfigurationProperty("switchType", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040009A9 RID: 2473
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009AA RID: 2474
		private Hashtable _attributes;
	}
}
