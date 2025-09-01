using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	internal struct OpenTypeLayoutTable
	{
		// Token: 0x040000AF RID: 175
		public List<OpenTypeLayoutScript> scripts;

		// Token: 0x040000B0 RID: 176
		public List<OpenTypeLayoutFeature> features;

		// Token: 0x040000B1 RID: 177
		[SerializeReference]
		public List<OpenTypeLayoutLookup> lookups;
	}
}
