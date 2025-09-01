using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FD RID: 509
	internal static class ClassLibraryInitializer
	{
		// Token: 0x06001679 RID: 5753 RVA: 0x00023FF0 File Offset: 0x000221F0
		[RequiredByNativeCode]
		private static void Init()
		{
			UnityLogWriter.Init();
		}
	}
}
