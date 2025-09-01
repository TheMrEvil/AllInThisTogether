using System;
using System.Reflection;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Contains the XML representing the serialized value of the setting. This class cannot be inherited.</summary>
	// Token: 0x020001C1 RID: 449
	public sealed class SettingValueElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingValueElement" /> class.</summary>
		// Token: 0x06000BC7 RID: 3015 RVA: 0x00031238 File Offset: 0x0002F438
		[MonoTODO]
		public SettingValueElement()
		{
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00031787 File Offset: 0x0002F987
		[MonoTODO]
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingValueElement" /> object by using an <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> object containing the value of a <see cref="T:System.Configuration.SettingElement" />.</returns>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0003178F File Offset: 0x0002F98F
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00031797 File Offset: 0x0002F997
		public XmlNode ValueXml
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000317A0 File Offset: 0x0002F9A0
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.original = new XmlDocument().ReadNode(reader);
			this.node = this.original.CloneNode(true);
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingValueElement" /> instance to the specified object.</summary>
		/// <param name="settingValue">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.SettingValueElement" /> instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BCC RID: 3020 RVA: 0x0000829A File Offset: 0x0000649A
		public override bool Equals(object settingValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</returns>
		// Token: 0x06000BCD RID: 3021 RVA: 0x0000829A File Offset: 0x0000649A
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000317C5 File Offset: 0x0002F9C5
		protected override bool IsModified()
		{
			return this.original != this.node;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000317D8 File Offset: 0x0002F9D8
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.node = null;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000317E1 File Offset: 0x0002F9E1
		protected override void ResetModified()
		{
			this.node = this.original;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000317EF File Offset: 0x0002F9EF
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.node == null)
			{
				return false;
			}
			this.node.WriteTo(writer);
			return true;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00031808 File Offset: 0x0002FA08
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (parentElement != null && sourceElement.GetType() != parentElement.GetType())
			{
				throw new ConfigurationErrorsException("Can't unmerge two elements of different type");
			}
			bool flag = saveMode == ConfigurationSaveMode.Minimal || saveMode == ConfigurationSaveMode.Modified;
			foreach (object obj in sourceElement.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					PropertyInformation propertyInformation2 = base.ElementInformation.Properties[propertyInformation.Name];
					object value = propertyInformation.Value;
					if (parentElement == null || !this.HasValue(parentElement, propertyInformation.Name))
					{
						propertyInformation2.Value = value;
					}
					else if (value != null)
					{
						object item = this.GetItem(parentElement, propertyInformation.Name);
						if (!this.PropertyIsElement(propertyInformation))
						{
							if (!object.Equals(value, item) || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere))
							{
								propertyInformation2.Value = value;
							}
						}
						else
						{
							ConfigurationElement configurationElement = (ConfigurationElement)value;
							if (!flag || this.ElementIsModified(configurationElement))
							{
								if (item == null)
								{
									propertyInformation2.Value = value;
								}
								else
								{
									ConfigurationElement parentElement2 = (ConfigurationElement)item;
									ConfigurationElement target = (ConfigurationElement)propertyInformation2.Value;
									this.ElementUnmerge(target, configurationElement, parentElement2, saveMode);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003196C File Offset: 0x0002FB6C
		private bool HasValue(ConfigurationElement element, string propName)
		{
			PropertyInformation propertyInformation = element.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin > PropertyValueOrigin.Default;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00031999 File Offset: 0x0002FB99
		private object GetItem(ConfigurationElement element, string property)
		{
			PropertyInformation propertyInformation = base.ElementInformation.Properties[property];
			if (propertyInformation == null)
			{
				throw new InvalidOperationException("Property '" + property + "' not found in configuration element");
			}
			return propertyInformation.Value;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000319CA File Offset: 0x0002FBCA
		private bool PropertyIsElement(PropertyInformation prop)
		{
			return typeof(ConfigurationElement).IsAssignableFrom(prop.Type);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000319E1 File Offset: 0x0002FBE1
		private bool ElementIsModified(ConfigurationElement element)
		{
			return (bool)element.GetType().GetMethod("IsModified", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(element, new object[0]);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00031A06 File Offset: 0x0002FC06
		private void ElementUnmerge(ConfigurationElement target, ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			target.GetType().GetMethod("Unmerge", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(target, new object[]
			{
				sourceElement,
				parentElement,
				saveMode
			});
		}

		// Token: 0x0400078E RID: 1934
		private XmlNode node;

		// Token: 0x0400078F RID: 1935
		private XmlNode original;
	}
}
