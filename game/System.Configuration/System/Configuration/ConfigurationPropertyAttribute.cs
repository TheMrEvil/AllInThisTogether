using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to instantiate a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ConfigurationPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Configuration.ConfigurationPropertyAttribute" /> class.</summary>
		/// <param name="name">Name of the <see cref="T:System.Configuration.ConfigurationProperty" /> object defined.</param>
		// Token: 0x0600017C RID: 380 RVA: 0x00006543 File Offset: 0x00004743
		public ConfigurationPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets a value indicating whether this is a key property for the decorated element property.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is a key property for an element of the collection; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000655D File Offset: 0x0000475D
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000656A File Offset: 0x0000476A
		public bool IsKey
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsKey) > ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsKey;
					return;
				}
				this.flags &= ~ConfigurationPropertyOptions.IsKey;
			}
		}

		/// <summary>Gets or sets a value indicating whether this is the default property collection for the decorated configuration property.</summary>
		/// <returns>
		///   <see langword="true" /> if the property represents the default collection of an element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000658D File Offset: 0x0000478D
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000659A File Offset: 0x0000479A
		public bool IsDefaultCollection
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsDefaultCollection) > ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsDefaultCollection;
					return;
				}
				this.flags &= ~ConfigurationPropertyOptions.IsDefaultCollection;
			}
		}

		/// <summary>Gets or sets the default value for the decorated property.</summary>
		/// <returns>The object representing the default value of the decorated configuration-element property.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000065BD File Offset: 0x000047BD
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000065C5 File Offset: 0x000047C5
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
			set
			{
				this.default_value = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> for the decorated configuration-element property.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values associated with the property.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000065CE File Offset: 0x000047CE
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000065D6 File Offset: 0x000047D6
		public ConfigurationPropertyOptions Options
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		/// <summary>Gets the name of the decorated configuration-element property.</summary>
		/// <returns>The name of the decorated configuration-element property.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000065DF File Offset: 0x000047DF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets a value indicating whether the decorated element property is required.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is required; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000065E7 File Offset: 0x000047E7
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000065F4 File Offset: 0x000047F4
		public bool IsRequired
		{
			get
			{
				return (this.flags & ConfigurationPropertyOptions.IsRequired) > ConfigurationPropertyOptions.None;
			}
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsRequired;
					return;
				}
				this.flags &= ~ConfigurationPropertyOptions.IsRequired;
			}
		}

		// Token: 0x040000A7 RID: 167
		private string name;

		// Token: 0x040000A8 RID: 168
		private object default_value = ConfigurationProperty.NoDefaultValue;

		// Token: 0x040000A9 RID: 169
		private ConfigurationPropertyOptions flags;
	}
}
