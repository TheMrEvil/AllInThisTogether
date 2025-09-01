using System;
using System.Diagnostics;

namespace System.Runtime
{
	// Token: 0x02000007 RID: 7
	internal static class AssertHelper
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002198 File Offset: 0x00000398
		internal static void FireAssert(string message)
		{
			try
			{
			}
			finally
			{
				Debug.Assert(false, message);
			}
		}
	}
}
