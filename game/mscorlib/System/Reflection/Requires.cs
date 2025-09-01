using System;

namespace System.Reflection
{
	// Token: 0x020008D3 RID: 2259
	internal static class Requires
	{
		// Token: 0x06004B4B RID: 19275 RVA: 0x000F0467 File Offset: 0x000EE667
		internal static void NotNull(object obj, string name)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(name);
			}
		}
	}
}
