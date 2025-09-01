using System;

namespace Mono.CSharp
{
	// Token: 0x02000123 RID: 291
	internal static class AttributeTester
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x00036D94 File Offset: 0x00034F94
		public static void Report_ObsoleteMessage(ObsoleteAttribute oa, string member, Location loc, Report Report)
		{
			if (oa.IsError)
			{
				Report.Error(619, loc, "`{0}' is obsolete: `{1}'", member, oa.Message);
				return;
			}
			if (oa.Message == null || oa.Message.Length == 0)
			{
				Report.Warning(612, 1, loc, "`{0}' is obsolete", member);
				return;
			}
			Report.Warning(618, 2, loc, "`{0}' is obsolete: `{1}'", member, oa.Message);
		}
	}
}
