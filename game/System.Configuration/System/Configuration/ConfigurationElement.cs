using System;
using System.Collections;
using System.Xml;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a configuration element within a configuration file.</summary>
	// Token: 0x02000019 RID: 25
	public abstract class ConfigurationElement
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000035EF File Offset: 0x000017EF
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000035F7 File Offset: 0x000017F7
		internal Configuration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				this._configuration = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationElement" /> class.</summary>
		// Token: 0x06000098 RID: 152 RVA: 0x00002050 File Offset: 0x00000250
		protected ConfigurationElement()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003600 File Offset: 0x00001800
		internal virtual void InitFromProperty(PropertyInformation propertyInfo)
		{
			this.elementInfo = new ElementInformation(this, propertyInfo);
			this.Init();
		}

		/// <summary>Gets an <see cref="T:System.Configuration.ElementInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.ElementInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003615 File Offset: 0x00001815
		public ElementInformation ElementInformation
		{
			get
			{
				if (this.elementInfo == null)
				{
					this.elementInfo = new ElementInformation(this, null);
				}
				return this.elementInfo;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003632 File Offset: 0x00001832
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000363A File Offset: 0x0000183A
		internal string RawXml
		{
			get
			{
				return this.rawXml;
			}
			set
			{
				if (this.rawXml == null || value != null)
				{
					this.rawXml = value;
				}
			}
		}

		/// <summary>Sets the <see cref="T:System.Configuration.ConfigurationElement" /> object to its initial state.</summary>
		// Token: 0x0600009D RID: 157 RVA: 0x000023B9 File Offset: 0x000005B9
		protected internal virtual void Init()
		{
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationElementProperty" /> object that represents the <see cref="T:System.Configuration.ConfigurationElement" /> object itself.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementProperty" /> that represents the <see cref="T:System.Configuration.ConfigurationElement" /> itself.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000364E File Offset: 0x0000184E
		protected internal virtual ConfigurationElementProperty ElementProperty
		{
			get
			{
				if (this.elementProperty == null)
				{
					this.elementProperty = new ConfigurationElementProperty(this.ElementInformation.Validator);
				}
				return this.elementProperty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> for the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current element is not associated with a context.</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003674 File Offset: 0x00001874
		protected ContextInformation EvaluationContext
		{
			get
			{
				if (this.Configuration != null)
				{
					return this.Configuration.EvaluationContext;
				}
				throw new ConfigurationErrorsException("This element is not currently associated with any context.");
			}
		}

		/// <summary>Gets the collection of locked attributes.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003694 File Offset: 0x00001894
		public ConfigurationLockCollection LockAllAttributesExcept
		{
			get
			{
				if (this.lockAllAttributesExcept == null)
				{
					this.lockAllAttributesExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute | ConfigurationLockType.Exclude);
				}
				return this.lockAllAttributesExcept;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000036B2 File Offset: 0x000018B2
		public ConfigurationLockCollection LockAllElementsExcept
		{
			get
			{
				if (this.lockAllElementsExcept == null)
				{
					this.lockAllElementsExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Element | ConfigurationLockType.Exclude);
				}
				return this.lockAllElementsExcept;
			}
		}

		/// <summary>Gets the collection of locked attributes</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000036D0 File Offset: 0x000018D0
		public ConfigurationLockCollection LockAttributes
		{
			get
			{
				if (this.lockAttributes == null)
				{
					this.lockAttributes = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute);
				}
				return this.lockAttributes;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000036ED File Offset: 0x000018ED
		public ConfigurationLockCollection LockElements
		{
			get
			{
				if (this.lockElements == null)
				{
					this.lockElements = new ConfigurationLockCollection(this, ConfigurationLockType.Element);
				}
				return this.lockElements;
			}
		}

		/// <summary>Gets or sets a value indicating whether the element is locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the element is locked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element has already been locked at a higher configuration level.</exception>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000370A File Offset: 0x0000190A
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003712 File Offset: 0x00001912
		public bool LockItem
		{
			get
			{
				return this.lockItem;
			}
			set
			{
				this.lockItem = value;
			}
		}

		/// <summary>Adds the invalid-property errors in this <see cref="T:System.Configuration.ConfigurationElement" /> object, and in all subelements, to the passed list.</summary>
		/// <param name="errorList">An object that implements the <see cref="T:System.Collections.IList" /> interface.</param>
		// Token: 0x060000A6 RID: 166 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		protected virtual void ListErrors(IList errorList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a property to the specified value.</summary>
		/// <param name="prop">The element property to set.</param>
		/// <param name="value">The value to assign to the property.</param>
		/// <param name="ignoreLocks">
		///   <see langword="true" /> if the locks on the property should be ignored; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs if the element is read-only or <paramref name="ignoreLocks" /> is <see langword="true" /> but the locks cannot be ignored.</exception>
		// Token: 0x060000A7 RID: 167 RVA: 0x00003724 File Offset: 0x00001924
		[MonoTODO]
		protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
		{
			try
			{
				if (value != null)
				{
					prop.Validate(value);
				}
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(string.Format("The value for the property '{0}' on type {1} is not valid.", prop.Name, this.ElementInformation.Type), inner);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003770 File Offset: 0x00001970
		internal ConfigurationPropertyCollection GetKeyProperties()
		{
			if (this.keyProps != null)
			{
				return this.keyProps;
			}
			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (configurationProperty.IsKey)
				{
					configurationPropertyCollection.Add(configurationProperty);
				}
			}
			return this.keyProps = configurationPropertyCollection;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000037F4 File Offset: 0x000019F4
		internal ConfigurationElementCollection GetDefaultCollection()
		{
			if (this.defaultCollection != null)
			{
				return this.defaultCollection;
			}
			ConfigurationProperty configurationProperty = null;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty2 = (ConfigurationProperty)obj;
				if (configurationProperty2.IsDefaultCollection)
				{
					configurationProperty = configurationProperty2;
					break;
				}
			}
			if (configurationProperty != null)
			{
				this.defaultCollection = (this[configurationProperty] as ConfigurationElementCollection);
			}
			return this.defaultCollection;
		}

		/// <summary>Gets or sets a property or attribute of this configuration element.</summary>
		/// <param name="prop">The property to access.</param>
		/// <returns>The specified property, attribute, or child element.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">
		///   <paramref name="prop" /> is <see langword="null" /> or does not exist within the element.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read only or locked.</exception>
		// Token: 0x17000032 RID: 50
		protected internal object this[ConfigurationProperty prop]
		{
			get
			{
				return this[prop.Name];
			}
			set
			{
				this[prop.Name] = value;
			}
		}

		/// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
		/// <param name="propertyName">The name of the <see cref="T:System.Configuration.ConfigurationProperty" /> to access.</param>
		/// <returns>The specified property, attribute, or child element</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read-only or locked.</exception>
		// Token: 0x17000033 RID: 51
		protected internal object this[string propertyName]
		{
			get
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[propertyName];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + propertyName + "' not found in configuration element");
				}
				return propertyInformation.Value;
			}
			set
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[propertyName];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + propertyName + "' not found in configuration element");
				}
				this.SetPropertyValue(propertyInformation.Property, value, false);
				propertyInformation.Value = value;
				this.modified = true;
			}
		}

		/// <summary>Gets the collection of properties.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003924 File Offset: 0x00001B24
		protected internal virtual ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.map == null)
				{
					this.map = ElementMap.GetMap(base.GetType());
				}
				return this.map.Properties;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.ConfigurationElement" /> instance to the specified object.</summary>
		/// <param name="compareTo">The object to compare with.</param>
		/// <returns>
		///   <see langword="true" /> if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElement" /> instance; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x060000AF RID: 175 RVA: 0x0000394C File Offset: 0x00001B4C
		public override bool Equals(object compareTo)
		{
			ConfigurationElement configurationElement = compareTo as ConfigurationElement;
			if (configurationElement == null)
			{
				return false;
			}
			if (base.GetType() != configurationElement.GetType())
			{
				return false;
			}
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty prop = (ConfigurationProperty)obj;
				if (!object.Equals(this[prop], configurationElement[prop]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</summary>
		/// <returns>A unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</returns>
		// Token: 0x060000B0 RID: 176 RVA: 0x000039E0 File Offset: 0x00001BE0
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty prop = (ConfigurationProperty)obj;
				object obj2 = this[prop];
				if (obj2 != null)
				{
					num += obj2.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003A4C File Offset: 0x00001C4C
		internal virtual bool HasLocalModifications()
		{
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere && propertyInformation.IsModified)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
		/// <param name="serializeCollectionKey">
		///   <see langword="true" /> to serialize only the collection key properties; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element to read is locked.  
		/// -or-
		///  An attribute of the current node is not recognized.  
		/// -or-
		///  The lock status of the current node cannot be determined.</exception>
		// Token: 0x060000B2 RID: 178 RVA: 0x00003ABC File Offset: 0x00001CBC
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			Hashtable hashtable = new Hashtable();
			reader.MoveToContent();
			this.elementPresent = true;
			while (reader.MoveToNextAttribute())
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[reader.LocalName];
				if (propertyInformation == null || (serializeCollectionKey && !propertyInformation.IsKey))
				{
					if (reader.LocalName == "lockAllAttributesExcept")
					{
						this.LockAllAttributesExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAllElementsExcept")
					{
						this.LockAllElementsExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAttributes")
					{
						this.LockAttributes.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockElements")
					{
						this.LockElements.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockItem")
					{
						this.LockItem = (reader.Value.ToLowerInvariant() == "true");
					}
					else if (!(reader.LocalName == "xmlns") && (!(this is ConfigurationSection) || !(reader.LocalName == "configSource")) && !this.OnDeserializeUnrecognizedAttribute(reader.LocalName, reader.Value))
					{
						throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.", reader);
					}
				}
				else
				{
					if (hashtable.ContainsKey(propertyInformation))
					{
						throw new ConfigurationErrorsException("The attribute '" + propertyInformation.Name + "' may only appear once in this element.", reader);
					}
					try
					{
						string value = reader.Value;
						this.ValidateValue(propertyInformation.Property, value);
						propertyInformation.SetStringValue(value);
					}
					catch (ConfigurationErrorsException)
					{
						throw;
					}
					catch (ConfigurationException)
					{
						throw;
					}
					catch (Exception ex)
					{
						throw new ConfigurationErrorsException(string.Format("The value for the property '{0}' is not valid. The error is: {1}", propertyInformation.Name, ex.Message), reader);
					}
					hashtable[propertyInformation] = propertyInformation.Name;
					ConfigXmlTextReader configXmlTextReader = reader as ConfigXmlTextReader;
					if (configXmlTextReader != null)
					{
						propertyInformation.Source = configXmlTextReader.Filename;
						propertyInformation.LineNumber = configXmlTextReader.LineNumber;
					}
				}
			}
			reader.MoveToElement();
			if (!reader.IsEmptyElement)
			{
				int depth = reader.Depth;
				reader.ReadStartElement();
				reader.MoveToContent();
				PropertyInformation propertyInformation2;
				for (;;)
				{
					if (reader.NodeType != XmlNodeType.Element)
					{
						reader.Skip();
					}
					else
					{
						propertyInformation2 = this.ElementInformation.Properties[reader.LocalName];
						if (propertyInformation2 == null || (serializeCollectionKey && !propertyInformation2.IsKey))
						{
							if (!this.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
							{
								if (propertyInformation2 != null)
								{
									break;
								}
								ConfigurationElementCollection configurationElementCollection = this.GetDefaultCollection();
								if (configurationElementCollection == null || !configurationElementCollection.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
								{
									break;
								}
							}
						}
						else
						{
							if (!propertyInformation2.IsElement)
							{
								goto Block_22;
							}
							if (hashtable.Contains(propertyInformation2))
							{
								goto Block_23;
							}
							((ConfigurationElement)propertyInformation2.Value).DeserializeElement(reader, serializeCollectionKey);
							hashtable[propertyInformation2] = propertyInformation2.Name;
							if (depth == reader.Depth)
							{
								reader.Read();
							}
						}
					}
					if (depth >= reader.Depth)
					{
						goto IL_367;
					}
				}
				throw new ConfigurationErrorsException("Unrecognized element '" + reader.LocalName + "'.", reader);
				Block_22:
				throw new ConfigurationErrorsException("Property '" + propertyInformation2.Name + "' is not a ConfigurationElement.");
				Block_23:
				throw new ConfigurationErrorsException("The element <" + propertyInformation2.Name + "> may only appear once in this section.", reader);
			}
			reader.Skip();
			IL_367:
			this.modified = false;
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation3 = (PropertyInformation)obj;
				if (!string.IsNullOrEmpty(propertyInformation3.Name) && propertyInformation3.IsRequired && !hashtable.ContainsKey(propertyInformation3) && this.ElementInformation.Properties[propertyInformation3.Name] == null)
				{
					object obj2 = this.OnRequiredPropertyNotFound(propertyInformation3.Name);
					if (!object.Equals(obj2, propertyInformation3.DefaultValue))
					{
						propertyInformation3.Value = obj2;
						propertyInformation3.IsModified = false;
					}
				}
			}
			this.PostDeserialize();
		}

		/// <summary>Gets a value indicating whether an unknown attribute is encountered during deserialization.</summary>
		/// <param name="name">The name of the unrecognized attribute.</param>
		/// <param name="value">The value of the unrecognized attribute.</param>
		/// <returns>
		///   <see langword="true" /> when an unknown attribute is encountered while deserializing; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000B3 RID: 179 RVA: 0x000023BB File Offset: 0x000005BB
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether an unknown element is encountered during deserialization.</summary>
		/// <param name="elementName">The name of the unknown subelement.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> being used for deserialization.</param>
		/// <returns>
		///   <see langword="true" /> when an unknown element is encountered while deserializing; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element identified by <paramref name="elementName" /> is locked.  
		/// -or-
		///  One or more of the element's attributes is locked.  
		/// -or-
		///  <paramref name="elementName" /> is unrecognized, or the element has an unrecognized attribute.  
		/// -or-
		///  The element has a Boolean attribute with an invalid value.  
		/// -or-
		///  An attempt was made to deserialize a property more than once.  
		/// -or-
		///  An attempt was made to deserialize a property that is not a valid member of the element.  
		/// -or-
		///  The element cannot contain a CDATA or text element.</exception>
		// Token: 0x060000B4 RID: 180 RVA: 0x000023BB File Offset: 0x000005BB
		protected virtual bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return false;
		}

		/// <summary>Throws an exception when a required property is not found.</summary>
		/// <param name="name">The name of the required attribute that was not found.</param>
		/// <returns>None.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">In all cases.</exception>
		// Token: 0x060000B5 RID: 181 RVA: 0x00003F20 File Offset: 0x00002120
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new ConfigurationErrorsException("Required attribute '" + name + "' not found.");
		}

		/// <summary>Called before serialization.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that will be used to serialize the <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		// Token: 0x060000B6 RID: 182 RVA: 0x000023B9 File Offset: 0x000005B9
		protected virtual void PreSerialize(XmlWriter writer)
		{
		}

		/// <summary>Called after deserialization.</summary>
		// Token: 0x060000B7 RID: 183 RVA: 0x000023B9 File Offset: 0x000005B9
		protected virtual void PostDeserialize()
		{
		}

		/// <summary>Used to initialize a default set of values for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		// Token: 0x060000B8 RID: 184 RVA: 0x000023B9 File Offset: 0x000005B9
		protected internal virtual void InitializeDefault()
		{
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded, when implemented in a derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the element has been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000B9 RID: 185 RVA: 0x00003F38 File Offset: 0x00002138
		protected internal virtual bool IsModified()
		{
			if (this.modified)
			{
				return true;
			}
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.IsElement)
				{
					ConfigurationElement configurationElement = propertyInformation.Value as ConfigurationElement;
					if (configurationElement != null && configurationElement.IsModified())
					{
						this.modified = true;
						break;
					}
				}
			}
			return this.modified;
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElement.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElement" /> object and all subelements.</summary>
		// Token: 0x060000BA RID: 186 RVA: 0x00003FC8 File Offset: 0x000021C8
		protected internal virtual void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000BB RID: 187 RVA: 0x00003FD1 File Offset: 0x000021D1
		public virtual bool IsReadOnly()
		{
			return this.readOnly;
		}

		/// <summary>Resets the internal state of the <see cref="T:System.Configuration.ConfigurationElement" /> object, including the locks and the properties collections.</summary>
		/// <param name="parentElement">The parent node of the configuration element.</param>
		// Token: 0x060000BC RID: 188 RVA: 0x00003FD9 File Offset: 0x000021D9
		protected internal virtual void Reset(ConfigurationElement parentElement)
		{
			this.elementPresent = false;
			if (parentElement != null)
			{
				this.ElementInformation.Reset(parentElement.ElementInformation);
				return;
			}
			this.InitializeDefault();
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to <see langword="false" /> when implemented in a derived class.</summary>
		// Token: 0x060000BD RID: 189 RVA: 0x00004000 File Offset: 0x00002200
		protected internal virtual void ResetModified()
		{
			this.modified = false;
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				propertyInformation.IsModified = false;
				ConfigurationElement configurationElement = propertyInformation.Value as ConfigurationElement;
				if (configurationElement != null)
				{
					configurationElement.ResetModified();
				}
			}
		}

		/// <summary>Writes the contents of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
		/// <param name="serializeCollectionKey">
		///   <see langword="true" /> to serialize only the collection key properties; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if any data was actually serialized; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current attribute is locked at a higher configuration level.</exception>
		// Token: 0x060000BE RID: 190 RVA: 0x00004078 File Offset: 0x00002278
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			this.PreSerialize(writer);
			if (serializeCollectionKey)
			{
				ConfigurationPropertyCollection keyProperties = this.GetKeyProperties();
				foreach (object obj in keyProperties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					writer.WriteAttributeString(configurationProperty.Name, configurationProperty.ConvertToString(this[configurationProperty.Name]));
				}
				return keyProperties.Count > 0;
			}
			bool flag = false;
			foreach (object obj2 in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj2;
				if (!propertyInformation.IsElement)
				{
					if (this.saveContext == null)
					{
						throw new InvalidOperationException();
					}
					if (this.saveContext.HasValue(propertyInformation))
					{
						writer.WriteAttributeString(propertyInformation.Name, propertyInformation.GetStringValue());
						flag = true;
					}
				}
			}
			foreach (object obj3 in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation2 = (PropertyInformation)obj3;
				if (propertyInformation2.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation2.Value;
					if (configurationElement != null)
					{
						flag = (configurationElement.SerializeToXmlElement(writer, propertyInformation2.Name) || flag);
					}
				}
			}
			return flag;
		}

		/// <summary>Writes the outer tags of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to be written.</param>
		/// <returns>
		///   <see langword="true" /> if writing was successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Exception">The element has multiple child elements.</exception>
		// Token: 0x060000BF RID: 191 RVA: 0x00004204 File Offset: 0x00002404
		protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.saveContext == null)
			{
				throw new InvalidOperationException();
			}
			if (!this.saveContext.HasValues())
			{
				return false;
			}
			if (elementName != null && elementName != "")
			{
				writer.WriteStartElement(elementName);
			}
			bool result = this.SerializeElement(writer, false);
			if (elementName != null && elementName != "")
			{
				writer.WriteEndElement();
			}
			return result;
		}

		/// <summary>Modifies the <see cref="T:System.Configuration.ConfigurationElement" /> object to remove all values that should not be saved.</summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" />, or <see langword="null" /> if this is the top level.</param>
		/// <param name="saveMode">One of the enumeration values that determines which property values to include.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x00004264 File Offset: 0x00002464
		protected internal virtual void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
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
					PropertyInformation propertyInformation2 = this.ElementInformation.Properties[propertyInformation.Name];
					object value = propertyInformation.Value;
					if (parentElement == null || !parentElement.HasValue(propertyInformation.Name))
					{
						propertyInformation2.Value = value;
					}
					else if (value != null)
					{
						object obj2 = parentElement[propertyInformation.Name];
						if (!propertyInformation.IsElement)
						{
							if (!object.Equals(value, obj2) || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere))
							{
								propertyInformation2.Value = value;
							}
						}
						else
						{
							ConfigurationElement configurationElement = (ConfigurationElement)value;
							if (!flag || configurationElement.IsModified())
							{
								if (obj2 == null)
								{
									propertyInformation2.Value = value;
								}
								else
								{
									ConfigurationElement parentElement2 = (ConfigurationElement)obj2;
									((ConfigurationElement)propertyInformation2.Value).Unmerge(configurationElement, parentElement2, saveMode);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000043BC File Offset: 0x000025BC
		internal bool HasValue(string propName)
		{
			PropertyInformation propertyInformation = this.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin > PropertyValueOrigin.Default;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043EC File Offset: 0x000025EC
		internal bool IsReadFromConfig(string propName)
		{
			PropertyInformation propertyInformation = this.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004419 File Offset: 0x00002619
		internal bool IsElementPresent
		{
			get
			{
				return this.elementPresent;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004424 File Offset: 0x00002624
		private void ValidateValue(ConfigurationProperty p, string value)
		{
			ConfigurationValidatorBase validator;
			if (p == null || (validator = p.Validator) == null)
			{
				return;
			}
			if (!validator.CanValidate(p.Type))
			{
				throw new ConfigurationErrorsException(string.Format("Validator does not support type {0}", p.Type));
			}
			validator.Validate(p.ConvertFromString(value));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004470 File Offset: 0x00002670
		internal bool HasValue(ConfigurationElement parent, PropertyInformation prop, ConfigurationSaveMode mode)
		{
			if (prop.ValueOrigin == PropertyValueOrigin.Default)
			{
				return false;
			}
			if (mode == ConfigurationSaveMode.Modified && prop.ValueOrigin == PropertyValueOrigin.SetHere && prop.IsModified)
			{
				return true;
			}
			object obj = (parent != null && parent.HasValue(prop.Name)) ? parent[prop.Name] : prop.DefaultValue;
			if (!prop.IsElement)
			{
				return !object.Equals(prop.Value, obj);
			}
			ConfigurationElement configurationElement = (ConfigurationElement)prop.Value;
			ConfigurationElement parent2 = (ConfigurationElement)obj;
			return configurationElement.HasValues(parent2, mode);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000044F8 File Offset: 0x000026F8
		internal virtual bool HasValues(ConfigurationElement parent, ConfigurationSaveMode mode)
		{
			if (mode == ConfigurationSaveMode.Full)
			{
				return true;
			}
			if (this.modified && mode == ConfigurationSaveMode.Modified)
			{
				return true;
			}
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation prop = (PropertyInformation)obj;
				if (this.HasValue(parent, prop, mode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004574 File Offset: 0x00002774
		internal virtual void PrepareSave(ConfigurationElement parent, ConfigurationSaveMode mode)
		{
			this.saveContext = new ConfigurationElement.SaveContext(this, parent, mode);
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation.Value;
					if (parent == null || !parent.HasValue(propertyInformation.Name))
					{
						configurationElement.PrepareSave(null, mode);
					}
					else
					{
						ConfigurationElement parent2 = (ConfigurationElement)parent[propertyInformation.Name];
						configurationElement.PrepareSave(parent2, mode);
					}
				}
			}
		}

		/// <summary>Gets a reference to the top-level <see cref="T:System.Configuration.Configuration" /> instance that represents the configuration hierarchy that the current <see cref="T:System.Configuration.ConfigurationElement" /> instance belongs to.</summary>
		/// <returns>The top-level <see cref="T:System.Configuration.Configuration" /> instance that the current <see cref="T:System.Configuration.ConfigurationElement" /> instance belongs to.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003527 File Offset: 0x00001727
		public Configuration CurrentConfiguration
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Configuration.ConfigurationElement.CurrentConfiguration" /> property is <see langword="null" />.</summary>
		/// <returns>false if the <see cref="P:System.Configuration.ConfigurationElement.CurrentConfiguration" /> property is <see langword="null" />; otherwise, <see langword="true" />.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004628 File Offset: 0x00002828
		protected bool HasContext
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Returns the transformed version of the specified assembly name.</summary>
		/// <param name="assemblyName">The name of the assembly.</param>
		/// <returns>The transformed version of the assembly name. If no transformer is available, the <paramref name="assemblyName" /> parameter value is returned unchanged. The <see cref="P:System.Configuration.Configuration.TypeStringTransformer" /> property is <see langword="null" /> if no transformer is available.</returns>
		// Token: 0x060000CA RID: 202 RVA: 0x00003527 File Offset: 0x00001727
		protected virtual string GetTransformedAssemblyString(string assemblyName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the transformed version of the specified type name.</summary>
		/// <param name="typeName">The name of the type.</param>
		/// <returns>The transformed version of the specified type name. If no transformer is available, the <paramref name="typeName" /> parameter value is returned unchanged. The <see cref="P:System.Configuration.Configuration.TypeStringTransformer" /> property is <see langword="null" /> if no transformer is available.</returns>
		// Token: 0x060000CB RID: 203 RVA: 0x00003527 File Offset: 0x00001727
		protected virtual string GetTransformedTypeString(string typeName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0400005D RID: 93
		private string rawXml;

		// Token: 0x0400005E RID: 94
		private bool modified;

		// Token: 0x0400005F RID: 95
		private ElementMap map;

		// Token: 0x04000060 RID: 96
		private ConfigurationPropertyCollection keyProps;

		// Token: 0x04000061 RID: 97
		private ConfigurationElementCollection defaultCollection;

		// Token: 0x04000062 RID: 98
		private bool readOnly;

		// Token: 0x04000063 RID: 99
		private ElementInformation elementInfo;

		// Token: 0x04000064 RID: 100
		private ConfigurationElementProperty elementProperty;

		// Token: 0x04000065 RID: 101
		private Configuration _configuration;

		// Token: 0x04000066 RID: 102
		private bool elementPresent;

		// Token: 0x04000067 RID: 103
		private ConfigurationLockCollection lockAllAttributesExcept;

		// Token: 0x04000068 RID: 104
		private ConfigurationLockCollection lockAllElementsExcept;

		// Token: 0x04000069 RID: 105
		private ConfigurationLockCollection lockAttributes;

		// Token: 0x0400006A RID: 106
		private ConfigurationLockCollection lockElements;

		// Token: 0x0400006B RID: 107
		private bool lockItem;

		// Token: 0x0400006C RID: 108
		private ConfigurationElement.SaveContext saveContext;

		// Token: 0x0200001A RID: 26
		private class SaveContext
		{
			// Token: 0x060000CC RID: 204 RVA: 0x00004643 File Offset: 0x00002843
			public SaveContext(ConfigurationElement element, ConfigurationElement parent, ConfigurationSaveMode mode)
			{
				this.Element = element;
				this.Parent = parent;
				this.Mode = mode;
			}

			// Token: 0x060000CD RID: 205 RVA: 0x00004660 File Offset: 0x00002860
			public bool HasValues()
			{
				return this.Mode == ConfigurationSaveMode.Full || this.Element.HasValues(this.Parent, this.Mode);
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00004684 File Offset: 0x00002884
			public bool HasValue(PropertyInformation prop)
			{
				return this.Mode == ConfigurationSaveMode.Full || this.Element.HasValue(this.Parent, prop, this.Mode);
			}

			// Token: 0x0400006D RID: 109
			public readonly ConfigurationElement Element;

			// Token: 0x0400006E RID: 110
			public readonly ConfigurationElement Parent;

			// Token: 0x0400006F RID: 111
			public readonly ConfigurationSaveMode Mode;
		}
	}
}
