using System;

namespace System.Reflection
{
	// Token: 0x020008D9 RID: 2265
	public static class ModuleExtensions
	{
		// Token: 0x06004B7A RID: 19322 RVA: 0x000F084B File Offset: 0x000EEA4B
		public static bool HasModuleVersionId(this Module module)
		{
			Requires.NotNull(module, "module");
			return true;
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x000F0859 File Offset: 0x000EEA59
		public static Guid GetModuleVersionId(this Module module)
		{
			Requires.NotNull(module, "module");
			return module.ModuleVersionId;
		}
	}
}
