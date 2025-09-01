using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x02000227 RID: 551
	internal class SwitchElement : ConfigurationElement
	{
		// Token: 0x06001009 RID: 4105 RVA: 0x00046C08 File Offset: 0x00044E08
		static SwitchElement()
		{
			SwitchElement._properties = new ConfigurationPropertyCollection();
			SwitchElement._properties.Add(SwitchElement._propName);
			SwitchElement._properties.Add(SwitchElement._propValue);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00046C77 File Offset: 0x00044E77
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

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00046C97 File Offset: 0x00044E97
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SwitchElement._propName];
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00046CA9 File Offset: 0x00044EA9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SwitchElement._properties;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00046CB0 File Offset: 0x00044EB0
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base[SwitchElement._propValue];
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00046CC2 File Offset: 0x00044EC2
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00046CD4 File Offset: 0x00044ED4
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

		// Token: 0x06001010 RID: 4112 RVA: 0x00046D25 File Offset: 0x00044F25
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00046D4C File Offset: 0x00044F4C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SwitchElement switchElement = sourceElement as SwitchElement;
			if (switchElement != null && switchElement._attributes != null)
			{
				this._attributes = switchElement._attributes;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00046D80 File Offset: 0x00044F80
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				SwitchElement._properties.Clear();
				SwitchElement._properties.Add(SwitchElement._propName);
				SwitchElement._properties.Add(SwitchElement._propValue);
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00031238 File Offset: 0x0002F438
		public SwitchElement()
		{
		}

		// Token: 0x040009C4 RID: 2500
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040009C5 RID: 2501
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040009C6 RID: 2502
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040009C7 RID: 2503
		private Hashtable _attributes;
	}
}
