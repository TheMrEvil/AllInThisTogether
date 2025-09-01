using System;
using System.Collections;
using Unity;

namespace System.Configuration
{
	/// <summary>Contains meta-information about an individual element within the configuration. This class cannot be inherited.</summary>
	// Token: 0x0200003F RID: 63
	public sealed class ElementInformation
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000077AC File Offset: 0x000059AC
		internal ElementInformation(ConfigurationElement owner, PropertyInformation propertyInfo)
		{
			this.propertyInfo = propertyInfo;
			this.owner = owner;
			this.properties = new PropertyInformationCollection();
			foreach (object obj in owner.Properties)
			{
				ConfigurationProperty property = (ConfigurationProperty)obj;
				this.properties.Add(new PropertyInformation(owner, property));
			}
		}

		/// <summary>Gets the errors for the associated element and subelements</summary>
		/// <returns>The collection containing the errors for the associated element and subelements</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		public ICollection Errors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is a <see cref="T:System.Configuration.ConfigurationElementCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is a <see cref="T:System.Configuration.ConfigurationElementCollection" /> collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007830 File Offset: 0x00005A30
		public bool IsCollection
		{
			get
			{
				return this.owner is ConfigurationElementCollection;
			}
		}

		/// <summary>Gets a value that indicates whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object cannot be modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object cannot be modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007840 File Offset: 0x00005A40
		public bool IsLocked
		{
			get
			{
				return this.propertyInfo != null && this.propertyInfo.IsLocked;
			}
		}

		/// <summary>Gets a value indicating whether the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is in the configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is in the configuration file; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007857 File Offset: 0x00005A57
		[MonoTODO("Support multiple levels of inheritance")]
		public bool IsPresent
		{
			get
			{
				return this.owner.IsElementPresent;
			}
		}

		/// <summary>Gets the line number in the configuration file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is defined.</summary>
		/// <returns>The line number in the configuration file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object is defined.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007864 File Offset: 0x00005A64
		public int LineNumber
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return 0;
				}
				return this.propertyInfo.LineNumber;
			}
		}

		/// <summary>Gets the source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</summary>
		/// <returns>The source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000787B File Offset: 0x00005A7B
		public string Source
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return null;
				}
				return this.propertyInfo.Source;
			}
		}

		/// <summary>Gets the type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00007892 File Offset: 0x00005A92
		public Type Type
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return this.owner.GetType();
				}
				return this.propertyInfo.Type;
			}
		}

		/// <summary>Gets the object used to validate the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The object used to validate the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000078B3 File Offset: 0x00005AB3
		public ConfigurationValidatorBase Validator
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return new DefaultValidator();
				}
				return this.propertyInfo.Validator;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000078CE File Offset: 0x00005ACE
		public PropertyInformationCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000078D8 File Offset: 0x00005AD8
		internal void Reset(ElementInformation parentInfo)
		{
			foreach (object obj in this.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				PropertyInformation parentProperty = parentInfo.Properties[propertyInformation.Name];
				propertyInformation.Reset(parentProperty);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00003518 File Offset: 0x00001718
		internal ElementInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000E2 RID: 226
		private readonly PropertyInformation propertyInfo;

		// Token: 0x040000E3 RID: 227
		private readonly ConfigurationElement owner;

		// Token: 0x040000E4 RID: 228
		private readonly PropertyInformationCollection properties;
	}
}
