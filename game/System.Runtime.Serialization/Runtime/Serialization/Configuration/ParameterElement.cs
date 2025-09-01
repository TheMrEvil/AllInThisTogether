using System;
using System.Configuration;
using System.Xml;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A6 RID: 422
	public sealed class ParameterElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> class.</summary>
		// Token: 0x0600153D RID: 5437 RVA: 0x000541C3 File Offset: 0x000523C3
		public ParameterElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> class with the specified type name.</summary>
		/// <param name="typeName">The name of the parameter's type.</param>
		// Token: 0x0600153E RID: 5438 RVA: 0x000541D6 File Offset: 0x000523D6
		public ParameterElement(string typeName) : this()
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			this.Type = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.ParameterElement" /> class with the specified index.</summary>
		/// <param name="index">Specifies a position in the collection of parameters.</param>
		// Token: 0x0600153F RID: 5439 RVA: 0x000541F8 File Offset: 0x000523F8
		public ParameterElement(int index) : this()
		{
			this.Index = index;
		}

		/// <summary>Gets or sets the position of the generic known type.</summary>
		/// <returns>The position of the parameter in the containing generic declared type.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00054207 File Offset: 0x00052407
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x00054219 File Offset: 0x00052419
		[ConfigurationProperty("index", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int Index
		{
			get
			{
				return (int)base["index"];
			}
			set
			{
				base["index"] = value;
			}
		}

		/// <summary>Gets the collection of parameters.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Configuration.ParameterElementCollection" /> that contains all parameters.</returns>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0005422C File Offset: 0x0005242C
		[ConfigurationProperty("", DefaultValue = null, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ParameterElementCollection Parameters
		{
			get
			{
				return (ParameterElementCollection)base[""];
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005423E File Offset: 0x0005243E
		protected override void PostDeserialize()
		{
			this.Validate();
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005423E File Offset: 0x0005243E
		protected override void PreSerialize(XmlWriter writer)
		{
			this.Validate();
		}

		/// <summary>Gets or sets the type name of the parameter of the generic known type.</summary>
		/// <returns>The type name of the parameter.</returns>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00053E6A File Offset: 0x0005206A
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x00054246 File Offset: 0x00052446
		[ConfigurationProperty("type", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["type"] = value;
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00054264 File Offset: 0x00052464
		private void Validate()
		{
			PropertyInformationCollection propertyInformationCollection = base.ElementInformation.Properties;
			if (propertyInformationCollection["index"].ValueOrigin == PropertyValueOrigin.Default && propertyInformationCollection["type"].ValueOrigin == PropertyValueOrigin.Default)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("Configuration parameter element must set either type or index.")));
			}
			if (propertyInformationCollection["index"].ValueOrigin != PropertyValueOrigin.Default && propertyInformationCollection["type"].ValueOrigin != PropertyValueOrigin.Default)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("Configuration parameter element can set only one of either type or index.")));
			}
			if (propertyInformationCollection["index"].ValueOrigin != PropertyValueOrigin.Default && this.Parameters.Count > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("Configuration parameter element must only add params with type.")));
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00054324 File Offset: 0x00052524
		internal Type GetType(string rootType, Type[] typeArgs)
		{
			return TypeElement.GetType(rootType, typeArgs, this.Type, this.Index, this.Parameters);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00054340 File Offset: 0x00052540
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("index", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("", typeof(ParameterElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection),
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000A86 RID: 2694
		internal readonly Guid identity = Guid.NewGuid();

		// Token: 0x04000A87 RID: 2695
		private ConfigurationPropertyCollection properties;
	}
}
