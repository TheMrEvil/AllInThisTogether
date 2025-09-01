using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000072 RID: 114
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00010D66 File Offset: 0x0000EF66
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00010D6D File Offset: 0x0000EF6D
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
