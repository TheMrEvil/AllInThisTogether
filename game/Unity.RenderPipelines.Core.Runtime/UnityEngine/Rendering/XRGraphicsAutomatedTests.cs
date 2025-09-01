using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E7 RID: 231
	public static class XRGraphicsAutomatedTests
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001E830 File Offset: 0x0001CA30
		private static bool activatedFromCommandLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001E833 File Offset: 0x0001CA33
		public static bool enabled
		{
			[CompilerGenerated]
			get
			{
				return XRGraphicsAutomatedTests.<enabled>k__BackingField;
			}
		} = XRGraphicsAutomatedTests.activatedFromCommandLine;

		// Token: 0x060006DC RID: 1756 RVA: 0x0001E83A File Offset: 0x0001CA3A
		// Note: this type is marked as 'beforefieldinit'.
		static XRGraphicsAutomatedTests()
		{
		}

		// Token: 0x040003CC RID: 972
		[CompilerGenerated]
		private static readonly bool <enabled>k__BackingField;

		// Token: 0x040003CD RID: 973
		public static bool running = false;
	}
}
