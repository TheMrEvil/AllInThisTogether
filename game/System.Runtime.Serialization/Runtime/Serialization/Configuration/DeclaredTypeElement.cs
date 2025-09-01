using System;
using System.Configuration;
using System.Security;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to add known types that are used for serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A1 RID: 417
	public sealed class DeclaredTypeElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> class.</summary>
		// Token: 0x0600151F RID: 5407 RVA: 0x00053E2E File Offset: 0x0005202E
		public DeclaredTypeElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> class with the specified type name.</summary>
		/// <param name="typeName">The name of the type that requires a collection of known types.</param>
		// Token: 0x06001520 RID: 5408 RVA: 0x00053E36 File Offset: 0x00052036
		public DeclaredTypeElement(string typeName) : this()
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			this.Type = typeName;
		}

		/// <summary>Gets the collection of known types.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Configuration.TypeElementCollection" /> that contains the known types.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00053E58 File Offset: 0x00052058
		[ConfigurationProperty("", DefaultValue = null, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public TypeElementCollection KnownTypes
		{
			get
			{
				return (TypeElementCollection)base[""];
			}
		}

		/// <summary>Gets or sets the name of the declared type that requires a collection of known types.</summary>
		/// <returns>The name of the declared type.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00053E6A File Offset: 0x0005206A
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x00053E7C File Offset: 0x0005207C
		[DeclaredTypeValidator]
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00053E8A File Offset: 0x0005208A
		[SecuritySafeCritical]
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			if (!PartialTrustHelpers.IsInFullTrust())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("Failed to load configuration section for dataContractSerializer.")));
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00053EB8 File Offset: 0x000520B8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(TypeElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection),
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new DeclaredTypeValidator(), ConfigurationPropertyOptions.IsKey)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000A84 RID: 2692
		private ConfigurationPropertyCollection properties;
	}
}
