using System;
using System.Reflection;

namespace QFSW.QC.ScanRules
{
	// Token: 0x02000002 RID: 2
	public class AssemblyExclusionScanRule : IQcScanRule
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ScanRuleResult ShouldScan<T>(T entity) where T : ICustomAttributeProvider
		{
			Assembly assembly = entity as Assembly;
			if (assembly != null)
			{
				string[] array = new string[]
				{
					"System",
					"Unity",
					"Microsoft",
					"Mono.",
					"mscorlib",
					"NSubstitute",
					"JetBrains",
					"nunit.",
					"GeNa."
				};
				string[] array2 = new string[]
				{
					"mcs",
					"AssetStoreTools",
					"Facepunch.Steamworks"
				};
				string fullName = assembly.FullName;
				foreach (string value in array)
				{
					if (fullName.StartsWith(value))
					{
						return ScanRuleResult.Reject;
					}
				}
				string name = assembly.GetName().Name;
				foreach (string b in array2)
				{
					if (name == b)
					{
						return ScanRuleResult.Reject;
					}
				}
			}
			return ScanRuleResult.Accept;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002146 File Offset: 0x00000346
		public AssemblyExclusionScanRule()
		{
		}
	}
}
