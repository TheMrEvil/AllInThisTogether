using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001F RID: 31
	internal class DisposeHelper
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004F88 File Offset: 0x00003188
		[Conditional("UNITY_UIELEMENTS_DEBUG_DISPOSE")]
		public static void NotifyMissingDispose(IDisposable disposable)
		{
			bool flag = disposable == null;
			if (!flag)
			{
				Debug.LogError("An IDisposable instance of type '" + disposable.GetType().FullName + "' has not been disposed.");
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004FC0 File Offset: 0x000031C0
		public static void NotifyDisposedUsed(IDisposable disposable)
		{
			Debug.LogError("An instance of type '" + disposable.GetType().FullName + "' is being used although it has been disposed.");
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000020C2 File Offset: 0x000002C2
		public DisposeHelper()
		{
		}
	}
}
