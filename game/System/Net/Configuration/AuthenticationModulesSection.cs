using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for authentication modules. This class cannot be inherited.</summary>
	// Token: 0x0200075D RID: 1885
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x06003B77 RID: 15223 RVA: 0x000CC440 File Offset: 0x000CA640
		static AuthenticationModulesSection()
		{
			AuthenticationModulesSection.properties = new ConfigurationPropertyCollection();
			AuthenticationModulesSection.properties.Add(AuthenticationModulesSection.authenticationModulesProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> class.</summary>
		// Token: 0x06003B78 RID: 15224 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public AuthenticationModulesSection()
		{
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000CC476 File Offset: 0x000CA676
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of authentication modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> that contains the registered authentication modules.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000CC47D File Offset: 0x000CA67D
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[AuthenticationModulesSection.authenticationModulesProp];
			}
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x0400237C RID: 9084
		private static ConfigurationPropertyCollection properties;

		// Token: 0x0400237D RID: 9085
		private static ConfigurationProperty authenticationModulesProp = new ConfigurationProperty("", typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
