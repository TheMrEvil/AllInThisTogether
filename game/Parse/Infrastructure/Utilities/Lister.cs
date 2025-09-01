using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x0200004F RID: 79
	public static class Lister
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public static IEnumerable<Assembly> AllAssemblies
		{
			get
			{
				HashSet<string> seen = new HashSet<string>();
				return AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly asm) => asm.DeepWalkReferences(seen));
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		private static IEnumerable<Assembly> DeepWalkReferences(this Assembly assembly, HashSet<string> seen = null)
		{
			if (seen == null)
			{
				seen = new HashSet<string>();
			}
			if (!seen.Add(assembly.FullName))
			{
				return Enumerable.Empty<Assembly>();
			}
			List<Assembly> list = new List<Assembly>
			{
				assembly
			};
			foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
			{
				if (!seen.Contains(assemblyName.FullName))
				{
					Assembly assembly2 = Assembly.Load(assemblyName);
					list.AddRange(assembly2.DeepWalkReferences(seen));
				}
			}
			return list;
		}

		// Token: 0x02000119 RID: 281
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600074C RID: 1868 RVA: 0x000162AA File Offset: 0x000144AA
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x000162B2 File Offset: 0x000144B2
			internal IEnumerable<Assembly> <get_AllAssemblies>b__0(Assembly asm)
			{
				return asm.DeepWalkReferences(this.seen);
			}

			// Token: 0x0400028A RID: 650
			public HashSet<string> seen;
		}
	}
}
