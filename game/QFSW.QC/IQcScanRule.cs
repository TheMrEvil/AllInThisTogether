using System;
using System.Reflection;

namespace QFSW.QC
{
	// Token: 0x02000035 RID: 53
	public interface IQcScanRule
	{
		// Token: 0x0600013B RID: 315
		ScanRuleResult ShouldScan<T>(T entity) where T : ICustomAttributeProvider;
	}
}
