using System;
using System.Collections.Generic;

// Token: 0x020000D1 RID: 209
internal static class SpawnExtensions
{
	// Token: 0x06000993 RID: 2451 RVA: 0x0003FEF4 File Offset: 0x0003E0F4
	public static List<SpawnType> MapTypes(this SpawnType st)
	{
		return new List<SpawnType>
		{
			SpawnType.Map_Goal,
			SpawnType.Map_Explore,
			SpawnType.Map_Intersection,
			SpawnType.Map_UnusedA,
			SpawnType.Map_UnusedB
		};
	}
}
