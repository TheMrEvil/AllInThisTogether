using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000216 RID: 534
	internal class ScriptingUtility
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x000259B0 File Offset: 0x00023BB0
		[RequiredByNativeCode]
		private static bool IsManagedCodeWorking()
		{
			ScriptingUtility.TestClass testClass = new ScriptingUtility.TestClass
			{
				value = 42
			};
			return testClass.value == 42;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00002072 File Offset: 0x00000272
		public ScriptingUtility()
		{
		}

		// Token: 0x02000217 RID: 535
		private struct TestClass
		{
			// Token: 0x04000805 RID: 2053
			public int value;
		}
	}
}
