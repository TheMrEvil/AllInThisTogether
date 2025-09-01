using System;

namespace System.Configuration
{
	/// <summary>Used internally as the class that represents metadata about an individual configuration property.</summary>
	// Token: 0x020001CC RID: 460
	public class SettingsProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class, based on the supplied parameter.</summary>
		/// <param name="propertyToCopy">Specifies a copy of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00031E70 File Offset: 0x00030070
		public SettingsProperty(SettingsProperty propertyToCopy) : this(propertyToCopy.Name, propertyToCopy.PropertyType, propertyToCopy.Provider, propertyToCopy.IsReadOnly, propertyToCopy.DefaultValue, propertyToCopy.SerializeAs, new SettingsAttributeDictionary(propertyToCopy.Attributes), propertyToCopy.ThrowOnErrorDeserializing, propertyToCopy.ThrowOnErrorSerializing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class. based on the supplied parameter.</summary>
		/// <param name="name">Specifies the name of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000BFA RID: 3066 RVA: 0x00031EC0 File Offset: 0x000300C0
		public SettingsProperty(string name) : this(name, null, null, false, null, SettingsSerializeAs.String, new SettingsAttributeDictionary(), false, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class based on the supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="propertyType">The type of <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object to use for persistence.</param>
		/// <param name="isReadOnly">A <see cref="T:System.Boolean" /> value specifying whether the <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</param>
		/// <param name="defaultValue">The default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used to set the serialization scheme for storing application settings.</param>
		/// <param name="attributes">A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</param>
		/// <param name="throwOnErrorDeserializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</param>
		/// <param name="throwOnErrorSerializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully serialized.</param>
		// Token: 0x06000BFB RID: 3067 RVA: 0x00031EE0 File Offset: 0x000300E0
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this.name = name;
			this.propertyType = propertyType;
			this.provider = provider;
			this.isReadOnly = isReadOnly;
			this.defaultValue = defaultValue;
			this.serializeAs = serializeAs;
			this.attributes = attributes;
			this.throwOnErrorDeserializing = throwOnErrorDeserializing;
			this.throwOnErrorSerializing = throwOnErrorSerializing;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object containing the attributes of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00031F38 File Offset: 0x00030138
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>An object containing the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00031F40 File Offset: 0x00030140
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00031F48 File Offset: 0x00030148
		public virtual object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether a <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.SettingsProperty" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00031F51 File Offset: 0x00030151
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00031F59 File Offset: 0x00030159
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00031F62 File Offset: 0x00030162
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00031F6A File Offset: 0x0003016A
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The type for the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00031F73 File Offset: 0x00030173
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x00031F7B File Offset: 0x0003017B
		public virtual Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
			set
			{
				this.propertyType = value;
			}
		}

		/// <summary>Gets or sets the provider for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProvider" /> object.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00031F84 File Offset: 0x00030184
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00031F8C File Offset: 0x0003018C
		public virtual SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Configuration.SettingsSerializeAs" /> object for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00031F95 File Offset: 0x00030195
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00031F9D File Offset: 0x0003019D
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
			set
			{
				this.serializeAs = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the error will be thrown when the property is unsuccessfully deserialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00031FA6 File Offset: 0x000301A6
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00031FAE File Offset: 0x000301AE
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this.throwOnErrorDeserializing;
			}
			set
			{
				this.throwOnErrorDeserializing = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully serialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the error will be thrown when the property is unsuccessfully serialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00031FB7 File Offset: 0x000301B7
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00031FBF File Offset: 0x000301BF
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this.throwOnErrorSerializing;
			}
			set
			{
				this.throwOnErrorSerializing = value;
			}
		}

		// Token: 0x0400079D RID: 1949
		private string name;

		// Token: 0x0400079E RID: 1950
		private Type propertyType;

		// Token: 0x0400079F RID: 1951
		private SettingsProvider provider;

		// Token: 0x040007A0 RID: 1952
		private bool isReadOnly;

		// Token: 0x040007A1 RID: 1953
		private object defaultValue;

		// Token: 0x040007A2 RID: 1954
		private SettingsSerializeAs serializeAs;

		// Token: 0x040007A3 RID: 1955
		private SettingsAttributeDictionary attributes;

		// Token: 0x040007A4 RID: 1956
		private bool throwOnErrorDeserializing;

		// Token: 0x040007A5 RID: 1957
		private bool throwOnErrorSerializing;
	}
}
