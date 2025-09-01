using System;

namespace System.Configuration
{
	/// <summary>Specifies the settings provider used to provide storage for the current application settings class or property. This class cannot be inherited.</summary>
	// Token: 0x020001D4 RID: 468
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerTypeName">A <see cref="T:System.String" /> containing the name of the settings provider.</param>
		// Token: 0x06000C4C RID: 3148 RVA: 0x000326BF File Offset: 0x000308BF
		public SettingsProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerType">A <see cref="T:System.Type" /> containing the settings provider type.</param>
		// Token: 0x06000C4D RID: 3149 RVA: 0x000326DC File Offset: 0x000308DC
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name of the settings provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the settings provider.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00032704 File Offset: 0x00030904
		public string ProviderTypeName
		{
			get
			{
				return this.providerTypeName;
			}
		}

		// Token: 0x040007B2 RID: 1970
		private string providerTypeName;
	}
}
