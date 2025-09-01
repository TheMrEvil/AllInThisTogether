using System;
using System.Configuration;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A0 RID: 416
	public sealed class DataContractSerializerSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.DataContractSerializerSection" /> class.</summary>
		// Token: 0x0600151B RID: 5403 RVA: 0x00053DA2 File Offset: 0x00051FA2
		public DataContractSerializerSection()
		{
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00053DAA File Offset: 0x00051FAA
		[SecurityCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static DataContractSerializerSection UnsafeGetSection()
		{
			DataContractSerializerSection dataContractSerializerSection = (DataContractSerializerSection)ConfigurationManager.GetSection(ConfigurationStrings.DataContractSerializerSectionPath);
			if (dataContractSerializerSection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("Failed to load configuration section for dataContractSerializer.")));
			}
			return dataContractSerializerSection;
		}

		/// <summary>Gets a collection of types added to the <see cref="P:System.Runtime.Serialization.DataContractSerializer.KnownTypes" /> property.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElementCollection" /> that contains the known types.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00053DD3 File Offset: 0x00051FD3
		[ConfigurationProperty("declaredTypes", DefaultValue = null)]
		public DeclaredTypeElementCollection DeclaredTypes
		{
			get
			{
				return (DeclaredTypeElementCollection)base["declaredTypes"];
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00053DE8 File Offset: 0x00051FE8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("declaredTypes", typeof(DeclaredTypeElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000A83 RID: 2691
		private ConfigurationPropertyCollection properties;
	}
}
