using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using QFSW.QC.Utilities;

namespace QFSW.QC.ScanRules
{
	// Token: 0x02000003 RID: 3
	public class QcIgnoreScanRule : IQcScanRule
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000214E File Offset: 0x0000034E
		public ScanRuleResult ShouldScan<T>(T entity) where T : ICustomAttributeProvider
		{
			if (entity.HasAttribute(false))
			{
				return ScanRuleResult.Reject;
			}
			if (!(entity is MemberInfo) && entity.HasAttribute(true))
			{
				return ScanRuleResult.Reject;
			}
			return ScanRuleResult.Accept;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000217E File Offset: 0x0000037E
		public QcIgnoreScanRule()
		{
		}
	}
}
