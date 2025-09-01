using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000049 RID: 73
	internal class InternalConfigurationFactory : IInternalConfigConfigurationFactory
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			InternalConfigurationSystem internalConfigurationSystem = new InternalConfigurationSystem();
			internalConfigurationSystem.Init(typeConfigHost, hostInitConfigurationParams);
			return new Configuration(internalConfigurationSystem, null);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007CF9 File Offset: 0x00005EF9
		public string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo)
		{
			return subPath;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00002050 File Offset: 0x00000250
		public InternalConfigurationFactory()
		{
		}
	}
}
