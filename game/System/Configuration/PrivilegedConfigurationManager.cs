using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000196 RID: 406
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0002DC2B File Offset: 0x0002BE2B
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002DC32 File Offset: 0x0002BE32
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
