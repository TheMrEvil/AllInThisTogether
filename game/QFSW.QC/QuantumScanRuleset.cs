using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QFSW.QC
{
	// Token: 0x02000036 RID: 54
	public class QuantumScanRuleset
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00007303 File Offset: 0x00005503
		public QuantumScanRuleset(IEnumerable<IQcScanRule> scanRules)
		{
			this._scanRules = scanRules.ToArray<IQcScanRule>();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007317 File Offset: 0x00005517
		public QuantumScanRuleset() : this(new InjectionLoader<IQcScanRule>().GetInjectedInstances(false))
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000732C File Offset: 0x0000552C
		public bool ShouldScan<T>(T entity) where T : ICustomAttributeProvider
		{
			bool result = true;
			IQcScanRule[] scanRules = this._scanRules;
			for (int i = 0; i < scanRules.Length; i++)
			{
				switch (scanRules[i].ShouldScan<T>(entity))
				{
				case ScanRuleResult.Reject:
					result = false;
					break;
				case ScanRuleResult.ForceAccept:
					return true;
				}
			}
			return result;
		}

		// Token: 0x040000FE RID: 254
		private readonly IQcScanRule[] _scanRules;
	}
}
